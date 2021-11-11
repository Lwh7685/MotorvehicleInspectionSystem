using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 检验照片队列，流水号最大次数
    /// </summary>
    public class UploadPic
    {
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("ID")]
        public int RowNum { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Lsh { get; set; }
        /// <summary>
        /// 检测线代号
        /// </summary>
        public string Jcxdh { get; set; }
        /// <summary>
        /// 检验次数
        /// </summary>
        public int Jycs { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 照片 base64
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
        /// <summary>
        /// 照片种类
        /// </summary>
        public string Zpzl { get; set; }
        /// <summary>
        /// 业务类别 1=上传  2=不上传
        /// </summary>
        [JsonProperty("Ywlb")]
        public string Bz01 { get; set; }
        /// <summary>
        /// 照片地址
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 照片种类名称
        /// </summary>
        public string ZpzlMc { get; set; }
    }
}
