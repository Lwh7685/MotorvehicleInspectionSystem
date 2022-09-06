using System;
using System.Xml.Serialization;

namespace MotorvehicleInspectionSystem.Models.Exhaust
{
    /// <summary>
    /// 外检结果
    /// </summary>
    [XmlRoot("result")]
    public class AppearanceExData
    {
        [XmlElement("result_data")]
        public AppearanceExResult AppearanceResult { get; set; }
    }
    /// <summary>
    /// 外检数据
    /// </summary>
    public class AppearanceExResult
    {
        /// <summary>
        /// check_id	varhcar(14)	检测报告编号（新增）
        /// </summary>
        public string check_id { get; set; }
        /// <summary>
        /// city_code	varchar2(6)	检测所在地编码
        /// </summary>
        public string city_code { get; set; }
        /// <summary>
        /// unit_id	varchar2(11)	检测机构编号
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// user_id	VARCHAR2(20)	检测用户登录名
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// uname	varchar2(200)	检测用户名（对应检测人员的中文名）
        /// </summary>
        public string uname { get; set; }
        /// <summary>
        /// vin	VARCHAR2(17)	车架号
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// clxh	VARCHAR2(200)	车辆型号
        /// </summary>
        public string clxh { get; set; }
        /// <summary>
        /// plate	VARCHAR2(10)	车牌
        /// </summary>
        public string plate { get; set; }
        /// <summary>
        /// check_date	Date	检查日期
        /// </summary>
        private DateTime _check_date;
        public string check_date
        {
            get
            {
                return _check_date.ToString("yyyy-MM-dd");
            }
            set
            {
                try
                {
                    _check_date = DateTime.Parse(value);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// odometer	NUMBER(5, 2)	车辆里程数
        /// </summary>
        public string odometer { get; set; }
        /// <summary>
        /// jxzk	CHAR(1)	机械状况是否良好    0：否 1：是
        /// </summary>
        public char jxzk { get; set; }
        /// <summary>
        /// wrkzzz	CHAR(1)	污染控制装置是否齐全、正常  0：否 1：是
        /// </summary>
        public char wrkzzz { get; set; }
        /// <summary>
        /// sjymhy	CHAR(1)	是否存在烧机油或严重冒黑烟状况 0：否 1：是
        /// </summary>
        public char sjymhy { get; set; }
        /// <summary>
        /// qzxtfxt	CHAR(1)	曲轴箱通风系统是否正常  0：否 1：是   2：不涉及
        /// </summary>
        public char qzxtfxt { get; set; }
        /// <summary>
        /// ryzfkzxt	CHAR(1)	燃油蒸发控制系统是否正常  0：否 1：是 2：不涉及
        /// </summary>
        public char ryzfkzxt { get; set; }
        /// <summary>
        /// ybgz	CHAR(1)	仪表工作是否正常 0：否 1：是
        /// </summary>
        public char ybgz { get; set; }
        /// <summary>
        /// yxaqjxgz	CHAR(1)	有无影响安全或引起测试偏差的机械故障 0：否 1：是
        /// </summary>
        public char yxaqjxgz { get; set; }
        /// <summary>
        /// pqxtxl	CHAR(1)	进排气系统是否存在泄露 0：否 1：是
        /// </summary>
        public char pqxtxl { get; set; }
        /// <summary>
        /// ytsl	CHAR(1)	发动机、变速箱等有无液体渗漏情况 0：否 1：是
        /// </summary>
        public char ytsl { get; set; }
        /// <summary>
        /// hasobd	CHAR(1)	是否带OBD 0：否 1：是
        /// </summary>
        public char hasobd { get; set; }
        /// <summary>
        /// ltpre	CHAR(1)	轮胎气压是否正常 0：否 1：是
        /// </summary>
        public char ltpre { get; set; }
        /// <summary>
        /// ltgz	CHAR(1)	轮胎是否干燥、清洁 0：否 1：是
        /// </summary>
        public char ltgz { get; set; }
        /// <summary>
        /// closefssb	CHAR(1)	是否关闭空调、暖风等附属设备 0：否 1：是
        /// </summary>
        public char closefssb { get; set; }
        /// <summary>
        /// closeyxgzkz	CHAR(1)	是否关闭ESP、ARS等可能影响测试的功能 0：否 1：是
        /// </summary>
        public char closeyxgzkz { get; set; }
        /// <summary>
        /// fuelgz	CHAR(1)	油箱和油品是否正常 0：否 1：是
        /// </summary>
        public char fuelgz { get; set; }
        /// <summary>
        /// fdjcydkb	CHAR(1)	发动机燃油系统采用电控泵 0：否 1：是
        /// </summary>
        public char fdjcydkb { get; set; }
        /// <summary>
        /// isasm	CHAR(1)	是否适合工况法检测 0：否 1：是
        /// </summary>
        public char isasm { get; set; }
        /// <summary>
        /// wgpics	VARCHAR2(500)	外观图片名称，多张图片名称中间用逗号间隔，如：a.jpg,b.jpg
        /// </summary>
        public string wgpics { get; set; }
        /// <summary>
        /// passed	CHAR(1)	检测结果： 0：不合格 1：合格 2：中止 3：无效
        /// </summary>
        public char passed { get; set; }
    }

}
