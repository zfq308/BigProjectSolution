using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BigProject.Utils;


namespace BigProject.SpecificModule.DataAccessLayer
{
    public class DBConnectionStringHandler
    {
        #region SQL Server Connection String

        private static string _SpecificModule_SQLServer_RO_ConnectionString;
        public static string SpecificModule_SQLServer_RO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_SQLServer_RO_ConnectionString))
                {
                    _SpecificModule_SQLServer_RO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_SQLServer_RO_ConnectionString"].ToString();
                }
                return _SpecificModule_SQLServer_RO_ConnectionString;
            }
            set
            {
                _SpecificModule_SQLServer_RO_ConnectionString = value;
            }
        }


        private static string _SpecificModule_SQLServer_WO_ConnectionString;
        public static string SpecificModule_SQLServer_WO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_SQLServer_WO_ConnectionString))
                {
                    _SpecificModule_SQLServer_WO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_SQLServer_WO_ConnectionString"].ToString();
                }
                return _SpecificModule_SQLServer_WO_ConnectionString;
            }
            set
            {
                _SpecificModule_SQLServer_WO_ConnectionString = value;
            }
        }

        #endregion

        #region MySQL Connection String

        private static string _SpecificModule_MySQL_RO_ConnectionString;
        public static string SpecificModule_MySQL_RO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_MySQL_RO_ConnectionString))
                {
                    _SpecificModule_MySQL_RO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_MySQL_RO_ConnectionString"].ToString();
                }
                return _SpecificModule_MySQL_RO_ConnectionString;
            }
            set
            {
                _SpecificModule_MySQL_RO_ConnectionString = value;
            }
        }


        private static string _SpecificModule_MySQL_WO_ConnectionString;
        public static string SpecificModule_MySQL_WO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_MySQL_WO_ConnectionString))
                {
                    _SpecificModule_MySQL_WO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_MySQL_WO_ConnectionString"].ToString();
                }
                return _SpecificModule_MySQL_WO_ConnectionString;
            }
            set
            {
                _SpecificModule_MySQL_WO_ConnectionString = value;
            }
        }

        #endregion

        #region MongoDB Connection String

        private static string _SpecificModule_MongoDB_RO_ConnectionString;
        public static string SpecificModule_MongoDB_RO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_MongoDB_RO_ConnectionString))
                {
                    _SpecificModule_MongoDB_RO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_MongoDB_RO_ConnectionString"].ToString();
                }
                return _SpecificModule_MongoDB_RO_ConnectionString;
            }
            set
            {
                _SpecificModule_MongoDB_RO_ConnectionString = value;
            }
        }


        private static string _SpecificModule_MongoDB_WO_ConnectionString;
        public static string SpecificModule_MongoDB_WO_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_MongoDB_WO_ConnectionString))
                {
                    _SpecificModule_MongoDB_WO_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_MongoDB_WO_ConnectionString"].ToString();
                }
                return _SpecificModule_MongoDB_WO_ConnectionString;
            }
            set
            {
                _SpecificModule_MongoDB_WO_ConnectionString = value;
            }
        }

        #endregion

        #region SQLite Connection String

        private static string _SpecificModule_SQLite_RW_ConnectionString;
        public static string SpecificModule_SQLite_RW_ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_SpecificModule_SQLite_RW_ConnectionString))
                {
                    _SpecificModule_SQLite_RW_ConnectionString = ConfigurationManager.ConnectionStrings["SpecificModule_SQLite_RW_ConnectionString"].ToString();
                }
                return _SpecificModule_SQLite_RW_ConnectionString;
            }
            set
            {
                _SpecificModule_SQLite_RW_ConnectionString = value;
            }
        }


        #endregion




        private static List<string> _SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString;
        public static List<string> SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_SQLServer_RO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString == null || _SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_SQLServer_SupportMutilDB_RO_ConnectionString;
            }
        }


        private static List<string> _SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString;
        public static List<string> SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_SQLServer_WO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString == null || _SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_SQLServer_SupportMutilDB_WO_ConnectionString;
            }
        }




        private static List<string> _SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString;
        public static List<string> SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_MySQL_RO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString == null || _SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_MySQL_SupportMutilDB_RO_ConnectionString;
            }
        }


        private static List<string> _SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString;
        public static List<string> SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_MySQL_WO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString == null || _SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_MySQL_SupportMutilDB_WO_ConnectionString;
            }
        }





        private static List<string> _SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString;
        public static List<string> SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_MongoDB_RO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString == null || _SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_MongoDB_SupportMutilDB_RO_ConnectionString;
            }
        }


        private static List<string> _SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString;
        public static List<string> SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString
        {
            get
            {
                var ConfigPath = ConfigurationManager.AppSettings["SpecificModule_MongoDB_WO_SupportMutilDatabase_ConnectionStringConfigFilePath"];
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.IO.Path.Combine(ConfigPath.Split('/')));
                    StandardConfigFileReader scr = new StandardConfigFileReader(path);
                    Dictionary<string, string> pairs = scr.AllConnectionStringPair;

                    if (_SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString == null || _SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString.Count == 0)
                    {
                        _SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString = new List<string>();
                        foreach (var item in pairs)
                        {
                            if (item.Key != "LocalSqlServer")
                            {
                                _SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString.Add(item.Value);
                            }
                        }
                    }
                }
                return _SpecificModule_MongoDB_SupportMutilDB_WO_ConnectionString;
            }
        }

    }
}
