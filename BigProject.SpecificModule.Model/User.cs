using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigProject.SpecificModule.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string LoginEmail { get; set; }
        public List<int> Roles { get; set; }
        public string Password { get; set; }
        public sbyte UserType { get; set; }
       

    }

    public enum EnumUserType
    {
        //New = -1,
        //Inactive = 0,
        Trial = 1,
        Live = 2,
        Internal = 3
    }
}
