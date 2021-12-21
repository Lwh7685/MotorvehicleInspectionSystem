using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models.Request
{
    /// <summary>
    /// 开票订单明细
    /// </summary>
    public class EInvoiceDetail
    {
        public string orderno; // [orderno] Not [nvarchar](20), --订单号, 联合主键
        public int detailno; // [detailno] [int] NOT NULL,
        public string goodsname; // goodsname   String  是
        public string num; // num  String  否
        public string price; // price  String  否
        public string hsbz; // hsbz  String  是
        public string taxrate; // taxrate  String  是
        public string spec; // spec  String  否
        public string unit; // unit  String  否
        public string spbm; // spbm  String  是
        public string zsbm; // zsbm  String  否
        public string fphxz; // fphxz  String  是
        public string yhzcbs; // yhzcbs  String  否
        public string zzstsgl; // zzstsgl  String  否
        public string lslbs; // lslbs  String  否
        public string kce; // kce  String  否
        public string taxfreeamt; // taxfreeamt  String  否
        public string tax; // tax  String  否
        public string taxamt; // taxamt  String  否
    }
}
