using System;
using System.Configuration;

namespace BigProject.SpecificModule.DataInterfaceLayer
{

    public enum EnumDBType
    {
        SQLServer,
        MySQL,
        Redis,
        MongoDB,
        SQLite,
    }

    public interface ITargetDBType
    {
        EnumDBType GetDBType(EnumStoredProcedures sp);
    }

    public class DBTypeAdapter : ITargetDBType
    {
        private string DataBaseType
        {
            get
            {
                string dbtype=ConfigurationManager.AppSettings["DBConfig_SpecificModule_DatabaseType"];
                if (string.IsNullOrEmpty(dbtype)) dbtype = "SQLServer";
                return dbtype;
            }
        }


        public EnumDBType GetDBType(EnumStoredProcedures sp)
        {
            return (EnumDBType)Enum.Parse(typeof(EnumDBType), DataBaseType);
        }
    }
}