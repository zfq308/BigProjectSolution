﻿using System;
using System.Data;
using BigProject.SpecificModule.Model;
using BigProject.SpecificModule.DataInterfaceLayer;
using System.Collections.Generic;

namespace BigProject.SpecificModule.DataAccessLayer.MySQL
{
    public class MySQLDAL : ISpecificModule_RO_Operations, ISpecificModule_WO_Operations
    {
        #region Properties
        public List<string> connectionstrings;
        public List<string> ConnectionStrings
        {
            get
            {
                if (connectionstrings == null || connectionstrings.Count == 0)
                {
                    connectionstrings = new List<string>();
                }

                return connectionstrings;
            }
            set
            {
                connectionstrings = value;
            }
        }

        #endregion

        #region Constractor
        public MySQLDAL(string ConnectionString)
        {
            connectionstrings = new List<string>();
            if (!connectionstrings.Contains(ConnectionString))
            {
                connectionstrings.Add(ConnectionString);
            }
        }

        public MySQLDAL(List<string> ConnectionStrings)
        {
            connectionstrings = ConnectionStrings;
        }
        #endregion

        public DataSet getlist(string p_inputparameter)
        {
            throw new NotImplementedException();
        }

        public int AddEntity(DemoModel dm)
        {
            throw new NotImplementedException();
        }

    }
}