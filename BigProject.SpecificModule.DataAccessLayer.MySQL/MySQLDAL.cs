using System;
using System.Data;
using BigProject.SpecificModule.Model;
using BigProject.SpecificModule.DataInterfaceLayer;

namespace BigProject.SpecificModule.DataAccessLayer.MySQL
{
    public class MySQLDAL : ISpecificModule_RO_Operations, ISpecificModule_WO_Operations
    {
        #region Properties
        public string connectionstring;
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionstring))
                {
                    connectionstring = "";
                }

                return connectionstring;
            }

            set
            {
                connectionstring = value;
            }
        }

        #endregion

        #region Constractor
        public MySQLDAL(string ConnectionString)
        {
            connectionstring = ConnectionString;
        }

        public DataSet getlist(string p_inputparameter)
        {
            throw new NotImplementedException();
        }

        public int AddEntity(DemoModel dm)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}