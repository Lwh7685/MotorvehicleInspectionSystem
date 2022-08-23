using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 人工检验项目返回数据
    /// </summary>
    public class ArtificialProjectR016
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 检验业务  环保  安检
        /// </summary>
        public string Jyyw { get; set; }
        /// <summary>
        /// 项目数量
        /// </summary>
        public int Xmsl { get; set; }
        /// <summary>
        /// 项目列表
        /// </summary>
        public ArtificialProject[] Xmlb { get; set; }
    }
    /// <summary>
    /// 人工检验项目
    /// </summary>
    public class ArtificialProject
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 外检车型
        /// </summary>
        [JsonProperty("Wjcxdm")]
        public string Cartype { get; set; }
        /// <summary>
        /// 外检车型名称
        /// </summary>
        public string Wjcxmc { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
        public string Fldm { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        [JsonProperty("Xmdm")]
        public string ItemDm { get; set; }
        /// <summary>
        /// 检验项目的中文描述
        /// </summary>
        public string Xmms { get; set; }
        /// <summary>
        /// 使用车型 0=不适用  1=适用 -=未查询
        /// </summary>
        public string Sycx { get; set; }
        /// <summary>
        /// 检验要求
        /// </summary>
        public string Jyyq { get; set; }
    }

}
