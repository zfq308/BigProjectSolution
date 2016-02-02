using NUnit.Framework;
using BigProject.SpecificModule.DataAccessLayer.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BigProject.SpecificModule.DataInterfaceLayer;

namespace BigProject.SpecificModule.DataAccessLayer.SQLServer.Tests
{
    [TestFixture()]
    public class SQLServerDALTests
    {
        [Test()]
        public void getlistTest()
        {
            var ops = DataAccessFactory.GetDBAccess<ISpecificModule_RO_Operations>(EnumStoredProcedures.getlist);
            DataSet ds = ops.getlist("inputvalue");

        }
    }
}