using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace BigProject.Utils
{
    /// <summary>
    /// 多文件源的标准Config文件读取器
    /// </summary>
    public class StandardConfigFileReader
    {
        public StandardConfigFileReader(params string[] cfgPath)
        {
            ConfigPath = cfgPath;
        }

        private string[] ConfigPath { get; set; }

        private Dictionary<string, string> AllConfigPair
        {
            get
            {
                if (!ConfigPath.Any(File.Exists))
                {
                    var list = new Dictionary<string, string> { { "", "" } };
                    return list;
                }
                var allConfigList = new Dictionary<string, string>();
                foreach (var s in ConfigPath)
                {
                    var map = new ExeConfigurationFileMap { ExeConfigFilename = s };
                    var list = ConfigurationManager.OpenMappedExeConfiguration(map, 0).AppSettings.Settings;
                    foreach (KeyValueConfigurationElement c in list)
                    {
                        if (!allConfigList.ContainsKey(c.Key))
                        {
                            allConfigList.Add(c.Key, c.Value);
                        }
                    }
                }
                return allConfigList;
            }
        }

        public Dictionary<string, string> AllConnectionStringPair
        {
            get
            {
                if (!ConfigPath.Any(File.Exists))
                {
                    var list = new Dictionary<string, string> { { "", "" } };
                    return list;
                }
                var allConfigList = new Dictionary<string, string>();
                foreach (var s in ConfigPath)
                {
                    var map = new ExeConfigurationFileMap { ExeConfigFilename = s };
                    var list = ConfigurationManager.OpenMappedExeConfiguration(map, 0).ConnectionStrings.ConnectionStrings;
                    foreach (ConnectionStringSettings c in list)
                    {
                        if (!allConfigList.ContainsKey(c.Name))
                        {
                            allConfigList.Add(c.Name, c.ConnectionString);
                        }
                    }
                }
                return allConfigList;
            }
        }

        public string GetConfigValue(string key)
        {
            return (from c in AllConfigPair where c.Key == key select c.Value).FirstOrDefault();
        }

        public string GetConnectionString(string connectionStringName)
        {
            return (from c in AllConnectionStringPair where c.Key == connectionStringName select c.Value).FirstOrDefault();
        }

    }
}