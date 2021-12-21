using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    public class EInvoiceOrder
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 号牌类型
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string Cllx { get; set; }
        /// <summary>
        /// 车主单位
        /// </summary>
        public string Czdw { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Amout { get; set; }
        public string Explain{ get; set; }
        public string Auditor { get; set; }
        public string Status { get; set; }
        public string Fpqqlsh { get; set; }
        public string Ret_message { get; set; }
        /// <summary>
        /// 身份认证
        /// </summary>
        public string Post_identity { get; set; }
        /// <summary>
        /// 购方名称
        /// </summary>
        public string Buyername { get; set; }
        /// <summary>
        /// 购方税号
        /// </summary>
        public string Taxnum { get; set; }
        /// <summary>
        /// 购方手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 购方地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 购方银行账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string Orderno { get; set; }
        /// <summary>
        /// 开票时间
        /// </summary>
        public string Invoicedate { get; set; }
        /// <summary>
        /// 开票员
        /// </summary>
        public string Clerk { get; set; }
        /// <summary>
        /// 销方银行账号
        /// </summary>
        public string Saleaccount { get; set; }
        /// <summary>
        /// 销方电话
        /// </summary>
        public string Salephone { get; set; }
        /// <summary>
        /// 销方地址
        /// </summary>
        public string Saleaddress { get; set; }
        /// <summary>
        /// 销方税号
        /// </summary>
        public string Saletaxnum { get; set; }
        /// <summary>
        /// 开票类型:1,正票;2,红 票
        /// </summary>
        public string Kptype { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public string Payee { get; set; }
        /// <summary>
        /// 复核人
        /// </summary>
        public string Checker { get; set; }
        /// <summary>
        /// 对应蓝票发票代码
        /// </summary>
        public string Fpdm { get; set; }
        /// <summary>
        /// 对应蓝票发票号码
        /// </summary>
        public string Fphm { get; set; }
        /// <summary>
        /// 推 送 方 式
        /// </summary>
        public string Tsfs { get; set; }
        /// <summary>
        /// 推送邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 清单标志
        /// </summary>
        public string Qdbz { get; set; }
        /// <summary>
        /// 清单项目名称
        /// </summary>
        public string Qdxmmc { get; set; }
        /// <summary>
        /// 购方电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 代开标志
        /// </summary>
        public string Dkbz { get; set; }
        /// <summary>
        /// 部门门店 id
        /// </summary>
        public string Deptid { get; set; }
        /// <summary>
        /// 开票员 id
        /// </summary>
        public string Clerkid { get; set; }
        /// <summary>
        /// 发票种类
        /// </summary>
        public string InvoiceLine { get; set; }
        public string C_orderno { get; set; }
        public string C_fpqqlsh { get; set; }
        public string C_status { get; set; }
        public string C_msg { get; set; }
        public string C_url { get; set; }
        public string C_jpg_url { get; set; }
        public string C_kprq { get; set; }
        public string C_fpdm { get; set; }
        public string C_fphm { get; set; }
        public string C_bhsje { get; set; }
        public string C_hsje { get; set; }
        public string C_resultmsg { get; set; }
        public string C_invoiceid { get; set; }
        public string C_buyername { get; set; }
        public string C_taxnum { get; set; }
        public string C_jym { get; set; }
    }
}
