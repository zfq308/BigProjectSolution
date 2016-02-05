using BigProject.SpecificModule.Business;
using System;
using System.Security.Principal;


namespace BigProject.Authentication
{
    public class APIPrincipal : IPrincipal
    {
        #region constructors

        public APIPrincipal(string gid, string email, bool[] m1, bool[] m2)
        {
            this.identity = new APIIdentity(email);
            this.Guid = gid;
            this.m1 = m1;
            this.m2 = m2;
            this.i1 = 0;
            this.i2 = 0;
            APIPrincipal.extractPermissionArray(m1, m2, out i1, out i2);
        }

        public APIPrincipal(string email, int i1, int i2)
        {
            this.identity = new APIIdentity(email);
            this.i1 = i1;
            this.i2 = i2;
            APIPrincipal.fillPermissionArray(i1, i2, out this.m1, out this.m2);
        }

        public APIPrincipal(string email, int i1, int i2, string gid)
        {
            this.identity = new APIIdentity(email);
            this.i1 = i1;
            this.i2 = i2;
            this.Guid = gid;
            APIPrincipal.fillPermissionArray(i1, i2, out this.m1, out this.m2);
        }


        public APIPrincipal(string email)
        {
            i1 = 0;
            i2 = 0;

            //Log.Info( "calling WS to get rights...");

            string msg = new UserManager().getRightsByEmail(email, GlobalStatics.ProductId, out i1, out i2);
            if (msg.Length == 0)
            {
                this.identity = new APIIdentity(email);
            }
            else
            {
                //Log.WriteLog(Category.General, Level.Error, email + "|" + GlobalStatics.ProductId + "|" + msg);
            }
            APIPrincipal.fillPermissionArray(i1, i2, out this.m1, out this.m2);
        }

        #endregion constructor

        #region Login

        public static APIPrincipal Login(string email, string pwd, out string msg)
        {
            APIPrincipal p = null;
            bool[] m1 = new bool[32];
            bool[] m2 = new bool[32];
            int i1, i2;
            long iid;
            msg = string.Empty;
            //Log.Info( "calling WS to verify login and get rights...");
            string gid = new UserManager().Login(email, pwd, GlobalStatics.ProductId, out i1, out i2, out iid, out msg);
            APIPrincipal.fillPermissionArray(i1, i2, out m1, out m2);

            if (gid.Length > 0)
                p = new APIPrincipal(gid, email, m1, m2);
            else
            {
                //Log.Error(email + ", Message: " + msg);
            }
            return p;
        }

        #endregion Login

        #region private

        private static void fillPermissionArray(int i1, int i2, out bool[] m1, out bool[] m2)
        {
            m1 = new bool[32];
            m2 = new bool[32];

            for (int i = 0; i < m1.Length - 1; i++)
            {
                m1[i] = (i1 % 2 == 1) ? true : false;
                i1 = i1 / 2;

                m2[i] = (i2 % 2 == 1) ? true : false;
                i2 = i2 / 2;
            }
        }//endof fillPermissionArray()

        private static void extractPermissionArray(bool[] m1, bool[] m2, out int i1, out int i2)
        {
            i1 = 0;
            i2 = 0;
            for (int i = 0; i < m1.Length - 1; i++)
            {
                if (m1[i])
                    i1 += (int)Math.Pow(2, i);
                if (m2[i])
                    i2 += (int)Math.Pow(2, i);
            }
        }

        #endregion private

        #region fields
        private IIdentity identity;
        public IIdentity CurrentIdentity { get { return this.identity; } }
        private bool[] m1;
        private bool[] m2;
        private int i1, i2;
        public override string ToString()
        {
            return this.identity.Name + "|" + i1 + "|" + i2 + "|" + DateTime.Now.Ticks + "|" + Guid;
        }
        public string Guid;

        #endregion fields

        #region IPrincipal Members

        IIdentity IPrincipal.Identity
        {
            get { return this.identity; }
        }

        bool IPrincipal.IsInRole(string role)
        {
            return true;
        }

        #endregion
    }
}