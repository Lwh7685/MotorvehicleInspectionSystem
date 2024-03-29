﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 查询条件,机动车信息相关
    /// </summary>
    public class QueryVehicleCriteria
    {
        /// <summary>
        /// 检验流水号
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
        /// 车辆识别代号
        /// </summary>
        public string Clsbdh { get; set; }
        /// <summary>
        /// 行驶证编号
        /// </summary>
        public string Xszbh { get; set; }
        /// <summary>
        /// 检测次数
        /// </summary>
        public int Jccs { get; set; }
        /// <summary>
        /// 检验项目  查询人工检验项目详细时使（UC,NQ,F1,C1,DC）
        /// </summary>
        public string Jyxm { get; set; }
        /// <summary>
        /// 安检业务类别   不包含时“-”
        /// </summary>
        public string Ajywlb { get; set; }
        /// <summary>
        /// 环检业务类别   不包含时“-”
        /// </summary>
        public string Hjywlb { get; set; }
        /// <summary>
        /// 安检车型
        /// </summary>
        public string Ajcx { get; set; }
        /// <summary>
        /// 安检流水号
        /// </summary>
        public string Ajlsh { get; set; }
        /// <summary>
        /// 环检流水号
        /// </summary>
        public string Hjlsh { get; set; }
        /// <summary>
        ///审核业务  0=同检查  1=安检  2=环检
        /// </summary>
        public string Shyw { get; set; }

        public int Ajjccs { get; set; }
        public int Hjjccs { get; set; }
        #region 查询收费使用
        /// <summary>
        /// 订单编号  收费
        /// </summary>
        public string Oid { get; set; }
        #endregion
        #region 查询客户开票信息使用
        /// <summary>
        /// 购方名称
        /// </summary>
        public string Buyername { get; set; }
        #endregion 
    }
}
