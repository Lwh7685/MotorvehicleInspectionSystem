using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 行政区划  LYYDJKR023
    /// </summary>
    public class AdministrativeRegionR023
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public string Xzqhdm { get; set; }
        /// <summary>
        /// 行政区划名称
        /// </summary>
        public string Xzqhmc { get; set; }
    }
}
