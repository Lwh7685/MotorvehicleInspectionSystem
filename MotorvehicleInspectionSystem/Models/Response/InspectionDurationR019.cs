using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    //查询人工检验项目要求最短检验时长
    public class InspectionDurationR019
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 要求时长 s
        /// </summary>
        public string Yqsc { get; set; }
    }
}
