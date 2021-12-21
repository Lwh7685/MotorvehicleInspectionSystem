using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 查询线上检验进度
    /// </summary>
    public class InspectionProgressR024
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 线上状态  isonline
        /// </summary>
        public string Xszt { get; set; }
        /// <summary>
        /// 检测工位
        /// </summary>
        public StationStatus[] Jcgw { get; set; }
    }
    /// <summary>
    /// 工位状态
    /// </summary>
    public class StationStatus
    {
        /// <summary>
        /// 工位名称
        /// </summary>
        public string Gwmc { get; set; }
        /// <summary>
        /// 工位状态
        /// </summary>
        public string Gwzt { get; set; }
    }

}
