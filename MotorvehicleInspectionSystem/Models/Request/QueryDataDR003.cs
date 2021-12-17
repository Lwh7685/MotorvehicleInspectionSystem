using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 查询数据字典的参数类，查询类接口的请求参数
    /// </summary>
    public class QueryDataDR003
    {
        /// <summary>
        /// 父类代码
        /// </summary>
        public string Fl { get; set; }
        /// <summary>
        /// 子类代码
        /// </summary>
        public string Dm { get; set; }
        /// <summary>
        /// 子类名称
        /// </summary>
        public string Mc { get; set; }
    }
}
