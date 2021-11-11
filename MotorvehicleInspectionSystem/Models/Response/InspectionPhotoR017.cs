using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 检验照片类
    /// </summary>
    public class InspectionPhotoR017
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 外检车型代码
        /// </summary>
        public string Wjcxdm { get; set; }
        /// <summary>
        /// 外检车型名称
        /// </summary>
        public string Wjcxmc { get; set; }
        /// <summary>
        /// 照片代码
        /// </summary>
        public string Zpdm { get; set; }
        /// <summary>
        /// 照片名称
        /// </summary>
        public string Zpmc { get; set; }
        /// <summary>
        /// 照片安检代码
        /// </summary>
        public string Zpajdm { get; set;}
        /// <summary>
        /// 照片环检代码
        /// </summary>
        public string Zphjdm { get; set; }
        /// <summary>
        /// 保存安检
        /// </summary>
        public string Bcaj { get; set; }
        /// <summary>
        /// 保存环检
        /// </summary>
        public string BcHj { get; set; }
    }
}
