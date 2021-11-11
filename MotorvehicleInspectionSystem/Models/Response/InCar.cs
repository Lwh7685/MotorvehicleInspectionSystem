using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 入场车辆队列
    /// </summary>
    public class InCar
    {
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 入场时间
        /// </summary>
        public DateTime Rcsj { get; set; }
        /// <summary>
        /// 预检状态
        /// </summary>
        public string Yjzt { get; set; }
    }
}
