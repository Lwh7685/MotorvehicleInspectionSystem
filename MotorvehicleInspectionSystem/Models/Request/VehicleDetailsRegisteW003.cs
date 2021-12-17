using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Request
{
    public class VehicleDetailsRegisteW003
    {
        /// <summary>
        /// 编号
        /// </summary>
        [XmlIgnore]
        public string Id { get; set; }
        /// <summary>
        /// 安检流水号
        /// </summary>
        [XmlElement("jylsh")]
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        [XmlIgnore]
        public string Hjlsh { get; set; }
        /// <summary>
        ///号牌号码
        ///</summary>
        [XmlElement("hphm")]
        public string Hphm { get; set; }
        /// <summary>
        ///号牌种类
        ///</summary>
        [XmlElement("hpzl")]
        public string Hpzl { get; set; }
        /// <summary>
        ///车辆识别代号
        ///</summary>
        [XmlElement("clsbdh")]
        public string Clsbdh { get; set; }
        /// <summary>
        ///车辆品牌1
        ///</summary>
        [XmlIgnore]
        public string Clpp1 { get; set; }
        /// <summary>
        ///车辆型号
        ///</summary>
        [XmlIgnore]
        public string Clxh { get; set; }
        /// <summary>
        ///车辆品牌2
        ///</summary>
        [XmlIgnore]
        public string Clpp2 { get; set; }
        /// <summary>
        ///国产/进口
        ///</summary>
        [XmlIgnore]
        public string Gcjk { get; set; }
        /// <summary>
        ///制造国
        ///</summary>
        [XmlIgnore]
        public string Zzg { get; set; }
        /// <summary>
        ///制造厂名称
        ///</summary>
        [XmlIgnore]
        public string Zzcmc { get; set; }
        /// <summary>
        ///发动机号
        ///</summary>
        [XmlElement("fdjh")]
        public string Fdjh { get; set; }
        /// <summary>
        ///车辆类型
        ///</summary>
        [XmlIgnore]
        public string Cllx { get; set; }
        /// <summary>
        ///车身颜色
        ///</summary>
        [XmlElement("csys")]
        public string Csys { get; set; }
        /// <summary>
        ///使用性质
        ///</summary>
        [XmlElement("syxz")]
        public string Syxz { get; set; }
        /// <summary>
        ///身份证明号码
        ///</summary>
        [XmlIgnore]
        public string Sfzmhm { get; set; }
        /// <summary>
        ///身份证明名称
        ///</summary>
        [XmlIgnore]
        public string Sfzmmc { get; set; }
        /// <summary>
        ///所有人
        ///</summary>
        [XmlIgnore]
        public string Syr { get; set; }
        /// <summary>
        ///初次登记日期
        ///</summary>
        [XmlElement("ccdjrq")]
        public string Ccdjrq { get; set; }
        /// <summary>
        ///最近定检日期
        ///</summary>
        [XmlElement("jyrq")]
        public string Djrq { get; set; }
        /// <summary>
        ///检验有效期止
        ///</summary>
        [XmlElement("jyyxqz")]
        public string Yxqz { get; set; }
        /// <summary>
        ///强制报废期止
        ///</summary>
        [XmlIgnore]
        public string Qzbfqz { get; set; }
        /// <summary>
        ///发证机关
        ///</summary>
        [XmlIgnore]
        public string Fzjg { get; set; }
        /// <summary>
        ///管理部门
        ///</summary>
        [XmlIgnore]
        public string Glbm { get; set; }
        /// <summary>
        ///保险终止日期
        ///</summary>
        [XmlElement("bxzzrq")]
        public string Bxzzrq { get; set; }
        /// <summary>
        ///机动车状态
        ///</summary>
        [XmlIgnore]
        public string Zt { get; set; }
        /// <summary>
        ///抵押标记   2020.09.01取消字段
        ///</summary>
        [XmlIgnore]
        public string Dybj { get; set; }
        /// <summary>
        ///发动机型号
        ///</summary>
        [XmlIgnore]
        public string Fdjxh { get; set; }
        /// <summary>
        ///燃料种类
        ///</summary>
        [XmlElement("rlzl")]
        public string Rlzl { get; set; }
        /// <summary>
        ///排量
        ///</summary>
        [XmlIgnore]
        public string Pl { get; set; }
        /// <summary>
        ///功率
        ///</summary>
        [XmlElement("gl")]
        public string Gl { get; set; }
        /// <summary>
        ///转向型式
        ///</summary>
        [XmlIgnore]
        public string Zxxs { get; set; }
        /// <summary>
        ///车外廓长
        ///</summary>
        [XmlElement("cwkc")]
        public string Cwkc { get; set; }
        /// <summary>
        ///车外廓宽
        ///</summary>
        [XmlElement("cwkk")]
        public string Cwkk { get; set; }
        /// <summary>
        ///车外廓高
        ///</summary>
        [XmlElement("cwkg")]
        public string Cwkg { get; set; }
        /// <summary>
        ///货箱内部长度
        ///</summary>
        [XmlIgnore]
        public string Hxnbcd { get; set; }
        /// <summary>
        ///货车内部宽度
        ///</summary>
        [XmlIgnore]
        public string Hxnbkd { get; set; }
        /// <summary>
        ///货箱内部高度
        ///</summary>
        [XmlIgnore]
        public string Hxnbgd { get; set; }
        /// <summary>
        ///钢板弹簧片数
        ///</summary>
        [XmlIgnore]
        public string Gbthps { get; set; }
        /// <summary>
        ///轴数
        ///</summary>
        [XmlElement("zs")]
        public string Zs { get; set; }
        /// <summary>
        ///轴距
        ///</summary>
        [XmlElement("zj")]
        public string Zj { get; set; }
        /// <summary>
        ///前轮距
        ///</summary>
        [XmlElement("qlj")]
        public string Qlj { get; set; }
        /// <summary>
        ///后轮距
        ///</summary>
        [XmlElement("hlj")]
        public string Hlj { get; set; }
        /// <summary>
        ///轮胎规格
        ///</summary>
        [XmlIgnore]
        public string Ltgg { get; set; }
        /// <summary>
        ///轮胎数
        ///</summary>
        [XmlIgnore]
        public string Lts { get; set; }
        /// <summary>
        ///总质量
        ///</summary>
        [XmlElement("zzl")]
        public string Zzl { get; set; }
        /// <summary>
        ///整备质量
        ///</summary>
        [XmlElement("zbzl")]
        public string Zbzl { get; set; }
        /// <summary>
        ///核定载质量
        ///</summary>
        [XmlIgnore]
        public string Hdzzl { get; set; }
        /// <summary>
        ///核定载客
        ///</summary>
        [XmlIgnore]
        public string Hdzk { get; set; }
        /// <summary>
        ///准牵引质量
        ///</summary>
        [XmlIgnore]
        public string Zqyzl { get; set; }
        /// <summary>
        ///前排载客
        ///</summary>
        [XmlIgnore]
        public string Qpzk { get; set; }
        /// <summary>
        ///后排载客
        ///</summary>
        [XmlIgnore]
        public string Hpzk { get; set; }
        /// <summary>
        ///出厂日期
        ///</summary>
        [XmlElement("ccrq")]
        public string Ccrq { get; set; }
        /// <summary>
        ///车辆用途
        ///</summary>
        [XmlElement("clyt")]
        public string Clyt { get; set; }
        /// <summary>
        ///用途属性
        ///</summary>
        [XmlElement("ytsx")]
        public string Ytsx { get; set; }
        /// <summary>
        ///行驶证编号
        ///</summary>
        [XmlIgnore]
        public string Xszbh { get; set; }
        /// <summary>
        ///检验合格标志编号
        ///</summary>
        [XmlIgnore]
        public string Jyhgbzbh { get; set; }
        /// <summary>
        ///行政区划
        ///</summary>
        [XmlIgnore]
        public string Xzqh { get; set; }
        /// <summary>
        ///住所地址行政区划
        ///</summary>
        [XmlIgnore]
        public string Zsxzqh { get; set; }
        /// <summary>
        ///联系地址行政区划
        ///</summary>
        [XmlIgnore]
        public string Zzxzqh { get; set; }
        /// <summary>
        ///是否免检
        ///</summary>
        [XmlIgnore]
        public string Sfmj { get; set; }
        /// <summary>
        ///乘用车
        ///</summary>
        [JsonProperty("Cyc"), XmlIgnore]
        public string Chych { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        [XmlElement("jylb")]
        public string Ajywlb { get; set; }
        /// <summary>
        ///综检业务类别
        ///</summary>
        [XmlIgnore]
        public string Zjywlb { get; set; }
        /// <summary>
        ///环检业务类别
        ///</summary>
        [XmlIgnore]
        public string Hjywlb { get; set; }
        /// <summary>
        ///环保检验方式
        ///</summary>
        [XmlIgnore]
        public string Hjxm { get; set; }
        /// <summary>
        ///安检车型
        ///</summary>
        [JsonProperty("Ajcx"), XmlIgnore]
        public string Aj_Veh_Type { get; set; }
        /// <summary>
        ///综检车型
        ///</summary>
        [JsonProperty("Zjcx"), XmlIgnore]
        public string Zj_Veh_Type { get; set; }
        /// <summary>
        ///外观车型
        ///</summary>
        [XmlIgnore]
        public string Wgchx { get; set; }
        /// <summary>
        ///环保达标情况
        ///</summary>
        [XmlIgnore]
        public string Hbdbqk { get; set; }
        /// <summary>
        ///检验日期
        ///</summary>
        [JsonProperty("Jcrq"), XmlIgnore]
        public string JcDate { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        [JsonProperty("Jcsj"), XmlIgnore]
        public string JcTime { get; set; }
        /// <summary>
        ///驱动形式
        ///</summary>
        [JsonProperty("Qdxs"), XmlElement("qdxs")]
        public string Qdfs { get; set; }
        /// <summary>
        ///驻车轴数
        ///</summary>
        [JsonProperty("Zczs"), XmlElement("zczs")]
        public string Zhchzhsh { get; set; }
        /// <summary>
        ///驻车轴位
        ///</summary>
        [JsonProperty("Zczw"), XmlElement("zczw")]
        public string Zhchzhw { get; set; }
        /// <summary>
        ///主轴数
        ///</summary>
        [JsonProperty("Zzs"), XmlElement("zzs")]
        public string Zhzhsh { get; set; }
        /// <summary>
        ///前照灯制
        ///</summary>
        [XmlElement("qzdz")]
        public string Qzdz { get; set; }
        /// <summary>
        ///远光独立调整
        ///</summary>
        [JsonProperty("Ygdltz"), XmlIgnore]
        public string DGTZFS { get; set; }
        /// <summary>
        ///转向轴悬架型式
        ///</summary>
        [JsonProperty("Zxzxjxs"), XmlElement("zxzxjxs")]
        public string ZXJFS { get; set; }
        /// <summary>
        ///里程表读数
        ///</summary>
        [JsonProperty("Lcbds"), XmlElement("lcbds")]
        public string XSLC { get; set; }
        /// <summary>
        ///驱动轴数
        ///</summary>
        [JsonProperty("Qdzs"), XmlIgnore]
        public string Qdzhsh { get; set; }
        /// <summary>
        ///驱动轴位
        ///</summary>
        [JsonProperty("Qdzw"), XmlIgnore]
        public string Qdzhw { get; set; }
        /// <summary>
        ///是否满载 0=空载 1=满载
        ///</summary>
        [JsonProperty("Sfmz"), XmlIgnore]
        public string SFKZ { get; set; }
        /// <summary>
        ///最大设计车速 km/h
        ///</summary>
        [JsonProperty("Zdsjcs"), XmlIgnore]
        public string Max_SD { get; set; }
        /// <summary>
        ///直接档位
        ///</summary>
        [XmlIgnore]
        public string Zjdw { get; set; }
        /// <summary>
        ///变速型式
        ///</summary>
        [JsonProperty("Bsxs"), XmlIgnore]
        public string BSQXS { get; set; }
        /// <summary>
        ///制动力源，制动方式
        ///</summary>
        [JsonProperty("Zdly"), XmlElement("zzly")]
        public string Zdfs { get; set; }
        /// <summary>
        ///气缸数
        ///</summary>
        [XmlIgnore]
        public string Qgs { get; set; }
        /// <summary>
        ///燃油规格
        ///</summary>
        [XmlIgnore]
        public string Rygg { get; set; }
        /// <summary>
        ///联系电话
        ///</summary>
        [JsonProperty("Lxdh"), XmlIgnore]
        public string Lsdh { get; set; }
        /// <summary>
        ///联系地址
        ///</summary>
        [XmlIgnore]
        public string Lxdz { get; set; }
        /// <summary>
        ///号牌颜色
        ///</summary>
        [XmlIgnore]
        public string Hpys { get; set; }
        /// <summary>
        ///额定转速
        ///</summary>
        [JsonProperty("Edzs"), XmlIgnore]
        public string Edzhs { get; set; }
        /// <summary>
        ///并装轴位
        ///</summary>
        [JsonProperty("Bzzw"), XmlElement("bzzw")]
        public string Bzhzhw { get; set; }
        /// <summary>
        ///机动车所属类别
        ///</summary>
        [JsonProperty("Jdcsslb"), XmlElement("clsslb")]
        public string Jdchsshlb { get; set; }
        /// <summary>
        ///检测线类别
        ///</summary>
        [XmlElement("jcxlb")]
        public string Jcxlb { get; set; }
        /// <summary>
        ///前轴数
        ///</summary>
        [JsonProperty("Qzs"), XmlElement("qzs")]
        public string Qzhsh { get; set; }
        /// <summary>
        ///转向轴数
        ///</summary>
        [JsonProperty("Zxzs"), XmlIgnore]
        public string Zhxzhsh { get; set; }
        /// <summary>
        ///综检检验类别   等级/在用
        ///</summary>
        [XmlIgnore]
        public string Zjjylb { get; set; }
        /// <summary>
        ///营运证号
        ///</summary>
        [JsonProperty("Yyzh"), XmlElement("dlyszh")]
        public string Yyzhh { get; set; }
        /// <summary>
        ///综检流水号
        ///</summary>
        [XmlIgnore]
        public string Zjlsh { get; set; }
        /// <summary>
        ///电子手刹
        ///</summary>
        [XmlIgnore]
        public string Dzss { get; set; }
        /// <summary>
        ///空气悬架轴位
        ///</summary>
        [XmlIgnore]
        public string Kqxjzw { get; set; }
        /// <summary>
        ///环保排放阶段
        ///</summary>
        [XmlIgnore]
        public string Pfjd { get; set; }
        /// <summary>
        ///排气管数
        ///</summary>
        [JsonProperty("Pqgs"), XmlIgnore]
        public string Pqgsh { get; set; }
        /// <summary>
        ///进气方式
        ///</summary>
        [XmlIgnore]
        public string Jqfs { get; set; }
        /// <summary>
        ///档位数
        ///</summary>
        [XmlIgnore]
        public string Dws { get; set; }
        /// <summary>
        ///供油方式
        ///</summary>
        [XmlIgnore]
        public string Gyfs { get; set; }
        /// <summary>
        ///送检人姓名
        ///</summary>
        [XmlIgnore]
        public string Sjr { get; set; }
        /// <summary>
        ///送检人电话
        ///</summary>
        [XmlIgnore]
        public string Sjrdh { get; set; }
        /// <summary>
        ///送检人身份证号
        ///</summary>
        [XmlIgnore]
        public string Sjrsfzh { get; set; }
        /// <summary>
        /// 环检登录时间  yyyyMMddHHmmss
        /// </summary>
        [XmlIgnore]
        public string Hjdlsj { get; set; }
        /// <summary>
        /// 安检检验机构编号
        /// </summary>
        [XmlElement("jyjgbh")]
        public string Jyjgbh { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        [XmlIgnore]
        public string Jcxh { get; set; }
        /// <summary>
        /// 机动车序号
        /// </summary>
        [XmlElement("xh")]
        public string Xh { get; set; }
        /// <summary>
        /// 检测项目安检
        /// </summary>
        [XmlElement("jyxm")]
        public string JcxmAj { get; set; }
        /// <summary>
        /// 检测项目环检
        /// </summary>
        [XmlIgnore]
        public string JcxmHJ { get; set; }
        /// <summary>
        /// 检测方法环检
        /// </summary>
        [XmlIgnore]
        public string JcffHJ { get; set; }
        /// <summary>
        /// 检测业务综检
        /// </summary>
        [XmlIgnore]
        public string JyywZj { get; set; }
        /// <summary>
        /// 不合格项
        /// </summary>
        [XmlIgnore]
        public string Bhgx { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [XmlElement("dlsj")]
        public string Dlsj { get; set; }
        /// <summary>
        /// 安检检测次数
        /// </summary>
        [XmlElement("jycs")]
        public string Ajjccs { get; set; }
        /// <summary>
        /// 环检检测次数
        /// </summary>
        [XmlIgnore]
        public string Hjjccs { get; set; }
        /// <summary>
        /// 登录员姓名
        /// </summary>
        [XmlElement("dly")]
        public string Dly { get; set; }
        /// <summary>
        /// 登录员身份证号
        /// </summary>
        [XmlElement("dlysfzh")]
        public string Dlysfzh { get; set; }
        /// <summary>
        /// 引车员
        /// </summary>
        [XmlElement("ycy")]
        public string Ycy { get; set; }
        /// <summary>
        /// 引车员身份证号
        /// </summary>
        [XmlElement("ycysfzh")]
        public string Ycysfzh { get; set; }
        /// <summary>
        /// 外检员
        /// </summary>
        [XmlElement("wjy")]
        public string Wjy { get; set; }
        /// <summary>
        /// 外检员身份证
        /// </summary>
        [XmlElement("wjysfzh")]
        public string Wjysfzh { get; set; }
        /// <summary>
        /// 动态检验员
        /// </summary>
        [XmlElement("dtjyy")]
        public string Dtjyy { get; set; }
        /// <summary>
        /// 动态检验员身份证号
        /// </summary>
        [XmlElement("dtjyysfzh")]
        public string Dtjyysfzh { get; set; }
        /// <summary>
        /// 底盘检验员
        /// </summary>
        [XmlElement("dpjyy")]
        public string Dpjyy { get; set; }
        /// <summary>
        /// 底盘检验员身份证
        /// </summary>
        [XmlElement("dpjyysfzh")]
        public string Dpjyysfzh { get; set; }
        [XmlIgnore]
        public string SCR { get; set; }
        [XmlIgnore]
        public string DPF { get; set; }
        /// <summary>
        /// 程冲数
        /// </summary>
        [XmlIgnore]
        public string Ccs { get; set; }
        /// <summary>
        /// 后处理方式
        /// </summary>
        [XmlIgnore]
        public string Hclfs { get; set; }
        [XmlIgnore ]
        public string Sfazwb { get; set; }
        [XmlIgnore ]
        public string Wbzl { get; set; }
        [XmlIgnore ]
        public string Qxclzhxx { get; set; }
    }
}
