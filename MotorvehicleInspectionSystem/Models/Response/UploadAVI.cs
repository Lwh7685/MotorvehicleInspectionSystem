using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 视频列表
    /// </summary>
    public class UploadAVI
    {
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("ID")]
        public int RowNum { get; set; }
        /// <summary>
        /// 线号
        /// </summary>
        public string SXH { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        [JsonProperty("Lsh")]
        public string Jcbh { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime Jcsj { get; set; }
        /// <summary>
        /// 检验次数
        /// </summary>
        [JsonProperty("Jccs")]
        public int Jklx { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Xmbh { get; set; }
        /// <summary>
        /// 检测开始时间
        /// </summary>
        public string JcKsSj { get; set; }
        /// <summary>
        /// 检测结束时间
        /// </summary>
        public string JcJsSj { get; set; }
        /// <summary>
        /// 录像地址
        /// </summary>
        [JsonProperty("Lxdz")]
        public string InBz_02 { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string Spmc { get; set; }
    }
}
