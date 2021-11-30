using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    public class InspectionItemsR006
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Lsh { get; set; }
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 安检检测次数
        /// </summary>
        public int Ajjccs { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        public int Hjjccs { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Xmmc { get; set; }
        /// <summary>
        /// 检测次数
        /// </summary>
        public int  Jccs { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 检测人员1
        /// </summary>
        public string Jcry_01 { get; set; }
        /// <summary>
        /// 检测人员2
        /// </summary>
        public string Jcry_02 { get; set; }
        /// <summary>
        /// 检测开始时间
        /// </summary>
        public DateTime Jckssj { get; set; }
        /// <summary>
        /// 检测结束时间
        /// </summary>
        public DateTime Jcjssj { get; set; }
        /// <summary>
        /// 检测评价
        /// </summary>
        public string Jcpj { get; set; }
        /// <summary>
        /// 检测状态
        /// </summary>
        public string Jczt { get; set; }
    }

    //public class InspectionItemCarInfo
    //{
    //    public string Lsh { get; set; }
    //    public string Jccs { get; set; }
    //    public string Ajywlb { get; set; }
    //    public string Hjywlb { get; set; }
    //    public InspectionItemsR006[] inspectionItems { get; set; }
    //}
}
