using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models;
using MotorvehicleInspectionSystem.Models.Response;
using MotorvehicleInspectionSystem.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MotorvehicleInspectionSystem.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VehicleInspectionController : ControllerBase
    {
        /// <summary>
        /// 使用安检数据库
        /// </summary>
        public static string SyAj { get; set; }
        /// <summary>
        /// 安检数据连接字符串
        /// </summary>
        public static string ConstrAj { get; set; }
        /// <summary>
        /// 使用环保数据库
        /// </summary>
        public static string SyHj { get; set; }
        /// <summary>
        /// 环保数据连接字符串
        /// </summary>
        public static string ConstrHj { get; set; }
        /// <summary>
        /// 使用城市大脑数据库
        /// </summary>
        public static string SyUb { get; set; }
        /// <summary>
        /// 城市大脑连接字符串
        /// </summary>
        public static string ConstrUB { get; set; }

        /// <summary>
        /// 查询类接口实例
        /// </summary>
        QueryController QC = new QueryController();
        /// <summary>
        /// 写入类接口实例
        /// </summary>
        WriteController WC = new WriteController();
        /// <summary>
        /// 响应数据的实例
        /// </summary>
        ResponseData responseData = new ResponseData();
        /// <summary>
        /// 申请数据的实例
        /// </summary>
        RequestData requestData = new RequestData();
        /// <summary>
        /// 查询类接口
        /// </summary>
        /// <param name="jkId">接口编号</param>
        /// <param name="zdbs">终端标识</param>
        /// <param name="jsonData">Json数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<object> Query(string jkId, string zdbs, string jsonData)
        {
            responseData.Body = new object[0];
            try
            {
                //jkId不能为空
                if (jkId.Trim() == "")
                {
                    responseData.Code = "-4";
                    responseData.Message = "接口编号（jkId）不能为空";
                    return responseData;
                }
                //zdbs不能为空
                if (zdbs.Trim() == "")
                {
                    responseData.Code = "-5";
                    responseData.Message = "终端标识（zdbs）不能为空";
                    return responseData;
                }
                //IP地址合法
                IPAddress ip;
                if (!IPAddress.TryParse(zdbs, out ip))
                {
                    responseData.Code = "-6";
                    responseData.Message = "终端标识（zdbs）不合法";
                    return responseData;
                }
                //jsonData合法
                requestData = JSONHelper.DeserializeJson<RequestData>(jsonData);
                switch (jkId)
                {
                    //查询所有用户信息
                    case "LYYDJKR001":
                        User[] users = QC.LYYDJKR001(responseData);
                        responseData.Body = users;
                        break;
                    //查询机动车队列  LYYDJKR002
                    case "LYYDJKR002":
                        VehicleQueue[] vehicleQueues = QC.LYYDJKR002(requestData, responseData);
                        responseData.Body = vehicleQueues;
                        break;
                    //查询数据字典
                    case "LYYDJKR003":
                        DataDictionary[] dataDictionaries = QC.LYYDJKR003(requestData, responseData);
                        responseData.Body = dataDictionaries;
                        break;
                    //查询收费条目
                    case "LYYDJKR004":
                        ChargeItem[] chargeItems = QC.LYYDJKR004(responseData);
                        responseData.Body = chargeItems;
                        break;
                    case "LYYDJKR005":
                        VehicleDetails[] vehicleDetails = QC.LYYDJKR005(requestData, responseData);
                        responseData.Body = vehicleDetails;
                        break;
                    case "LYYDJKR006":
                        InspectionItemsR006[] inspectionItemsR006s = QC.LYYDJKR006(requestData, responseData);
                        responseData.Body = inspectionItemsR006s;
                        break;
                    case "LYYDJKR007":
                        UploadPic[] uploadPics = QC.LYYDJKR007(requestData, responseData);
                        responseData.Body = uploadPics;
                        break;
                    case "LYYDJKR008":
                        UploadAVI[] uploadAVIs = QC.LYYDJKR008(requestData, responseData);
                        responseData.Body = uploadAVIs;
                        break;
                    case "LYYDJKR009":
                        InCar[] inCars = QC.LYYDJKR009(responseData);
                        responseData.Body = inCars;
                        break;
                    case "LYYDJKR010":
                        AppointmentEntityAj.ResponseAppointmentAjR010[] responseAppointmentAjR010s = QC.LYYDJKR010(responseData);
                        responseData.Body = responseAppointmentAjR010s;
                        break;
                    case "LYYDJKR011":
                        ServerTimeR011[] dateTimes = QC.LYYDJKR011(responseData);
                        responseData.Body = dateTimes;
                        break;
                    case "LYYDJKR013":
                        ModerationQueueR013[] moderationQueueR013s = QC.LYYDJKR013(requestData ,responseData );
                        responseData.Body = moderationQueueR013s;
                        break;
                    case "LYYDJKR015":
                        SystemParameter[] systemParameters = QC.LYYDJKR015(responseData);
                        responseData.Body = systemParameters;
                        break;
                    case "LYYDJKR016":
                        ArtificialProjectR016[] artificialProjectR016s = QC.LYYDJKR016(requestData, responseData);
                        responseData.Body = artificialProjectR016s;
                        break;
                    case "LYYDJKR017":
                        InspectionPhotoR017[] inspectionPhotoR017s = QC.LYYDJKR017(requestData, responseData);
                        responseData.Body = inspectionPhotoR017s;
                        break;
                    case "LYYDJKR018":
                        ArtificialProjectR016[] artificialProjectR016s1 = QC.LYYDJKR018(requestData, responseData);
                        responseData.Body = artificialProjectR016s1;
                        break;
                    case "LYYDJKR019":
                        InspectionDurationR019[] inspectionDurationR019s = QC.LYYDJKR019(requestData, responseData);
                        responseData.Body = inspectionDurationR019s;
                        break;
                    case "LYYDJKR020":
                        ArtificialProjectR016[] artificialProjectR016s2 = QC.LYYDJKR020(requestData, responseData);
                        responseData.Body = artificialProjectR016s2;
                        break;
                    case "LYYDJKR021":
                        AppVersion[] appVersions = QC.LYYDJKR021(responseData );
                        responseData.Body = appVersions;
                        break;
                    case "LYYDJKR022":
                        VehicleDetails[] vehicleDetails1 = QC.LYYDJKR022(requestData,responseData);
                        responseData.Body = vehicleDetails1;
                        break;
                    case "LYYDJKR023":
                        AdministrativeRegionR023[] administrativeRegionR023s = QC.LYYDJKR023(responseData);
                        responseData.Body = administrativeRegionR023s;
                        break;
                    case "LYYDJKR024":
                        InspectionProgressR024[] inspectionProgressR024s = QC.LYYDJKR024(requestData, responseData);
                        responseData.Body = inspectionProgressR024s;
                        break;
                    default:
                        responseData.Code = "-7";
                        responseData.Message = "上传正确的接口编号（jkId）";
                        break;
                }
            }
            catch (NullReferenceException)
            {
                responseData.Code = "-8";
                responseData.Message = "参数不能为空";
            }
            catch (JsonSerializationException)
            {
                responseData.Code = "-2";
                responseData.Message = "数据格式不规范（jsonData）";
            }
            responseData.RowNum = responseData.Body.Count();
            responseData.Message = responseData.Message + "(" + jkId + ")";
            return responseData;
        }
        /// <summary>
        /// 写入类接口
        /// </summary>
        /// <param name="jkId">接口编号</param>
        /// <param name="zdbs">终端标识</param>
        /// <param name="jsonData">Json数据</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<object> Write(string jkId, string zdbs, [FromBody] string jsonData)
        {
            responseData.Body = new object[0];
            try
            {
                //jkId不能为空
                if (jkId.Trim() == "")
                {
                    responseData.Code = "-99";
                    responseData.Message = "接口编号（jkId）不能为空";
                    return responseData;
                }
                //zdbs不能为空
                if (zdbs.Trim() == "")
                {
                    responseData.Code = "-99";
                    responseData.Message = "终端标识（zdbs）不能为空";
                    return responseData;
                }
                //IP地址合法
                IPAddress ip;
                if (!IPAddress.TryParse(zdbs, out ip))
                {
                    responseData.Code = "-99";
                    responseData.Message = "终端标识（zdbs）不合法";
                    return responseData;
                }
                //jsonData合法
                requestData = JSONHelper.DeserializeJson<RequestData>(jsonData);
                //将文本追加到文件末尾
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\WriteLines2.txt", true))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----" + jkId + "----" + jsonData + "/n");

                }
                switch (jkId)
                {
                    //用户登录
                    case "LYYDJKW001":
                        User[] users = WC.LYYDJKW001(requestData, responseData);
                        responseData.Body = users;
                        break;
                    case "LYYDJKW003":
                        SaveResult[] saveResults003 = WC.LYYDJKW003(requestData, responseData);
                        responseData.Body = saveResults003;
                        break;
                    //保存签名
                    case "LYYDJKW006":
                        SaveResult[] saveResults006 = WC.LYYDJKW006(requestData, responseData);
                        responseData.Body = saveResults006;
                        break;
                    //保存照片
                    case "LYYDJKW007":
                        SaveResult[] saveResults007 = WC.LYYDJKW007(requestData, responseData);
                        responseData.Body = saveResults007;
                        break;
                    case "LYYDJKW008":
                        SaveResult[] saveResults008 = WC.LYYDJKW008(requestData, responseData);
                        responseData.Body = saveResults008;
                        break;
                    case "LYYDJKW009":
                        SaveResult[] saveResults009 = WC.LYYDJKW009(requestData, responseData);
                        responseData.Body = saveResults009;
                        break;
                    //检验项目开始
                    case "LYYDJKW010":
                        SaveResult[] saveResults010 = WC.LYYDJKW010(requestData, responseData, zdbs);
                        responseData.Body = saveResults010;
                        break;
                    case "LYYDJKW011":
                        SaveResult[] saveResults011 = WC.LYYDJKW011(requestData, responseData, zdbs);
                        responseData.Body = saveResults011;
                        break;
                    //检验项目结束
                    case "LYYDJKW012":
                        SaveResult[] saveResults012 = WC.LYYDJKW012(requestData, responseData, zdbs);
                        responseData.Body = saveResults012;
                        break;
                    case "LYYDJKW015":
                        SaveResult[] saveResults015= WC.LYYDJKW015(requestData,responseData );
                        responseData.Body = saveResults015;
                        break;
                    default:
                        responseData.Code = "-1";
                        responseData.Message = "上传正确的接口编号（jkId）";
                        break;
                }
            }
            catch (NullReferenceException)
            {
                responseData.Code = "-99";
                responseData.Message = "参数不能为空";
            }
            catch (JsonSerializationException)
            {
                responseData.Code = "-99";
                responseData.Message = "数据格式不规范（jsonData）";
            }
            responseData.RowNum = responseData.Body.Count();
            responseData.Message = responseData.Message + "(" + jkId + ")";
            return responseData;
        }
    }
}
