using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 数据字典响应的内容
    /// </summary>
    public class DataDictionary
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("Id")]
        public int InfoID { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
        public string Fl { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string FlMc { get; set; }
        /// <summary>
        /// 子类代码
        /// </summary>
        public string Dm { get; set; }
        /// <summary>
        /// 子类名称
        /// </summary>
        public string Mc { get; set; }
    }
}
