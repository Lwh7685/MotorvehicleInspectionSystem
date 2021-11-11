using HCNETWebService;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models;
using MotorvehicleInspectionSystem.Models.Request;
using MotorvehicleInspectionSystem.Models.Response;
using MotorvehicleInspectionSystem.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using TmriOutNewAccess;

namespace MotorvehicleInspectionSystem.Controllers
{
    public class WriteController : Controller
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public User[] LYYDJKW001(RequestData requestData, ResponseData responseData)
        {
            List<User> users = new List<User>();
            try
            {
                Login login = JSONHelper.ConvertObject<Login>(requestData.Body[0]);
                IPAddress ip;
                if (!IPAddress.TryParse(login.IP, out ip))
                {
                    responseData.Code = "-99";
                    responseData.Message = "登录地址（IP）不合法";
                    return users.ToArray();
                }
                DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = string.Format("select *,STUFF((SELECT ',' + roleDm FROM Tb_UserRole WHERE username =a.UserName  FOR xml path('')),1,1,'') as RoleDm from Tab_UserInfo A where UserName ='{0}' and PassWord ='{1}'", login.UserName, login.PassWord);
                //判断用户名密码
                users = db.QueryForList<User>(sql, null);
                //判断地址
                sql = string.Format("select * from Tab_User_ZzInfo where SfzInfoID ='{0}' and Zz_IP ='{1}'", users[0].SfzInfoID, login.IP);
                DataTable dataTable = db.ExecuteDataTable(sql, null);
                if (dataTable.Rows.Count <= 0)
                {
                    responseData.Code = "-1";
                    responseData.Message = "该用户不允许在当前终端登录（" + login.IP + "）";
                    return new User[0];
                }
                else
                {
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }

            }
            catch (ArgumentNullException ex)
            {
                responseData.Code = "-1";
                responseData.Message = "用户名或密码错误";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return users.ToArray();

        }
        /// <summary>
        /// 保存签名
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW006(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            string sql;
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                foreach (Object o in requestData.Body)
                {
                    ElectronicSignatureW006 electronicSignatureW006 = JSONHelper.ConvertObject<ElectronicSignatureW006>(o);
                    SaveResult saveResult = new SaveResult();
                    saveResult.ID = electronicSignatureW006.ID;
                    saveResult.Jkbh = "LYYDJKW006";
                    saveResult.Ryxm = electronicSignatureW006.Ryxm;
                    saveResult.Jcxm = electronicSignatureW006.Jcxm;
                    if (electronicSignatureW006.Bcaj == "1")
                    {
                        if (VehicleInspectionController.SyAj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from QianMing_Info where Lsh ='" + electronicSignatureW006.Lsh + "' and Jccs ='" + electronicSignatureW006.Jccs + "' and JcXm ='" + electronicSignatureW006.Jcxm + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[QianMing_Info] ";
                                sql += " ([Lsh],[hphm],[Jccs],[JcXm],[Ry_Name],[JcDate],[Base64]) VALUES(";
                                sql += " '" + electronicSignatureW006.Lsh + "',";// (< Lsh, varchar(32),>
                                sql += " '" + electronicSignatureW006.Hphm + "',";//  ,< hphm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Jccs + "',";//  ,< Jccs, varchar(2),>
                                sql += " '" + electronicSignatureW006.Jcxm + "',";//  ,< JcXm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Ryxm + "',";//  ,< Ry_Name, varchar(32),>
                                sql += " '" + electronicSignatureW006.Jcsj + "',";//  ,< JcDate, varchar(32),>
                                sql += " '" + electronicSignatureW006.Qm + "')";//  ,< Base64, text,>))";
                                dbAj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgAj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgAj = "fail";
                                saveResult.BcsbyyAj = e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = "不包含安检业务部分";
                        }
                    }
                    if (electronicSignatureW006.BcHj == "1")
                    {
                        if (VehicleInspectionController.SyHj == "1")
                        {
                            try
                            {
                                //先删除
                                //先删除
                                sql = "delete from QianMing_Info where Lsh ='" + electronicSignatureW006.Lsh + "' and Jccs ='" + electronicSignatureW006.Jccs + "' and JcXm ='" + electronicSignatureW006.Jcxm + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[QianMing_Info] ";
                                sql += " ([Lsh],[hphm],[Jccs],[JcXm],[Ry_Name],[JcDate],[Base64]) VALUES(";
                                sql += " '" + electronicSignatureW006.Lsh + "',";// (< Lsh, varchar(32),>
                                sql += " '" + electronicSignatureW006.Hphm + "',";//  ,< hphm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Jccs + "',";//  ,< Jccs, varchar(2),>
                                sql += " '" + electronicSignatureW006.Jcxm + "',";//  ,< JcXm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Ryxm + "',";//  ,< Ry_Name, varchar(32),>
                                sql += " '" + electronicSignatureW006.Jcsj + "',";//  ,< JcDate, varchar(32),>
                                sql += " '" + electronicSignatureW006.Qm + "')";//  ,< Base64, text,>))";
                                dbHj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgHj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgHj = "fail";
                                saveResult.BcsbyyHj = e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgHj = "fail";
                            saveResult.BcsbyyHj = "不包含环检业务部分";
                        }
                    }
                    saveResults.Add(saveResult);
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }

        /// <summary>
        /// 保存检验照片
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW007(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            string sql;
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                foreach (Object o in requestData.Body)
                {
                    InspectionPhotoW007 inspectionPhotoW007 = JSONHelper.ConvertObject<InspectionPhotoW007>(o);
                    SaveResult saveResult = new SaveResult();
                    saveResult.ID = inspectionPhotoW007.ID;
                    saveResult.Jkbh = "LYYDJKW007";
                    saveResult.Zpdm = inspectionPhotoW007.Zpdm;
                    saveResult.Zpmc = inspectionPhotoW007.Zpmc;
                    if (inspectionPhotoW007.Bcaj == "1")
                    {
                        if (VehicleInspectionController.SyAj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from UpLoad_Pic where lsh ='" + inspectionPhotoW007.Lsh + "' and Jycs ='" + inspectionPhotoW007.Jccs + "' and Zpzl ='" + inspectionPhotoW007.Zpdm + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_Pic] ";
                                sql += " ([lsh],[Jyjgbh],[Jcxdh],[Jycs],[Hphm],[Hpzl],[Clsbdh],[Zp],[Pssj],[Jyxm],[Zpzl],[upload_OK]) VALUES( ";
                                sql += " '" + inspectionPhotoW007.Lsh + "',";// (< lsh, varchar(32),>
                                sql += " '" + inspectionPhotoW007.JyjgbhAj + "',";// ,< Jyjgbh, varchar(32),>
                                sql += " '" + inspectionPhotoW007.Jcxh + "',";// ,< Jcxdh, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Jccs + "',";// ,< Jycs, int,>
                                sql += " '" + inspectionPhotoW007.Hphm + "',";// ,< Hphm, varchar(15),>
                                sql += " '" + inspectionPhotoW007.Hpzl + "',";// ,< Hpzl, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Clsbdh + "',";// ,< Clsbdh, varchar(25),>
                                sql += " '" + inspectionPhotoW007.Zp + "',";// ,< Zp, text,>
                                sql += " '" + inspectionPhotoW007.Pssj + "',";// ,< Pssj, varchar(14),>
                                sql += " '" + inspectionPhotoW007.Jyxm + "',";// ,< Jyxm, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Zpdm + "',";// ,< Zpzl, varchar(4),>
                                sql += " '0')";// ,< upload_OK, varchar(8),>)";
                                dbAj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgAj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgAj = "fail";
                                saveResult.BcsbyyAj = "照片（" + inspectionPhotoW007.Zpdm + "）" + e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = "不包含安检业务部分";
                        }
                    }
                    if (inspectionPhotoW007.BcHj == "1")
                    {
                        if (VehicleInspectionController.SyHj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from UpLoad_Wg_Pic where Jylsh ='" + inspectionPhotoW007.Lsh + "' and Jycs ='" + inspectionPhotoW007.Jccs + "' and Zpzl ='" + inspectionPhotoW007.Zphjdm + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_Wg_Pic] ";
                                sql += " ([Jylsh],[Jyjgbh],[Jcxdh],[Jycs],[Hphm],[Hpzl],[Clsbdh],[Zp],[Pssj],[Jyxm],[Zpzl],[upload_OK],[BzO2]) VALUES( ";
                                sql += " '" + inspectionPhotoW007.Lsh + "',";// (< Jylsh, varchar(17),>
                                sql += " '" + inspectionPhotoW007.JyjgbhHj + "',";//,< Jyjgbh, varchar(10),>
                                sql += " '" + inspectionPhotoW007.Jcxh + "',";//,< Jcxdh, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Jccs + "',";//,< Jycs, int,>
                                sql += " '" + inspectionPhotoW007.Hphm + "',";//,< Hphm, varchar(15),>
                                sql += " '" + inspectionPhotoW007.Hpzl + "',";//,< Hpzl, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Clsbdh + "',";//,< Clsbdh, varchar(25),>
                                sql += " '" + inspectionPhotoW007.Zp + "',";//,< Zp, nvarchar(max),>
                                sql += " '" + inspectionPhotoW007.Pssj + "',";//,< Pssj, varchar(14),>
                                sql += " '" + inspectionPhotoW007.Jyxm + "',";//,< Jyxm, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Zphjdm + "',";//,< Zpzl, varchar(8),>
                                sql += " '0',";//,< upload_OK, varchar(8),>
                                sql += " '" + inspectionPhotoW007.Hjdlsj + "')";//,< BzO1, varchar(128),>)"
                                dbHj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgHj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgHj = "fail";
                                saveResult.BcsbyyHj = "照片（" + inspectionPhotoW007.Zpdm + "）" + e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgHj = "fail";
                            saveResult.BcsbyyHj = "不包含环检业务部分";
                        }
                    }
                    saveResults.Add(saveResult);
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }

        /// <summary>
        /// 保存检验视频截取信息
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW008(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            string sql;
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                foreach (Object o in requestData.Body)
                {
                    InspectionVideoW008 inspectionVideoW008 = JSONHelper.ConvertObject<InspectionVideoW008>(o);
                    SaveResult saveResult = new SaveResult();
                    saveResult.ID = inspectionVideoW008.ID;
                    saveResult.Jkbh = "LYYDJKW008";
                    saveResult.Spbh = inspectionVideoW008.Spbhaj + "-" + inspectionVideoW008.Spbhhj;
                    if (inspectionVideoW008.Bcaj == "1")
                    {
                        if (VehicleInspectionController.SyAj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from UpLoad_AVI_XML where jcbh ='" + inspectionVideoW008.Lsh + "' and jklx ='" + inspectionVideoW008.Jccs + "' and xmbh ='" + inspectionVideoW008.Spbhaj + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_AVI_XML] ";
                                sql += " ([SXH],[jcbh],[HPZL],[hphm],[JCXZ],[jcrq],[TimS],[jklx],[xmbh] ";
                                sql += " ,[JcKsSj],[JcJsSj],[clpp],[czdw],[upload_OK],[InBz_01],[InBz_05]) VALUES( ";
                                sql += " '" + inspectionVideoW008.Jcxh + "',";// (< SXH, varchar(25),>
                                sql += " '" + inspectionVideoW008.Lsh + "',";// ,< jcbh, varchar(32),>
                                sql += " '" + inspectionVideoW008.Hpzl + "',";// ,< HPZL, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hphm + "',";// ,< hphm, varchar(24),>
                                sql += " '" + inspectionVideoW008.Ajywlb + "',";// ,< JCXZ, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcrq + "',";// ,< jcrq, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcsj + "',";// ,< TimS, varchar(16),>
                                sql += " '" + inspectionVideoW008.Jccs + "',";// ,< jklx, int,>
                                sql += " '" + inspectionVideoW008.Spbhaj + "',";// ,< xmbh, varchar(12),>
                                sql += " '" + inspectionVideoW008.Jckssj + "',";// ,< JcKsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcjssj + "',";// ,< JcJsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Clpp + "',";// ,< clpp, varchar(48),>
                                sql += " '" + inspectionVideoW008.Czdw + "',";// ,< czdw, varchar(125),>
                                sql += " '0',";// ,< upload_OK, varchar(4),>
                                sql += " '" + inspectionVideoW008.Lxxx + "',";// ,< InBz_01, varchar(72),>
                                sql += " '')";// ,< InBz_05, varchar(16),>)";
                                dbAj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgAj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgAj = "fail";
                                saveResult.BcsbyyAj = e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = "不包含安检业务部分";
                        }
                    }
                    if (inspectionVideoW008.Bchj == "1")
                    {
                        if (VehicleInspectionController.SyHj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from UpLoad_AVI_XML where jcbh ='" + inspectionVideoW008.Lsh + "' and jklx ='" + inspectionVideoW008.Jccs + "' and xmbh ='" + inspectionVideoW008.Spbhhj + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_AVI_XML] ";
                                sql += " ([SXH],[jcbh],[HPZL],[hphm],[JCXZ],[jcrq],[TimS],[jklx],[xmbh] ";
                                sql += " ,[JcKsSj],[JcJsSj],[clpp],[czdw],[upload_OK],[InBz_01],[InBz_05]) VALUES( ";
                                sql += " '" + inspectionVideoW008.Jcxh + "',";// (< SXH, varchar(25),>
                                sql += " '" + inspectionVideoW008.Lsh + "',";// ,< jcbh, varchar(32),>
                                sql += " '" + inspectionVideoW008.Hpzl + "',";// ,< HPZL, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hphm + "',";// ,< hphm, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hjywlb + "',";// ,< JCXZ, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcrq + "',";// ,< jcrq, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcsj + "',";// ,< TimS, varchar(16),>
                                sql += " '" + inspectionVideoW008.Jccs + "',";// ,< jklx, int,>
                                sql += " '" + inspectionVideoW008.Spbhhj + "',";// ,< xmbh, varchar(12),>
                                sql += " '" + inspectionVideoW008.Jckssj + "',";// ,< JcKsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcjssj + "',";// ,< JcJsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Clpp + "',";// ,< clpp, varchar(48),>
                                sql += " '" + inspectionVideoW008.Czdw + "',";// ,< czdw, varchar(125),>
                                sql += " '0',";// ,< upload_OK, varchar(4),>
                                sql += " '" + inspectionVideoW008.Lxxx + "',";// ,< InBz_01, varchar(72),>
                                sql += " '" + inspectionVideoW008.Hjdlsj + "')";// ,< InBz_05, varchar(16),>)";
                                dbHj.ExecuteNonQuery(sql, null);
                                saveResult.BcjgHj = "success";
                            }
                            catch (Exception e)
                            {
                                saveResult.BcjgHj = "fail";
                                saveResult.BcsbyyHj = e.Message;
                            }
                        }
                        else
                        {
                            saveResult.BcjgHj = "fail";
                            saveResult.BcsbyyHj = "不包含环检业务部分";
                        }
                    }
                    saveResults.Add(saveResult);
                }
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }
        /// <summary>
        /// 触发摄像头拍照
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW009(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                foreach (Object o in requestData.Body)
                {
                    PhotographW009 photographW009 = JSONHelper.ConvertObject<PhotographW009>(o);
                    SaveResult saveResult = new SaveResult();
                    saveResult.ID = photographW009.ID;
                    saveResult.Jkbh = "LYYDJKW009";
                    saveResult.Zpgw = photographW009.Zpgw;
                    saveResult.Zpdm = photographW009.Zpdm;
                    string jcyw = "1"; //标识照片是否需要上传到监管平台 0=不传  1=传
                    if (photographW009.Ajywlb == "00" | photographW009.Ajywlb == "01" | photographW009.Ajywlb == "02" | photographW009.Ajywlb == "03" | photographW009.Ajywlb == "04")
                    {
                        jcyw = "1";
                    }
                    else
                        jcyw = "0";
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        try
                        {
                            BasicHttpBinding binding = new BasicHttpBinding();
                            EndpointAddress address = new EndpointAddress("http://localhost:8072/HCNETWebService.asmx");
                            HCNETWebServiceSoapClient hCNETWebServiceSoapClient = new HCNETWebServiceSoapClient(binding, address);
                            Task<ShutterResponse> shutter = hCNETWebServiceSoapClient.ShutterAsync(photographW009.Jcxh, photographW009.Zpgw, photographW009.Lsh, photographW009.Jccs.ToString(), photographW009.Hphm, photographW009.Hpzl, photographW009.Clsbdh, photographW009.Jcxm, photographW009.Zpdm, jcyw);
                            string a = shutter.Result.Body.ShutterResult;
                            if (a == "")
                            {
                                saveResult.BcjgAj = "success";
                            }
                            else
                                saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = a;
                        }
                        catch (Exception e)
                        {
                            saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = e.Message;
                        }
                    }
                    else
                    {
                        saveResult.BcjgAj = "fail";
                        saveResult.BcsbyyAj = "不包含安检业务部分";
                    }
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }
        /// <summary>
        /// 检验项目开始
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW010(RequestData requestData, ResponseData responseData, string zdbs)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                //安检数据库连接
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                //环检数据库连接
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                //项目开始类实例化
                ProjectStartW010 projectStartW010 = JSONHelper.ConvertObject<ProjectStartW010>(responseData.Body[0]);
                //序列化为XML
                string xmlDocStr = XMLHelper.XmlSerializeStr<ProjectStartW010>(projectStartW010);
                //调用接口
                string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", projectStartW010.AjJkxlh, "18C55", xmlDocStr);
                //分析返回结果
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resultXml);
                String code = XMLHelper.GetNodeValue(doc, "code");
                if (code == "1")
                {
                    //成功时写日志
                    //记录项目开始
                    if (saveDetectionProcess("1", projectStartW010, zdbs, dbAj))
                    {
                        responseData.Code = "1";
                        responseData.Message = "SUCCESS";
                    }
                    else
                    {
                        responseData.Code = "-11";
                        responseData.Message = "日志记录失败";
                    }
                }
                else
                {
                    responseData.Code = "-1";
                    responseData.Message = "resultXml";
                }
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }
        /// <summary>
        /// 上传检验数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <param name="zdbs"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW011(RequestData requestData, ResponseData responseData, string zdbs)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            ProjectDataNQ projectDataNQ;
            ProjectDataUC projectDataUC;
            ProjectDataF1 projectDataF1;
            ProjectDataDC projectDataDC;
            ProjectDataC1 projectDataC1;
            try
            {
                ProjectData projectData = JSONHelper.ConvertObject<ProjectData>(requestData.Body[0]);
                switch (projectData.Jyxm)
                {
                    case "NQ":
                        projectDataNQ = JSONHelper.ConvertObject<ProjectDataNQ>(projectData.Jcsj);
                        string xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataNQ>(projectDataNQ);
                        string jyxm = projectDataNQ.Jyxm;
                        break;
                }
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }

        /// <summary>
        /// 检验项目结束
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <param name="zdbs"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW012(RequestData requestData, ResponseData responseData, string zdbs)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                //安检数据库连接
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                //环检数据库连接
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                //项目开始类实例化
                ProjectEndW012 projectEndW012 = JSONHelper.ConvertObject<ProjectEndW012>(requestData.Body[0]);
                //序列化为XML
                string xmlDocStr = XMLHelper.XmlSerializeStr<ProjectEndW012>(projectEndW012);
                //调用接口
                string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", projectEndW012.AjJkxlh, "18C58", xmlDocStr);
                //分析返回结果
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(resultXml);
                String code = XMLHelper.GetNodeValue(doc, "code");
                if (code == "1")
                {
                    //成功时写日志
                    //记录项目开始
                    if (saveDetectionProcess("3", projectEndW012, zdbs, dbAj))
                    {
                        responseData.Code = "1";
                        responseData.Message = "SUCCESS";
                    }
                    else
                    {
                        responseData.Code = "-11";
                        responseData.Message = "日志记录失败";
                    }
                }
                else
                {
                    responseData.Code = "-1";
                    responseData.Message = "resultXml";
                }
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (NullReferenceException nre)
            {
                responseData.Code = "-2";
                responseData.Message = "请求数据格式不正确：" + nre.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return saveResults.ToArray();
        }
        /// <summary>
        /// 写检验项目过程
        /// </summary>
        /// <param name="czgc">操作过程 1=开始  2=上传数据  3=结束</param>
        /// <param name="obj">数据实体类</param>
        /// <param name="zdbs">终端标识 IP地址</param>
        /// <param name="dbUtility">数据库连接</param>
        /// <returns></returns>
        public bool saveDetectionProcess(string czgc, object obj, string zdbs, DbUtility dbUtility)
        {
            try
            {
                string sqlStr = "";
                // '判断该操作过程，开始，上传数据，或结束
                switch (czgc)
                {
                    case "1":
                        ProjectStartW010 projectStartW010 = (ProjectStartW010)obj;
                        //项目开始，先作废上一条
                        sqlStr = "update [dbo].[tb_DetectionProcess] set zt ='4' where lsh='" + projectStartW010.Lsh + "' and jccs ='" + projectStartW010.Jccs + "' and jcxm ='" + projectStartW010.Jyxm + "'";
                        dbUtility.ExecuteNonQuery(sqlStr, null);
                        //项目开始，写入当前条
                        sqlStr = "INSERT INTO [dbo].[tb_DetectionProcess] ";
                        sqlStr += " ([lsh],[jccs],[jcxm],[sbbh],[zt],[kssj],[bcsjsj],[jssj],[jcxh]) VALUES( ";
                        sqlStr += " '" + projectStartW010.Lsh + "',";//(< lsh, varchar(17),>
                        sqlStr += " '" + projectStartW010.Jccs + "',";//,< jccs, int,>
                        sqlStr += " '" + projectStartW010.Jyxm + "',";//,< jcxm, varchar(8),>
                        sqlStr += " '" + zdbs + "',"; //,< sbbh, varchar(64),>
                        sqlStr += " '" + "1" + "',";// ,< zt, int,>
                        sqlStr += " '" + projectStartW010.Kssj + "',";// ,< kssj, varchar(20),>
                        sqlStr += " '-',";// ,< bcsjsj, varchar(20),>
                        sqlStr += " '-',";// ,< jssj, varchar(20),>
                        sqlStr += " '" + projectStartW010.Jcxh + "')";// ,< jcxh, varchar(8),>)";
                        break;
                    case "2":
                        sqlStr = "UPDATE [dbo].[tb_DetectionProcess]";
                        sqlStr += " SET [zt] = '" + czgc + "'";
                        sqlStr += " ,[bcsjsj] = convert(varchar(20),getdate(),20)";
                        sqlStr += " where lsh='" + 1 + "' and jccs='" + 1 + "' and jcxm='" + 1 + "'";
                        break;
                    case "3":
                        ProjectEndW012 projectEndW012 = (ProjectEndW012)obj;
                        sqlStr = "select * from tb_DetectionProcess where lsh='" + projectEndW012.Lsh + "' and jccs='" + projectEndW012.Jccs + "' and jcxm='" + projectEndW012.Jyxm + "'";
                        DataTable dt = dbUtility.ExecuteDataTable(sqlStr, null);
                        if (dt.Rows.Count > 0)
                        {
                            sqlStr = "UPDATE [dbo].[tb_DetectionProcess]";
                            sqlStr += " SET [zt] = '" + czgc + "'";
                            sqlStr += " ,[jssj] = '" + projectEndW012.Jssj + "'";
                            sqlStr += " where lsh='" + projectEndW012.Lsh + "' and jccs='" + projectEndW012.Jccs + "' and jcxm='" + projectEndW012.Jyxm + "'";
                        }
                        else
                        {
                            sqlStr = "INSERT INTO [dbo].[tb_DetectionProcess] ";
                            sqlStr += " ([lsh],[jccs],[jcxm],[sbbh],[zt],[kssj],[bcsjsj],[jssj],[jcxh]) VALUES( ";
                            sqlStr += " '" + projectEndW012.Lsh + "',";//(< lsh, varchar(17),>
                            sqlStr += " '" + projectEndW012.Jccs + "',";//,< jccs, int,>
                            sqlStr += " '" + projectEndW012.Jyxm + "',";//,< jcxm, varchar(8),>
                            sqlStr += " '" + zdbs + "',"; //,< sbbh, varchar(64),>
                            sqlStr += " '" + "3" + "',";// ,< zt, int,>
                            sqlStr += " '-',";// ,< kssj, varchar(20),>
                            sqlStr += " '-',";// ,< bcsjsj, varchar(20),>
                            sqlStr += " '" + projectEndW012.Jssj + "',";// ,< jssj, varchar(20),>
                            sqlStr += " '" + projectEndW012.Jcxh + "')";// ,< jcxh, varchar(8),>)";
                        }
                        break;
                }
                dbUtility.ExecuteNonQuery(sqlStr, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



       
    }
}
