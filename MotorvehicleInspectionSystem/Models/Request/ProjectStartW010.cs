﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 检验项目开始实体类,安检接口，环保日志
    /// </summary>
    public class ProjectStartW010
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [XmlElement("jylsh")]
        public string Ajlsh { get; set; }
        /// <summary>
        /// 检验机构编号
        /// </summary>
       [XmlElement ("jyjgbh")]
        public string Jyjgbh { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        [XmlElement ("jcxdh")]
        public string Jcxh { get; set; }
        /// <summary>
        /// 检测次数
        /// </summary>
        [XmlElement ("jycs")]
        public int Ajjccs { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
       [XmlElement ("hpzl")]
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
       [XmlElement ("hphm")]
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        [XmlElement ("clsbdh")]
        public string Clsbdh { get; set; }
        /// <summary>
        /// 工位检验设备编号
        /// </summary>
        [XmlElement ("gwjysbbh")]
        public string Gwjysbbh { get; set; }
        /// <summary>
        /// 检验项目
        /// </summary>
       [XmlElement ("jyxm")]
        public string Jyxm { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [XmlElement ("kssj")]
        public string Kssj { get; set; }
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
        /// 环检流水号
        /// </summary>
       [XmlIgnore ]
        public string Hjlsh { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        [XmlIgnore ]
        public string Hjjccs { get; set; }

           
    }
}
