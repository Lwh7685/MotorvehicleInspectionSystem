using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    public class AppointmentEntityAj
    {
        public struct ACTIVEDRETURN
        {
            public string code;
            public ResponseAppointmentAjR010[] data;
            public string message;
        }
        /// <summary>
        /// 安检预约情况  LYYDJKR010
        /// </summary>
        public class  ResponseAppointmentAjR010
        {
            /// <summary>
            /// 车辆识别代号
            /// </summary>
            [JsonProperty("clsbdh")]
            public string Clsbdh { get; set; }
            /// <summary>
            /// 号牌号码
            /// </summary>
            [JsonProperty("hphm")]
            public string Hphm { get; set; }
            /// <summary>
            /// 号牌种类
            /// </summary>
            [JsonProperty("hpzl")]
            public string Hpzl { get; set; }
            /// <summary>
            /// 编号
            /// </summary>
            [JsonProperty("id")]
            public int Id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("origin")]
            public string Origin { get; set; }
            /// <summary>
            /// 送检人
            /// </summary>
            [JsonProperty("sjr")]
            public string Sjr { get; set; }
            /// <summary>
            /// 送检人电话
            /// </summary>
            [JsonProperty("sjrdh")]
            public string Sjrdh { get; set; }
            /// <summary>
            /// 送检人身份证号
            /// </summary>
            [JsonProperty("sjrsfzh")]
            public string Sjrsfzh { get; set; }
        }

    }
}
