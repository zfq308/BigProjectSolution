using BigProject.SpecificModule.DataInterfaceLayer;
using BigProject.SpecificModule.DataAccessLayer.SQLServer;
using BigProject.SpecificModule.DataAccessLayer.MySQL;

namespace BigProject.SpecificModule.DataAccessLayer
{
    public class DataAccessFactory
    {
        public static ID GetDBAccess<ID>(EnumStoredProcedures sp)
        {
            ITargetDBType targetDB = new DBTypeAdapter();
            return GetDBAccess<ID>(targetDB.GetDBType(sp));
        }

        private static ID GetDBAccess<ID>(EnumDBType sqlType)
        {
            object obj = null;
            if (typeof(ID) == typeof(ISpecificModule_RO_Operations))
            {
                switch (sqlType)
                {
                    case EnumDBType.SQLServer:
                        obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_RO_ConnectionString);
                        break;
                    case EnumDBType.MySQL:
                        obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_RO_ConnectionString);
                        break;
                }
            }

            if (typeof(ID) == typeof(ISpecificModule_WO_Operations))
            {
                switch (sqlType)
                {
                    case EnumDBType.SQLServer:
                        obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_WO_ConnectionString);
                        break;
                    case EnumDBType.MySQL:
                        obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_WO_ConnectionString);
                        break;
                }
            }

            return obj != null ? (ID)obj : default(ID);
        }
    }
}