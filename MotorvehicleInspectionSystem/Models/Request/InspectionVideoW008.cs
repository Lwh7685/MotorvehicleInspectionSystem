using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 检验视频信息
    /// </summary>
    public class InspectionVideoW008
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
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
        /// 检测次数
        /// </summary>
        public int Jccs { get; set; }
        /// <summary>
        /// 检测线号
        /// </summary>
        public string Jcxh { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string Jcxm { get; set; }
        /// <summary>
        /// 视频编号 安检
        /// </summary>
        public string Spbhaj { get; set; }
        /// <summary>
        /// 视频编号 环检
        /// </summary>
        public string Spbhhj { get; set; }
        /// <summary>
        /// 安检业务类别
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 检测日期
        /// </summary>
        public string Jcrq { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public string Jcsj { get; set; }
        /// <summary>
        /// 检测开始时间
        /// </summary>
        public string Jckssj { get; set; }
        /// <summary>
        /// 检测结束时间
        /// </summary>
        public string Jcjssj { get; set; }
        /// <summary>
        /// 录像信息
        /// </summary>
        public string Lxxx { get; set; }
        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string Clpp { get; set; }
        /// <summary>
        /// 车主单位
        /// </summary>
        public string Czdw { get; set; }
        /// <summary>
        /// 保存安检
        /// </summary>
        public string Bcaj { get; set; }
        /// <summary>
        /// 保存环检
        /// </summary>
        public string Bchj { get; set; }
        /// <summary>
        /// 环保登陆时间 环保上传的唯一标识
        /// </summary>
        public string Hjdlsj { get; set; }

        //后加的参数，用于手机上传视频信息，不在后台截取
        /// <summary>
        /// 录像保存地址
        /// </summary>
        public string Lxdz { get; set; }
        /// <summary>
        /// 录像保存标志  0=无视频，由后台截取  1=有视频，文件已上传
        /// </summary>
        public string Lxbz { get; set; }
    }
}
