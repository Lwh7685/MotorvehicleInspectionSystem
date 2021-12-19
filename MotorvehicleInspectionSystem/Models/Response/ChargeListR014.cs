using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 收费开票队列
    /// </summary>
    public class ChargeListR014
    {
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 安检检测次数
        /// </summary>
        public int Ajjccs { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        public int Hjjccs { get; set; }
        /// <summary>
        /// 环检检验方式
        /// </summary>
        public string Hjjyfs { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string Cllx { get; set; }
        /// <summary>
        /// 总质量
        /// </summary>
        public string Zzl { get; set; }
        /// <summary>
        /// 整备质量
        /// </summary>
        public string Zbzl { get; set; }
        /// <summary>
        /// 燃料种类
        /// </summary>
        public string Rlzl { get; set; }
        /// <summary>
        /// 使用性质
        /// </summary>
        public string Syxz { get; set; }
        /// <summary>
        /// 是否收费 0=未收费  1=已收费
        /// </summary>
        public string Sfsf { get; set; }
        /// <summary>
        /// 是否开票 0=未开票  1=已开票
        /// </summary>
        public string Sfkp { get; set; }
    }
}
