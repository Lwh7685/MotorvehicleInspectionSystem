using MotorvehicleInspectionSystem.Models;
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
                //Type t = obj.GetType();
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
                string a = ex.Message;
                return null;
            }
        }
        


        public static string XmlSerializeStr<T>(T obj, string jklb="Write")
        {
            try
            {
                Root r = new Root();
                if(jklb == "Write")
                {
                    r.vehispara = obj;
                }
                else
                {
                    r.QueryCondition = obj;
                }
                //转换
                string strrxml = XmlSerialize<Root>(r);
                //返回
                return strrxml;
            }
            catch 
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
        public static string GetNodeXML(XmlDocument xmlDou, string nodeName, string defaultValue = "")
        {
            XmlNode xmlnode = xmlDou.SelectSingleNode(".//" + nodeName);
            if (xmlnode == null)
                return defaultValue;
            else
                return xmlnode.InnerXml ;
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        public static void AddNode(System.Xml.XmlElement parent, string nodeName, string nodeValue)
        {
            System.Xml.XmlElement newNode;
            newNode = parent.OwnerDocument.CreateElement(nodeName);
            newNode.InnerText = nodeValue;
            parent.AppendChild(newNode);
            parent.AppendChild(parent.OwnerDocument.CreateTextNode(System.Environment.NewLine));
        }
    }
    //注意！body类的vehispara的类型是dynamic 所以需要使用XmlInclude表示body可以解析的类型
    [System.Xml.Serialization.XmlInclude(typeof(ProjectStartW010))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectEndW012))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataNQ))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataUC))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataF1))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataDC))]
    [System.Xml.Serialization.XmlInclude(typeof(ProjectDataC1))]
    [System.Xml.Serialization.XmlInclude(typeof(NetworkQueryR022 ))]
    [XmlInclude(typeof(VehicleDetails))]
    
    public partial class Root
    {
        public dynamic vehispara { get; set; }//接受动态业务类型 
        public dynamic QueryCondition { get; set; }
    }
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
    
}
