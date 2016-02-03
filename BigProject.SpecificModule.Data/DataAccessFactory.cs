using BigProject.SpecificModule.DataInterfaceLayer;
using BigProject.SpecificModule.DataAccessLayer.SQLServer;
using BigProject.SpecificModule.DataAccessLayer.MySQL;
using BigProject.SpecificModule.DataAccessLayer.MongoDB;
using System.Configuration;

namespace BigProject.SpecificModule.DataAccessLayer
{
    public class DataAccessFactory
    {
        private static bool SupportMutilDatabaseOperation
        {
            get
            {
                string flag = ConfigurationManager.AppSettings["DBConfig_SpecificModule_SupportMutilDatabase"];
                if (string.IsNullOrEmpty(flag)) return false;
                if (flag.ToLower() == "y" || flag.ToLower() == "true" || flag.ToLower() == "yes" || flag.ToLower() == "on")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get object from Database.
        /// </summary>
        /// <typeparam name="ID">Operation object type</typeparam>
        /// <param name="spName">StoredProcedures Enum</param>
        /// <returns></returns>
        public static ID GetDBAccess<ID>(EnumStoredProcedures spName)
        {
            ITargetDBType targetDB = new DBTypeAdapter();
            return GetDBAccess<ID>(targetDB.GetDBType(spName), SupportMutilDatabaseOperation);
        }

        /// <summary>
        /// Get object from Database.
        /// </summary>
        /// <typeparam name="ID">Operation object type</typeparam>
        /// <param name="spName">StoredProcedures Enum</param>
        /// <param name="SupportMutilDatabase">Support mutil database operation</param>
        /// <returns></returns>
        public static ID GetDBAccess<ID>(EnumStoredProcedures spName, bool SupportMutilDatabase)
        {
            ITargetDBType targetDB = new DBTypeAdapter();
            return GetDBAccess<ID>(targetDB.GetDBType(spName), SupportMutilDatabase);
        }

        private static ID GetDBAccess<ID>(EnumDBType sqlType, bool SupportMutilDatabase)
        {
            object obj = null;
            if (typeof(ID) == typeof(ISpecificModule_RO_Operations))
            {
                switch (sqlType)
                {
                    case EnumDBType.SQLServer:
                        if(SupportMutilDatabase)
                        {
                            obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString);
                        }
                        else
                        {
                            obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_RO_ConnectionString);
                        }
                        break;
                    case EnumDBType.MySQL:
                        if (SupportMutilDatabase)
                        {
                            obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString);
                        }
                        else
                        {
                            obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_RO_ConnectionString);
                        }
                        break;
                    case EnumDBType.MongoDB:
                        if (SupportMutilDatabase)
                        {
                            obj = new MongoDBDAL(DBConnectionStringHandler.SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString);
                        }
                        else
                        {
                            obj = new MongoDBDAL(DBConnectionStringHandler.SpecificModule_MongoDB_RO_ConnectionString);
                        }
                        break;
                    case EnumDBType.SQLite:
                        break;
                    case EnumDBType.Redis:
                        break;

                }
            }

            if (typeof(ID) == typeof(ISpecificModule_WO_Operations))
            {
                switch (sqlType)
                {
                    case EnumDBType.SQLServer:
                        if (SupportMutilDatabase)
                        {
                            obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString);
                        }
                        else
                        {
                            obj = new SQLServerDAL(DBConnectionStringHandler.SpecificModule_SQLServer_WO_ConnectionString);
                        }
                        break;
                    case EnumDBType.MySQL:
                        if (SupportMutilDatabase)
                        {
                            obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString);
                        }
                        else
                        {
                            obj = new MySQLDAL(DBConnectionStringHandler.SpecificModule_MySQL_WO_ConnectionString);
                        }
                        break;
                    case EnumDBType.MongoDB:
                        if (SupportMutilDatabase)
                        {
                            obj = new MongoDBDAL(DBConnectionStringHandler.SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString);
                        }
                        else
                        {
                            obj = new MongoDBDAL(DBConnectionStringHandler.SpecificModule_MongoDB_WO_ConnectionString);
                        }
                        break;
                    case EnumDBType.SQLite:
                        break;
                    case EnumDBType.Redis:
                        break;
                }
            }

            return obj != null ? (ID)obj : default(ID);
        }
    }
}