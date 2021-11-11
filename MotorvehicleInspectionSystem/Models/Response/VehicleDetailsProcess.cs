using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 机动车检测流水详细信息
    /// </summary>
    public class VehicleDetailsProcess
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
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
        ///检测线号
        ///</summary>
        public string Jcxh { get; set; }
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
        public int Pl { get; set; }
        /// <summary>
        ///功率
        ///</summary>
        public int Gl { get; set; }
        /// <summary>
        ///转向型式
        ///</summary>
        public string Zxxs { get; set; }
        /// <summary>
        ///车外廓长
        ///</summary>
        public int Cwkc { get; set; }
        /// <summary>
        ///车外廓宽
        ///</summary>
        public int Cwkk { get; set; }
        /// <summary>
        ///车外廓高
        ///</summary>
        public int Cwkg { get; set; }
        /// <summary>
        ///货箱内部长度
        ///</summary>

        public int Hxnbcd { get; set; }
        /// <summary>
        ///货车内部宽度
        ///</summary>
        public int Hxnbkd { get; set; }
        /// <summary>
        ///货箱内部高度
        ///</summary>
        public int Hxnbgd { get; set; }
        /// <summary>
        ///钢板弹簧片数
        ///</summary>
        public int Gbthps { get; set; }
        /// <summary>
        ///轴数
        ///</summary>
        public int Zs { get; set; }
        /// <summary>
        ///轴距
        ///</summary>
        public int Zj { get; set; }
        /// <summary>
        ///前轮距
        ///</summary>
        public int Qlj { get; set; }
        /// <summary>
        ///后轮距
        ///</summary>
        public int Hlj { get; set; }
        /// <summary>
        ///轮胎规格
        ///</summary>
        public string Ltgg { get; set; }
        /// <summary>
        ///轮胎数
        ///</summary>
        public int Lts { get; set; }
        /// <summary>
        ///总质量
        ///</summary>
        public int Zzl { get; set; }
        /// <summary>
        ///整备质量
        ///</summary>
        public int Zbzl { get; set; }
        /// <summary>
        ///核定载质量
        ///</summary>
        public int Hdzzl { get; set; }
        /// <summary>
        ///核定载客
        ///</summary>
        public int Hdzk { get; set; }
        /// <summary>
        ///准牵引质量
        ///</summary>
        public int Zqyzl { get; set; }
        /// <summary>
        ///前排载客
        ///</summary>
        public int Qpzk { get; set; }
        /// <summary>
        ///后排载客
        ///</summary>
        public int Hpzk { get; set; }
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
        public string Cyc { get; set; }
        /// <summary>
        ///检测项目
        ///</summary>
        public string JcxmStr { get; set; }
        /// <summary>
        ///检测次数
        ///</summary>
        public int Jccs { get; set; }
        /// <summary>
        ///安检检测次数
        ///</summary>
        ///<value></value>

        ///<remarks></remarks>
        public int Ajjccs { get; set; }
        /// <summary>
        ///安检业务列别
        ///</summary>
        public string Ajywlb { get; set; }
        /// <summary>
        ///安检评价
        ///</summary>
        public string Ajpj { get; set; }
        /// <summary>
        ///综检检验次数
        ///</summary>
        ///<value></value>

        ///<remarks></remarks>
        public int Zjjccs { get; set; }
        /// <summary>
        ///综检业务类别
        ///</summary>
        public string Zjywlb { get; set; }
        /// <summary>
        ///环检检验次数
        ///</summary>
        public int Hjjccs { get; set; }
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
        public string Ajcx { get; set; }
        /// <summary>
        ///综检车型
        ///</summary>
        public string Zjcx { get; set; }
        /// <summary>
        ///外观车型
        ///</summary>
        public string Wgchx { get; set; }
        /// <summary>
        ///综检预设等级
        ///</summary>
        public string Ysdj { get; set; }
        /// <summary>
        ///环保达标情况
        ///</summary>
        public string Hbdbqk { get; set; }
        /// <summary>
        ///检验日期
        ///</summary>
        public string Jyrq { get; set; }
        /// <summary>
        ///检验有效期止
        ///</summary>
        public string Jyyxqz { get; set; }
        /// <summary>
        ///驱动形式
        ///</summary>
        public string Qdxs { get; set; }
        /// <summary>
        ///驻车轴数
        ///</summary>
        public int Zczs { get; set; }
        /// <summary>
        ///驻车轴位
        ///</summary>
        public string Zczw { get; set; }
        /// <summary>
        ///主轴数
        ///</summary>
        public int Zzs { get; set; }
        /// <summary>
        ///制动力源
        ///</summary>
        public string Zzly { get; set; }
        /// <summary>
        ///前照灯制
        ///</summary>
        public string Qzdz { get; set; }
        /// <summary>
        ///远光独立调整
        ///</summary>
        public string Ygdltz { get; set; }
        /// <summary>
        ///转向轴悬架型式
        ///</summary>
        public string Zxzxjxs { get; set; }
        /// <summary>
        ///里程表读数
        ///</summary>
        public int Lcbds { get; set; }
        /// <summary>
        ///检验项目
        ///</summary>
        public string Jyxm { get; set; }
        /// <summary>
        ///检验类别（安检业务类别）
        ///</summary>
        public string Jylb { get; set; }
        /// <summary>
        ///初次登陆时间
        ///</summary>
        public string Ccdlsj { get; set; }
        /// <summary>
        ///登录时间
        ///</summary>
        public string Dlsj { get; set; }
        /// <summary>
        ///车辆所属类别
        ///</summary>
        public string Clsslb { get; set; }
        /// <summary>
        ///驱动轴数
        ///</summary>
        public int Qdzs { get; set; }
        /// <summary>
        ///驱动轴位
        ///</summary>
        public string Qdzw { get; set; }
        /// <summary>
        ///是否满载 0=空载 1=满载
        ///</summary>
        public string Sfmz { get; set; }
        /// <summary>
        ///行驶里程 km
        ///</summary>
        public int Xslc { get; set; }
        /// <summary>
        ///最大设计车速 km/h
        ///</summary>
        public int Zdsjcs { get; set; }
        /// <summary>
        ///直接档位
        ///</summary>
        public string Zjdw { get; set; }
        /// <summary>
        ///变速型式
        ///</summary>
        public string Bsxsh { get; set; }
        /// <summary>
        ///制动力源，制动方式
        ///</summary>
        public string Zhdly { get; set; }
        /// <summary>
        ///气缸数
        ///</summary>
        public int Qgsh { get; set; }
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
        public string Lxdh { get; set; }
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
        public int Edzs { get; set; }
        /// <summary>
        ///安检检测评价
        ///</summary>
        public string Jcpj { get; set; }
        /// <summary>
        ///并装轴位
        ///</summary>
        public string Bzhzhw { get; set; }
        /// <summary>
        ///机动车所属类别
        ///</summary>
        public string Sshlb { get; set; }
        /// <summary>
        ///检测线类别
        ///</summary>
        public string Jcxlb { get; set; }
        /// <summary>
        ///前轴数
        ///</summary>
        public int Qzs { get; set; }
        /// <summary>
        ///额定扭矩
        ///</summary>
        public string Ednj { get; set; }
        /// <summary>
        ///额定扭矩转速
        ///</summary>
        public string Ednjzs { get; set; }
        /// <summary>
        ///功率表征方式
        ///</summary>
        public string Glbzfs { get; set; }
        /// <summary>
        ///客车等级
        ///</summary>
        public string Kcdj { get; set; }
        /// <summary>
        ///货车车身形式
        ///</summary>
        public string Hccsxs { get; set; }
        /// <summary>
        ///转向轴数
        ///</summary>
        public int Zhxzhsh { get; set; }
        /// <summary>
        ///驱动轴质量
        ///</summary>
        public int Qdzhzhl { get; set; }
        /// <summary>
        ///综检检验类别   等级/在用
        ///</summary>
        public string Zjjylb { get; set; }
        /// <summary>
        ///营运证号
        ///</summary>
        public string Yyzh { get; set; }
        /// <summary>
        ///综检流水号
        ///</summary>
        public string Zjlsh { get; set; }
        /// <summary>
        ///弟子手刹
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
        public int Pqgsh { get; set; }
        /// <summary>
        ///进气方式
        ///</summary>
        public string Jqfsh { get; set; }
        /// <summary>
        ///档位数
        ///</summary>
        public string Dwsh { get; set; }
        /// <summary>
        ///供油方式
        ///</summary>
        public string Gyfsh { get; set; }
        /// <summary>
        ///送检人姓名
        ///</summary>
        public string Sjrxm { get; set; }
        /// <summary>
        ///送检人电话
        ///</summary>
        public string Sjrdh { get; set; }
        /// <summary>
        ///送检人身份证号
        ///</summary>
        public string Sjrsfzh { get; set; }
        /// <summary>
        ///登录员
        ///</summary>
        public string Dly { get; set; }
        /// <summary>
        ///引车员
        ///</summary>
        public string Ycy { get; set; }
        /// <summary>
        ///授权签字人
        ///</summary>
        public string Sqqzr { get; set; }
        /// <summary>
        ///外检单是否已打印
        ///</summary>
        public bool PrintWjdFlag { get; set; }
        /// <summary>
        ///过程单是否已打印
        ///</summary>
        public bool PrintSheetFlag { get; set; }
        /// <summary>
        ///结果单是否已打印
        ///</summary>
        public bool PrintCardFlag { get; set; }
        /// <summary>
        ///检测时间 yyyy-MM-dd HH:mm:ss
        ///</summary>
        public string Jcsj { get; set; }
        /// <summary>
        ///检验结论
        ///</summary>
        public string Jyjl { get; set; }
        /// <summary>
        ///移动端审核状态
        ///</summary>
        public string Shzt { get; set; }
    }
}
