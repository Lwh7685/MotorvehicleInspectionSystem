using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 上传审核信息
    /// </summary>
    public class Examine
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 安检申请次数
        /// </summary>
        public int Ajsqcs { get; set; }
        /// <summary>
        /// 环检申请次数
        /// </summary>
        public int Hjsqcs { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Shr { get; set; }
        /// <summary>
        /// 审核人身份证号
        /// </summary>
        public string Shrsfzh { get; set; }
        /// <summary>
        /// 审核次数
        /// </summary>
        public string Shcs { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        public string Shjg { get; set; }
        /// <summary>
        /// 审核说明
        /// </summary>
        public string Shsm { get; set; }
        /// <summary>
        /// 签名  Base64
        /// </summary>
        public string Qm { get; set; }
    }
}
