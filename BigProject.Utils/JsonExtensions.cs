using System;
using Newtonsoft.Json;

namespace BigProject.Utils
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 对象转为json字符串
        /// 序列化
        /// </summary>
        /// <param name = "o"></param>
        /// <returns></returns>
        public static string SerializeObject(this object o)
        {
            if (o == null)
            {
                return string.Empty;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }

        public static string SerializeObject(this object o, Newtonsoft.Json.Formatting f)
        {
            if (o == null)
            {
                return string.Empty;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(o, f);
        }

        /// <summary>
        /// 字符串解析为对象
        /// 反序列化
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "str"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string str)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception)
            {
                return default (T);
            }
        }

        /// <summary>
        /// 根据key获得json对象
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "str"></param>
        /// <param name = "key"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string str, string key)
        {
            try
            {
                var p = Newtonsoft.Json.Linq.JObject.Parse(str);
                return JsonConvert.DeserializeObject<T>(p[key].ToString());
            }
            catch (Exception)
            {
            }

            return default (T);
        }
    }
}