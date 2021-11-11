using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class ProjectDataDC
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
        /// 项目列表
        /// </summary>
        [XmlIgnore]
        public ProjectDataItem[] Xmlb { get; set; }
        /// <summary>
        /// 方向盘最大自由转动量
        /// </summary>
        [XmlElement("fxpzdzyzdl")]
        public string Fxpzdzyzdl { get; set; }
        /// <summary>
        /// 检验员建议
        /// </summary>
        [XmlElement ("jyyjy")]
        public string Jyyjy { get; set; }
        /// <summary>
        /// 底盘动态检验员
        /// </summary>
        [XmlElement ("dpdtjyy")]
        public string Dpdtjyy { get; set; }
        /// <summary>
        /// 底盘动态检验员身份证号
        /// </summary>
        [XmlElement ("dpdtjyysfzh")]
        public string Dpdtjyysfzh { get; set; }
        /// <summary>
        /// 引车员
        /// </summary>
        [XmlElement("ycy")]
        public string Ycy { get; set; }
        /// <summary>
        /// 引车员身份证号
        /// </summary>
        [XmlElement ("ycysfzh")]
        public string Ycysfzh { get; set; }
        /// <summary>
        /// 引车员建议
        /// </summary>
        [XmlElement("ycyjy")]
        public string Ycyjy { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement ("bz")]
        public string Bz { get; set; }
    }
}
