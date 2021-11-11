using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class ProjectDataNQ
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [XmlElement("jylsh")]
        public string Lsh { get; set; }
        /// <summary>
        /// 检验机构编号
        /// </summary>
        [XmlElement("jyjgbh")]
        public string Jyjgbh { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        [XmlElement("jcxdh")]
        public string Jcxh { get; set; }
        /// <summary>
        /// 检测次数
        /// </summary>
        [XmlElement("jycs")]
        public int Jccs { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        [XmlElement("hpzl")]
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        [XmlElement("hphm")]
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        [XmlElement("clsbdh")]
        public string Clsbdh { get; set; }
        /// <summary>
        /// 检验项目
        /// </summary>
        [XmlElement("jyxm")]
        public string Jyxm { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        [XmlIgnore]
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        [XmlIgnore]
        public string Hjywlb { get; set; }
        /// <summary>
        /// 安检接口序列号
        /// </summary>
        [XmlIgnore]
        public string AjJkxlh { get; set; }

        /// <summary>
        /// 联网查询结果描述
        /// </summary>
        [XmlElement("lwcxjgms")]
        public string Lwcxjgms { get; set; }
        /// <summary>
        /// 机动车所有人
        /// </summary>
        [XmlElement("syr")]
        public string Syr { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [XmlElement("sjhm")]
        public string Sjhm { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [XmlElement("lxdz")]
        public string Lxdz { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [XmlElement("yzbm")]
        public string Yzbm { get; set; }
        /// <summary>
        /// 项目列表
        /// </summary>
        [XmlIgnore ]
        public ProjectDataItem [] Xmlb { get; set; }
    }
}
