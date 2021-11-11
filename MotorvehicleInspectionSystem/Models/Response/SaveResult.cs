using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 数据保存结果
    /// </summary>
    public class SaveResult
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 接口编号
        /// </summary>
        public string Jkbh { get; set; }

        #region  LYYDJKW006 保存签名时使用
        /// <summary>
        /// 检验项目
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string Ryxm { get; set; }
        #endregion 

        #region LYYDJKW007 保存照片时使用
        /// <summary>
        /// 照片代码
        /// </summary>
        public string Zpdm { get; set; }
        /// <summary>
        /// 照片名称
        /// </summary>
        public string Zpmc { get; set; }
        #endregion

        #region LYYDJKW008 保存检验视频截取信息时使用
        /// <summary>
        /// 视频编号
        /// </summary>
        public string Spbh { get; set; }
        #endregion

        #region LYYDJKW009 摄像头拍照时使用
        /// <summary>
        /// 照片工位
        /// </summary>
        public string Zpgw { get; set; }
        //public string Zpdm { get; set; } 已存在共用
        #endregion 
        /// <summary>
        /// 保存结果 安检
        /// </summary>
        public string BcjgAj { get; set; }
        /// <summary>
        /// 保存失败原因  安检
        /// </summary>
        public string BcsbyyAj { get; set; }
        /// <summary>
        /// 保存结果  环检
        /// </summary>
        public string BcjgHj { get; set; }
        /// <summary>
        /// 保存失败原因  环检
        /// </summary>
        public string BcsbyyHj { get; set; }
    }
}
