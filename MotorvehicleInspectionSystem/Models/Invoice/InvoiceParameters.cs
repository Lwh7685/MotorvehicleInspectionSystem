using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    /// <summary>
    /// 开票参数
    /// </summary>
    public class InvoiceParameters
    {
        /// <summary>
        /// 收款单位识别号
        /// </summary>
        public string Skdwsbh { get; set; }
        /// <summary>
        /// 收款单位名称
        /// </summary>
        public string Skdwmc { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Shr { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Dh { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Dz { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string Khh { get; set; }
        /// <summary>
        /// 发票接口身份证识别码
        /// </summary>
        public string Fpjksfsbm { get; set; }
        /// <summary>
        /// 部门代码
        /// </summary>
        public string Bmdm { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Spbm { get; set; }
    }
}
