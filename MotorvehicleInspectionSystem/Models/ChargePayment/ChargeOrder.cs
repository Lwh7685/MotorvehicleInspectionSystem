using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.ChargePayment
{
    /// <summary>
    /// 支付订单
    /// </summary>
    public class ChargeOrder
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 通联分配的appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 终端编号
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Oid { get; set; }
        /// <summary>
        /// 交易金额   单位:分
        /// </summary>
        public string Amt { get; set; }
        /// <summary>
        /// 业务备注信息
        /// </summary>
        public string Trxreserve { get; set; }
        /// <summary>
        /// sign校验码
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string Addtime { get; set; }
        /// <summary>
        /// 查询标志   0
        /// </summary>
        public string Queryflag { get; set; }
    }
}
