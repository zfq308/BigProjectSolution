using BigProject.SpecificModule.Business;
using BigProject.SpecificModule.Model;
using BigProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigProject.Authentication
{
    public class APILogin
    {

        const string LocalUserId = "21A480D5-1DD6-4A4C-B4EB-7E35CE4A88AE";
        const string LocalUserLoginEmail = "Benjamin.Zhou@ms.com";

        #region Properties

        string _Password = string.Empty;
        APIPrincipal _APIUserPrincipal = null;

        User _UserInfo = null;
        public User UserInfo
        {
            get { return _UserInfo; }
            set { _UserInfo = value; }
        }



        #endregion

        #region Constractors

        public APILogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Email can't be NULL or empty.");

            // Create a User instance
            _UserInfo = new User();
            _UserInfo.LoginEmail = email;
            _Password = password;
        }

        public APILogin(int id, string email, string password)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Email can't be NULL or empty.");

            // Create a User instance
            _UserInfo = new User();
            _UserInfo.Id = id;
            _UserInfo.LoginEmail = email;
            _Password = password;
        }

        #endregion

        public bool Login(out string msg)
        {


            bool isLoginSuccess = false;
            msg = string.Empty;
            try
            {
                //Log.Debug("Start to login, LoginEmail is:" + _UserInfo.LoginEmail);
                DateTime startTime = DateTime.Now;
                if (HttpContext.Current.Request.IsLocal)
                {
                    //Log.Debug("The account is Local account." + _UserInfo.LoginEmail);
                    bool[] m1 = new bool[32];
                    bool[] m2 = new bool[32];
                    for (int i = 0; i < m1.Length; i++)
                    {
                        m1[i] = true;
                        m2[i] = true;
                    }

                    _APIUserPrincipal = new APIPrincipal(LocalUserId, LocalUserLoginEmail, m1, m2);
                    if (_APIUserPrincipal == null)
                    {
                        //Log.Warning("To build local account fail.");
                    }
                }
                else
                {
                    _APIUserPrincipal = APIPrincipal.Login(_UserInfo.LoginEmail, _Password, out msg);
                    if (_APIUserPrincipal == null)
                    {
                        //Log.Debug("To build " + _UserInfo.LoginEmail + " account fail.");
                    }
                }

                GlobalStatics.MonitorProcessTime("Login from Authentication Center", ref startTime, DateTime.Now);
                //Log.Debug(_APIUserPrincipal);
                if (_APIUserPrincipal != null)
                {
                    CheckLoginedUser(ref msg, ref isLoginSuccess, ref startTime);
                }
                else
                {
                    isLoginSuccess = false;
                    msg = "Failed to get UserPrincipal entity by UserId.";
                }
                HttpContext.Current.User = _APIUserPrincipal;
            }
            catch (Exception)
            {
                isLoginSuccess = false;
                //Log.Error(ex);
            }
            //Log.Debug("Login status: " + isLoginSuccess + ":" + msg + ",LoginEmail:" + _UserInfo.LoginEmail);
            return isLoginSuccess;
        }




        private void CheckLoginedUser(ref string msg, ref bool isLoginSuccess, ref DateTime startTime)
        {
            User existedUser = new UserManager().GetUserbyId(this._APIUserPrincipal.Guid);
            GlobalStatics.MonitorProcessTime("Get User from DB", ref startTime, DateTime.Now);
            if (existedUser != null)
            {
                Process4ExistedAPIUser(ref msg, ref isLoginSuccess, ref startTime, existedUser);
            }
            else
            {
                Process4ExistedGLUser(ref msg, ref isLoginSuccess);
            }
        }

        private void Process4ExistedAPIUser(ref string msg, ref bool isLoginSuccess, ref DateTime startTime, User existedUser)
        {
            existedUser.Roles = new UserManager().GetUserRoles(this._APIUserPrincipal.Guid);
            GlobalStatics.MonitorProcessTime("Get role from DB", ref startTime, DateTime.Now);
            existedUser.Password = _Password;

            isLoginSuccess = true;
            _UserInfo = existedUser;
        }

        private void Process4ExistedGLUser(ref string msg, ref bool isLoginSuccess)
        {
            if (new UserManager().GetUserByEmail(_UserInfo.LoginEmail) != null)
            {
                int effectedRow = new UserManager().UpdateUserIdByEmail(_UserInfo.LoginEmail, _APIUserPrincipal.Guid);
                if (effectedRow <= 0)
                {
                    //Log.Error("No data effected at Id:" + _UserInfo.LoginEmail);
                    msg = "Failed to update account in Equity API Database.";
                    isLoginSuccess = false;
                }
                else
                {
                    _UserInfo = new UserManager().GetUserbyId(_APIUserPrincipal.Guid);
                    if (_UserInfo != null)
                    {
                        _UserInfo.Roles = new UserManager().GetUserRoles(this._APIUserPrincipal.Guid);
                        _UserInfo.Password = _Password;
                        //UpdateUserNotificationMessage(_UserInfo.UserId);
                        isLoginSuccess = true;
                    }
                    else
                    {
                        isLoginSuccess = false;
                        msg = "Failed to get User entity by UserId.";
                    }
                }
            }
            else
            {
                msg = "Your account was not existed in Equity API Database.";
                isLoginSuccess = false;
            }
        }

        public void SaveCookie(bool isSaveLogin)
        {
            List<string> authInfos = new List<string>();
            HttpCookie cookie = new HttpCookie(GlobalStatics.CookieName);

            authInfos.Add("LoginEmail\0" + _UserInfo.LoginEmail.ToString());
            authInfos.Add("Password\0" + _UserInfo.Password.ToString());
            cookie["API"] = CryptUtil.DESEncrypt(string.Join("\t", authInfos.ToArray<string>()), GlobalStatics.Enkey);
            if (HttpContext.Current.Request.Url.Authority.IndexOf("geapi81") > -1 || HttpContext.Current.Request.Url.Authority.IndexOf("localhost") > -1)
            {
                cookie.Domain = "";
            }
            else
            {
                cookie.Domain = GlobalStatics.CookieDomain;
            }
            if (isSaveLogin)
            {
                setCookieExpires(cookie);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
            //Log.Debug("New cookie: " + cookie);
            return;
        }

        private void setCookieExpires(HttpCookie cookie)
        {
            DateTime expireDate = DateTime.Now.AddDays(14);
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["CookieExpireDays"]))
            {
                int expireMins = 14;
                if (int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CookieExpireDays"], out expireMins))
                {
                    cookie.Expires = DateTime.Now.AddDays(expireMins);
                }
            }
            else
                cookie.Expires = expireDate;
        }
    }
}
