namespace MotorvehicleInspectionSystem.Models.Response
{
    /// <summary>
    /// 底盘提示信息
    /// </summary>
    public class ChassisPromptInformation
    {
        /// <summary>
        /// 检测线号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string Lsh { get; set; }
        /// <summary>
        /// 号牌种类
        /// </summary>
        public string Hpzl { get; set; }
        /// <summary>
        /// 号牌号码
        /// </summary>
        public string Hphm { get; set; }
        /// <summary>
        /// 引车员
        /// </summary>
        public string Ycy { get; set; }
        /// <summary>
        /// 上行显示
        /// </summary>
        public string Shxs { get; set; }
        /// <summary>
        /// 下行显示
        /// </summary>
        public string Xhxs { get; set; }
        /// <summary>
        /// 显示标志 0=未显示  1=已显示
        /// </summary>
        public int Xsbz { get; set; }=0;    

    }
}
