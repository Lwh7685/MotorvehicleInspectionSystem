using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class ProjectData
    {
        /// <summary>
        /// 检验项目
        /// </summary>
        public string Jyxm { get; set; }
        /// <summary>
        /// 检测数据
        /// </summary>
        public object  Jcsj { get; set; }
    }
}
