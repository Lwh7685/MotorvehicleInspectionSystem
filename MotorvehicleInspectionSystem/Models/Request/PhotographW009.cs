using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 摄像头触发拍照接口
    /// </summary>
    public class PhotographW009
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆识别代号
        /// </summary>
        public string Clsbdh { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 照片工位
        /// </summary>
        public string Zpgw { get; set; }
        /// <summary>
        /// 照片代码
        /// </summary>
        public string Zpdm { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        public string Ajlsh { get; set; }
        public string Hjlsh { get; set; }
        public int Ajjccs { get; set; }
        public int Hjjccs { get; set; }
    }
}
