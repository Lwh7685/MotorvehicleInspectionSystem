using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 用户登录接口
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
    }
}
