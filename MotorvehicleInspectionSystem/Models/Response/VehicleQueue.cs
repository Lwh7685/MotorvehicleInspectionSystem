using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 机动车队列，所有功能的车辆选择都从这里开始
    /// </summary>
    public class VehicleQueue
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public string Lsh { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌种类的汉字标识
        /// </summary>
        public string HpzlCc { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime Djrq { get; set; }
        /// <summary>
        /// 业务类别
        /// </summary>
        public string Ywlb { get; set; }
        /// <summary>
        /// 检验状态
        /// </summary>
        public string Jyzt { get; set; }
        /// <summary>
        /// 号牌颜色
        /// </summary>
        public string Hpys { get; set; }
        /// <summary>
        /// 号牌颜色的汉字标识
        /// </summary>
        public string HpysCc { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 安检业务类别汉字标识
        /// </summary>
        public string AjywlbCc { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 环检业务类别汉字标识
        /// </summary>
        public string HjywlbCc { get; set; }

    }
}
