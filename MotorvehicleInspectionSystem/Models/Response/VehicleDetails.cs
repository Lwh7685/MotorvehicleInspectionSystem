using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 机动车详细信息
    /// </summary>
    public class VehicleDetails
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string  Id { get; set; }
        /// <summary>
        ///流水号
        ///</summary>
        public string Lsh { get; set; }
        /// <summary>
        ///号牌号码
        ///</summary>
        public string Hphm { get; set; }
        /// <summary>
        ///号牌种类
        ///</summary>
        public string Hpzl { get; set; }
        /// <summary>
        ///车辆识别代号
        ///</summary>
        public string Clsbdh { get; set; }
        /// <summary>
        ///车辆品牌1
        ///</summary>
        public string Clpp1 { get; set; }
        /// <summary>
        ///车辆型号
        ///</summary>
        public string Clxh { get; set; }
        /// <summary>
        ///车辆品牌2
        ///</summary>
        public string Clpp2 { get; set; }
        /// <summary>
        ///国产/进口
        ///</summary>
        public string Gcjk { get; set; }
        /// <summary>
        ///制造国
        ///</summary>
        public string Zzg { get; set; }
        /// <summary>
        ///制造厂名称
        ///</summary>
        public string Zzcmc { get; set; }
        /// <summary>
        ///发动机号
        ///</summary>
        public string Fdjh { get; set; }
        /// <summary>
        ///车辆类型
        ///</summary>
        public string Cllx { get; set; }
        /// <summary>
        ///车身颜色
        ///</summary>
        public string Csys { get; set; }
        /// <summary>
        ///使用性质
        ///</summary>
        public string Syxz { get; set; }
        /// <summary>
        ///身份证明号码
        ///</summary>
        public string Sfzmhm { get; set; }
        /// <summary>
        ///身份证明名称
        ///</summary>
        public string Sfzmmc { get; set; }
        /// <summary>
        ///所有人
        ///</summary>
        public string Syr { get; set; }
        /// <summary>
        ///初次登记日期
        ///</summary>
        public string Ccdjrq { get; set; }
        /// <summary>
        ///最近定检日期
        ///</summary>
        public string Djrq { get; set; }
        /// <summary>
        ///检验有效期止
        ///</summary>
        public string Yxqz { get; set; }
        /// <summary>
        ///强制报废期止
        ///</summary>
        public string Qzbfqz { get; set; }
        /// <summary>
        ///发证机关
        ///</summary>
        public string Fzjg { get; set; }
        /// <summary>
        ///管理部门
        ///</summary>
        public string Glbm { get; set; }
        /// <summary>
        ///保险终止日期
        ///</summary>
        public string Bxzzrq { get; set; }
        /// <summary>
        ///机动车状态
        ///</summary>
        public string Zt { get; set; }
        /// <summary>
        ///抵押标记   2020.09.01取消字段
        ///</summary>
        public string Dybj { get; set; }
        /// <summary>
        ///发动机型号
        ///</summary>
        public string Fdjxh { get; set; }
        /// <summary>
        ///燃料种类
        ///</summary>
        public string Rlzl { get; set; }
        /// <summary>
        ///排量
        ///</summary>
        public string  Pl { get; set; }
        /// <summary>
        ///功率
        ///</summary>
        public string  Gl { get; set; }
        /// <summary>
        ///转向型式
        ///</summary>
        public string Zxxs { get; set; }
        /// <summary>
        ///车外廓长
        ///</summary>
        public string  Cwkc { get; set; }
        /// <summary>
        ///车外廓宽
        ///</summary>
        public string  Cwkk { get; set; }
        /// <summary>
        ///车外廓高
        ///</summary>
        public string  Cwkg { get; set; }
        /// <summary>
        ///货箱内部长度
        ///</summary>
        public string  Hxnbcd { get; set; }
        /// <summary>
        ///货车内部宽度
        ///</summary>
        public string  Hxnbkd { get; set; }
        /// <summary>
        ///货箱内部高度
        ///</summary>
        public string  Hxnbgd { get; set; }
        /// <summary>
        ///钢板弹簧片数
        ///</summary>
        public string  Gbthps { get; set; }
        /// <summary>
        ///轴数
        ///</summary>
        public string  Zs { get; set; }
        /// <summary>
        ///轴距
        ///</summary>
        public string  Zj { get; set; }
        /// <summary>
        ///前轮距
        ///</summary>
        public string  Qlj { get; set; }
        /// <summary>
        ///后轮距
        ///</summary>
        public string  Hlj { get; set; }
        /// <summary>
        ///轮胎规格
        ///</summary>
        public string Ltgg { get; set; }
        /// <summary>
        ///轮胎数
        ///</summary>
        public string  Lts { get; set; }
        /// <summary>
        ///总质量
        ///</summary>
        public string  Zzl { get; set; }
        /// <summary>
        ///整备质量
        ///</summary>
        public string  Zbzl { get; set; }
        /// <summary>
        ///核定载质量
        ///</summary>
        public string  Hdzzl { get; set; }
        /// <summary>
        ///核定载客
        ///</summary>
        public string  Hdzk { get; set; }
        /// <summary>
        ///准牵引质量
        ///</summary>
        public string  Zqyzl { get; set; }
        /// <summary>
        ///前排载客
        ///</summary>
        public string  Qpzk { get; set; }
        /// <summary>
        ///后排载客
        ///</summary>
        public string  Hpzk { get; set; }
        /// <summary>
        ///出厂日期
        ///</summary>
        public string Ccrq { get; set; }
        /// <summary>
        ///车辆用途
        ///</summary>
        public string Clyt { get; set; }
        /// <summary>
        ///用途属性
        ///</summary>
        public string Ytsx { get; set; }
        /// <summary>
        ///行驶证编号
        ///</summary>
        public string Xszbh { get; set; }
        /// <summary>
        ///检验合格标志编号
        ///</summary>
        public string Jyhgbzbh { get; set; }
        /// <summary>
        ///行政区划
        ///</summary>
        public string Xzqh { get; set; }
        /// <summary>
        ///住所地址行政区划
        ///</summary>
        public string Zsxzqh { get; set; }
        /// <summary>
        ///联系地址行政区划
        ///</summary>
        public string Zzxzqh { get; set; }
        /// <summary>
        ///是否免检
        ///</summary>
        public string Sfmj { get; set; }
        /// <summary>
        ///乘用车
        ///</summary>
        [JsonProperty("Cyc")]
        public string Chych { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        ///综检业务类别
        ///</summary>
        public string Zjywlb { get; set; }
        /// <summary>
        ///环检业务类别
        ///</summary>
        public string Hjywlb { get; set; }
        /// <summary>
        ///环保检验方式
        ///</summary>
        public string Hjxm { get; set; }
        /// <summary>
        ///安检车型
        ///</summary>
        [JsonProperty("Ajcx")]
        public string Aj_Veh_Type { get; set; }
        /// <summary>
        ///综检车型
        ///</summary>
        [JsonProperty("Zjcx")]
        public string Zj_Veh_Type { get; set; }
        /// <summary>
        ///外观车型
        ///</summary>
        public string Wgchx { get; set; }
        /// <summary>
        ///环保达标情况
        ///</summary>
        public string Hbdbqk { get; set; }
        /// <summary>
        ///检验日期
        ///</summary>
        [JsonProperty("Jcrq")]
        public string JcDate { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        [JsonProperty("Jcsj")]
        public string JcTime { get; set; }
        /// <summary>
        ///驱动形式
        ///</summary>
        [JsonProperty("Qdxs")]
        public string Qdfs { get; set; }
        /// <summary>
        ///驻车轴数
        ///</summary>
        [JsonProperty("Zczs")]
        public string  Zhchzhsh { get; set; }
        /// <summary>
        ///驻车轴位
        ///</summary>
        [JsonProperty("Zczw")]
        public string Zhchzhw { get; set; }
        /// <summary>
        ///主轴数
        ///</summary>
        [JsonProperty("Zzs")]
        public string  Zhzhsh { get; set; }
        /// <summary>
        ///前照灯制
        ///</summary>
        public string Qzdz { get; set; }
        /// <summary>
        ///远光独立调整
        ///</summary>
        [JsonProperty("Ygdltz")]
        public string DGTZFS { get; set; }
        /// <summary>
        ///转向轴悬架型式
        ///</summary>
        [JsonProperty("Zxzxjxs")]
        public string ZXJFS { get; set; }
        /// <summary>
        ///里程表读数
        ///</summary>
        [JsonProperty("Lcbds")]
        public string  XSLC { get; set; }
        /// <summary>
        ///驱动轴数
        ///</summary>
        [JsonProperty("Qdzs")]
        public string  Qdzhsh { get; set; }
        /// <summary>
        ///驱动轴位
        ///</summary>
        [JsonProperty("Qdzw")]
        public string Qdzhw { get; set; }
        /// <summary>
        ///是否满载 0=空载 1=满载
        ///</summary>
        [JsonProperty("Sfmz")]
        public string SFKZ { get; set; }
        /// <summary>
        ///最大设计车速 km/h
        ///</summary>
        [JsonProperty("Zdsjcs")]
        public string  Max_SD { get; set; }
        /// <summary>
        ///直接档位
        ///</summary>
        public string Zjdw { get; set; }
        /// <summary>
        ///变速型式
        ///</summary>
        [JsonProperty("Bsxs")]
        public string BSQXS { get; set; }
        /// <summary>
        ///制动力源，制动方式
        ///</summary>
        [JsonProperty("Zdly")]
        public string ZDFS { get; set; }
        /// <summary>
        ///气缸数
        ///</summary>
        public string  Qgs { get; set; }
        /// <summary>
        ///燃油规格
        ///</summary>
        public string Rygg { get; set; }
        /// <summary>
        ///维修单位
        ///</summary>
        public string Wxdw { get; set; }
        /// <summary>
        ///竣工日期
        ///</summary>
        public string Jgrq { get; set; }
        /// <summary>
        ///联系电话
        ///</summary>
        [JsonProperty("Lxdh")]
        public string Lsdh { get; set; }
        /// <summary>
        ///联系地址
        ///</summary>
        public string Lxdz { get; set; }
        /// <summary>
        ///号牌颜色
        ///</summary>
        public string Hpys { get; set; }
        /// <summary>
        ///额定转速
        ///</summary>
        [JsonProperty("Edzs")]
        public string  Edzhs { get; set; }
        /// <summary>
        ///并装轴位
        ///</summary>
        [JsonProperty("Bzzw")]
        public string Bzhzhw { get; set; }
        /// <summary>
        ///机动车所属类别
        ///</summary>
        [JsonProperty("Jdcsslb")]
        public string Jdchsshlb { get; set; }
        /// <summary>
        ///检测线类别
        ///</summary>
        public string Jcxlb { get; set; }
        /// <summary>
        ///前轴数
        ///</summary>
        [JsonProperty("Qzs")]
        public string  Qzhsh { get; set; }
        /// <summary>
        ///额定扭矩
        ///</summary>
        public string Ednj { get; set; }
        /// <summary>
        ///额定扭矩转速
        ///</summary>
        [JsonProperty("Ednjzs")]
        public string Ednjzhs { get; set; }
        /// <summary>
        ///功率表征方式
        ///</summary>
        [JsonProperty("Glbzfs")]
        public string glbzhfsh { get; set; }
        /// <summary>
        ///客车等级
        ///</summary>
        [JsonProperty("Kcdj")]
        public string Kchdj { get; set; }
        /// <summary>
        ///货车车身形式
        ///</summary>
        [JsonProperty("Hccsxs")]
        public string Hchchshxsh { get; set; }
        /// <summary>
        ///转向轴数
        ///</summary>
        [JsonProperty("Zxzs")]
        public string  Zhxzhsh { get; set; }
        /// <summary>
        ///驱动轴质量
        ///</summary>
        [JsonProperty("Qdzzl")]
        public string  Qdzhzhl { get; set; }
        /// <summary>
        ///综检检验类别   等级/在用
        ///</summary>
        public string Zjjylb { get; set; }
        /// <summary>
        ///营运证号
        ///</summary>
        [JsonProperty("Yyzh")]
        public string Yyzhh { get; set; }
        /// <summary>
        ///综检流水号
        ///</summary>
        public string Zjlsh { get; set; }
        /// <summary>
        ///电子手刹
        ///</summary>
        public string Dzss { get; set; }
        /// <summary>
        ///空气悬架轴位
        ///</summary>
        public string Kqxjzw { get; set; }
        /// <summary>
        ///环保排放阶段
        ///</summary>
        public string Pfjd { get; set; }
        /// <summary>
        ///排气管数
        ///</summary>
        [JsonProperty("Pqgs")]
        public string  Pqgsh { get; set; }
        /// <summary>
        ///进气方式
        ///</summary>
        public string Jqfs { get; set; }
        /// <summary>
        ///档位数
        ///</summary>
        public string Dws { get; set; }
        /// <summary>
        ///供油方式
        ///</summary>
        public string Gyfs { get; set; }
        /// <summary>
        ///送检人姓名
        ///</summary>
        public string Sjr { get; set; }
        /// <summary>
        ///送检人电话
        ///</summary>
        public string Sjrdh { get; set; }
        /// <summary>
        ///送检人身份证号
        ///</summary>
        public string Sjrsfzh { get; set; }


    }
}
