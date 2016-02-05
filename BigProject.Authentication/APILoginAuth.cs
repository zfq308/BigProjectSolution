using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using BigProject.SpecificModule.Model;
using BigProject.SpecificModule.Business;

namespace BigProject.Authentication
{
    public class APILoginAuth
    {

        const string LocalUserId = "21A480D5-1DD6-4A4C-B4EB-7E35CE4A88AE";
        const string LocalUserLoginEmail = "Benjamin.Zhou@ms.com";

        APIPrincipal _APIUserPrincipal = null;

        public APIPrincipal CurrentPrincipal
        {
            get { return _APIUserPrincipal; }
        }

        User _User = null;

        public User CurrentUser
        {
            get { return _User; }

        }

        public APILoginAuth()
        {
            GetPrincipal();

        }

        public APIPrincipal GetPrincipalPermission(HttpContext c, Page page, Control[] cs)
        {
            APIPrincipal p = null;
            try
            {
                p = c.User as APIPrincipal;

                foreach (Control ctrl in cs)
                {
                    if (ctrl is WebControl) ((WebControl)ctrl).Enabled = false;
                    else
                    {
                        //Log.Info("No specific handling for " + ctrl.GetType() + ", set it to invisible.");
                        ctrl.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                //Log.Error(ex);
            }
            return p;
        }

        private void GetPrincipal()
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                HttpCookie cookie;
                cookie = HttpContext.Current.Request.Cookies[GlobalStatics.CookieName];
                #region If is local machine, use localuser
                if (HttpContext.Current.Request.IsLocal)
                {
                    bool[] m1 = new bool[32];
                    bool[] m2 = new bool[32];
                    for (int i = 0; i < m1.Length; i++)
                    {
                        m1[i] = true;
                        m2[i] = true;
                    }

                    _APIUserPrincipal = new APIPrincipal(LocalUserId, LocalUserLoginEmail, m1, m2);
                    HttpContext.Current.User = _APIUserPrincipal;

                    UserManager userManager = new UserManager();
                    _User = userManager.GetUserbyId(ConfigurationManager.AppSettings["LocalUserId"]);
                    _User.Roles = new UserManager().GetUserRoles(ConfigurationManager.AppSettings["LocalUserId"]);
                    this.SavePrin(_APIUserPrincipal);
                    this.CheckPermittedRole();
                    //this.CheckEnvironment();
                    return;
                }

                #endregion disable EdgarAuth for special needs

                //Log.Debug(cookie);

                GlobalStatics.MonitorProcessTime("First log", ref currentTime, DateTime.Now);
                if (cookie != null)
                {
                    _User = GetUserFromCookie();
                    if (_User != null)
                    {
                        this.CheckPermittedRole();
                        //this.CheckEnvironment();
                    }
                    else
                    {
                        this.GoToLogin();
                        //Log.Error("Failed to get auth info from cookie.");
                    }
                }
                else
                {
                    this.GoToLogin();
                }
                GlobalStatics.MonitorProcessTime("EndAuthenticate", ref currentTime, DateTime.Now);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw ex;
            }
        }

        //private void CheckEnvironment()
        //{
        //    if ((ConfigurationManager.AppSettings["Environment"] + "").Equals(EnumEnvironment.Live.ToString(), StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        //if (_User.UserType == EnumUserType.Trial.GetHashCode())
        //        //{
        //        //    HttpContext.Current.Response.Write("<script>alert('Sorry, you are trial user, so you just can log into Sandbox API.');location.href='" + ConfigurationManager.AppSettings["SandboxURI"] + HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "") + "?isdirect=true'</script>");
        //        //}
        //    }
        //}

        private void CheckPermittedRole()
        {
            //if (_User.UserType == EnumUserType.Internal)
            //{
            //    if (ConfigurationManager.AppSettings["Environment"] == EnumEnvironment.EACMS.ToString())
            //    {
            foreach (int roleId in _User.Roles)
            {
                foreach (int permitId in GetPermittedRoles())
                {
                    if (permitId == roleId)
                        return;
                }
            }
            HttpContext.Current.Response.Write("<script>alert('Sorry, you are not permitted to log in.');location.href='" + ConfigurationManager.AppSettings["LoginPage"] + "?isdirect=true'</script>");
            //    }
            //}
        }

