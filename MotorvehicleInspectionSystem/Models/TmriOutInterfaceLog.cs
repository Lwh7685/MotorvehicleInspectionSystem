using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models.Invoice;
using System;
using System.Security.Cryptography.Xml;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 监管平台的接口
    /// </summary>
    public class TmriOutInterfaceLog
    {
        public int Id { get; set; }
        public DateTime Adddate { get; set; }
        public string SendStr { get; set; }
        public string ReturnStr { get; set; }
        public string TerminalIp { get; set; }
        public string Jkid { get; set; }
        public string KeyInfo { get; set; }
        public bool SaveLog(DbUtility dbUtility)
        {
            try
            {
                var sqlStr = "";
                sqlStr += " INSERT INTO [dbo].[tb_interfacelog]";
                sqlStr += " ([adddate],[sendStr],[returnStr],[terminalIp],jkid,bz) VALUES(";
                sqlStr += "'" + DateTime.Now.ToString() + "',"; // (<adddate, datetime,>
                sqlStr += "'" + SendStr + "',"; // ,<sendStr, varchar(max),>
                sqlStr += "'" + ReturnStr + "',"; // ,<returnStr, varchar(max),>
                sqlStr += "'" + TerminalIp + "',"; // ,<terminalIp, varchar(32),>)"
                sqlStr += "'" + Jkid + "',"; // 
                sqlStr += "'" + KeyInfo + "')"; // 
                dbUtility.ExecuteNonQuery(sqlStr, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
