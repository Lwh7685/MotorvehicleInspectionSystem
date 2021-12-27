using MotorvehicleInspectionSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Invoice
{
    public class Invoice
    {
        public EInvoiceOrder InvoiceOrder { get; set; }
        public EInvoiceDetail[] InvoiceDetails { get; set; }
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
                SqlStr = SqlStr + " ('" + InvoiceOrder.Ajlsh + "'";  // (<jylsh, nvarchar(24),>"
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Hpzl + "'";  // ,<hplx, nvarchar(16),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Hphm + "'";  // ,<hphm, nvarchar(24),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Czdw + "'";  // ,<czdw, nvarchar(128),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Tel + "'";  // ,<tel, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Amout + "'";  // ,<amout, numeric(16,2),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Explain + "'";   // ,<explain, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Auditor + "'";  // ,<auditor, nvarchar(24),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Status + "'";  // ,<status, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Fpqqlsh + "'";  // ,<fpqqlsh, nvarchar(50),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Ret_message + "'";  // ,<ret_message, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Post_identity + "'";  // ,<post_identity, nvarchar(128),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Buyername + "'";  // ,<buyername, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Taxnum + "'";  // ,<taxnum, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Phone + "'";  // ,<phone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Address + "'";  // ,<address, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Account + "'";  // ,<account, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Orderno + "'";  // ,<orderno, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Invoicedate + "'";  // ,<invoicedate, datetime,>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Clerk + "'";  // ,<clerk, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Saleaccount + "'";  // ,<saleaccount, nvarchar(100),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Salephone + "'";  // ,<salephone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Saleaddress + "'";  // ,<saleaddress, nvarchar(80),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Saletaxnum + "'";  // ,<saletaxnum, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Kptype + "'";  // ,<kptype, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Message + "'";  // ,<message, nvarchar(130),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Payee + "'";  // ,<payee, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Checker + "'";  // ,<checker, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Fpdm + "'";  // ,<fpdm, nvarchar(12),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Fphm + "'";  // ,<fphm, nvarchar(8),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Tsfs + "'";  // ,<tsfs, int,>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Email + "'";  // ,<email, nvarchar(50),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Qdbz + "'";  // ,<qdbz, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Qdxmmc + "'";  // ,<qdxmmc, nvarchar(90),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Telephone + "'";  // ,<telephone, nvarchar(20),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Dkbz + "'";  // ,<dkbz, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Deptid + "'";  // ,<deptid, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Clerkid + "'";  // ,<clerkid, nvarchar(32),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.InvoiceLine + "'";   // ,<invoiceLine, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.C_fpdm + "'";    // ,<c_fpdm, char(1),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.C_fphm + "'";    // ,<c_fphm, char(1),>
                SqlStr = SqlStr + " ,'" + ip + "*' + " + "convert(VarChar(20), getdate(), 120)" + "+'*" + InvoiceOrder.Clerk + "'"; // ,<ex1, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Clerk + "'";  // ,<ex2, nvarchar(1024),>
                SqlStr = SqlStr + " ,'" + InvoiceOrder.Cllx + "')";  // ,<ex4, nvarchar(1024),>  ''' 车辆类型
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
                foreach (EInvoiceDetail detailInfo in InvoiceDetails)
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
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Kce == "" ? "0" : detailInfo.Kce), 2).ToString() + "' "; // <kce, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Taxfreeamt == "" ? "0" : detailInfo.Taxfreeamt), 2).ToString() + "' "; // <taxfreeamt, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Tax == "" ? "0" : detailInfo.Tax), 2).ToString() + "' "; // <tax, numeric(16,2),>"
                    SqlStr = SqlStr + ",'" + Math.Round(Convert.ToDouble(detailInfo.Taxamt == "" ? "0" : detailInfo.Taxamt), 2).ToString() + "') "; // <taxamt, numeric(16,2),>)"
                    reint = dbUtility.ExecuteNonQuery(SqlStr, null);
                    if (reint != 1)
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                string a = e.Message;
                return false;
            }
        }
        /// <summary>
        /// 保存购方信息
        /// </summary>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveBuyer(DbUtility dbUtility)
        {
            try
            {

                string SqlStr = "delete from T_EInvoiceBuyer where buyername='" + InvoiceOrder.Buyername + "' ";
                dbUtility.ExecuteNonQuery(SqlStr, null);
                SqlStr = " INSERT T_EInvoiceBuyer([taxnum], [buyername], [address], [openBank], [account], [phone], [type], [mobile], [changetime], [special]) VALUES ";
                SqlStr = SqlStr + "(" + "'" + InvoiceOrder.Taxnum + "',";
                SqlStr = SqlStr + "'" + InvoiceOrder.Buyername + "',";
                SqlStr = SqlStr + "'" + InvoiceOrder.Address + "',";
                SqlStr = SqlStr + "'" + (InvoiceOrder.Account.Split(' ').Count() > 1 ? InvoiceOrder.Account.Split(' ')[0] : "") + "',";
                SqlStr = SqlStr + "'" + (InvoiceOrder.Account.Split(' ').Count() > 1 ? InvoiceOrder.Account.Split(' ')[1] : "") + "',";
                SqlStr = SqlStr + "'" + InvoiceOrder.Phone + "',";
                SqlStr = SqlStr + "'" + InvoiceOrder.Buyertype + "',";
                SqlStr = SqlStr + "'" + InvoiceOrder.Telephone + "',";
                SqlStr = SqlStr + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                SqlStr = SqlStr + "'" + "0" + "')";
                dbUtility.ExecuteNonQuery(SqlStr, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
