using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.ChargePayment
{
    /// <summary>
    /// 支付明细
    /// </summary>
    public class ChargeDetail
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Orderno;
        /// <summary>
        /// 明细编号
        /// </summary>
        public string Detailno;
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Goodsno;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Goodsname;
        /// <summary>
        /// 数量
        /// </summary>
        public string Num;
        /// <summary>
        /// 单价
        /// </summary>
        public string Price;
        /// <summary>
        /// 总金额
        /// </summary>
        public string Zje;
    }
}
