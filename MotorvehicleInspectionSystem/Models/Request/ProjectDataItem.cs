using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 人工检验项目明细结果
    /// </summary>
    public class ProjectDataItem
    {
        /// <summary>
        /// 项目代码
        /// </summary>
        public string Xmdm { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Xmms { get; set; }
        /// <summary>
        /// 项目评价
        /// </summary>
        public string Xmpj { get; set; }
        /// <summary>
        /// 项目备注
        /// </summary>
        public string Xmbz { get; set; }
    }
}
