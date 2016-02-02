using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigProject.SpecificModule.Model;
using BigProject.SpecificModule.DataAccessLayer;
using BigProject.SpecificModule.DataInterfaceLayer;
using System.Data;

namespace BigProject.DemoTest.FirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpecificModule_RO_Operations ops = DataAccessFactory.GetDBAccess<ISpecificModule_RO_Operations>(EnumStoredProcedures.getlist);
            DataSet ds = ops.getlist("inputvalue");
            return;

        }
    }
}
