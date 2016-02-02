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

        #endregion

        int ISpecificModule_WO_Operations.AddEntity(DemoModel dm)
        {
            throw new NotImplementedException();
        }


        DataSet ISpecificModule_RO_Operations.getlist(string p_inputparameter)
        {
            throw new NotImplementedException();
        }
    }
}