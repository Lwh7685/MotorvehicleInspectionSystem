using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TmriOutNewAccess;

namespace MotorvehicleInspectionSystem.Tools
{
    /// <summary>
    /// 调用安检监管平台接口
    /// </summary>
    public class CallingSecurityInterface
    {
        public static string WriteObjectOutNew(string xtlb, string jkxlh, string jkid, string xmlDocStr)
        {
            //xml转为UTF8
            xmlDocStr = HttpUtility.UrlEncode(xmlDocStr, Encoding.UTF8);
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://localhost:8072/HCNETWebService.asmx");
            //连接服务
            TmriOutNewAccessSoapClient tmriOutNewAccessSoapClient = new TmriOutNewAccessSoapClient(binding, address);
            //调用接口
            Task<writeObjectOutNewResponse> writeObjectOutNew = tmriOutNewAccessSoapClient.writeObjectOutNewAsync(xtlb, jkxlh, jkid, "", "", "", "", "", "", xmlDocStr);
            //获取返回值
            string result = writeObjectOutNew.Result.Body.writeObjectOutNewResult;
            //解码UTF8
            result = HttpUtility.UrlDecode(result);

            return result;
        }
    }
}
