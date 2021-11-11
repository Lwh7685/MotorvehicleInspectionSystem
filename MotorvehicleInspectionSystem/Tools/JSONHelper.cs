using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Tools
{
    public class JSONHelper
    {
        /// <summary>
        /// 将实体类序列化为JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(T data)
        {
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch (JsonReaderException e)
            {
                throw new JsonReaderException(e.Message);
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException(e.Message);
            }

        }
        /// <summary>
        /// 反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string json)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            catch(JsonReaderException e)
            {
                throw new JsonReaderException(e.Message );
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime dateTime = new DateTime(1993, 1, 2, 3, 4, 5, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - dateTime).TotalMilliseconds;
        }
        /// <summary>
        /// 将object对象转换为实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类名</typeparam>
        /// <param name="asObject">object对象</param>
        /// <returns></returns>
        public static T ConvertObject<T>(object asObject) where T : new()
        {
            //将object对象转换为json字符
            var json = JsonConvert.SerializeObject(asObject);
            //将json字符转换为实体对象
            var t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }
    }
}
