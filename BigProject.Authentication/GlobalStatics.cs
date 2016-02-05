using BigProject.Utils;
using System;
using System.Collections.Generic;

namespace BigProject.Authentication
{
    public class GlobalStatics
    {
        static GlobalStatics()
        {
            string logRoot = ("" + System.Configuration.ConfigurationManager.AppSettings["LoginPage"]).Trim();
            if (logRoot.Length == 0)
                //Log.Warn("loginurl is empty. set it in config file if necessary.");
                URLLogin = logRoot + "/login";

            CookieDomain = ("" + System.Configuration.ConfigurationManager.AppSettings["CookieDomain"]).Trim();

            CookieName = ("" + System.Configuration.ConfigurationManager.AppSettings["CookieName"]).Trim();
            if (CookieName.Length == 0)
                CookieName = "MsEACMS";
        }

        public static readonly string ProductId = "EA";
        public static readonly string URLLogin;
        public static readonly string CookieDomain;
        public static readonly string CookieName;
        public static readonly string Enkey = "D8F78364ICNPNAN3546JCB";
        public static readonly int reVerifyHours = 96;
        public static readonly string DefaultPassword = "WelcomeAPI1@";

        public static Dictionary<string, string> ParseCookie(System.Web.HttpCookie cookie)
        {
            string[] authInfos = CryptUtil.DESDecrypt(cookie["API"], GlobalStatics.Enkey).Split('\t');
            Dictionary<string, string> authKeyPair = new Dictionary<string, string>();
            foreach (string auth in authInfos)
            {
                string[] keyValue = auth.Split('\0');
                if (keyValue.Length == 2)
                    authKeyPair.Add(keyValue[0], keyValue[1]);
            }
            if (authKeyPair.Count == 0)
            {
                //Log.Error("Failed to parse cookie.");
            }
            return authKeyPair;
        }

        public static void MonitorProcessTime(string type, ref DateTime start, DateTime end)
        {
            TimeSpan s = end - start;
            if (s.TotalSeconds > 10)
            {
                //Log.Warn("MonitorProcessTime(1) time cost(seconds):" + type + "," + s.TotalSeconds.ToString());
            }
            start = end;
        }

        public static void MonitorProcessTime(string type, object start, DateTime end)
        {
            if (start == null)
                return;
            DateTime startTime = DateTime.Parse(start.ToString());
            TimeSpan s = end - startTime;
            if (s.TotalSeconds > 10)
            {
                //Log.Warn("MonitorProcessTime(2) time cost(seconds):" + type + "," + s.TotalSeconds.ToString());
            }
            start = end;
        }
    }
}
