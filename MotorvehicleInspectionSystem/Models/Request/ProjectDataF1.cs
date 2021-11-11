using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class ProjectDataF1
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
        /// 车外廓长
        /// </summary>
        [XmlElement("cwkc")]
        public string Cwkc { get; set; }
        /// <summary>
        /// 车外廓宽
        /// </summary>
        [XmlElement ("cwkk")]
        public string Cwkk { get; set; }
        /// <summary>
        /// 车外廓高
        /// </summary>
        [XmlElement ("cwkg")]
        public string Cwkg { get; set; }
        /// <summary>
        /// 轴距
        /// </summary>
        [XmlElement("zj")]
        public string Zj { get; set; }
        /// <summary>
        /// 车厢栏板高度
        /// </summary>
        [XmlElement ("cxlbgd")]
        public string Cxlbgd { get; set; }
        /// <summary>
        /// 单车转向轮轮胎花纹深度
        /// </summary>
        [XmlElement("dczxlhwsd")]
        public string Dczxlhwsd { get; set; }
        /// <summary>
        /// 单车其他轮轮胎花纹深度
        /// </summary>
        [XmlElement("dcqtlhwsd")]
        public string Dcqtlhwsd { get; set; }
        /// <summary>
        /// 挂车轮胎花纹深度
        /// </summary>
        [XmlElement ("gchwsd")]
        public string Gchwsd { get; set; }
        /// <summary>
        /// 第一轴左高度
        /// </summary>
        [XmlElement("yzzgd")]
        public string Yzzgd { get; set; }
        /// <summary>
        /// 第一轴右高度
        /// </summary>
        [XmlElement("yzygd")]
        public string Yzygd { get; set; }
        /// <summary>
        /// 第一轴左右高度差
        /// </summary>
        [XmlElement ("yzzygdc")]
        public string Yzzygdc { get; set; }
        /// <summary>
        /// 最后轴左高度
        /// </summary>
        [XmlElement ("zhzzgd")]
        public string Zhzzgd { get; set; }
        /// <summary>
        /// 最后轴右高度
        /// </summary>
        [XmlElement ("zhzygd")]
        public string Zhzygd { get; set; }
        /// <summary>
        /// 最后轴左右高度差
        /// </summary>
        [XmlElement("zhzzygdc")]
        public string Zhzzygdc { get; set; }
        /// <summary>
        /// 是否全时/适时四驱
        /// </summary>
        [XmlElement("sfqssq")]
        public string Sfqssq { get; set; }
        /// <summary>
        /// 驻车制动是否使用电子控制装置
        /// </summary>
        [XmlElement("sfdzzc")]
        public string Sfdzzc { get; set; }
        /// <summary>
        /// 是否配备空气悬架
        /// </summary>
        [XmlElement("sfkqxj")]
        public string Sfkqxj { get; set; }
        /// <summary>
        /// 空气悬架轴
        /// </summary>
        [XmlElement("kqxjz")]
        public string Kqxjz { get; set; }
        /// <summary>
        /// 转向轴数量
        /// </summary>
        [XmlElement ("zxzsl")]
        public string Zxzsl { get; set; }
        /// <summary>
        /// 检验员建议
        /// </summary>
        [XmlElement ("jyyjy")]
        public string Jyyjy { get; set; }
        /// <summary>
        /// 外观检验员
        /// </summary>
        [XmlElement("wgjcjyy")]
        public string Wgjcjyy { get; set; }
        /// <summary>
        /// 外观检验员身份证号
        /// </summary>
        [XmlElement ("wgjcjyysfzh")]
        public string Wgjcjyysfzh { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement ("bz")]
        public string Bz { get; set; }
    }
}
