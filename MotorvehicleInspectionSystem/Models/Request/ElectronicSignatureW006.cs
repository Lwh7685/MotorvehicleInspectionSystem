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
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
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
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
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
