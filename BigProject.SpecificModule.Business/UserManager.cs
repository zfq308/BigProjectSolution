using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigProject.SpecificModule.Model;

namespace BigProject.SpecificModule.Business
{
    public class UserManager
    {
        public User GetUserbyId(string UserId)
        {
            throw new NotImplementedException("NotImplementedException");
        }

        public List<int> GetUserRoles(string userId)
        {
            throw new NotImplementedException("NotImplementedException");
        }

        public User GetUserByEmail(string p_EmailAddress)
        {
            throw new NotImplementedException("NotImplementedException");
        }

        public int UpdateUserIdByEmail(string p_LoginEmail, string p_UserId)
        {
            throw new NotImplementedException("NotImplementedException");
        }


        public string getRightsByEmail(string Email, string ProductId, out int i1, out int i2)
        {
            //TODO: need to impletement the getRightsByEmail method.
            i1 = 0;
            i2 = 0;
            return ""; //Success
        }


        public string Login(string Email,string Password,string ProductionId, out int i1, out int i2, out long iid,out string msg)
        {
            //TODO: need to impletement the Login method.
            i1 = 0;
            i2 = 0;
            iid = 0;
            msg = Guid.NewGuid().ToString();
            return msg;
        }

    }
}
