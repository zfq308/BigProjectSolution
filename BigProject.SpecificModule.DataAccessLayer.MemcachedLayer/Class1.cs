using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Enyim.Caching;
using System.Diagnostics;
using System.Net;
using System.IO;
using BigProject.Utils;

namespace BigProject.SpecificModule.DataAccessLayer.MemcachedLayer
{

    [Serializable]
    public class a
    {
        public string b { get; set; }
        public string c { get; set; }
    }

    public class MemCachedHelper
    {
        #region Definition

        IPAddress serverIP = null;
        int port = 11211;
        string authenticationUserName = "";
        string authenticationPassword = "";
        MemcachedClient mac = null;
        MemcachedClient MemcachedClient
        {
            get
            {
                MemcachedClientConfiguration config = new MemcachedClientConfiguration();//创建配置参数
                config.Protocol = MemcachedProtocol.Text;//随便哪个枚举都行

                //协议修改成 config.Protocol = MemcachedProtocol.Text; 就可以加验证模式了。
                //config.Authentication.Type = typeof(PlainTextAuthenticator);//设置验证模式
                //config.Authentication.Parameters["userName"] = "memcache";//用户名参数
                //config.Authentication.Parameters["password"] = "password";//密码参数
                if (mac == null)
                {
                    mac = new MemcachedClient(config);
                }
                return mac;
            }
        }

        #endregion

        #region Constructure

        private MemCachedHelper() { }

        public MemCachedHelper(string ServerIP, int Port, string AuthenticationUserName = "", string AuthenticationPassword = "")
        {
            try
            {
                serverIP = IPAddress.Parse(ServerIP);
            }
            catch (Exception)
            {
                serverIP = IPAddress.Parse("127.0.0.1");
            }
            port = Port;
            authenticationUserName = AuthenticationUserName;
            authenticationPassword = AuthenticationPassword;

        }
        public MemCachedHelper(IPAddress ServerIP, int Port, string AuthenticationUserName = "", string AuthenticationPassword = "")
        {
            serverIP = ServerIP;
            port = Port;
            authenticationUserName = AuthenticationUserName;
            authenticationPassword = AuthenticationPassword;
        }

        #endregion


        public bool Store(string key, object value, TimeSpan expiresTime = default(TimeSpan))
        {

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            bool result = false;
            result = mac.Store(StoreMode.Add, key, value, expiresTime);//写入
            //sw.Stop();
            //TimeSpan CostTime = sw.Elapsed;
            return result;
        }

        public bool Update(string key, object value, TimeSpan expiresTime = default(TimeSpan))
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            bool result = false;
            result = mac.Store(StoreMode.Set, key, value, expiresTime);//写入
            //sw.Stop();
            //TimeSpan CostTime = sw.Elapsed;
            return result;
        }


        public void test()
        {
            var obj = new a()
            {
                b = "abcd",
                c = "defg",
            };

            string setstring = "abcdefg";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();//创建配置参数

            config.Servers.Add(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 11211));//增加服务节点
            config.Protocol = MemcachedProtocol.Text;//随便哪个枚举都行
            //config.Authentication.Type = typeof(PlainTextAuthenticator);//设置验证模式
            //config.Authentication.Parameters["userName"] = "memcache";//用户名参数
            //config.Authentication.Parameters["password"] = "password";//密码参数
            using (var mac = new MemcachedClient(config))//创建连接
            {
                //STRING
                mac.Store(StoreMode.Add, "textbox11", setstring);//写入
                mac.Store(StoreMode.Set, "textbox11", setstring);//更新
                                                                 //JSON
                mac.Store(StoreMode.Add, "aa1", obj.SerializeObject());
                //字节数组
                var bytes = SerializeBinary(obj);
                mac.Store(StoreMode.Add, "bytes1", bytes);

                //string str = mac.Get<string>("textbox1");//获取信息
                //string cache = mac.Get<string>("aa");
                //var cacheObj = cache.DeserializeObject<a>();
                //var cachebytes = DeserializeBinary(mac.Get("bytes") as byte[]) as a;


                string str = mac.Get<string>("textbox11");//获取信息
                Console.WriteLine(str);
            }
            sw.Stop();


        }

        ///   <summary>   
        ///   序列化为二进制字节数组   
        ///   </summary>   
        ///   <param   name="request">要序列化的对象</param>   
        ///   <returns>字节数组</returns>   
        public static byte[] SerializeBinary(object request)
        {
            var serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (MemoryStream memStream = new System.IO.MemoryStream())
            {
                serializer.Serialize(memStream, request);
                return memStream.GetBuffer();
            }
        }

        ///   <summary>   
        ///   从二进制数组反序列化得到对象   
        ///   </summary>   
        ///   <param   name="buf">字节数组</param>   
        ///   <returns>得到的对象</returns>   
        public static object DeserializeBinary(byte[] buf)
        {
            using (MemoryStream memStream = new MemoryStream(buf))
            {
                memStream.Position = 0;
                var deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                object newobj = deserializer.Deserialize(memStream);
                memStream.Close();
                return newobj;
            }
        }

    }
}
