using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    /// <summary>
    /// 开票订单明细
    /// </summary>
    public class EInvoiceDetail
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string Orderno { get; set; } 
        /// <summary>
        /// 明细编号
        /// </summary>
        public string Detailno { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Goodsname { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 单价含税标志，0:不含税,1:含税
        /// </summary>
        public string Hsbz { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public string Taxrate { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string Spbm { get; set; }
        /// <summary>
        /// 自行编码
        /// </summary>
        public string Zsbm { get; set; }
        /// <summary>
        /// 发票行性质:0,正常行;1,折扣行;2,被折扣行
        /// </summary>
        public string Fphxz { get; set; }
        /// <summary>
        /// 优惠政策标识:0,不使用;1,使用
        /// </summary>
        public string Yhzcbs { get; set; }
        /// <summary>
        /// 增值税特殊管理，如：即征即退、免税、简易征收 等
        /// </summary>
        public string Zzstsgl { get; set; }
        /// <summary>
        /// 零税率标识:空,非零税率;1,免税;2,不征税;3,普通零税率
        /// </summary>
        public string Lslbs { get; set; }
        /// <summary>
        /// 扣除额，小数点后两位。差额征收的发票目前 只支持一行 明细。不含税差额 = 不含税金额 - 扣除额；税额 = 不含税差额*税率。
        /// </summary>
        public string Kce { get; set; }
        /// <summary>
        /// 不含税金额
        /// </summary>
        public string Taxfreeamt { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public string Tax { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        public string Taxamt { get; set; }
    }
}
