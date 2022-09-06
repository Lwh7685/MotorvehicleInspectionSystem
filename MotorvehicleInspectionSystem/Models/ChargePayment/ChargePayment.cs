using MotorvehicleInspectionSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.ChargePayment
{
    /// <summary>
    /// 上传收费信息
    /// </summary>
    public class ChargePayment
    {
        /// <summary>
        /// 订单
        /// </summary>
        public ChargeOrder chargeOrder { get; set; }
        /// <summary>
        /// 明细【】
        /// </summary>
        public ChargeDetail[] chargeDetails { get; set; }
        /// <summary>
        /// 保存明细
        /// </summary>
        /// <returns></returns>
        public bool SaveDetails( DbUtility dbUtility)
        {
            try
            {
                string sqlStr = "";
                for (int i = 0; i <= chargeDetails.Count() - 1; i += 1)
                {
                    sqlStr = "INSERT INTO [dbo].[tb_ChargePaymentDetail] ";
                    sqlStr += " ([orderno],[detailno],goodsno,[goodsname],[num],[price],[zje]) VALUES( ";
                    sqlStr += " '" + chargeDetails[i].Orderno  + "',"; // (<orderno, nvarchar(20),>
                    sqlStr += " '" + chargeDetails[i].Detailno  + "',"; // ,<detailno, int,>
                    sqlStr += " '" + chargeDetails[i].Goodsno + "',";  // goodsno
                    sqlStr += " '" + chargeDetails[i].Goodsname + "',"; // ,<goodsname, nvarchar(90),>
                    sqlStr += " '" + chargeDetails[i].Num + "',"; // ,<num, int,>
                    sqlStr += " '" + chargeDetails[i].Price + "',"; // ,<price, decimal(16,2),>
                    sqlStr += " '" + chargeDetails[i].Zje + "')"; // ,<zje, varchar(16),>)"
                    int reInt = dbUtility.ExecuteNonQuery(sqlStr,null);
                    if (reInt !=1)
                        return false;
                }
                return true;
            }catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveOrder(DbUtility dbUtility)
        {
            try
            {
                string sqlStr = "INSERT INTO [dbo].[tb_ChargePaymentOrders]";
                sqlStr += " ([lsh],[appid],[c],[oid],[amt],[trxreserve],[sign],[addtime],[queryFlag]) VALUES(";
                sqlStr += " '" + chargeOrder.Ajlsh  + "',"; // (<lsh, varchar(32),>
                sqlStr += " '" + chargeOrder.Appid  + "',"; // ,<appid, varchar(8),>
                sqlStr += " '" + chargeOrder.C  + "',"; // ,<c, varchar(8),>
                sqlStr += " '" + chargeOrder.Oid  + "',"; // ,<oid, varchar(32),>
                sqlStr += " '" + chargeOrder.Amt  + "',"; // ,<amt, varchar(12),>
                sqlStr += " '" + chargeOrder.Trxreserve  + "',"; // ,<trxreserve, varchar(160),>
                sqlStr += " '" + chargeOrder.Sign  + "',"; // ,<sign, varchar(32),>
                sqlStr += " getdate(),"; // ,<addtime, datetime,>
                sqlStr += " '0')"; // ,<queryFlag, varchar(2),>)"
                int reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                if (reInt!=1)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
