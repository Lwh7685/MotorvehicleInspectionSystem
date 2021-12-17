using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 申请审核队列
    /// </summary>
    public class ModerationQueueR013
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string Clsbdh { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string Cllx { get; set; }
        /// <summary>
        /// 安检检测次数
        /// </summary>
        public string Ajjccs { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        public string Hjjccs { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 安检申请人
        /// </summary>
        public string Ajsqr { get; set; }
        /// <summary>
        /// 环检申请人
        /// </summary>
        public string Hjsqr { get; set; }
        /// <summary>
        /// 安检申请时间
        /// </summary>
        public string Ajsqsj { get; set; }
        /// <summary>
        /// 环检申请时间
        /// </summary>
        public string Hjsqsj { get; set; }
        /// <summary>
        /// 安检检测时间
        /// </summary>
        public string Ajjcsj { get; set; }
        /// <summary>
        /// 环检检测时间
        /// </summary>
        public string Hjjcsj { get; set; }        
    }
}
