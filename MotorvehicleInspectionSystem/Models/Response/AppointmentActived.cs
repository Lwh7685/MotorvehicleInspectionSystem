using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 已预约激活队列
    /// </summary>
    public class AppointmentActived
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string Clsbdh { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 预约来源 :  1: 线上预约、2: 线下预约
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// 送检人
        /// </summary>
        public string Sjr { get; set; }
        /// <summary>
        /// 送检人电话
        /// </summary>
        public string Sjrdh { get; set; }
        /// <summary>
        /// 送检人身份证号
        /// </summary>
        public string Sjrsfzh { get; set; }
    }
}
