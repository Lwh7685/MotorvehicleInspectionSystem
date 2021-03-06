using MotorvehicleInspectionSystem.Controllers;
using MotorvehicleInspectionSystem.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    public class SystemParameterAj
    {
       public static   SystemParameterAj m_instance = new SystemParameterAj();
        public SystemParameterAj()
        {
            DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
            string sql = "select top 1 * from SystermCs_All ";
            DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
            if (dataTable.Rows.Count > 0)
            {
                Jcfs = dataTable.Rows[0]["jcfs"].ToString();
                Jyjgbh = dataTable.Rows[0]["jyjgbh"].ToString();
                Fwqdz = dataTable.Rows[0]["controlIP"].ToString();
                Spbcdz = dataTable.Rows[0]["Web_VideoRecordPath"].ToString();//Web_VideoRecordPath
                Jkxlh = dataTable.Rows[0]["Web_Pass"].ToString();
                AppointmentURL = dataTable.Rows[0]["appointmentURL"].ToString();//appointmentURL
            }
        }

        /// <summary>
        /// 检测方式 0=本地检测  1=联网检测
        /// </summary>
        public string Jcfs { get; set; }        
        /// <summary>
        /// 检验机构编号
        /// </summary>
        public string Jyjgbh { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Fwqdz { get; set; }
        /// <summary>
        /// 视频保存地址
        /// </summary>
        public string Spbcdz { get; set; }
        /// <summary>
        /// 接口序列号
        /// </summary>
        public string Jkxlh { get; set; }
        /// <summary>
        /// 预约查询接口
        /// </summary>
        public string AppointmentURL { get; set; }
    }
}
