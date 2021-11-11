using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class SystemParameter
    {
        ///<summary>
        ///检验机构编号
        ///</summary>
        public string Jyjgbh { get; set; }
        ///<summary>
        ///检验数据有效期（天）
        ///</summary>
        public string Jcsjyxq { get; set; }
        ///<summary>
        ///检验数据保存年限
        ///</summary>
        public string Jcsjbcnx { get; set; }
        ///<summary>
        ///检验业务授权码
        ///</summary>
        public string Web_Pass { get; set; }
        ///<summary>
        ///单位许可证号
        ///</summary>
        public string Dw_Xkzh { get; set; }
        ///<summary>
        ///单位电话号码
        ///</summary>
        public string Dw_Dhhm { get; set; }
        ///<summary>
        ///单位名称
        ///</summary>
        public string Dw_mc { get; set; }
        ///<summary>
        ///单位地址
        ///</summary>
        public string Dw_dz { get; set; }
        ///<summary>
        ///是否收费工能
        ///</summary>
        public string SFF { get; set; }
        ///<summary>
        ///是否开票功能
        ///</summary>
        public string KPF { get; set; }
        ///<summary>
        ///流水号首字母
        ///</summary>
        public string LshSzm { get; set; }
        /// <summary>
        /// 数据类别
        /// </summary>
        public string Sjlb { get; set; }
    }
}
