using BigProject.SpecificModule.DataAccessLayer;
using BigProject.SpecificModule.DataAccessLayer.MemcachedLayer;
using System.Collections.Generic;

namespace BigProject.DemoTest.FirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = DBConnectionStringHandler.SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString;
            return;

        }
    }
}
