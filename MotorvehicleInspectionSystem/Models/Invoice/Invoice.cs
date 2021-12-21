using MotorvehicleInspectionSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    public class Invoice
    {
        public EInvoiceOrder invoiceOrder { get; set; }
        public EInvoiceDetail[] eInvoiceDetails { get; set; }
        /// <summary>
        /// 保存订单表
        /// </summary>
        /// <param name="dbUtility"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool SaveInvoiceOrder(DbUtility dbUtility, string ip)
        {
            try
            {
                string SqlStr = "INSERT INTO [T_EInvoiceOrder]";
                SqlStr = SqlStr + " ([jylsh],[hplx],[hphm],[czdw],[tel],[amout],[explain],[auditor],[status],[fpqqlsh],[ret_message]";
                SqlStr = SqlStr + " ,[post_identity],[buyername],[taxnum],[phone],[address],[account],[orderno],[invoicedate],[clerk]";
                SqlStr = SqlStr + " ,[saleaccount],[salephone],[saleaddress],[saletaxnum],[kptype],[message],[payee],[checker],[fpdm]";
                SqlStr = SqlStr + " ,[fphm],[tsfs],[email],[qdbz],[qdxmmc],[telephone],[dkbz],[deptid],[clerkid],[invoiceLine],[c_fpdm],[c_fphm],[ex1],[ex2],[ex4]) VALUES";
                SqlStr = SqlStr + " ('" + invoiceOrder.Ajlsh + "'";  // (<jylsh, nvarchar(24),>"
                SqlStr = SqlStr + " ,'" + invoiceOrder.Hpzl + "'";  // ,<hplx, nvarchar(16),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Hphm + "'";  // ,<hphm, nvarchar(24),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Czdw + "'";  // ,<czdw, nvarchar(128),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Tel + "'";  // ,<tel, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Amout + "'";  // ,<amout, numeric(16,2),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Explain + "'";   // ,<explain, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Auditor + "'";  // ,<auditor, nvarchar(24),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Status + "'";  // ,<status, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Fpqqlsh + "'";  // ,<fpqqlsh, nvarchar(50),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Ret_message + "'";  // ,<ret_message, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Post_identity + "'";  // ,<post_identity, nvarchar(128),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Buyername + "'";  // ,<buyername, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Taxnum + "'";  // ,<taxnum, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Phone + "'";  // ,<phone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Address + "'";  // ,<address, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Account + "'";  // ,<account, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Orderno + "'";  // ,<orderno, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Invoicedate + "'";  // ,<invoicedate, datetime,>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Clerk + "'";  // ,<clerk, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Saleaccount + "'";  // ,<saleaccount, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Salephone + "'";  // ,<salephone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Saleaddress + "'";  // ,<saleaddress, nvarchar(80),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Saletaxnum + "'";  // ,<saletaxnum, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Kptype + "'";  // ,<kptype, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Message + "'";  // ,<message, nvarchar(130),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Payee + "'";  // ,<payee, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Checker + "'";  // ,<checker, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Fpdm + "'";  // ,<fpdm, nvarchar(12),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Fphm + "'";  // ,<fphm, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Tsfs + "'";  // ,<tsfs, int,>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Email + "'";  // ,<email, nvarchar(50),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Qdbz + "'";  // ,<qdbz, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Qdxmmc + "'";  // ,<qdxmmc, nvarchar(90),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Telephone + "'";  // ,<telephone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Dkbz + "'";  // ,<dkbz, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Deptid + "'";  // ,<deptid, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Clerkid + "'";  // ,<clerkid, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.InvoiceLine + "'";   // ,<invoiceLine, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.C_fpdm + "'";    // ,<c_fpdm, char(1),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.C_fphm + "'";    // ,<c_fphm, char(1),>
                SqlStr = SqlStr + " ,'" + ip + "*' + " + "convert(VarChar(20), getdate(), 120)" + "+'*" + invoiceOrder.Clerk + "'"; // ,<ex1, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Clerk + "'";  // ,<ex2, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + invoiceOrder.Cllx + "')";  // ,<ex4, nvarchar(1024),>  ''' 车辆类型
                int reint = dbUtility.ExecuteNonQuery(SqlStr, null);
                if (reint == 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存明细表
        /// </summary>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveInvoiceDetails(DbUtility dbUtility)
        {
            string SqlStr = "";
            try
            {
                foreach (EInvoiceDetail detailInfo in eInvoiceDetails)
                {
                    SqlStr = "Delete from T_EInvoiceDetail where orderno='" + detailInfo.Orderno + "' and price='" + Math.Round(Convert.ToDouble(detailInfo.Price), 2) + "' and goodsname='" + detailInfo.Goodsname + "'";
                    int reint = dbUtility.ExecuteNonQuery(SqlStr, null);
                    SqlStr = "INSERT INTO [T_EInvoiceDetail]";
                    SqlStr = SqlStr + "([orderno],[detailno],[goodsname],[num],[price],[hsbz],[taxrate],[spec],[unit],[spbm],[zsbm]";
                    SqlStr = SqlStr + ",[fphxz],[yhzcbs],[zzstsgl],[lslbs],[kce],[taxfreeamt],[tax],[taxamt]) VALUES";
                    SqlStr = SqlStr + "('" + detailInfo.Orderno + "' ";  // <orderno, nvarchar(20),>"
                    SqlStr = SqlStr + ",'" + System.Convert.ToString(detailInfo.Detailno) + "' ";  // <detailno, int,>"
                    SqlStr = SqlStr + ",'" + detailInfo.Goodsname + "' ";  // <goodsname, nvarchar(90),>"
                    SqlStr = SqlStr + ",'" + System.Convert.ToString(detailInfo.Num) + "' ";  // <num, int,>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Price), 2) + "' "; // <price, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Hsbz + "' ";  // <hsbz, char(1),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Taxrate + "' ";  // <taxrate, numeric(1,1),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Spec + "' ";  // <spec, nvarchar(40),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Unit + "' ";  // <unit, nvarchar(20),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Spbm + "' ";  // <spbm, nvarchar(19),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Zsbm + "' ";  // <zsbm, nvarchar(20),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Fphxz + "' ";  // <fphxz, char(1),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Yhzcbs + "' ";  // <yhzcbs, char(1),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Zzstsgl + "' ";  // <zzstsgl, nvarchar(50),>"
                    SqlStr = SqlStr + ",'" + detailInfo.Lslbs + "' ";  // <lslbs, char(1),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Kce), 2) + "' "; // <kce, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Taxfreeamt), 2) + "' "; // <taxfreeamt, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Tax), 2) + "' "; // <tax, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Taxamt), 2) + "') "; // <taxamt, numeric(16,2),>)"
                    reint = dbUtility.ExecuteNonQuery(SqlStr, null);
                    if (reint != 1)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
