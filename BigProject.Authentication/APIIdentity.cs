using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace BigProject.Authentication
{
    public class APIIdentity : IIdentity
    {

        public string LoginEmail { get; set; }

        public APIIdentity(string email)
        {
            this.LoginEmail = Regex.Replace(email, "@.*", "");
        }

        #region IIdentity Members

        string IIdentity.AuthenticationType
        {
            get { return "GL Auth"; }
        }

        bool IIdentity.IsAuthenticated
        {
            get { return true; }
        }

        string IIdentity.Name
        {
            get { return this.LoginEmail; }
        }

        #endregion
    }
}
