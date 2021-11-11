using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 电子签名实体类
    /// </summary>
    public class ElectronicSignatureW006
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Lsh { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 检测次数
        /// </summary>
        public int Jccs { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string Ryxm { get; set; }
        /// <summary>
        /// 检测时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string Jcsj { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Qm { get; set; }
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