        private void SavePrin(APIPrincipal newUser)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalStatics.CookieName];
            if (cookie == null)
            {
                //Log.WriteLog(Category.General, Level.Error, "can't get cookie, sth wrong");
                return;
            }
            //cookie["Prin"] = SecurityProvider.Encrypt40(newUser.ToString(), GlobalStatics.Enkey);
            cookie.Expires = DateTime.Now.AddDays(2);
            //cookie.Domain = GlobalStatics.cookieDomain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private void GoToLogin()
        {
            System.Uri uri = HttpContext.Current.Request.Url;
            string rtUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            string rdUrl = ("" + System.Configuration.ConfigurationManager.AppSettings["LoginPage"]).Trim();
            HttpContext.Current.Response.Redirect(rdUrl + "?isredirect=true", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected virtual User GetUserFromCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalStatics.CookieName];
            if (cookie == null)
                return null;
            if (cookie["API"] == null)
                return null;
            Dictionary<string, string> authKeyPair = GlobalStatics.ParseCookie(cookie);
            if (authKeyPair.Keys.Count == 0)
                return null;
            if (!authKeyPair.ContainsKey("LoginEmail"))
                return null;
            User user = new UserManager().GetUserByEmail(authKeyPair["LoginEmail"]);
            user.Password = authKeyPair["Password"];
            user.Roles = new UserManager().GetUserRoles(user.UserId);
            return user;
        }

        public void LogOut()
        {
            HttpContext.Current.Response.Cookies.Remove(GlobalStatics.CookieName);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[GlobalStatics.CookieName];
            if (cookie != null)
            {
                if (cookie.HasKeys)
                {
                    if (HttpContext.Current.Request.Url.Authority.IndexOf("geapi81") > -1 || HttpContext.Current.Request.Url.Authority.IndexOf("localhost") > -1)
                    {
                        cookie.Domain = "";
                    }
                    else
                        cookie.Domain = GlobalStatics.CookieDomain;
                    cookie.Path = "/";
                    cookie.Expires = DateTime.Now.AddDays(-10);
                    NameValueCollection col = cookie.Values;
                    foreach (string key in col.AllKeys)
                        cookie[key] = "";
                    cookie.Value = "";
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    //Log.Info(HttpContext.Current.Request.Cookies[GlobalStatics.CookieName]);
                    //Log.Info("EquityAPI Loged Out");

                }
            }
            GoToLogin();
        }

        public List<int> GetPermittedUserTypes()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["PermittedUserTypes"] + "";
            if (string.IsNullOrEmpty(value)) value = "3"; //TODO: This is a specific logic.
            List<int> userTypes = new List<int>();
            if (value.IndexOf(',') > -1)
            {
                string[] valueList = value.Split(',');
                foreach (string permitedType in valueList)
                {
                    int type = 0;
                    int.TryParse(permitedType, out type);
                    if (type > 0)
                    {
                        if (!userTypes.Contains(type))
                            userTypes.Add(type);
                    }
                }
            }
            else
            {
                int type = 0;
                int.TryParse(value, out type);
                if (type > 0)
                {
                    userTypes.Add(type);
                }
            }
            return userTypes;
        }

        public List<int> GetPermittedRoles()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["PermittedRoles"] + "";
            List<int> roles = new List<int>();
            if (value.IndexOf(',') > -1)
            {
                string[] valueList = value.Split(',');
                foreach (string permitedRole in valueList)
                {
                    int roleId = 0;
                    int.TryParse(permitedRole, out roleId);
                    if (roleId > 0)
                    {
                        if (!roles.Contains(roleId))
                            roles.Add(roleId);
                    }
                }
            }
            else
            {
                int roleId = 0;
                int.TryParse(value, out roleId);
                if (roleId > 0)
                {
                    roles.Add(roleId);
                }
            }
            return roles;
        }
    }
}
