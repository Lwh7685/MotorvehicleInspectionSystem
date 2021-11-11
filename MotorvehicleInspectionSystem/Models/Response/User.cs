using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("ID")]
        public string SfzInfoID { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string GongHao { get; set; }
        /// <summary>
        /// 用户名，唯一
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 真实姓名，监管接口使用
        /// </summary>
        [JsonProperty ("TrueName")]
        public string turename { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime  AddDate { get; set; }
        /// <summary>
        /// 角色代码，多个角色以","分开
        /// </summary>
        public string RoleDm { get; set; }
    }
}
