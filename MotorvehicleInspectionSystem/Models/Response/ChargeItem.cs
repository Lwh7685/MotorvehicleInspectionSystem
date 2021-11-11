using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 收费项目
    /// </summary>
    public class ChargeItem
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        [JsonProperty("Spbh")]
        public string Dm { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("Spmc")]
        public string Mc { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [JsonProperty("Spdj")]
        public string Bz1 { get; set; }
    }
}
