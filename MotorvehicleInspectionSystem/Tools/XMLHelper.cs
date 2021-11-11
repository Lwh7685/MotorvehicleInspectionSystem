using MotorvehicleInspectionSystem.Models.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Tools
{
    public class XMLHelper
    {
        /// <summary>
        /// 实体类转XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj)
        {
            using (System.IO.StringWriter sw = new Utf8StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ////不要命名空间
                ns.Add(string.Empty, string.Empty);
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj, ns);
                sw.Close();
                return sw.ToString();
            }
        }
        /// <summary>
        /// XML转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static string XmlSerializeStr<T>(T obj)
        {
            try
            {
                Root r = new Root();
                Body b = new Body();
                b.vehispara = obj ;
                r.body = b;
                //转换
                string strrxml = XmlSerialize<Root>(r);
                //返回
                return strrxml;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 读取节点值
        /// </summary>
        /// <param name="xmlDou"></param>
        /// <param name="nodeName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetNodeValue(XmlDocument xmlDou, string nodeName, string defaultValue = "")
        {
            XmlNode xmlnode = xmlDou.SelectSingleNode(".//" + nodeName);
            if (xmlnode == null)
                return defaultValue;
            else
                return xmlnode.InnerText;
        }
    }
    public class Root
    {
        public Body body { get; set; }
    }
    //注意！body类的vehispara的类型是dynamic 所以需要使用XmlInclude表示body可以解析的类型
    [System.Xml.Serialization.XmlInclude(typeof(ProjectStartW010))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectEndW012))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataNQ))]
    public partial class Body
    {
        public dynamic vehispara { get; set; }//接受动态业务类型 
    }
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

}
