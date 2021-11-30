using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 保存检验照片的实体类
    /// </summary>
    public class InspectionPhotoW007
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 检验结构编号  安检
        /// </summary>
        public string JyjgbhAj { get; set; }
        /// <summary>
        /// 检验机构编号   环保
        /// </summary>
        public string JyjgbhHj { get; set; }
        /// <summary>
        /// 检测线代号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string Clsbdh { get; set; }
        /// <summary>
        /// 照片  base64字符串
        /// </summary>
        public string Zp { get; set; }
        /// <summary>
        /// 拍摄时间  yyyyMMddHHmmss
        /// </summary>
        public string Pssj { get; set; }
        /// <summary>
        /// 检验项目
        /// </summary>
        public string Jyxm { get; set; }
        // <summary>
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
        public string Zpajdm { get; set; }
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
        /// <summary>
        /// 环检登陆时间  环保照片上传时的唯一标识
        /// </summary>
        public string Hjdlsj { get; set; }
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        /// 安检检测次数
        /// </summary>
        public int Ajjccs { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        public int Hjjccs { get; set; }

    }
}
