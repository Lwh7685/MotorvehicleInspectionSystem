using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Models
{
    /// <summary>
    /// 接口响应的信息格式
    /// </summary>
    public class ResponseData
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int RowNum { get; set; }
        public object[]  Body { get; set; }
    }
}
