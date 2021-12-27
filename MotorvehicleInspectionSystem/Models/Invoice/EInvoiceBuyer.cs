using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    /// <summary>
    /// 购方
    /// </summary>
    public class EInvoiceBuyer
    {
        /// <summary>
        /// 税号
        /// </summary>
        public string Taxnum { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Buyername { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string OpenBank { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }
    }
}
