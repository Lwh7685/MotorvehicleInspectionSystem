using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 车辆上线
    /// </summary>
    public class StartDetectionW015
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 引车员
        /// </summary>
        public string Ycy { get; set; }
    }
}
