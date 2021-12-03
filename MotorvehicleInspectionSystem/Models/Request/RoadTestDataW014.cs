using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class RoadTestDataW014
    {
        public string Ajlsh { get; set; }
        public string Hjlsh { get; set; }
        public string Hjywlb { get; set; }
        public string Ajywlb { get; set; }
        public int Ajjccs { get; set; }
        public int Hjjccs { get; set; }
        public string Jyjgbh { get; set; }
        public string Jcxh { get; set; }
        public string Jcxm { get; set; }
        public string Hphm { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        [XmlElement ("hpzl")]
        public string Hpzl { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        [XmlElement ("clsbdh")]
        public string Clsbdh { get; set; }
        /// <summary>
        /// 路试员姓名
        /// </summary>
        [XmlElement ("lsy")]
        public string Lsy { get; set; }
        /// <summary>
        /// 行车制动初速度
        /// </summary>
        [XmlElement ("zdcsd")]
        public string Zdcsd { get; set; }
        /// <summary>
        /// 行车制动协调时间
        /// </summary>
        [XmlElement ("zdxtsj")]
        public string Zdxtsj { get; set; }
        /// <summary>
        /// 行车制动稳定性
        /// </summary>
        [XmlElement ("zdwdx")]
        public string Zdwdx { get; set; }
        /// <summary>
        /// 行车空载制动距离
        /// </summary>
        [XmlElement ("xckzzdjl")]
        public string Xckzzdjl { get; set; }
        /// <summary>
        /// 行车满载制动制动距离
        /// </summary>
        [XmlElement ("xcmzzdjl")]
        public string Xcmzzdjl { get; set; }
        /// <summary>
        /// 行车空载制动MFDD
        /// </summary>
        [XmlElement("xckzmfdd")]
        public string Xckzmfdd { get; set; }
        /// <summary>
        /// 行车满载制动MFDD
        /// </summary>
        [XmlElement ("xcmzmfdd")]
        public string Xcmzmfdd { get; set; }
        /// <summary>
        /// 行车制动踏板力值
        /// </summary>
        [XmlElement ("xczdczlz")]
        public string Xczdczlz { get; set; }
        /// <summary>
        /// 行车路试制动判定
        /// </summary>
        [XmlElement ("lszdpd")]
        public string Lszdpd { get; set; }
        /// <summary>
        /// 应急制动初速度
        /// </summary>
       [XmlElement ("yjzdcsd")]
        public string Yjzdcsd { get; set; }
        /// <summary>
        /// 应急空载制动距离
        /// </summary>
        [XmlElement ("yjkzzdjl")]
        public string Yjkzzdjl { get; set; }
        /// <summary>
        /// 应急空载 MFDD
        /// </summary>
       [XmlElement ("yjkzmfdd")]
        public string Yjkzmfdd { get; set; }
        /// <summary>
        /// 应急满载制动距离
        /// </summary>
        [XmlElement ("yjmzzdjl")]
        public string Yjmzzdjl { get; set; }
        /// <summary>
        /// 应急满载 MFDD
        /// </summary>
        [XmlElement ("yjmzmfdd")]
        public string Yjmzmfdd { get; set; }
        /// <summary>
        /// 应急操纵力方式
        /// </summary>
        [XmlElement ("yjzdczlfs")]
        public string Yjzdczlfs { get; set; }
        /// <summary>
        /// 应急操纵力值
        /// </summary>
        [XmlElement ("yjzdczlz")]
        public string Yjzdczlz { get; set; }
        /// <summary>
        /// 应急路试制动判定
        /// </summary>
        [XmlElement ("yjzdpd")]
        public string Yjzdpd { get; set; }
        /// <summary>
        /// 驻车坡度
        /// </summary>
        [XmlElement ("zcpd")]
        public string Zcpd { get; set; }
        /// <summary>
        /// 路试驻车制动判定
        /// </summary>
        [XmlElement ("lszczdpd")]
        public string Lszczdpd { get; set; }
        /// <summary>
        /// 路试结果
        /// </summary>
        [XmlElement("lsjg")]
        public string Lsjg { get; set; }
        /// <summary>
        /// 驻车拉力
        /// </summary>
        [XmlIgnore]
        public string Zcll { get; set; }
    }
}
