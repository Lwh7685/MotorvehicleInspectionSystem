using HCNETWebService;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models;
using MotorvehicleInspectionSystem.Models.ChargePayment;
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
                responseData.Message = "用户名或密码错误---" + ex.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return users.ToArray();

        }
        /// <summary>
        /// 车辆信息登记
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW003(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                VehicleDetailsRegisteW003 vehicleDetailsRegisteW003 = JSONHelper.ConvertObject<VehicleDetailsRegisteW003>(requestData.Body[0]);
                //判断安检业务类别
                if (vehicleDetailsRegisteW003.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj != "1")
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return saveResults.ToArray();
                    }
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    string code = "-1";
                    string[] ajywlbLw = new string[] { "00", "01", "02", "03", "04" };
                    SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                    if (ajywlbLw.Contains(vehicleDetailsRegisteW003.Ajywlb) && systemParameterAj.Jcfs == "1")
                    {
                        string xmlDocStr = XMLHelper.XmlSerializeStr<VehicleDetailsRegisteW003>(vehicleDetailsRegisteW003);
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(xmlDocStr);
                        xmlDocument.Save(@"D:\TestXml\18C51_S.xml");
                        //调用接口
                        string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", systemParameterAj.Jkxlh, "18C51", xmlDocStr);
                        //分析返回结果
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(resultXml);
                        code = XMLHelper.GetNodeValue(doc, "code");
                        //记录接口日志
                        if (code != "1")
                        {
                            responseData.Code = "-1";
                            responseData.Message = resultXml;
                            return saveResults.ToArray();
                        }
                    }
                    else
                    {
                        code = "1";
                    }

                    //保存 baseinfo_net
                    if (SaveBaseinfoNetAj(vehicleDetailsRegisteW003, dbAj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveBaseinfoNetAj保存失败";
                        return saveResults.ToArray();
                    }
                    //保存  baseinfo_hand
                    if (SaveBaseinfoHandAj(vehicleDetailsRegisteW003, dbAj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveBaseinfoHandAj保存失败";
                        return saveResults.ToArray();
                    }
                    //保存
                    if (SaveFlowInfoAj(vehicleDetailsRegisteW003, dbAj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveFlowInfoAj保存失败";
                        return saveResults.ToArray();
                    }
                    if (SaveFlowdataAj(vehicleDetailsRegisteW003, dbAj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveFlowdataAj保存失败";
                        return saveResults.ToArray();
                    }
                    if (SaveCoverdataAj(vehicleDetailsRegisteW003, dbAj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveCoverdataAj保存失败";
                        return saveResults.ToArray();
                    }
                }
                if (vehicleDetailsRegisteW003.Hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return saveResults.ToArray();
                    }
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    if (SaveBaseinfoNetHj(vehicleDetailsRegisteW003, dbHj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveBaseinfoNetHj保存失败";
                        return saveResults.ToArray();
                    }
                    if (SaveJcFlowHj(vehicleDetailsRegisteW003, dbHj))
                    {
                        responseData.Code = "-1";
                        responseData.Message = "SaveJcFlowHj保存失败";
                        return saveResults.ToArray();
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
        /// 保存收费信息
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW004(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                ChargePayment chargePayment = JSONHelper.ConvertObject<ChargePayment>(requestData.Body[0]);
                //保存明细
                if (!chargePayment.SaveDetails(dbAj))
                {
                    responseData.Code = "-99";
                    responseData.Message = "SaveDetails()";
                    return saveResults.ToArray();
                }
                //保存订单
                if (!chargePayment.SaveOrder(dbAj))
                {
                    responseData.Code = "-99";
                    responseData.Message = "SaveOrder()";
                    return saveResults.ToArray();
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
                    SaveResult saveResult = new SaveResult
                    {
                        ID = electronicSignatureW006.ID,
                        Jkbh = "LYYDJKW006",
                        Ryxm = electronicSignatureW006.Ryxm,
                        Jcxm = electronicSignatureW006.Jcxm
                    };
                    if (electronicSignatureW006.Ajywlb != "-")
                    {
                        if (VehicleInspectionController.SyAj == "1")
                        {
                            try
                            {
                                //先删除
                                sql = "delete from QianMing_Info where Lsh ='" + electronicSignatureW006.Ajlsh + "' and Jccs ='" + electronicSignatureW006.Ajjccs + "' and JcXm ='" + electronicSignatureW006.Jcxm + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[QianMing_Info] ";
                                sql += " ([Lsh],[hphm],[Jccs],[JcXm],[Ry_Name],[JcDate],[Base64]) VALUES(";
                                sql += " '" + electronicSignatureW006.Ajlsh + "',";// (< Lsh, varchar(32),>
                                sql += " '" + electronicSignatureW006.Hphm + "',";//  ,< hphm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Ajjccs + "',";//  ,< Jccs, varchar(2),>
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
                    if (electronicSignatureW006.Hjywlb != "-")
                    {
                        if (VehicleInspectionController.SyHj == "1")
                        {
                            try
                            {
                                //先删除
                                //先删除
                                sql = "delete from QianMing_Info where Lsh ='" + electronicSignatureW006.Hjlsh + "' and Jccs ='" + electronicSignatureW006.Hjjccs + "' and JcXm ='" + electronicSignatureW006.Jcxm + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[QianMing_Info] ";
                                sql += " ([Lsh],[hphm],[Jccs],[JcXm],[Ry_Name],[JcDate],[Base64]) VALUES(";
                                sql += " '" + electronicSignatureW006.Hjlsh + "',";// (< Lsh, varchar(32),>
                                sql += " '" + electronicSignatureW006.Hphm + "',";//  ,< hphm, varchar(16),>
                                sql += " '" + electronicSignatureW006.Hjjccs + "',";//  ,< Jccs, varchar(2),>
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
                                sql = "delete from UpLoad_Pic where lsh ='" + inspectionPhotoW007.Ajlsh + "' and Jycs ='" + inspectionPhotoW007.Ajjccs + "' and Zpzl ='" + inspectionPhotoW007.Zpdm + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_Pic] ";
                                sql += " ([lsh],[Jyjgbh],[Jcxdh],[Jycs],[Hphm],[Hpzl],[Clsbdh],[Zp],[Pssj],[Jyxm],[Zpzl],[upload_OK]) VALUES( ";
                                sql += " '" + inspectionPhotoW007.Ajlsh + "',";// (< lsh, varchar(32),>
                                sql += " '" + inspectionPhotoW007.JyjgbhAj + "',";// ,< Jyjgbh, varchar(32),>
                                sql += " '" + inspectionPhotoW007.Jcxh + "',";// ,< Jcxdh, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Ajjccs + "',";// ,< Jycs, int,>
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
                                sql = "delete from UpLoad_Wg_Pic where Jylsh ='" + inspectionPhotoW007.Hjlsh + "' and Jycs ='" + inspectionPhotoW007.Hjjccs + "' and Zpzl ='" + inspectionPhotoW007.Zphjdm + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_Wg_Pic] ";
                                sql += " ([Jylsh],[Jyjgbh],[Jcxdh],[Jycs],[Hphm],[Hpzl],[Clsbdh],[Zp],[Pssj],[Jyxm],[Zpzl],[upload_OK],[BzO2]) VALUES( ";
                                sql += " '" + inspectionPhotoW007.Hjlsh + "',";// (< Jylsh, varchar(17),>
                                sql += " '" + inspectionPhotoW007.JyjgbhHj + "',";//,< Jyjgbh, varchar(10),>
                                sql += " '" + inspectionPhotoW007.Jcxh + "',";//,< Jcxdh, varchar(2),>
                                sql += " '" + inspectionPhotoW007.Hjjccs + "',";//,< Jycs, int,>
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
                    if (inspectionVideoW008.Ajywlb != "-")
                    {
                        if (VehicleInspectionController.SyAj == "1")
                        {
                            try
                            {
                                string lxxx = QueryIPCInfo(inspectionVideoW008.Jcxh, inspectionVideoW008.Spbhaj, dbAj, "Aj");
                                //先删除
                                sql = "delete from UpLoad_AVI_XML where jcbh ='" + inspectionVideoW008.Ajlsh + "' and jklx ='" + inspectionVideoW008.Ajjccs + "' and xmbh ='" + inspectionVideoW008.Spbhaj + "'";
                                dbAj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_AVI_XML] ";
                                sql += " ([SXH],[jcbh],[HPZL],[hphm],[JCXZ],[jcrq],[TimS],[jklx],[xmbh] ";
                                sql += " ,[JcKsSj],[JcJsSj],[clpp],[czdw],[upload_OK],[InBz_01],[InBz_02]) VALUES( ";
                                sql += " '" + inspectionVideoW008.Jcxh + "',";// (< SXH, varchar(25),>
                                sql += " '" + inspectionVideoW008.Ajlsh + "',";// ,< jcbh, varchar(32),>
                                sql += " '" + inspectionVideoW008.Hpzl + "',";// ,< HPZL, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hphm + "',";// ,< hphm, varchar(24),>
                                sql += " '" + inspectionVideoW008.Ajywlb + "',";// ,< JCXZ, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcrq + "',";// ,< jcrq, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcsj + "',";// ,< TimS, varchar(16),>
                                sql += " '" + inspectionVideoW008.Ajjccs + "',";// ,< jklx, int,>
                                sql += " '" + inspectionVideoW008.Spbhaj + "',";// ,< xmbh, varchar(12),>
                                sql += " '" + inspectionVideoW008.Jckssj + "',";// ,< JcKsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcjssj + "',";// ,< JcJsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Clpp + "',";// ,< clpp, varchar(48),>
                                sql += " '" + inspectionVideoW008.Czdw + "',";// ,< czdw, varchar(125),>
                                sql += " '" + inspectionVideoW008.Lxbz + "',";// ,< upload_OK, varchar(4),>
                                sql += " '" + lxxx + "',";// ,< InBz_01, varchar(72),>
                                sql += " '" + inspectionVideoW008.Lxdz + "')";// ,< InBz_02, varchar(16),>)";
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
                    if (inspectionVideoW008.Hjywlb != "-")
                    {
                        if (VehicleInspectionController.SyHj == "1")
                        {
                            try
                            {
                                string lxxx = QueryIPCInfo(inspectionVideoW008.Jcxh, inspectionVideoW008.Spbhhj, dbHj, "Hj");
                                //先删除
                                sql = "delete from UpLoad_AVI_XML where jcbh ='" + inspectionVideoW008.Hjlsh + "' and jklx ='" + inspectionVideoW008.Hjjccs + "' and xmbh ='" + inspectionVideoW008.Spbhhj + "'";
                                dbHj.ExecuteNonQuery(sql, null);
                                //保存
                                sql = "INSERT INTO [dbo].[UpLoad_AVI_XML] ";
                                sql += " ([SXH],[jcbh],[HPZL],[hphm],[JCXZ],[jcrq],[TimS],[jklx],[xmbh] ";
                                sql += " ,[JcKsSj],[JcJsSj],[clpp],[czdw],[upload_OK],[InBz_01],[InBz_05]) VALUES( ";
                                sql += " '" + inspectionVideoW008.Jcxh + "',";// (< SXH, varchar(25),>
                                sql += " '" + inspectionVideoW008.Hjlsh + "',";// ,< jcbh, varchar(32),>
                                sql += " '" + inspectionVideoW008.Hpzl + "',";// ,< HPZL, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hphm + "',";// ,< hphm, varchar(24),>
                                sql += " '" + inspectionVideoW008.Hjywlb + "',";// ,< JCXZ, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcrq + "',";// ,< jcrq, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcsj + "',";// ,< TimS, varchar(16),>
                                sql += " '" + inspectionVideoW008.Hjjccs + "',";// ,< jklx, int,>
                                sql += " '" + inspectionVideoW008.Spbhhj + "',";// ,< xmbh, varchar(12),>
                                sql += " '" + inspectionVideoW008.Jckssj + "',";// ,< JcKsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Jcjssj + "',";// ,< JcJsSj, varchar(24),>
                                sql += " '" + inspectionVideoW008.Clpp + "',";// ,< clpp, varchar(48),>
                                sql += " '" + inspectionVideoW008.Czdw + "',";// ,< czdw, varchar(125),>
                                sql += " '0',";// ,< upload_OK, varchar(4),>
                                sql += " '" + lxxx + "',";// ,< InBz_01, varchar(72),>
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
        /// 查询摄像头信息
        /// </summary>
        /// <param name="jcxh">线号</param>
        /// <param name="jcxm">项目</param>
        /// <param name="dbUtility">数据库连接</param>
        /// <param name="ajrohj">安检或者环保</param>
        /// <returns></returns>
        public string QueryIPCInfo(string jcxh, string jcxm, DbUtility dbUtility, string ajrohj)
        {
            try
            {
                string lxxx = "";
                if (ajrohj == "Aj")
                {
                    string sql = "select * from T_IPCInfo where Jcxh='" + jcxh + "' and Gw_DM ='" + jcxm + "'";
                    DataTable dataTable = dbUtility.ExecuteDataTable(sql, null);
                    if (dataTable.Rows.Count > 0)
                    {
                        lxxx = dataTable.Rows[0]["NVRIP"].ToString() + "/" + dataTable.Rows[0]["NVRAccount"].ToString() + "/" + dataTable.Rows[0]["NVRPassword"].ToString() + "/" + dataTable.Rows[0]["NVRChannel"].ToString();
                    }
                }
                else
                {
                    string sql = "select * from T_IPCInfo where LineNumber ='" + jcxh + "' and WorkplaceCode ='" + jcxm + "'";
                    DataTable dataTable = dbUtility.ExecuteDataTable(sql, null);
                    if (dataTable.Rows.Count > 0)
                    {
                        lxxx = dataTable.Rows[0]["NVRIP"].ToString() + "/" + dataTable.Rows[0]["NVRAccount"].ToString() + "/" + dataTable.Rows[0]["NVRPassword"].ToString() + "/" + dataTable.Rows[0]["NVRChannel"].ToString();
                    }
                }
                return lxxx;
            }
            catch
            {
                return "";
            }
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
                            Task<ShutterResponse> shutter = hCNETWebServiceSoapClient.ShutterAsync(photographW009.Jcxh, photographW009.Zpgw, photographW009.Ajlsh, photographW009.Ajjccs.ToString(), photographW009.Hphm, photographW009.Hpzl, photographW009.Clsbdh, photographW009.Jcxm, photographW009.Zpdm, jcyw);
                            string a = shutter.Result.Body.ShutterResult;
                            if (a == "")
                            {
                                responseData.Code = "1";
                                responseData.Message = "SUCCESS";
                                saveResult.BcjgAj = "success";
                            }
                            else
                            {
                                responseData.Code = "-1";
                                responseData.Message = "拍照失败：" + a;
                                saveResult.BcjgAj = "fail";
                                saveResult.BcsbyyAj = a;
                            }
                        }
                        catch (Exception e)
                        {
                            saveResult.BcjgAj = "fail";
                            saveResult.BcsbyyAj = e.Message;
                        }
                    }
                    else
                    {
                        responseData.Code = "-1";
                        responseData.Message = "不包含安检业务部分";
                        saveResult.BcjgAj = "fail";
                        saveResult.BcsbyyAj = "不包含安检业务部分";
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
        /// 检验项目开始
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW010(RequestData requestData, ResponseData responseData, string zdbs)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            string code = "-1";
            try
            {
                //安检数据库连接
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                //环检数据库连接
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                //项目开始类实例化
                ProjectStartW010 projectStartW010 = JSONHelper.ConvertObject<ProjectStartW010>(requestData.Body[0]);
                //判断是否安检操作,并且上传平台                
                if (projectStartW010.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj != "1")
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return saveResults.ToArray();
                    }
                    string[] ajywlbLw = new string[] { "00", "01", "02", "03", "04" };
                    SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                    if (ajywlbLw.Contains(projectStartW010.Ajywlb) && systemParameterAj.Jcfs == "1")
                    {
                        //序列化为XML
                        string xmlDocStr = XMLHelper.XmlSerializeStr<ProjectStartW010>(projectStartW010);
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(xmlDocStr);
                        xmlDocument.Save(@"D:\TestXml\" + projectStartW010.Jyxm + "_S.xml");
                        //调用接口
                        string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", projectStartW010.AjJkxlh, "18C55", xmlDocStr);
                        //分析返回结果
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(resultXml);
                        code = XMLHelper.GetNodeValue(doc, "code");
                        if (code != "1")
                        {
                            responseData.Code = "-1";
                            responseData.Message = resultXml;
                        }
                    }
                    else
                    {
                        //不用上传平台，code作为标志
                        code = "1";
                    }
                    if (code == "1")
                    {
                        //成功时写日志

                        //更新检验项目状态
                        if (UpdateJcxmStatusAj(projectStartW010.Ajlsh, projectStartW010.Jyxm, "2", projectStartW010.Jcxh, "", dbAj))
                        {
                            responseData.Code = "1";
                            responseData.Message = "SUCCESS";
                        }
                        else
                        {
                            responseData.Code = "-13";
                            responseData.Message = "检验项目检验状态更新失败";
                        }
                        //记录项目开始
                        if (SaveDetectionProcess("1", projectStartW010, zdbs, dbAj, "Aj"))
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
                }
                //对环保数据库的操作
                if (projectStartW010.Hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return saveResults.ToArray();
                    }
                    //判断检测项目 F1,C1
                    string sqlHj = "";
                    if (projectStartW010.Jyxm == "F1")
                    {
                        //直接更新状态吧，0=未检 1=正在检测，2=检测完成 3=上传数据                        
                        sqlHj = "update LY_Flow_Info set GW_01 ='1' where Lsh ='" + projectStartW010.Hjlsh + "'";
                    }
                    if (projectStartW010.Jyxm == "C1")
                    {
                        sqlHj = "update LY_Flow_Info set GW_03 ='1' where Lsh ='" + projectStartW010.Hjlsh + "'";
                    }
                    int reInt = dbHj.ExecuteNonQuery(sqlHj, null);
                    if (reInt == 1)
                    {

                    }
                    else
                    {
                        responseData.Code = "-1";
                        responseData.Message = "检验项目状态修改失败";
                    }
                    //记录项目开始
                    if (SaveDetectionProcess("1", projectStartW010, zdbs, dbHj, "Hj"))
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
        /// 上传检验数据
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <param name="zdbs"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW011(RequestData requestData, ResponseData responseData, string zdbs)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement xmlNode;
            List<SaveResult> saveResults = new List<SaveResult>();
            ProjectDataNQ projectDataNQ;
            ProjectDataUC projectDataUC;
            ProjectDataF1 projectDataF1;
            ProjectDataDC projectDataDC;
            ProjectDataC1 projectDataC1;
            ProjectDataItem[] projectDataItems;
            List<string> jcxmAndPj = new List<string>();
            string jcBz;
            string ajjkxlh;
            string sqlDelete;
            string sqlInsert;
            List<string> bhgXm = new List<string>();
            string jcpj = "1";
            try
            {
                ProjectData projectData = JSONHelper.ConvertObject<ProjectData>(requestData.Body[0]);
                //安检数据库连接
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                //环检数据库连接
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                string xmlDocStr;
                //实例化各项目检测类
                if (VehicleInspectionController.SyAj == "1")
                {
                    string code;
                    SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                    string ajywlb = "";
                    switch (projectData.Jyxm)
                    {
                        case "NQ":
                            projectDataNQ = JSONHelper.ConvertObject<ProjectDataNQ>(projectData.Jcsj);
                            ajywlb = projectDataNQ.Ajywlb;
                            projectDataItems = projectDataNQ.Xmlb;
                            ajjkxlh = projectDataNQ.AjJkxlh;
                            xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataNQ>(projectDataNQ);
                            xmlDocument.LoadXml(xmlDocStr);
                            xmlNode = (XmlElement)xmlDocument.SelectSingleNode(".//vehispara");
                            foreach (ProjectDataItem projectDataItem in projectDataItems)
                            {
                                switch (projectDataItem.Xmdm)
                                {
                                    case "01":
                                        XMLHelper.AddNode(xmlNode, "rlwcx", projectDataItem.Xmpj);
                                        break;
                                    default:
                                        break;
                                }
                                //拼接检验结果
                                jcxmAndPj.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmpj);
                                //不合格项目
                                if (projectDataItem.Xmpj == "2")
                                {
                                    bhgXm.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmbz);
                                    jcpj = "-1";
                                }
                            }
                            sqlDelete = "delete from [dbo].[JcData_RG] where lsh ='" + projectDataNQ.Ajlsh + "' and jccs ='" + projectDataNQ.Ajjccs + "' and dalb ='NQ'";
                            sqlInsert = "INSERT INTO [dbo].[JcData_RG] " +
                                " ([lsh],[hpzl],[hphm],[jccs],[jcdate],[kstime],[jstime],[jcpj],[dalb],[xmbh],[Bz1],[Bz2],[Bz3],[Bz4],[Bz5],[bz6],[bz7],[bz8],[bz9],[bz10],[jcxh],[jcry_01],[jcry_02]) VALUES( ";
                            sqlInsert += " '" + projectDataNQ.Ajlsh + "',";//                      (< lsh, varchar(32),>
                            sqlInsert += " '" + projectDataNQ.Hpzl + "',";// ,< hpzl, varchar(20),>
                            sqlInsert += " '" + projectDataNQ.Hphm + "',";// ,< hphm, varchar(15),>
                            sqlInsert += " '" + projectDataNQ.Ajjccs + "',";// ,< jccs, int,>
                            sqlInsert += " CONVERT(varchar(20),GETDATE(),23),";// ,< jcdate, varchar(16),>
                            if (projectDataNQ.Jckssj != "")
                            {
                                sqlInsert += " '" + projectDataNQ.Jckssj + "',";// ,< kstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< kstime, varchar(16),>
                            if (projectDataNQ.Jcjssj != "")
                            {
                                sqlInsert += " '" + projectDataNQ.Jcjssj + "',";// ,< jstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< jstime, varchar(16),>
                            sqlInsert += " '" + jcpj + "',";// ,< jcpj, varchar(2),>
                            sqlInsert += " 'NQ',";// ,< dalb, varchar(2),>
                            sqlInsert += " '" + String.Join(";", jcxmAndPj.ToArray()) + "',";// ,< xmbh, varchar(400),>
                            sqlInsert += " '" + String.Join(";", bhgXm.ToArray()) + "',";// ,< Bz1, text,>
                            sqlInsert += " '',";// ,< Bz2, varchar(32),>
                            sqlInsert += " '',";// ,< Bz3, varchar(32),>
                            sqlInsert += " '',";// ,< Bz4, varchar(max),>
                            sqlInsert += " '',";// ,< Bz5, varchar(200),>
                            sqlInsert += " '',";// ,< bz6, varchar(200),>
                            sqlInsert += " '',";// ,< bz7, varchar(200),>
                            sqlInsert += " '',";// ,< bz8, varchar(200),>
                            sqlInsert += " '',";// ,< bz9, varchar(200),>
                            sqlInsert += " '',";// ,< bz10, varchar(200),>
                            sqlInsert += " '" + projectDataNQ.Jcxh + "',";// ,< jcxh, varchar(2),>
                            sqlInsert += " '" + projectDataNQ.Lwcxjyy + "',";// ,< jcry_01, varchar(8),>
                            sqlInsert += " '')";// ,< jcry_02, varchar(8),>)
                            break;
                        case "UC":
                            projectDataUC = JSONHelper.ConvertObject<ProjectDataUC>(projectData.Jcsj);
                            ajywlb = projectDataUC.Ajywlb;
                            projectDataItems = projectDataUC.Xmlb;
                            ajjkxlh = projectDataUC.AjJkxlh;
                            xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataUC>(projectDataUC);
                            xmlDocument.LoadXml(xmlDocStr);
                            xmlNode = (XmlElement)xmlDocument.SelectSingleNode(".//vehispara");
                            foreach (ProjectDataItem projectDataItem in projectDataItems)
                            {
                                switch (projectDataItem.Xmdm)
                                {
                                    case "02":
                                        XMLHelper.AddNode(xmlNode, "rhplx", projectDataItem.Xmpj);
                                        break;
                                    case "03":
                                        XMLHelper.AddNode(xmlNode, "rppxh", projectDataItem.Xmpj);
                                        break;
                                    case "04":
                                        XMLHelper.AddNode(xmlNode, "rvin", projectDataItem.Xmpj);
                                        break;
                                    case "05":
                                        XMLHelper.AddNode(xmlNode, "rfdjh", projectDataItem.Xmpj);
                                        break;
                                    case "06":
                                        XMLHelper.AddNode(xmlNode, "rcsys", projectDataItem.Xmpj);
                                        break;
                                    default:
                                        break;
                                }
                                //拼接检验结果
                                jcxmAndPj.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmpj);
                                //不合格项目
                                if (projectDataItem.Xmpj == "2")
                                {
                                    bhgXm.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmbz);
                                    jcpj = "-1";
                                }
                            }
                            jcBz = projectDataUC.Bz;
                            sqlDelete = "delete from [dbo].[JcData_RG] where lsh ='" + projectDataUC.Ajlsh + "' and jccs ='" + projectDataUC.Ajjccs + "' and dalb ='UC'";
                            sqlInsert = "INSERT INTO [dbo].[JcData_RG] " +
                                " ([lsh],[hpzl],[hphm],[jccs],[jcdate],[kstime],[jstime],[jcpj],[dalb],[xmbh],[Bz1],[Bz2],[Bz3],[Bz4],[Bz5],[bz6],[bz7],[bz8],[bz9],[bz10],[jcxh],[jcry_01],[jcry_02]) VALUES( ";
                            sqlInsert += " '" + projectDataUC.Ajlsh + "',";//                      (< lsh, varchar(32),>
                            sqlInsert += " '" + projectDataUC.Hpzl + "',";// ,< hpzl, varchar(20),>
                            sqlInsert += " '" + projectDataUC.Hphm + "',";// ,< hphm, varchar(15),>
                            sqlInsert += " '" + projectDataUC.Ajjccs + "',";// ,< jccs, int,>
                            sqlInsert += " CONVERT(varchar(20),GETDATE(),23),";// ,< jcdate, varchar(16),>
                            if (projectDataUC.Jckssj != "")
                            {
                                sqlInsert += " '" + projectDataUC.Jckssj + "',";// ,< kstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< kstime, varchar(16),>
                            if (projectDataUC.Jcjssj != "")
                            {
                                sqlInsert += " '" + projectDataUC.Jcjssj + "',";// ,< jstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< jstime, varchar(16),>
                            sqlInsert += " '" + jcpj + "',";// ,< jcpj, varchar(2),>
                            sqlInsert += " 'UC',";// ,< dalb, varchar(2),>
                            sqlInsert += " '" + String.Join(";", jcxmAndPj.ToArray()) + "',";// ,< xmbh, varchar(400),>
                            sqlInsert += " '" + String.Join(";", bhgXm.ToArray()) + "',";// ,< Bz1, text,>                   
                            sqlInsert += " '',";// ,< Bz2, varchar(32),>
                            sqlInsert += " '',";// ,< Bz3, varchar(32),>
                            sqlInsert += " '',";// ,< Bz4, varchar(max),>
                            sqlInsert += " '" + projectDataUC.Bz + "',";// ,< Bz5, varchar(200),>
                            sqlInsert += " '',";// ,< bz6, varchar(200),>
                            sqlInsert += " '',";// ,< bz7, varchar(200),>
                            sqlInsert += " '',";// ,< bz8, varchar(200),>
                            sqlInsert += " '',";// ,< bz9, varchar(200),>
                            sqlInsert += " '',";// ,< bz10, varchar(200),>
                            sqlInsert += " '" + projectDataUC.Jcxh + "',";// ,< jcxh, varchar(2),>
                            sqlInsert += " '" + projectDataUC.Wgjcjyy + "',";// ,< jcry_01, varchar(8),>
                            sqlInsert += " '')";// ,< jcry_02, varchar(8),>)
                            break;
                        case "F1":
                            projectDataF1 = JSONHelper.ConvertObject<ProjectDataF1>(projectData.Jcsj);
                            ajywlb = projectDataF1.Ajywlb;
                            projectDataItems = projectDataF1.XmlbAJ;
                            ajjkxlh = projectDataF1.AjJkxlh;
                            xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataF1>(projectDataF1);
                            xmlDocument.LoadXml(xmlDocStr);
                            xmlNode = (XmlElement)xmlDocument.SelectSingleNode(".//vehispara");
                            foreach (ProjectDataItem projectDataItem in projectDataItems)
                            {
                                switch (projectDataItem.Xmdm)
                                {
                                    case "07":
                                        XMLHelper.AddNode(xmlNode, "rwkcc", projectDataItem.Xmpj);
                                        break;
                                    case "08":
                                        XMLHelper.AddNode(xmlNode, "rzj", projectDataItem.Xmpj);
                                        break;
                                    case "09":
                                        XMLHelper.AddNode(xmlNode, "rhdzrs", projectDataItem.Xmpj);
                                        break;
                                    case "10":
                                        XMLHelper.AddNode(xmlNode, "rlbgd", projectDataItem.Xmpj);
                                        break;
                                    case "11":
                                        XMLHelper.AddNode(xmlNode, "rhzgbthps", projectDataItem.Xmpj);
                                        break;
                                    case "12":
                                        XMLHelper.AddNode(xmlNode, "rkcyjck", projectDataItem.Xmpj);
                                        break;
                                    case "13":
                                        XMLHelper.AddNode(xmlNode, "rkccktd", projectDataItem.Xmpj);
                                        break;
                                    case "14":
                                        XMLHelper.AddNode(xmlNode, "rhx", projectDataItem.Xmpj);
                                        break;
                                    case "15":
                                        XMLHelper.AddNode(xmlNode, "rcswg", projectDataItem.Xmpj);
                                        break;
                                    case "16":
                                        XMLHelper.AddNode(xmlNode, "rwgbs", projectDataItem.Xmpj);
                                        break;
                                    case "17":
                                        XMLHelper.AddNode(xmlNode, "rwbzm", projectDataItem.Xmpj);
                                        break;
                                    case "18":
                                        XMLHelper.AddNode(xmlNode, "rlt", projectDataItem.Xmpj);
                                        break;
                                    case "19":
                                        XMLHelper.AddNode(xmlNode, "rhpaz", projectDataItem.Xmpj);
                                        break;
                                    case "20":
                                        XMLHelper.AddNode(xmlNode, "rjzgj", projectDataItem.Xmpj);
                                        break;
                                    case "21":
                                        XMLHelper.AddNode(xmlNode, "rqcaqd", projectDataItem.Xmpj);
                                        break;
                                    case "22":
                                        XMLHelper.AddNode(xmlNode, "rsjp", projectDataItem.Xmpj);
                                        break;
                                    case "23":
                                        XMLHelper.AddNode(xmlNode, "rmhq", projectDataItem.Xmpj);
                                        break;
                                    case "24":
                                        XMLHelper.AddNode(xmlNode, "rxsjly", projectDataItem.Xmpj);
                                        break;
                                    case "25":
                                        XMLHelper.AddNode(xmlNode, "rcsfgbs", projectDataItem.Xmpj);
                                        break;
                                    case "26":
                                        XMLHelper.AddNode(xmlNode, "rclwbzb", projectDataItem.Xmpj);
                                        break;
                                    case "27":
                                        XMLHelper.AddNode(xmlNode, "rchfh", projectDataItem.Xmpj);
                                        break;
                                    case "28":
                                        XMLHelper.AddNode(xmlNode, "ryjc", projectDataItem.Xmpj);
                                        break;
                                    case "29":
                                        XMLHelper.AddNode(xmlNode, "rjjx", projectDataItem.Xmpj);
                                        break;
                                    case "30":
                                        XMLHelper.AddNode(xmlNode, "rxsgn", projectDataItem.Xmpj);
                                        break;
                                    case "31":
                                        XMLHelper.AddNode(xmlNode, "rfbs", projectDataItem.Xmpj);
                                        break;
                                    case "32":
                                        XMLHelper.AddNode(xmlNode, "rfzzd", projectDataItem.Xmpj);
                                        break;
                                    case "33":
                                        XMLHelper.AddNode(xmlNode, "rpszdq", projectDataItem.Xmpj);
                                        break;
                                    case "34":
                                        XMLHelper.AddNode(xmlNode, "rjxtz", projectDataItem.Xmpj);
                                        break;
                                    case "35":
                                        XMLHelper.AddNode(xmlNode, "rjjqd", projectDataItem.Xmpj);
                                        break;
                                    case "36":
                                        XMLHelper.AddNode(xmlNode, "rfdjcmh", projectDataItem.Xmpj);
                                        break;
                                    case "37":
                                        XMLHelper.AddNode(xmlNode, "rsddd", projectDataItem.Xmpj);
                                        break;
                                    case "38":
                                        XMLHelper.AddNode(xmlNode, "rfzdtb", projectDataItem.Xmpj);
                                        break;
                                    case "39":
                                        XMLHelper.AddNode(xmlNode, "rxcbz", projectDataItem.Xmpj);
                                        break;
                                    case "40":
                                        XMLHelper.AddNode(xmlNode, "rwxhwbz", projectDataItem.Xmpj);
                                        break;
                                    case "41":
                                        XMLHelper.AddNode(xmlNode, "rjsqglss", projectDataItem.Xmpj);
                                        break;
                                    case "42":
                                        XMLHelper.AddNode(xmlNode, "ztcjrfzzz", projectDataItem.Xmpj);
                                        break;
                                    default:
                                        break;
                                }
                                //拼接检验结果
                                jcxmAndPj.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmpj);
                                //不合格项目
                                if (projectDataItem.Xmpj == "2")
                                {
                                    bhgXm.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmbz);
                                    jcpj = "-1";
                                }
                            }
                            jcBz = projectDataF1.Bz;
                            sqlDelete = "delete from [dbo].[JcData_RG] where lsh ='" + projectDataF1.Ajlsh + "' and jccs ='" + projectDataF1.Ajjccs + "' and dalb ='F1'";
                            sqlInsert = "INSERT INTO [dbo].[JcData_RG] " +
                                " ([lsh],[hpzl],[hphm],[jccs],[jcdate],[kstime],[jstime],[jcpj],[dalb],[xmbh],[Bz1],[Bz2],[Bz3],[Bz4],[Bz5],[bz6],[bz7],[bz8],[bz9],[bz10],[jcxh],[jcry_01],[jcry_02]) VALUES( ";
                            sqlInsert += " '" + projectDataF1.Ajlsh + "',";//                      (< lsh, varchar(32),>
                            sqlInsert += " '" + projectDataF1.Hpzl + "',";// ,< hpzl, varchar(20),>
                            sqlInsert += " '" + projectDataF1.Hphm + "',";// ,< hphm, varchar(15),>
                            sqlInsert += " '" + projectDataF1.Ajjccs + "',";// ,< jccs, int,>
                            sqlInsert += " CONVERT(varchar(20),GETDATE(),23),";// ,< jcdate, varchar(16),>
                            if (projectDataF1.Jckssj != "")
                            {
                                sqlInsert += " '" + projectDataF1.Jckssj + "',";// ,< kstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< kstime, varchar(16),>
                            if (projectDataF1.Jcjssj != "")
                            {
                                sqlInsert += " '" + projectDataF1.Jcjssj + "',";// ,< jstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< jstime, varchar(16),>
                            sqlInsert += " '" + jcpj + "',";// ,< jcpj, varchar(2),>
                            sqlInsert += " 'F1',";// ,< dalb, varchar(2),>
                            sqlInsert += " '" + String.Join(";", jcxmAndPj.ToArray()) + "',";// ,< xmbh, varchar(400),>
                            sqlInsert += " '" + String.Join(";", bhgXm.ToArray()) + "',";// ,< Bz1, text,>                   
                            sqlInsert += " '',";// ,< Bz2, varchar(32),>
                            if (projectDataF1.Cwkc != "" | projectDataF1.Cwkk != "" | projectDataF1.Cwkg != "")
                            {
                                sqlInsert += " '" + projectDataF1.Cwkc + "*" + projectDataF1.Cwkk + "*" + projectDataF1.Cwkg + "',";// ,< Bz3, varchar(32),>
                            }
                            else
                                sqlInsert += " '',";// ,< Bz3, varchar(32),>
                            sqlInsert += " '',";// ,< Bz4, varchar(max),>
                            sqlInsert += " '" + projectDataF1.Bz + "',";// ,< Bz5, varchar(200),>
                            if (projectDataF1.Dczxlhwsd != "" | projectDataF1.Dcqtlhwsd != "")
                            {
                                sqlInsert += " '" + projectDataF1.Dczxlhwsd + "|" + projectDataF1.Dcqtlhwsd + "',";// ,< bz6, varchar(200),> 轮胎花纹深度
                            }
                            else if (projectDataF1.Gchwsd != "")
                            {
                                sqlInsert += " '" + projectDataF1.Gchwsd + "',";// ,< bz6, varchar(200),> 轮胎花纹深度
                            }
                            else
                            {
                                sqlInsert += " '',";// ,< bz6, varchar(200),> 轮胎花纹深度
                            }
                            sqlInsert += " '" + projectDataF1.Zj + "',";// ,< bz7, varchar(200),>
                            sqlInsert += " '" + projectDataF1.Cxlbgd + "',";// ,< bz8, varchar(200),>
                            if (projectDataF1.Yzzygdc != "" && projectDataF1.Zhzzygdc != "")
                            {
                                sqlInsert += " '" + projectDataF1.Yzzgd + "," + projectDataF1.Yzygd + "|" + projectDataF1.Zhzzgd + "," + projectDataF1.Zhzygd + "',";// ,< bz9, varchar(200),>对称部位高度差
                            }
                            else if (projectDataF1.Zhzzygdc != "")
                            {
                                sqlInsert += " '" + projectDataF1.Zhzzgd + "," + projectDataF1.Zhzygd + "',";// ,< bz9, varchar(200),>对称部位高度差
                            }
                            else
                            {
                                sqlInsert += " '',";// ,< bz9, varchar(200),>对称部位高度差
                            }
                            sqlInsert += " '',";// ,< bz10, varchar(200),>
                            sqlInsert += " '" + projectDataF1.Jcxh + "',";// ,< jcxh, varchar(2),>
                            sqlInsert += " '" + projectDataF1.Wgjcjyy + "',";// ,< jcry_01, varchar(8),>
                            sqlInsert += " '')";// ,< jcry_02, varchar(8),>)
                            break;
                        case "DC":
                            projectDataDC = JSONHelper.ConvertObject<ProjectDataDC>(projectData.Jcsj);
                            ajywlb = projectDataDC.Ajywlb;
                            projectDataItems = projectDataDC.Xmlb;
                            ajjkxlh = projectDataDC.AjJkxlh;
                            xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataDC>(projectDataDC);
                            xmlDocument.LoadXml(xmlDocStr);
                            xmlNode = (XmlElement)xmlDocument.SelectSingleNode(".//vehispara");
                            foreach (ProjectDataItem projectDataItem in projectDataItems)
                            {
                                switch (projectDataItem.Xmdm)
                                {
                                    case "43":
                                        XMLHelper.AddNode(xmlNode, "rzxx", projectDataItem.Xmpj);
                                        break;
                                    case "44":
                                        XMLHelper.AddNode(xmlNode, "rcdx", projectDataItem.Xmpj);
                                        break;
                                    case "45":
                                        XMLHelper.AddNode(xmlNode, "rzdx", projectDataItem.Xmpj);
                                        break;
                                    case "46":
                                        XMLHelper.AddNode(xmlNode, "rybzsq", projectDataItem.Xmpj);
                                        break;
                                    default:
                                        break;
                                }
                                //拼接检验结果
                                jcxmAndPj.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmpj);
                                //不合格项目
                                if (projectDataItem.Xmpj == "2")
                                {
                                    bhgXm.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmbz);
                                }
                            }
                            jcBz = projectDataDC.Bz;
                            sqlDelete = "delete from [dbo].[JcData_RG] where lsh ='" + projectDataDC.Ajlsh + "' and jccs ='" + projectDataDC.Ajjccs + "' and dalb ='DC'";
                            sqlInsert = "INSERT INTO [dbo].[JcData_RG] " +
                                " ([lsh],[hpzl],[hphm],[jccs],[jcdate],[kstime],[jstime],[jcpj],[dalb],[xmbh],[Bz1],[Bz2],[Bz3],[Bz4],[Bz5],[bz6],[bz7],[bz8],[bz9],[bz10],[jcxh],[jcry_01],[jcry_02]) VALUES( ";
                            sqlInsert += " '" + projectDataDC.Ajlsh + "',";//                      (< lsh, varchar(32),>
                            sqlInsert += " '" + projectDataDC.Hpzl + "',";// ,< hpzl, varchar(20),>
                            sqlInsert += " '" + projectDataDC.Hphm + "',";// ,< hphm, varchar(15),>
                            sqlInsert += " '" + projectDataDC.Ajjccs + "',";// ,< jccs, int,>
                            sqlInsert += " CONVERT(varchar(20),GETDATE(),23),";// ,< jcdate, varchar(16),>
                            if (projectDataDC.Jckssj != "")
                            {
                                sqlInsert += " '" + projectDataDC.Jckssj + "',";// ,< kstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< kstime, varchar(16),>
                            if (projectDataDC.Jcjssj != "")
                            {
                                sqlInsert += " '" + projectDataDC.Jcjssj + "',";// ,< jstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< jstime, varchar(16),>
                            sqlInsert += " '" + jcpj + "',";// ,< jcpj, varchar(2),>
                            sqlInsert += " 'DC',";// ,< dalb, varchar(2),>
                            sqlInsert += " '" + String.Join(";", jcxmAndPj.ToArray()) + "',";// ,< xmbh, varchar(400),>
                            sqlInsert += " '" + String.Join(";", bhgXm.ToArray()) + "',";// ,< Bz1, text,>                   
                            sqlInsert += " '',";// ,< Bz2, varchar(32),>
                            sqlInsert += " '',";// ,< Bz3, varchar(32),>
                            sqlInsert += " '',";// ,< Bz4, varchar(max),>
                            sqlInsert += " '" + projectDataDC.Bz + "',";// ,< Bz5, varchar(200),>
                            sqlInsert += " '',";// ,< bz6, varchar(200),>
                            sqlInsert += " '',";// ,< bz7, varchar(200),>
                            sqlInsert += " '',";// ,< bz8, varchar(200),>
                            sqlInsert += " '',";// ,< bz9, varchar(200),>
                            sqlInsert += " '" + projectDataDC.Fxpzdzyzdl + "',";// ,< bz10, varchar(200),>
                            sqlInsert += " '" + projectDataDC.Jcxh + "',";// ,< jcxh, varchar(2),>
                            sqlInsert += " '" + projectDataDC.Dpdtjyy + "',";// ,< jcry_01, varchar(8),>
                            sqlInsert += " '" + projectDataDC.Ycy + "')";// ,< jcry_02, varchar(8),>)
                            break;
                        case "C1":
                            projectDataC1 = JSONHelper.ConvertObject<ProjectDataC1>(projectData.Jcsj);
                            ajywlb = projectDataC1.Ajywlb;
                            projectDataItems = projectDataC1.Xmlb;
                            ajjkxlh = projectDataC1.AjJkxlh;
                            xmlDocStr = XMLHelper.XmlSerializeStr<ProjectDataC1>(projectDataC1);
                            xmlDocument.LoadXml(xmlDocStr);
                            xmlNode = (XmlElement)xmlDocument.SelectSingleNode(".//vehispara");
                            foreach (ProjectDataItem projectDataItem in projectDataItems)
                            {
                                switch (projectDataItem.Xmdm)
                                {
                                    case "47":
                                        XMLHelper.AddNode(xmlNode, "rzxxbj", projectDataItem.Xmpj);
                                        break;
                                    case "48":
                                        XMLHelper.AddNode(xmlNode, "rcdxbj", projectDataItem.Xmpj);
                                        break;
                                    case "49":
                                        XMLHelper.AddNode(xmlNode, "rxsxbj", projectDataItem.Xmpj);
                                        break;
                                    case "50":
                                        XMLHelper.AddNode(xmlNode, "rzdxbj", projectDataItem.Xmpj);
                                        break;
                                    case "51":
                                        XMLHelper.AddNode(xmlNode, "rqtbj", projectDataItem.Xmpj);
                                        break;
                                    default:
                                        break;
                                }
                                //拼接检验结果
                                jcxmAndPj.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmpj);
                                //不合格项目
                                if (projectDataItem.Xmpj == "2")
                                {
                                    bhgXm.Add(projectDataItem.Xmdm + "," + projectDataItem.Xmbz);
                                }
                            }
                            jcBz = projectDataC1.Bz;
                            sqlDelete = "delete from [dbo].[JcData_RG] where lsh ='" + projectDataC1.Ajlsh + "' and jccs ='" + projectDataC1.Ajjccs + "' and dalb ='C1'";
                            sqlInsert = "INSERT INTO [dbo].[JcData_RG] " +
                                " ([lsh],[hpzl],[hphm],[jccs],[jcdate],[kstime],[jstime],[jcpj],[dalb],[xmbh],[Bz1],[Bz2],[Bz3],[Bz4],[Bz5],[bz6],[bz7],[bz8],[bz9],[bz10],[jcxh],[jcry_01],[jcry_02]) VALUES( ";
                            sqlInsert += " '" + projectDataC1.Ajlsh + "',";//                      (< lsh, varchar(32),>
                            sqlInsert += " '" + projectDataC1.Hpzl + "',";// ,< hpzl, varchar(20),>
                            sqlInsert += " '" + projectDataC1.Hphm + "',";// ,< hphm, varchar(15),>
                            sqlInsert += " '" + projectDataC1.Ajjccs + "',";// ,< jccs, int,>
                            sqlInsert += " CONVERT(varchar(20),GETDATE(),23),";// ,< jcdate, varchar(16),>
                            if (projectDataC1.Jckssj != "")
                            {
                                sqlInsert += " '" + projectDataC1.Jckssj + "',";// ,< kstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< kstime, varchar(16),>
                            if (projectDataC1.Jcjssj != "")
                            {
                                sqlInsert += " '" + projectDataC1.Jcjssj + "',";// ,< jstime, varchar(16),>
                            }
                            else
                                sqlInsert += " '" + DateTime.Now.ToString("yyyyMmddHHmmss") + "',";// ,< jstime, varchar(16),>
                            sqlInsert += " '" + jcpj + "',";// ,< jcpj, varchar(2),>
                            sqlInsert += " 'C1',";// ,< dalb, varchar(2),>
                            sqlInsert += " '" + String.Join(";", jcxmAndPj.ToArray()) + "',";// ,< xmbh, varchar(400),>
                            sqlInsert += " '" + String.Join(";", bhgXm.ToArray()) + "',";// ,< Bz1, text,>                   
                            sqlInsert += " '',";// ,< Bz2, varchar(32),>
                            sqlInsert += " '',";// ,< Bz3, varchar(32),>
                            sqlInsert += " '',";// ,< Bz4, varchar(max),>
                            sqlInsert += " '" + projectDataC1.Bz + "',";// ,< Bz5, varchar(200),>
                            sqlInsert += " '',";// ,< bz6, varchar(200),>
                            sqlInsert += " '',";// ,< bz7, varchar(200),>
                            sqlInsert += " '',";// ,< bz8, varchar(200),>
                            sqlInsert += " '',";// ,< bz9, varchar(200),>
                            sqlInsert += " '',";// ,< bz10, varchar(200),>
                            sqlInsert += " '" + projectDataC1.Jcxh + "',";// ,< jcxh, varchar(2),>
                            sqlInsert += " '" + projectDataC1.Dpjcjyy + "',";// ,< jcry_01, varchar(8),>
                            sqlInsert += " '" + projectDataC1.Ycy + "')";// ,< jcry_02, varchar(8),>)
                            break;
                        default:
                            responseData.Code = "-12";
                            responseData.Message = "不能識別的參數（Jyxm）";
                            return saveResults.ToArray();
                    }
                    //判断联网方式，安检业务类别
                    string[] ajywlbLw = new string[] { "00", "01", "02", "03", "04" };
                    if (systemParameterAj.Jcfs == "1" && ajywlbLw.Contains(ajywlb))
                    {
                        xmlDocStr = xmlDocument.InnerXml.ToString();
                        xmlDocument.Save(@"D:\TestXml\" + projectData.Jyxm + ".xml");
                        //调用接口
                        string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", ajjkxlh, "18C80", xmlDocStr);
                        //分析返回结果
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(resultXml);
                        code = XMLHelper.GetNodeValue(doc, "code");
                    }
                    else
                    {
                        code = "1";
                    }
                    if (code == "1")
                    {
                        //成功时写日志
                        //更新检验项目状态
                        bool statusFlag = false;
                        switch (projectData.Jyxm)
                        {
                            case "NQ":
                                projectDataNQ = JSONHelper.ConvertObject<ProjectDataNQ>(projectData.Jcsj);
                                statusFlag = UpdateJcxmStatusAj(projectDataNQ.Ajlsh, projectDataNQ.Jyxm, "3", projectDataNQ.Jcxh, projectDataNQ.Lwcxjyy, dbAj);
                                break;
                            case "UC":
                                projectDataUC = JSONHelper.ConvertObject<ProjectDataUC>(projectData.Jcsj);
                                statusFlag = UpdateJcxmStatusAj(projectDataUC.Ajlsh, projectDataUC.Jyxm, "3", projectDataUC.Jcxh, projectDataUC.Wgjcjyy, dbAj);
                                break;
                            case "F1":
                                projectDataF1 = JSONHelper.ConvertObject<ProjectDataF1>(projectData.Jcsj);
                                statusFlag = UpdateJcxmStatusAj(projectDataF1.Ajlsh, projectDataF1.Jyxm, "3", projectDataF1.Jcxh, projectDataF1.Wgjcjyy, dbAj);
                                break;
                            case "C1":
                                projectDataC1 = JSONHelper.ConvertObject<ProjectDataC1>(projectData.Jcsj);
                                statusFlag = UpdateJcxmStatusAj(projectDataC1.Ajlsh, projectDataC1.Jyxm, "3", projectDataC1.Jcxh, projectDataC1.Dpjcjyy, dbAj);
                                break;
                            case "DC":
                                projectDataDC = JSONHelper.ConvertObject<ProjectDataDC>(projectData.Jcsj);
                                statusFlag = UpdateJcxmStatusAj(projectDataDC.Ajlsh, projectDataDC.Jyxm, "3", projectDataDC.Jcxh, projectDataDC.Dpdtjyy, dbAj);
                                break;
                        }
                        if (statusFlag)
                        {
                            responseData.Code = "1";
                            responseData.Message = "SUCCESS";
                        }
                        else
                        {
                            responseData.Code = "-13";
                            responseData.Message = "检验项目检验状态更新失败";
                        }
                        //记录项目开始
                        if (SaveDetectionProcess("2", projectData, zdbs, dbAj, "Aj"))
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
                    //保存数据库
                    dbAj.ExecuteNonQuery(sqlDelete, null);
                    dbAj.ExecuteNonQuery(sqlInsert, null);
                }

                //保存环检数据库
                //判断检测项目 F1,C1
                string sqlHj = "";
                string hjywlb = "-";
                if (projectData.Jyxm == "F1")
                {
                    projectDataF1 = JSONHelper.ConvertObject<ProjectDataF1>(projectData.Jcsj);
                    hjywlb = projectDataF1.Hjywlb;
                }
                if (projectData.Jyxm == "C1")
                {
                    projectDataC1 = JSONHelper.ConvertObject<ProjectDataC1>(projectData.Jcsj);
                    hjywlb = projectDataC1.Hjywlb;
                    sqlHj = "update LY_Flow_Info set GW_03 ='3' where Lsh ='" + projectDataC1.Hjlsh + "'";

                }
                if (hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return saveResults.ToArray();
                    }
                    //保存数据
                    if (projectData.Jyxm == "F1")
                    {
                        projectDataF1 = JSONHelper.ConvertObject<ProjectDataF1>(projectData.Jcsj);
                        projectDataItems = projectDataF1.XmlbHJ;
                        StringBuilder xmXh = new StringBuilder();
                        foreach (ProjectDataItem projectDataItem in projectDataItems)
                        {
                            xmXh.Append(projectDataItem.Xmdm).Append(":").Append(projectDataItem.Xmpj).Append(";");
                            if (projectDataItem.Xmpj == "2")
                            {
                                jcpj = "-1";
                            }
                        }
                        sqlHj = "INSERT INTO [dbo].[JcDate_Work_JC]([Lsh] ,[hpzl] ,[hphm],[Jccs],[JcDate],[KsTime],[JsTime],[JcPj],[DaLB],[WorkJcxm],[WorkMan],[TD_JC])VALUES (";
                        sqlHj += " '" + projectDataF1.Hjlsh + "',";
                        sqlHj += " '" + projectDataF1.Hpzl + "',";
                        sqlHj += " '" + projectDataF1.Hphm + "',";
                        sqlHj += " '" + projectDataF1.Hjjccs + "',";
                        sqlHj += " '" + DateTime.Now.ToString("yyyyMMdd") + "',";
                        sqlHj += " '" + projectDataF1.Jckssj + "',";
                        sqlHj += " '" + projectDataF1.Jcjssj + "',";
                        sqlHj += " '" + jcpj + "',";
                        sqlHj += " '" + "F1" + "',";
                        sqlHj += " '" + xmXh.ToString() + "',";
                        sqlHj += " '" + projectDataF1.Wgjcjyy + "',";
                        sqlHj += " '" + projectDataF1.Jcxh + "')";
                        dbHj.ExecuteNonQuery(sqlHj, null);
                        //直接更新状态吧，0=未检 1=正在检测，2=检测完成 3=上传数据                        
                        sqlHj = "update LY_Flow_Info set GW_01 ='3' where Lsh ='" + projectDataF1.Hjlsh + "'";
                    }
                    //更新状态
                    int reInt = dbHj.ExecuteNonQuery(sqlHj, null);
                    if (reInt == 1)
                    {

                    }
                    else
                    {
                        responseData.Code = "-1";
                        responseData.Message = "检验项目状态修改失败";
                    }
                    //记录项目开始
                    if (SaveDetectionProcess("2", projectData, zdbs, dbHj, "Hj"))
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
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException e)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS----" + e.Message;
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
                String code;
                //安检数据库连接
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                //环检数据库连接
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                //项目开始类实例化
                ProjectEndW012 projectEndW012 = JSONHelper.ConvertObject<ProjectEndW012>(requestData.Body[0]);
                if (projectEndW012.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj != "1")
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return saveResults.ToArray();
                    }
                    //序列化为XML
                    string xmlDocStr = XMLHelper.XmlSerializeStr<ProjectEndW012>(projectEndW012);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xmlDocStr);
                    xmlDocument.Save(@"D:\TestXml\" + projectEndW012.Jyxm + "_E.xml");
                    //调用接口
                    SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                    string[] ajywlbLw = new string[] { "00", "01", "02", "03", "04" };
                    if (systemParameterAj.Jcfs == "1" && ajywlbLw.Contains(projectEndW012.Ajywlb))
                    {
                        string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", projectEndW012.AjJkxlh, "18C58", xmlDocStr);
                        //分析返回结果
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(resultXml);
                        code = XMLHelper.GetNodeValue(doc, "code");
                    }
                    else
                    {
                        code = "1";
                    }
                    if (code == "1")
                    {
                        //成功时写日志
                        //更新检验项目状态
                        if (UpdateJcxmStatusAj(projectEndW012.Ajlsh, projectEndW012.Jyxm, "1", projectEndW012.Jcxh, "", dbAj))
                        {
                            responseData.Code = "1";
                            responseData.Message = "SUCCESS";
                        }
                        else
                        {
                            responseData.Code = "-13";
                            responseData.Message = "检验项目检验状态更新失败";
                        }
                        //记录项目开始
                        if (SaveDetectionProcess("3", projectEndW012, zdbs, dbAj, "Aj"))
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
                if (projectEndW012.Hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return saveResults.ToArray();
                    }
                    //判断检测项目 F1,C1
                    string sqlHj = "";
                    if (projectEndW012.Jyxm == "F1")
                    {
                        //直接更新状态吧，0=未检 1=正在检测，2=检测完成 3=上传数据                        
                        sqlHj = "update LY_Flow_Info set GW_01 ='2' where Lsh ='" + projectEndW012.Hjlsh + "'";
                    }
                    if (projectEndW012.Jyxm == "C1")
                    {
                        sqlHj = "update LY_Flow_Info set GW_03 ='2' where Lsh ='" + projectEndW012.Hjlsh + "'";
                    }
                    int reInt = dbHj.ExecuteNonQuery(sqlHj, null);
                    if (reInt == 1)
                    {

                    }
                    else
                    {
                        responseData.Code = "-1";
                        responseData.Message = "检验项目状态修改失败";
                    }
                    //记录项目开始
                    if (SaveDetectionProcess("3", projectEndW012, zdbs, dbHj, "Hj"))
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
        /// 机动车上线
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SaveResult[] LYYDJKW015(RequestData requestData, ResponseData responseData)
        {
            List<SaveResult> saveResults = new List<SaveResult>();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                StartDetectionW015 startDetectionW015 = JSONHelper.ConvertObject<StartDetectionW015>(requestData.Body[0]);
                if (string.IsNullOrEmpty(startDetectionW015.Ajlsh))
                {
                    responseData.Code = "-8";
                    responseData.Message = "参数不能为空:Ajlsh";
                    return saveResults.ToArray();
                }
                if (string.IsNullOrEmpty(startDetectionW015.Ycy))
                {
                    responseData.Code = "-8";
                    responseData.Message = "参数不能为空:Ycy";
                    return saveResults.ToArray();
                }
                if (string.IsNullOrEmpty(startDetectionW015.Jcxh))
                {
                    responseData.Code = "-8";
                    responseData.Message = "参数不能为空:Jcxh";
                    return saveResults.ToArray();
                }
                string sql = "update LY_Flow_Info set isonline ='1',sxsj=convert(varchar(20),getdate(),120),Ry_05='" + startDetectionW015.Ycy + "',SB_TD ='" + startDetectionW015.Jcxh + "' where Lsh ='" + startDetectionW015.Ajlsh + "'";
                int reI = dbAj.ExecuteNonQuery(sql, null);
                if (reI == 1)
                {
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                else
                {
                    responseData.Code = "-1";
                    responseData.Message = "上线失败：" + reI;
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
        public bool SaveDetectionProcess(string czgc, object obj, string zdbs, DbUtility dbUtility, string ajorhj)
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
                        sqlStr = "update [dbo].[tb_DetectionProcess] set zt ='4' where lsh='" + projectStartW010.Ajlsh + "' and jccs ='" + projectStartW010.Ajjccs + "' and jcxm ='" + projectStartW010.Jyxm + "'";
                        dbUtility.ExecuteNonQuery(sqlStr, null);
                        //项目开始，写入当前条
                        sqlStr = "INSERT INTO [dbo].[tb_DetectionProcess] ";
                        sqlStr += " ([lsh],[jccs],[jcxm],[sbbh],[zt],[kssj],[bcsjsj],[jssj],[jcxh]) VALUES( ";
                        if (ajorhj == "Aj")
                        {
                            sqlStr += " '" + projectStartW010.Ajlsh + "',";//(< lsh, varchar(17),>
                            sqlStr += " '" + projectStartW010.Ajjccs + "',";//,< jccs, int,>
                        }
                        else
                        {
                            sqlStr += " '" + projectStartW010.Hjlsh + "',";//(< lsh, varchar(17),>
                            sqlStr += " '" + projectStartW010.Hjjccs + "',";//,< jccs, int,>
                        }
                        sqlStr += " '" + projectStartW010.Jyxm + "',";//,< jcxm, varchar(8),>
                        sqlStr += " '" + zdbs + "',"; //,< sbbh, varchar(64),>
                        sqlStr += " '" + "1" + "',";// ,< zt, int,>
                        sqlStr += " '" + projectStartW010.Kssj + "',";// ,< kssj, varchar(20),>
                        sqlStr += " '-',";// ,< bcsjsj, varchar(20),>
                        sqlStr += " '-',";// ,< jssj, varchar(20),>
                        sqlStr += " '" + projectStartW010.Jcxh + "')";// ,< jcxh, varchar(8),>)";
                        break;
                    case "2":
                        ProjectData projectData = (ProjectData)obj;
                        sqlStr = "UPDATE [dbo].[tb_DetectionProcess]";
                        sqlStr += " SET [zt] = '" + czgc + "'";
                        sqlStr += " ,[bcsjsj] = convert(varchar(20),getdate(),20)";
                        switch (projectData.Jyxm)
                        {
                            case "NQ":
                                ProjectDataNQ projectDataNQ = JSONHelper.ConvertObject<ProjectDataNQ>(projectData.Jcsj);
                                if (ajorhj == "Aj")
                                {
                                    sqlStr += " where lsh='" + projectDataNQ.Ajlsh + "' and jccs='" + projectDataNQ.Ajjccs + "' and jcxm='" + "NQ" + "' and zt<>'4'";
                                }
                                else
                                {
                                    sqlStr += " where lsh='" + projectDataNQ.Hjlsh + "' and jccs='" + projectDataNQ.Hjjccs + "' and jcxm='" + "NQ" + "' and zt<>'4'";
                                }
                                break;
                            case "UC":
                                ProjectDataUC projectDataUC = JSONHelper.ConvertObject<ProjectDataUC>(projectData.Jcsj);
                                if (ajorhj == "Aj")
                                {
                                    sqlStr += " where lsh='" + projectDataUC.Ajlsh + "' and jccs='" + projectDataUC.Ajjccs + "' and jcxm='" + "UC" + "' and zt<>'4'";
                                }
                                else
                                {
                                    sqlStr += " where lsh='" + projectDataUC.Hjlsh + "' and jccs='" + projectDataUC.Hjjccs + "' and jcxm='" + "UC" + "' and zt<>'4'";
                                }
                                break;
                            case "F1":
                                ProjectDataF1 projectDataF1 = JSONHelper.ConvertObject<ProjectDataF1>(projectData.Jcsj);
                                if (ajorhj == "Aj")
                                {
                                    sqlStr += " where lsh='" + projectDataF1.Ajlsh + "' and jccs='" + projectDataF1.Ajjccs + "' and jcxm='" + "F1" + "' and zt<>'4'";
                                }
                                else
                                {
                                    sqlStr += " where lsh='" + projectDataF1.Hjlsh + "' and jccs='" + projectDataF1.Hjjccs + "' and jcxm='" + "F1" + "' and zt<>'4'";
                                }
                                break;
                            case "C1":
                                ProjectDataC1 projectDataC1 = JSONHelper.ConvertObject<ProjectDataC1>(projectData.Jcsj);
                                sqlStr += " where lsh='" + projectDataC1.Ajlsh + "' and jccs='" + projectDataC1.Ajjccs + "' and jcxm='" + "C1" + "' and zt<>'4'";
                                break;
                            case "DC":
                                ProjectDataDC projectDataDC = JSONHelper.ConvertObject<ProjectDataDC>(projectData.Jcsj);
                                if (ajorhj == "Aj")
                                {
                                    sqlStr += " where lsh='" + projectDataDC.Ajlsh + "' and jccs='" + projectDataDC.Ajjccs + "' and jcxm='" + "DC" + "' and zt<>'4'";
                                }
                                else
                                {
                                    sqlStr += " where lsh='" + projectDataDC.Hjlsh + "' and jccs='" + projectDataDC.Hjjccs + "' and jcxm='" + "DC" + "' and zt<>'4'";
                                }
                                break;
                        }
                        break;
                    case "3":
                        ProjectEndW012 projectEndW012 = (ProjectEndW012)obj;
                        sqlStr = "select * from tb_DetectionProcess where lsh='" + projectEndW012.Ajlsh + "' and jccs='" + projectEndW012.Ajjccs + "' and jcxm='" + projectEndW012.Jyxm + "'";
                        DataTable dt = dbUtility.ExecuteDataTable(sqlStr, null);
                        if (dt.Rows.Count > 0)
                        {
                            sqlStr = "UPDATE [dbo].[tb_DetectionProcess]";
                            sqlStr += " SET [zt] = '" + czgc + "'";
                            sqlStr += " ,[jssj] = '" + projectEndW012.Jssj + "'";
                            if (ajorhj == "Aj")
                            {
                                sqlStr += " where lsh='" + projectEndW012.Ajlsh + "'  and jccs='" + projectEndW012.Ajjccs + "'";
                            }
                            else
                            {
                                sqlStr += " where lsh='" + projectEndW012.Hjlsh + "'  and jccs='" + projectEndW012.Hjjccs + "'";
                            }
                            sqlStr += " and jcxm='" + projectEndW012.Jyxm + "' and zt<>'4'";
                        }
                        else
                        {
                            sqlStr = "INSERT INTO [dbo].[tb_DetectionProcess] ";
                            sqlStr += " ([lsh],[jccs],[jcxm],[sbbh],[zt],[kssj],[bcsjsj],[jssj],[jcxh]) VALUES( ";
                            if (ajorhj == "Aj")
                            {
                                sqlStr += " '" + projectEndW012.Ajlsh + "',";//(< lsh, varchar(17),>
                                sqlStr += " '" + projectEndW012.Ajjccs + "',";//,< jccs, int,>
                            }
                            else
                            {
                                sqlStr += " '" + projectEndW012.Hjlsh + "',";//(< lsh, varchar(17),>
                                sqlStr += " '" + projectEndW012.Hjjccs + "',";//,< jccs, int,>
                            }
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
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 更新检验项目状态 安检数据库
        /// </summary>
        /// <param name="lsh">流水号</param>
        /// <param name="jcxm">检验项目</param>
        /// <param name="status">状态</param>
        /// <param name="jcxh">检测线号</param>
        /// <param name="jyy">检验员</param>
        /// <param name="dbUtility">数据库连接</param>
        /// <returns></returns>
        public bool UpdateJcxmStatusAj(string lsh, string jcxm, string status, string jcxh, string jyy, DbUtility dbUtility)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                string sql = "select * from LY_Flow_Info where Lsh ='" + lsh + "'";
                DataTable dataTable = dbUtility.ExecuteDataTable(sql, null);
                if (dataTable.Rows.Count > 0)
                {
                    string[] jcxmZtArr = dataTable.Rows[0]["jyxmstatus"].ToString().Split(";");
                    foreach (string jcxmZt in jcxmZtArr)
                    {
                        if (jcxmZt.IndexOf(":") > 0)
                        {
                            dictionary.Add(jcxmZt.Split(":")[0], jcxmZt.Split(":")[1]);
                        }
                    }
                }
                if (!dictionary.ContainsKey(jcxm))
                {
                    dictionary.Add(jcxm, status);//不含则加
                }
                else
                {
                    dictionary[jcxm] = status;//含则改
                }
                StringBuilder jcxmZtSB = new StringBuilder();
                foreach (var item in dictionary)
                {
                    jcxmZtSB.Append(item.Key).Append(":").Append(item.Value).Append(";");
                }
                sql = "update  LY_Flow_Info set ";
                switch (jcxm)
                {
                    case "NQ":
                        sql = sql + " jyxmstatus='" + jcxmZtSB.ToString() + "',lwcxstatus = '" + status + "'";

                        break;
                    case "UC":
                        sql = sql + " jyxmstatus='" + jcxmZtSB.ToString() + "',wyxjcstatus = '" + status + "'";

                        break;
                    case "F1":
                        sql = sql + " jyxmstatus='" + jcxmZtSB.ToString() + "',wgstatus = '" + status + "'";
                        if (!string.IsNullOrEmpty(jcxh))
                        {
                            sql += ",Man_TD_WG='" + jcxh + "'";
                        }
                        if (!string.IsNullOrEmpty(jyy))
                        {
                            sql += ",Ry_02='" + jyy + "'";
                        }
                        break;
                    case "C1":
                        sql = sql + " jyxmstatus='" + jcxmZtSB.ToString() + "',dpstatus = '" + status + "'";
                        if (!string.IsNullOrEmpty(jcxh))
                        {
                            sql += ",Man_TD_DP='" + jcxh + "'";
                        }
                        if (!string.IsNullOrEmpty(jyy))
                        {
                            sql += ",Ry_03='" + jyy + "'";
                        }
                        break;
                    case "DC":
                        sql = sql + " jyxmstatus='" + jcxmZtSB.ToString() + "',dpdtstatus = '" + status + "'";
                        if (!string.IsNullOrEmpty(jcxh))
                        {
                            sql += ",Man_TD_DT='" + jcxh + "'";
                        }
                        if (!string.IsNullOrEmpty(jyy))
                        {
                            sql += ",Ry_04='" + jyy + "'";
                        }
                        break;
                    default:
                        return false;
                }
                sql += " where lsh='" + lsh + "'";
                dbUtility.ExecuteNonQuery(sql, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 更新检验项目状态 环检数据库
        /// </summary>
        /// <param name="lsh">流水号</param>
        /// <param name="jcxm">检测项目F1、C1</param>
        /// <param name="status">状态</param>
        /// <param name="jcxh">检测线号</param>
        /// <param name="jyy">检验员</param>
        /// <param name="dbUtility">数据库连接</param>
        /// <returns></returns>
        public bool UpdateJcxmStatusHj(string lsh, string jcxm, string status, string jcxh, string jyy, DbUtility dbUtility)
        {
            try
            {
                string sql = "";
                if (jcxm == "F1")
                {
                    sql = "update LY_Flow_Info set GW_01 ='" + status + "',Wjy ='" + jyy + "',Man_TD_WG ='" + jcxh + "' where Lsh ='" + lsh + "'";
                }
                if (jcxm == "C1")
                {
                    sql = "update LY_Flow_Info set GW_03 ='" + status + "',Djy ='" + jyy + "',Man_TD_DP ='" + jcxh + "' where lsh='" + lsh + "'";
                }
                dbUtility.ExecuteNonQuery(sql, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存安检数据库 Baseinfo_net
        /// </summary>
        /// <param name="vehicleDetailsRegisteW003"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveBaseinfoNetAj(VehicleDetailsRegisteW003 vehicleDetailsRegisteW003, DbUtility dbUtility)
        {
            try
            {
                string sql = "INSERT INTO [dbo].[BaseInfo_Net] ";
                sql += " ([Lsh],[hpzl],[hphm],[clpp1],[clxh],[clpp2],[gcjk],[zzg],[zzcmc],[clsbdh],[fdjh],[cllx] ";
                sql += " ,[csys],[syxz],[sfzmhm],[sfzmmc],[syr],[ccdjrq],[djrq],[yxqz],[qzbfqz],[fzjg],[glbm],[bxzzrq],[zt] ";
                sql += " ,[dybj],[fdjxh],[rlzl],[pl],[gl],[zxxs],[cwkc],[cwkk],[cwkg],[hxnbcd],[hxnbkd],[hxnbgd],[gbthps] ";
                sql += " ,[zs],[zj],[qlj],[hlj],[ltgg],[lts],[zzl],[zbzl],[hdzzl],[hdzk],[zqyzl],[qpzk],[hpzk],[hbdbqk],[ccrq] ";
                sql += " ,[clyt],[ytsx],[xszbh],[jyhgbzbh],[xzqh],[zsxzqh],[zzxzqh],[sgcssbwqk],[sfmj],[bmjyy],[sfxny],[xnyzl],[sfazwb],[wbzl],[qxclzhxx]) VALUES( ";
                sql += " '" + vehicleDetailsRegisteW003.Ajlsh + "',";// (< Lsh, varchar(32),>
                sql += " '" + vehicleDetailsRegisteW003.Hpzl + "',";// ,< hpzl, varchar(2),>
                sql += " '" + vehicleDetailsRegisteW003.Hphm + "',";// ,< hphm, varchar(15),>
                sql += " '" + vehicleDetailsRegisteW003.Clpp1 + "',";// ,< clpp1, varchar(32),>
                sql += " '" + vehicleDetailsRegisteW003.Clxh + "',";// ,< clxh, varchar(32),>
                sql += " '" + vehicleDetailsRegisteW003.Clpp2 + "',";//  ,< clpp2, varchar(32),>
                sql += " '" + vehicleDetailsRegisteW003.Gcjk + "',";//  ,< gcjk, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Zzg + "',";//  ,< zzg, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Zzcmc + "',";//  ,< zzcmc, varchar(64),>
                sql += " '" + vehicleDetailsRegisteW003.Clsbdh + "',";// ,< clsbdh, varchar(25),>
                sql += " '" + vehicleDetailsRegisteW003.Fdjh + "',";//  ,< fdjh, varchar(30),>
                sql += " '" + vehicleDetailsRegisteW003.Cllx + "',";// ,< cllx, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Csys + "',";// ,< csys, varchar(5),>
                sql += " '" + vehicleDetailsRegisteW003.Syxz + "',";//  ,< syxz, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Sfzmhm + "',";//  ,< sfzmhm, varchar(18),>
                sql += " '" + vehicleDetailsRegisteW003.Sfzmmc + "',";//  ,< sfzmmc, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Syr + "',";//  ,< syr, varchar(128),>
                sql += " '" + vehicleDetailsRegisteW003.Ccdjrq + "',";//  ,< ccdjrq, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Djrq + "',";//  ,< djrq, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Yxqz + "',";//  ,< yxqz, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Qzbfqz + "',";//  ,< qzbfqz, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Fzjg + "',";//  ,< fzjg, varchar(10),>
                sql += " '" + vehicleDetailsRegisteW003.Glbm + "',";//  ,< glbm, varchar(12),>
                sql += " " + vehicleDetailsRegisteW003.Bxzzrq + "',";//  ,< bxzzrq, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Zt + "',";//  ,< zt, varchar(6),>
                sql += " '" + vehicleDetailsRegisteW003.Dybj + "',";//  ,< dybj, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Fdjxh + "',";//  ,< fdjxh, varchar(64),>
                sql += " '" + vehicleDetailsRegisteW003.Rlzl + "',";//  ,< rlzl, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Pl + "',";//  ,< pl, varchar(6),>
                sql += " '" + vehicleDetailsRegisteW003.Gl + "',";//  ,< gl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Zxxs + "',";//  ,< zxxs, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Cwkc + "',";//  ,< cwkc, varchar(5),>
                sql += " '" + vehicleDetailsRegisteW003.Cwkk + "',";//  ,< cwkk, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Cwkg + "',";//  ,< cwkg, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Hxnbcd + "',";//  ,< hxnbcd, varchar(5),>
                sql += " '" + vehicleDetailsRegisteW003.Hxnbkd + "',";// ,< hxnbkd, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Hxnbgd + "',";//  ,< hxnbgd, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Gbthps + "',";//  ,< gbthps, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Zs + "',";//  ,< zs, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Zj + "',";//  ,< zj, varchar(5),>
                sql += " '" + vehicleDetailsRegisteW003.Qlj + "',";//  ,< qlj, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Hlj + "',";//  ,< hlj, varchar(4),>
                sql += " '" + vehicleDetailsRegisteW003.Ltgg + "',";//  ,< ltgg, varchar(64),>
                sql += " '" + vehicleDetailsRegisteW003.Lts + "',";//  ,< lts, varchar(2),>
                sql += " '" + vehicleDetailsRegisteW003.Zzl + "',";//  ,< zzl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Zbzl + "',";//  ,< zbzl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Hdzzl + "',";//  ,< hdzzl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Hdzk + "',";//  ,< hdzk, varchar(3),>
                sql += " '" + vehicleDetailsRegisteW003.Zqyzl + "',";//  ,< zqyzl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Qpzk + "',";// ,< qpzk, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Hpzk + "',";// ,< hpzk, varchar(2),>
                sql += " '" + vehicleDetailsRegisteW003.Hbdbqk + "',";// ,< hbdbqk, varchar(128),>
                sql += " '" + vehicleDetailsRegisteW003.Ccrq + "',";//  ,< ccrq, varchar(24),>
                sql += " '" + vehicleDetailsRegisteW003.Clyt + "',";//  ,< clyt, varchar(2),>
                sql += " '" + vehicleDetailsRegisteW003.Ytsx + "',";//  ,< ytsx, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Xszbh + "',";// ,< xszbh, varchar(20),>
                sql += " '" + vehicleDetailsRegisteW003.Jyhgbzbh + "',";//  ,< jyhgbzbh, varchar(20),>
                sql += " '" + vehicleDetailsRegisteW003.Xzqh + "',";//  ,< xzqh, varchar(10),>
                sql += " '" + vehicleDetailsRegisteW003.Zsxzqh + "',";//  ,< zsxzqh, varchar(10),>
                sql += " '" + vehicleDetailsRegisteW003.Zzxzqh + "',";//  ,< zzxzqh, varchar(10),>
                sql += " '" + "" + "',";//  ,< sgcssbwqk, varchar(4000),>
                sql += " '" + vehicleDetailsRegisteW003.Sfmj + "',";//  ,< sfmj, varchar(1),>
                sql += " '" + "" + "',";//  ,< bmjyy, varchar(4000),>
                sql += " '" + "" + "',";//  ,< sfxny, varchar(1),>
                sql += " '" + "" + "',";//  ,< xnyzl, varchar(1),>
                sql += " '" + vehicleDetailsRegisteW003.Sfazwb + "',";//  ,< sfazwb, varchar(2),>
                sql += " '" + vehicleDetailsRegisteW003.Wbzl + "',";//  ,< wbzl, varchar(8),>
                sql += " '" + vehicleDetailsRegisteW003.Qxclzhxx + "')";//  ,< qxclzhxx, varchar(4000),>)";
                dbUtility.ExecuteNonQuery(sql, null);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存安检数据库 Baseinfo_Hand
        /// </summary>
        /// <param name="v"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveBaseinfoHandAj(VehicleDetailsRegisteW003 v, DbUtility dbUtility)
        {
            try
            {
                string sqlStr = "INSERT INTO [dbo].[BaseInfo_Hand] ";
                sqlStr += " ([Lsh],[hpzl],[hphm],[QZDZ],[QDFS],[ZXJFS],[DGTZFS],[SYCH],[SFKZ],[XSLC],[Max_SD],[ZJDW],[BSQXS],[ZDFS] ";
                sqlStr += " ,[WRZY],[QGS],[RyGG],[WXDW],[JGRQ],[Lsdh],[Lxdz],[Aj_Veh_Type],[hpys],[qdzhw],[qdzhsh],[zhzhsh],[zhchzhw] ";
                sqlStr += " ,[edzhs],[ajywlb],[zjywlb],[hjywlb],[jdchsshlb],[jcxlb],[qzhsh],[zhchzhsh],[wgchx],[chych],[bzhzhw] ";
                sqlStr += " ,[zhxzhsh],[zjjylb],[yyzhh],[zjlsh],[sfjf],[sfkp],[tb],[sfgp],[dzss],[kqxjzw],[zhchlsh],[hjxm],[sjr],[sjrsfzh] ";
                sqlStr += " ,[sjrdh],[pqgsh],[JcDate],[JcTime],[Jqfs],[Gyfs],[Pfjd],[Dws]) VALUES( ";
                sqlStr += " '" + v.Ajlsh + "',";// (< Lsh, varchar(32),>
                sqlStr += " '" + v.Hpzl + "',";// ,< hpzl, varchar(2),>
                sqlStr += " '" + v.Hphm + "',";// ,< hphm, varchar(15),>
                sqlStr += " '" + v.Qzdz + "',";// ,< QZDZ, varchar(3),>
                sqlStr += " '" + v.Qdfs + "',";// ,< QDFS, varchar(32),>
                sqlStr += " '" + v.ZXJFS + "',";// ,< ZXJFS, varchar(2),>
                sqlStr += " '" + v.DGTZFS + "',";// ,< DGTZFS, varchar(2),>
                sqlStr += " '" + "-" + "',";// ,< SYCH, varchar(2),>
                sqlStr += " '" + v.SFKZ + "',";// ,< SFKZ, varchar(2),>
                sqlStr += " '" + v.XSLC + "',";// ,< XSLC, varchar(12),>
                sqlStr += " '" + v.Max_SD + "',";// ,< Max_SD, varchar(8),>
                sqlStr += " '" + v.Zjdw + "',";// ,< ZJDW, varchar(16),>
                sqlStr += " '" + v.BSQXS + "',";// ,< BSQXS, varchar(16),>
                sqlStr += " '" + v.Zdfs + "',";// ,< ZDFS, varchar(16),>
                sqlStr += " '" + "-" + "',";// ,< WRZY, varchar(2),>
                sqlStr += " '" + v.Qgs + "',";// ,< QGS, varchar(8),>
                sqlStr += " '" + v.Rygg + "',";// ,< RyGG, varchar(32),>
                sqlStr += " '" + "" + "',";// ,< WXDW, varchar(128),>
                sqlStr += " '" + "" + "',";// ,< JGRQ, varchar(32),>
                sqlStr += " '" + v.Lsdh + "',";// ,< Lsdh, varchar(18),>
                sqlStr += " '" + v.Lxdz + "',";// ,< Lxdz, varchar(200),>
                sqlStr += " '" + v.Aj_Veh_Type + "',";// ,< Aj_Veh_Type, varchar(8),>
                sqlStr += " '" + v.Hpys + "',";// ,< hpys, varchar(8),>
                sqlStr += " '" + v.Qdzhw + "',";// ,< qdzhw, varchar(8),>
                sqlStr += " '" + v.Qdzhsh + "',";// ,< qdzhsh, int,>
                sqlStr += " '" + v.Zhzhsh + "',";// ,< zhzhsh, int,>
                sqlStr += " '" + v.Zhchzhw + "',";// ,< zhchzhw, varchar(8),>
                sqlStr += " '" + v.Edzhs + "',";// ,< edzhs, varchar(8),>
                sqlStr += " '" + v.Ajywlb + "',";// ,< ajywlb, varchar(8),>
                sqlStr += " '" + v.Zjywlb + "',";// ,< zjywlb, varchar(8),>
                sqlStr += " '" + v.Hjywlb + "',";// ,< hjywlb, varchar(8),>
                sqlStr += " '" + v.Jdchsshlb + "',";// ,< jdchsshlb, varchar(8),>
                sqlStr += " '" + v.Jcxlb + "',";// ,< jcxlb, varchar(8),>
                sqlStr += " '" + v.Qzhsh + "',";// ,< qzhsh, int,>
                sqlStr += " '" + v.Zhchzhsh + "',";// ,< zhchzhsh, int,>
                sqlStr += " '" + v.Wgchx + "',";// ,< wgchx, varchar(8),>
                sqlStr += " '" + v.Chych + "',";// ,< chych, varchar(8),>
                sqlStr += " '" + v.Bzhzhw + "',";// ,< bzhzhw, varchar(8),>
                sqlStr += " '" + v.Zhxzhsh + "',";// ,< zhxzhsh, varchar(8),>
                sqlStr += " '" + v.Zjjylb + "',";// ,< zjjylb, varchar(8),>
                sqlStr += " '" + v.Yyzhh + "',";// ,< yyzhh, varchar(24),>
                sqlStr += " '" + v.Zjlsh + "',";// ,< zjlsh, varchar(50),>
                sqlStr += " '" + "0" + "',";// ,< sfjf, varchar(2),>
                sqlStr += " '" + "0" + "',";// ,< sfkp, varchar(2),>
                sqlStr += " '" + "0" + "',";// ,< tb, varchar(2),>
                sqlStr += " '" + "0" + "',";// ,< sfgp, varchar(2),>
                sqlStr += " '" + v.Dzss + "',";// ,< dzss, varchar(2),>
                sqlStr += " '" + v.Kqxjzw + "',";// ,< kqxjzw, varchar(8),>
                sqlStr += " '" + "" + "',";// ,< zhchlsh, varchar(24),>
                sqlStr += " '" + "-" + "',";// ,< hjxm, varchar(24),>
                sqlStr += " '" + v.Sjr + "',";// ,< sjr, varchar(20),>
                sqlStr += " '" + v.Sjrsfzh + "',";// ,< sjrsfzh, varchar(50),>
                sqlStr += " '" + v.Sjrdh + "',";// ,< sjrdh, varchar(20),>
                sqlStr += " '" + v.Pqgsh + "',";// ,< pqgsh, varchar(8),>
                sqlStr += " '" + v.JcDate + "',";// ,< JcDate, varchar(16),>
                sqlStr += " '" + v.JcTime + "',";// ,< JcTime, varchar(16),>
                sqlStr += " '" + v.Jqfs + "',";// ,< Jqfs, varchar(8),>
                sqlStr += " '" + v.Gyfs + "',";// ,< Gyfs, varchar(8),>
                sqlStr += " '" + v.Pfjd + "',";// ,< Pfjd, varchar(8),>
                sqlStr += " '" + v.Dws + "')";// ,< Dws, int,>)";
                dbUtility.ExecuteNonQuery(sqlStr, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存安检数据库 LY_Flow_Info
        /// </summary>
        /// <param name="autoInfo"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        private bool SaveFlowInfoAj(VehicleDetailsRegisteW003 autoInfo, DbUtility dbUtility)
        {
            string sqlStr = "";
            try
            {
                // '先删除已存在的
                sqlStr = "delete from [dbo].[LY_Flow_Info] where Lsh='" + autoInfo.Ajlsh + "'";
                int reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                if (reInt < 0)
                    return false;
                // '保存新增的
                sqlStr = "INSERT INTO [dbo].[LY_Flow_Info]";
                sqlStr += " ([Lsh],ajywlb,ajjccs,ajjcxm,[Jccs],[AddRq],[JcXm],[JcJg],[Jc_S],[Ry_01],[Ry_02],[Ry_03],[Ry_04]";
                sqlStr += " ,[Ry_05],[Ry_06],[Ry_07],[GW_01],[GW_02],[GW_03],[GW_04],[GW_05],[GW_06]";
                sqlStr += " ,[GW_07],[GW_08],[GW_09],[GW_10],[GW_11],[GW_12],[Man_TD_WG],[Man_TD_DP]";
                sqlStr += " ,[Man_TD_DT],[Man_TD_LS],[SB_TD],wgstatus,dpstatus,dpdtstatus,wkccstatus,lszdstatus,isonline,zzcc,zjlsh,zbzlstatus,lwcxstatus,wyxjcstatus,jyxmstatus,jyxmjg,jczt) VALUES";
                sqlStr += " ('" + autoInfo.Ajlsh + "',"; // ' (<Lsh, varchar(17),>
                sqlStr += " '" + autoInfo.Ajywlb + "',"; // '  ,<ajywlb, varchar(8),>
                sqlStr += " '" + autoInfo.Ajjccs.ToString() + "',"; // '  ,<ajjccs, int,>
                sqlStr += " '" + autoInfo.Ajjccs.ToString() + "',"; // '  ,<ajjccs, int,>
                sqlStr += " '" + autoInfo.JcxmAj + "',"; // '  ,<ajjcxm, varchar(128),>
                sqlStr += " '" + autoInfo.Ajjccs.ToString() + "',"; // '  ,<Jccs, varchar(4),>
                sqlStr += "  getdate(),"; // ' ,<AddRq, datetime,>
                sqlStr += " '" + autoInfo.JcxmAj + "',"; // ' ,<JcXm, varchar(128),>
                string jcjg = "";
                jcjg = jcjg.PadLeft(autoInfo.JcxmAj.Split(",").Count() - 1, '-');
                string jc_s = "";
                jc_s = jc_s.PadLeft(autoInfo.JcxmAj.Split(",").Count() - 1, '0');
                sqlStr += " '" + jcjg + "',"; // ' ,<JcJg, varchar(128),>
                sqlStr += " '" + jc_s + "',"; // ' ,<Jc_S, varchar(128),>
                sqlStr += " '" + autoInfo.Dly + "',"; // ' ,<Ry_01, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_02, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_03, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_04, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_05, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_06, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<Ry_07, varchar(12),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_01, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_02, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_03, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_04, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_05, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_06, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_07, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_08, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_09, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_10, varchar(2),>
                sqlStr += " '" + "-" + "',"; // '  ,<GW_11, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<GW_12, varchar(2),>
                sqlStr += " '" + "-" + "',"; // ' ,<Man_TD_WG, varchar(8),>
                sqlStr += " '" + "-" + "',"; // ' ,<Man_TD_DP, varchar(8),>
                sqlStr += " '" + "-" + "',"; // ' ,<Man_TD_DT, varchar(8),>
                sqlStr += " '" + "-" + "',"; // ' ,<Man_TD_LS, varchar(8),>
                sqlStr += " '" + "-" + "',"; // ' ,<SB_TD, varchar(8),>)"
                if (autoInfo.JcxmAj.IndexOf("F1") >= 0)
                    sqlStr += " '" + "0" + "',"; // ' , [wgstatus] [varchar](2) NULL,			--外观检测状态
                else
                    sqlStr += " '" + "-" + "',";// ' , [wgstatus] [varchar](2) NULL,			--外观检测状态
                if (autoInfo.JcxmAj.IndexOf("C1") >= 0)
                    sqlStr += " '" + "0" + "',"; // ' ,[dpstatus] [varchar](2) NULL,			--底盘检测状态
                else
                    sqlStr += " '" + "-" + "',";// ' ,[dpstatus] [varchar](2) NULL,			--底盘检测状态
                if (autoInfo.JcxmAj.IndexOf("DC") >= 0)
                    sqlStr += " '" + "0" + "',"; // ' ,[dpdtstatus] [varchar](2) NULL,			--底盘动态检测状态
                else
                    sqlStr += " '" + "-" + "',";// ' ,[dpdtstatus] [varchar](2) NULL,			--底盘动态检测状态
                                                // wkccstatus
                if (autoInfo.JcxmAj.IndexOf("M1") >= 0)
                    sqlStr += " '" + "0" + "',"; // ' ,[wkccstatus] [varchar](2) NULL,			--外廓尺寸检测状态
                else
                    sqlStr += " '" + "-" + "',";// ' ,[wkccstatus] [varchar](2) NULL,			--外廓尺寸检测状态
                if (autoInfo.JcxmAj.IndexOf("R") >= 0)
                    sqlStr += " '" + "0" + "',"; // ' ,[lszdstatus] [varchar](2) NULL,			--路试检测状态
                else
                    sqlStr += " '" + "-" + "',";// ' ,[lszdstatus] [varchar](2) NULL,			--路试检测状态
                sqlStr += " '" + "0" + "',"; // ' ,<isonline, varchar(8),>)"
                if (autoInfo.JcxmAj.IndexOf("ZZ") >= 0)
                    sqlStr += " '" + "1" + "',"; // ' ,<zzcc, varchar(8),>)"
                else
                    sqlStr += " '" + "0" + "',";// ' ,<zzcc, varchar(8),>)"
                sqlStr += " '" + "-" + "',"; // ' ,<zjlsh, varchar(8),>)"
                if (autoInfo.JcxmAj.IndexOf("Z1") >= 0)
                    sqlStr += " '0',"; // ' ,<zbzlstatus, varchar(8),>)"
                else
                    sqlStr += " '" + "-" + "',";// ' ,<zbzlstatus, varchar(8),>)"
                if (autoInfo.JcxmAj.IndexOf("NQ") >= 0)
                    sqlStr += " '0',"; // ' ,<lwcxstatus, varchar(8),>)"
                else
                    sqlStr += " '" + "-" + "',";// ' ,<lwcxstatus, varchar(8),>)"
                if (autoInfo.JcxmAj.IndexOf("UC") >= 0)
                    sqlStr += " '0',"; // ' ,<wyxjcstatus, varchar(8),>)"
                else
                    sqlStr += " '" + "-" + "',";// ' ,<wyxjcstatus, varchar(8),>)"
                string jcxmjg = "";
                for (int i = 0; i <= autoInfo.JcxmAj.Split(",").Count() - 2; i += 1)
                    jcxmjg += autoInfo.JcxmAj.Split(",")[i] + ":-;";
                string jcxmzt = "";
                for (int i = 0; i <= autoInfo.JcxmAj.Split(",").Count() - 2; i += 1)
                    jcxmzt += autoInfo.JcxmAj.Split(",")[i] + ":0;";
                sqlStr += " '" + jcxmzt + "',"; // ' ,<jyxmstatus, varchar(8),>)"
                sqlStr += " '" + jcxmjg + "',"; // ' ,<jyxmjg, varchar(8),>)"
                sqlStr += " '" + "-" + "')"; // ' ,<jczt, varchar(8),>)"
                reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                if (reInt != 1)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存安检数据库 QcyJcDateFlow
        /// </summary>
        /// <param name="autoInfo"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        private bool SaveFlowdataAj(VehicleDetailsRegisteW003 autoInfo, DbUtility dbUtility)
        {
            try
            {
                string sqlStr;
                // '先删除对应流水号，当前次数
                sqlStr = "delete from [dbo].[QcyJcDateFlow] where Lsh='" + autoInfo.Ajlsh + "' and Jccs='" + autoInfo.Ajjccs + "'";
                int reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                if (reInt < 0)
                    return false;
                // '添加一条新的
                sqlStr = "INSERT INTO [dbo].[QcyJcDateFlow]";
                sqlStr += " ([Lsh],ajywlb,ajjccs,ajjcxm,[Jccs],[Jcxm],[Jcrq],[JcTime],[JcKsTime],[JcJsTime]";
                sqlStr += " ,[JcXH],[JcJg],[Ry_01],[Ry_02],[Ry_03],[Ry_04],[Ry_05]";
                sqlStr += " ,[Ry_06],[Ry_07],zjlsh) VALUES";
                sqlStr += " ('" + autoInfo.Ajlsh + "',"; // ' (<Lsh, varchar(17),>
                sqlStr += " '" + autoInfo.Ajywlb + "',"; // '  ,<ajywlb, varchar(8),>
                sqlStr += " '" + autoInfo.Ajjccs + "',"; // '  ,<ajjccs, int,>
                sqlStr += " '" + autoInfo.JcxmAj + "',"; // '  ,<ajjcxm, varchar(128),>
                sqlStr += " '" + autoInfo.Ajjccs + "',"; // ' ,<Jccs, int,>
                sqlStr += " '" + autoInfo.JcxmAj + "',"; // ' ,<Jcxm, varchar(128),>
                sqlStr += " '" + DateTime.Now.ToString("yyyy-MM-dd") + "',"; // ' ,<Jcrq, date,>
                sqlStr += " '" + DateTime.Now.ToString("HH:mm:ss") + "',"; // ' ,<JcTime, time(7),>
                sqlStr += " '" + DateTime.Now.ToString() + "',"; // ' ,<JcKsTime, datetime,>
                sqlStr += " '" + "" + "',"; // ' ,<JcJsTime, datetime,>
                sqlStr += " '" + "" + "',"; // ' ,<JcXH, varchar(4),>
                sqlStr += " '" + "" + "',"; // ' ,<JcJg, varchar(24),>
                sqlStr += " '" + autoInfo.Dly + "',"; // ' ,<Ry_01, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_02, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_03, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_04, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_05, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_06, varchar(12),>
                sqlStr += " '" + "" + "',"; // ' ,<Ry_07, varchar(12),>
                sqlStr += " '" + "-" + "')"; // ' ,<zjlsh, varchar(12),>
                reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                if (reInt != 1)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存安检数据库 QcyJcDateCover
        /// </summary>
        /// <param name="autoInfo"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        private bool SaveCoverdataAj(VehicleDetailsRegisteW003 autoInfo, DbUtility dbUtility)
        {
            try
            {
                string sqlStr = "";
                int reInt = -1;
                // '查询是否存在
                sqlStr = "select * from [dbo].[QcyJcDateCover] where Lsh ='" + autoInfo.Ajlsh + "'";
                DataTable reDt = dbUtility.ExecuteDataTable(sqlStr, null);
                if (reDt.Rows.Count > 0)
                {
                    // '存在，更新
                    sqlStr = "UPDATE [dbo].[QcyJcDateCover] set ";
                    sqlStr += " jccs='" + autoInfo.Ajjccs + "',"; // ',[Jccs] = <Jccs, int,>
                    sqlStr += " jcrq='" + DateTime.Now.ToString("yyyy-MM-dd") + "',"; // ',[Jcrq] = <Jcrq, date,>
                    sqlStr += " JcTime='" + DateTime.Now.ToString("HH:mm:ss") + "',"; // ',[JcTime] = <JcTime, time(7),>
                    sqlStr += " JcKsTime='" + DateTime.Now.ToString() + "',"; // ',[JcKsTime] = <JcKsTime, datetime,>
                    sqlStr += " Ry_01='" + autoInfo.Dly + "'"; // ',[Ry_01] = <Ry_01, varchar(12),>
                    sqlStr += " where lsh='" + autoInfo.Ajlsh + "'"; // ' WHERE <搜索条件,,>"
                    reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                    if (reInt != 1)
                        return false;
                }
                else
                {
                    // '不存在，添加
                    sqlStr = "INSERT INTO [dbo].[QcyJcDateCover]";
                    sqlStr += " ([Lsh],ajywlb,ajjccs,ajjcxm,zjywlb,zjjccs,zjjcxm,hjywlb,hjjccs,hjjcxm,[Jccs],[Jcxm],[Jcrq],[JcTime],[JcKsTime],[JcJsTime],[JcXH],[JcJg]";
                    sqlStr += " ,[Ry_01],[Ry_02],[Ry_03],[Ry_04],[Ry_05],[Ry_06],[Ry_07],zjlsh,printWj) VALUES";
                    sqlStr += " ('" + autoInfo.Ajlsh + "',"; // '(<Lsh, varchar(17),>                    
                    sqlStr += " '" + autoInfo.Ajywlb + "',"; // '  ,<ajywlb, varchar(8),>
                    sqlStr += " '" + autoInfo.Ajjccs + "',"; // '  ,<ajjccs, int,>
                    sqlStr += " '" + autoInfo.JcxmAj + "',"; // '  ,<ajjcxm, varchar(128),>                    
                    sqlStr += " '" + autoInfo.Ajjccs + "',"; // ' ,<Jccs, int,>
                    sqlStr += " '" + autoInfo.JcxmAj + "',"; // ',<Jcxm, varchar(128),>
                    sqlStr += " '" + DateTime.Now.ToString("yyyy-MM-dd") + "',"; // ',<Jcrq, date,>
                    sqlStr += " '" + DateTime.Now.ToString("HH:mm:ss") + "',"; // ',<JcTime, time(7),>
                    sqlStr += " '" + DateTime.Now.ToString() + "',"; // ',<JcKsTime, datetime,>
                    sqlStr += " '" + "" + "',"; // ',<JcJsTime, datetime,>
                    sqlStr += " '" + "" + "',"; // ',<JcXH, varchar(4),>
                    sqlStr += " '" + "" + "',"; // ',<JcJg, varchar(24),>
                    sqlStr += " '" + autoInfo.Dly + "',"; // ',<Ry_01, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_02, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_03, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_04, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_05, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_06, varchar(12),>
                    sqlStr += " '" + "" + "',"; // ',<Ry_07, varchar(12),>)"
                    sqlStr += " '" + "-" + "',"; // ',<zjlsh, varchar(12),>)"
                    if (autoInfo.JcxmAj.IndexOf("F1") >= 0)
                        sqlStr += " '0')";
                    else
                        sqlStr += " '1')";
                    reInt = dbUtility.ExecuteNonQuery(sqlStr, null);
                    if (reInt != 1)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 保存环保数据库 Baseinfo_net
        /// </summary>
        /// <param name="v"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        public bool SaveBaseinfoNetHj(VehicleDetailsRegisteW003 v, DbUtility dbUtility)
        {
            try
            {
                string CLLXEP = "";
                if (Convert.ToDouble(v.Zzl) >= 3500)
                {
                    if (Convert.ToDouble(v.Zzl) <= 2500 && Convert.ToInt32(v.Hdzk) <= 6)
                    {
                        if (v.Cllx.IndexOf("H") >= 0)
                        {
                            CLLXEP = "MN2";//                  二类车
                        }
                        else
                        {
                            CLLXEP = "M11";
                        }
                    }
                    else
                    {
                        CLLXEP = "MN2";
                    }
                }
                else
                {
                    CLLXEP = "MN3";
                }
                string SqlStr;
                SqlStr = "Delete From BaseInfo_Net where lsh='" + v.Hjlsh + "' and hpzl='" + v.Hpzl + "' and hphm='" + v.Hphm + "'";
                dbUtility.ExecuteNonQuery(SqlStr, null);
                SqlStr = "";
                SqlStr = "insert into  BaseInfo_Net ";
                SqlStr = SqlStr + "values (";
                SqlStr = SqlStr + "'" + v.Hjlsh + "',"; // Lsh	varchar(17), 	---	机动车序号	char	14
                SqlStr = SqlStr + "'" + v.Hpzl + "',"; // hpzl	varchar(2),	--号牌种类	char	2
                SqlStr = SqlStr + "'" + v.Hphm + "',"; // hphm	varchar(15),	--号牌号码	varchar2	15
                SqlStr = SqlStr + "'" + v.Clpp1 + "',"; // clpp1	varchar(32),	--中文品牌	varchar2	32
                SqlStr = SqlStr + "'" + v.Clxh + "',"; // clxh	varchar(32),	---	车辆型号	varchar2	32
                SqlStr = SqlStr + "'" + v.Clpp2 + "',"; // clpp2	varchar(32),	---英文品牌	varchar2	32
                SqlStr = SqlStr + "'" + v.Gcjk + "',"; // gcjk	varchar(1),	---国产/进口	char	1
                SqlStr = SqlStr + "'" + v.Hpys + "',"; // zzg	varchar(3),	---	制造国	char	3   ----作为号牌颜色
                SqlStr = SqlStr + "'" + v.Zzcmc + "',"; // zzcmc	varchar(64),	--制造厂名称	varchar2	64
                SqlStr = SqlStr + "'" + v.Clsbdh + "',"; // clsbdh	varchar(25),	--车辆识别代号	varchar2	25
                SqlStr = SqlStr + "'" + v.Fdjh + "',"; // fdjh	varchar(30),	--发动机号	varchar2	30
                SqlStr = SqlStr + "'" + v.Cllx + "',"; // cllx	varchar(3),	---车辆类型	char	3
                SqlStr = SqlStr + "'" + v.Csys + "',"; // csys	varchar(5),	---车身颜色	varchar2	5
                SqlStr = SqlStr + "'" + v.Syxz + "',"; // syxz	varchar(1),	---使用性质	char	1
                SqlStr = SqlStr + "'" + "-" + "',"; // sfzmhm	varchar(18),	--身份证明号码	varchar2	18
                SqlStr = SqlStr + "'" + "-" + "',"; // sfzmmc	varchar(1),	--身份证明名称	char	1
                SqlStr = SqlStr + "'" + v.Syr + "',"; // syr	varchar(128),	---机动车所有人	varchar2	128
                SqlStr = SqlStr + "'" + v.Ccdjrq + "',"; // ccdjrq	varchar(24),	--初次登记日期	date	7
                SqlStr = SqlStr + "'" + v.Djrq + "',"; // djrq	varchar(24),	--最近定检日期	date	7
                SqlStr = SqlStr + "'" + v.Yxqz + "',"; // yxqz	varchar(24),	--检验有效期止	date	7
                SqlStr = SqlStr + "'" + v.Qzbfqz + "',"; // qzbfqz	varchar(24),	---强制报废期止	date	7
                SqlStr = SqlStr + "'" + "-" + "',"; // fzjg	varchar(10),	----	发证机关	varchar2	10
                SqlStr = SqlStr + "'" + "-" + "',"; // glbm	varchar(12),	---	管理部门	varchar2	12
                SqlStr = SqlStr + "'" + v.Bxzzrq + "',"; // bxzzrq	varchar(24),	---保险终止日期	date	7
                SqlStr = SqlStr + "'" + v.Zt + "',"; // zt	varchar(6),	---	机动车状态	varchar2	6
                SqlStr = SqlStr + "'" + v.Dybj + "',"; // dybj	varchar(1),	--抵押标记0-未抵押，1-已抵押	char	1
                SqlStr = SqlStr + "'" + v.Fdjxh + "',"; // fdjxh	varchar(64),	---发动机型号	varchar2	64
                SqlStr = SqlStr + "'" + v.Rlzl + "',"; // rlzl	varchar(3),	---燃料种类	varchar2	3
                SqlStr = SqlStr + "'" + v.Pl + "',"; // pl	varchar(6),	---排量	number	6
                SqlStr = SqlStr + "'" + v.Gl + "',"; // gl	varchar(8),	--功率	number	5,1
                SqlStr = SqlStr + "'" + v.Zxxs + "',"; // zxxs	varchar(1),	---转向形式	char	1
                SqlStr = SqlStr + "'" + v.Cwkc + "',"; // cwkc	varchar(5),	---车外廓长	number	5
                SqlStr = SqlStr + "'" + v.Cwkk + "',"; // cwkk	varchar(4),	---车外廓宽	number	4
                SqlStr = SqlStr + "'" + v.Cwkg + "',"; // cwkg	varchar(4),	---车外廓高	number	4
                SqlStr = SqlStr + "'" + v.Hxnbcd + "',"; // hxnbcd	varchar(5),	--货箱内部长度	number	5
                SqlStr = SqlStr + "'" + v.Hxnbkd + "',"; // hxnbkd	varchar(4),	---货箱内部宽度	number	4
                SqlStr = SqlStr + "'" + v.Hxnbgd + "',"; // hxnbgd	varchar(4),	---货箱内部高度	number	4
                SqlStr = SqlStr + "'" + v.Gbthps + "',"; // gbthps	varchar(3),	---钢板弹簧片数	number	3
                SqlStr = SqlStr + "'" + v.Zs + "',"; // zs	varchar(3),	---轴数	number	1
                SqlStr = SqlStr + "'" + v.Zj + "',"; // zj	varchar(5),	---轴距	number	5
                SqlStr = SqlStr + "'" + v.Qlj + "',"; // qlj	varchar(4),	---前轮距	number	4
                SqlStr = SqlStr + "'" + v.Hlj + "',"; // hlj	varchar(4),	---后轮距	number	4
                SqlStr = SqlStr + "'" + v.Ltgg + "',"; // ltgg	varchar(64),	---轮胎规格	varchar2	64
                SqlStr = SqlStr + "'" + v.Lts + "',"; // lts	varchar(2),	---轮胎数	number	2
                SqlStr = SqlStr + "'" + v.Zzl + "',"; // zzl	varchar(8),	---总质量	number	8
                SqlStr = SqlStr + "'" + v.Zbzl + "',"; // zbzl	varchar(8),	---整备质量	number	8
                SqlStr = SqlStr + "'" + v.Hdzzl + "',"; // hdzzl	varchar(8),	---核定载质量	number	8
                SqlStr = SqlStr + "'" + v.Hdzk + "',"; // hdzk	varchar(3),	---核定载客	number	3
                SqlStr = SqlStr + "'0',"; // DPF         zqyzl	varchar(8),	---准牵引总质量	number	8
                SqlStr = SqlStr + "'" + v.DPF + "',"; // DPF型号     qpzk	varchar(1),	---驾驶室前排载客人数	number	1
                SqlStr = SqlStr + "'" + v.SCR + "',"; // SCR	        varchar(2),	---驾驶室后排载客人数	number	2
                SqlStr = SqlStr + "'" + v.Pfjd + "',"; // SCR型号   	varchar(128),	---环保达标情况	varchar2	128
                SqlStr = SqlStr + "'" + v.Ccrq + "',"; // ccrq	varchar(24),	---出厂日期	date	7
                SqlStr = SqlStr + "'" + v.Clyt + "',"; // clyt	varchar(2),	---车辆用途	char	2
                SqlStr = SqlStr + "'" + v.Ytsx + "',"; // ytsx	varchar(1),	---用途属性	char	1
                SqlStr = SqlStr + "'" + v.Xszbh + "',"; // xszbh	varchar(20),	---行驶证证芯编号	varchar2	20
                SqlStr = SqlStr + "'" + "-" + "',"; // jyhgbzbh	varchar(20),	---检验合格标志	varchar2	20
                SqlStr = SqlStr + "'" + v.Gyfs + "',"; // 供油方式 'xzqh	varchar(10),	---管理辖区	varchar2	10
                SqlStr = SqlStr + "'" + v.Pqgsh + "',"; // zsxzqh	varchar(10),	---住所地址行政区划	varchar2	10
                SqlStr = SqlStr + "'" + v.Xzqh + "',"; // zzxzqh	varchar(10),	---联系地址行政区划	varchar2	10
                SqlStr = SqlStr + "'" + v.Qzdz + "',"; // QZDZ(varchar(3), --前照灯制)
                SqlStr = SqlStr + "'" + v.Qdfs.Substring(1, 1) + "',"; // QDFS(varchar(32), ---驱动方式)
                SqlStr = SqlStr + "'" + v.ZXJFS + "',"; // ZXJFS(varchar(2), ---转向角方式)
                SqlStr = SqlStr + "'" + "1" + "',"; // DGTZFS(varchar(2), --前照灯调整)
                SqlStr = SqlStr + "'" + CLLXEP + "',"; // CLSSLB(varchar(32), --车辆所属类别) 车辆类型(EP)  M11-第一类轻型汽车,MN2 -第二类轻型汽车,MN3 -重型汽车
                SqlStr = SqlStr + "'" + v.Hclfs + "',"; // SYCH	varchar(2),	--是否三元催化 (0 无三元催化。1带有三元催化)
                SqlStr = SqlStr + "'" + "0" + "',"; // SFKZ  0空载、1满载
                SqlStr = SqlStr + "'" + v.XSLC + "',"; // XSLC(varchar(12), ---行驶里程数)
                if (Convert.ToDouble(v.Max_SD) <= 70)
                    SqlStr = SqlStr + "'" + "0" + "',"; // Max_SD(varchar(8), ---设计最大行驶速度)
                else if (Convert.ToDouble(v.Max_SD) < 100)
                    SqlStr = SqlStr + "'" + "1" + "',"; // Max_SD(varchar(8), ---设计最大行驶速度)
                else
                    SqlStr = SqlStr + "'" + "2" + "',";// Max_SD(varchar(8), ---设计最大行驶速度)
                SqlStr = SqlStr + "'" + v.Dws + "',"; // & "'" & GetDm_FromStr(ComboBox22.Text, "28") & "'," ' ZJDW(varchar(16), ---直接档档位)
                SqlStr = SqlStr + "'" + v.BSQXS + "',"; // BSQXS(varchar(16), ---变速器形式)
                SqlStr = SqlStr + "'" + v.Zdfs + "',"; // ZDFS	varchar(16),	---制动方式  0-气压制动，1-液压制动，2-气推油制动	 
                SqlStr = SqlStr + "'" + v.Jqfs + "',"; // WRZY	varchar(2),	---涡轮增压（0 自然吸气式，1 涡然增压式)	
                SqlStr = SqlStr + "'" + v.Qgs + "',";  // 气缸数 YHXZ(varchar(8),  --油耗限值)
                SqlStr = SqlStr + "'" + v.Rygg + "',"; // 燃油规格  YYZHDM(varchar(32), --营运证号)  & "#"
                SqlStr = SqlStr + "'" + v.Gcjk + "',"; // WXDW(varchar(128), ---维修单位)  作为国产进口
                SqlStr = SqlStr + "'" + v.Hjywlb + "',"; // JGRQ(varchar(16), ---峻工日期) 作为业务类别
                SqlStr = SqlStr + "'" + "-" + "',"; // Bz01   		    varchar(128),---， 		   ---备注
                SqlStr = SqlStr + "'" + v.Edzhs + "',"; // Bz02   		    varchar(128),---， 		   ---备注 作为额定转速
                SqlStr = SqlStr + "'" + v.Lsdh + "')"; // Bz03 varchar(128) - --备注  作为国产进口
                dbUtility.ExecuteNonQuery(SqlStr, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存环检数据库 LY_Flow_Info
        /// </summary>
        /// <param name="v"></param>
        /// <param name="dbUtility"></param>
        /// <returns></returns>
        private bool SaveJcFlowHj(VehicleDetailsRegisteW003 v, DbUtility dbUtility)
        {
            string SqlStr;
            string JcxmStr;            // '
            string JcGGStr;
            string JcZTStr;
            string Vams_Pf_Cx = "E";
            string TwoWq_Pf_CxFl = "G";
            try
            {
                SqlStr = "insert into  LY_Flow_Info (Lsh,hpzl,hphm,Cpcx,Fdjh,VIN,Cllx,SYR,JcDate,Jctime,Jccs,JcXH,Cyc,QZDZ,QDFS,ZXJFS,DGTZFS,";
                SqlStr = SqlStr + "ZZL,ZbZl,HDZK,ZS,CCRQ,DjRq,GBCX,DgSfTz,JcXm,JcJg,Jc_S,Dly,InBz_01,GW_01,GW_02,GW_03,GW_04,GW_10,GW_05,GW_06,GW_07,Jclb,InBz_02,InBz_03)";
                SqlStr = SqlStr + "values (";
                // [ID] [int] IDENTITY(1,1) NOT NULL,			--数据编号，自增量
                SqlStr = SqlStr + "'" + v.Hjlsh + "',"; // Lsh(varchar(17), ---机动车序号)
                SqlStr = SqlStr + "'" + v.Hpzl + "',";  // hpzl(varchar(2), --号牌种类)
                SqlStr = SqlStr + "'" + v.Hphm + "',";  // hphm(varchar(15), --号牌号码)
                SqlStr = SqlStr + "'" + v.Clpp1 + "',";  // Cpcx(varchar(64), --厂牌车型)
                SqlStr = SqlStr + "'" + v.Fdjh + "',";  // Fdjh(varchar(48), --发动机号)
                SqlStr = SqlStr + "'" + v.Clsbdh + "',"; // VIN(varchar(18), --VIN号)
                SqlStr = SqlStr + "'" + v.Cllx + "',"; // Cllx(varchar(3), --车辆类型)
                SqlStr = SqlStr + "'" + v.Syr + "',"; // SYR(varchar(128), --车辆所有人)
                SqlStr = SqlStr + "'" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "',"; // JcDate(varchar(16), ---检测日期(初次检测时间))
                SqlStr = SqlStr + "'" + DateTime.Now.ToString("HH:mm:ss") + "',";  // Jctime(varchar(16), ---检测时间)              
                SqlStr = SqlStr + "'" + v.Hjjccs + "',";// Jccs(varchar(4), --检测次数)
                SqlStr = SqlStr + "'" + v.Jcxh + "',"; // JcXH(varchar(4), --检测线号)
                SqlStr = SqlStr + "'" + v.Chych + "',"; // Cyc		varchar(4),	--乘用车(0 非乘用车 1 乘用车)
                SqlStr = SqlStr + "'" + v.Qzdz + "',"; // QZDZ(varchar(3), --前照灯制(1 - 四灯远近光, 2 - 四灯远光, 3 - 二灯远近光, 4 - 二灯近光, 5 - 一灯远光))
                SqlStr = SqlStr + "'" + v.Qdfs.Substring(1, 1) + "',"; // QDFS(varchar(32), ---驱动方式)
                SqlStr = SqlStr + "'" + v.ZXJFS + "',"; // ZXJFS(varchar(2), ---转向角方式(0 - 独立悬架, 1 - 非独立悬架))
                SqlStr = SqlStr + "'0',"; // DGTZFS(varchar(2), --前照灯调整(0 - 不能单独调整, 1 - 单独调整))
                SqlStr = SqlStr + "'" + v.Zzl + "',"; // ZZL(varchar(12), ---总质量)
                SqlStr = SqlStr + "'" + v.Zbzl + "',"; // ZbZl(varchar(12), ---整备质量)
                SqlStr = SqlStr + "'" + v.Hdzk + "',"; // HDZK(varchar(8), --核定载客)
                if (v.Hjywlb == "2")
                    SqlStr = SqlStr + "'02',"; // '         ZS(varchar(8), ---轴数(  改成拍照车型种类 ))
                else
                    SqlStr = SqlStr + "'01',";// '         ZS(varchar(8), ---轴数(  改成拍照车型种类 ))
                SqlStr = SqlStr + "'" + v.Ccrq + "',"; // '         CCRQ(varchar(8), --出厂日期)
                SqlStr = SqlStr + "'" + v.Ccdjrq + "',"; // '         DjRq(Varchar(8), --初次登记日期)
                SqlStr = SqlStr + "'" + TwoWq_Pf_CxFl + "',"; // 'GBCX(varchar(4), ---国标车型   双怠速车型)
                SqlStr = SqlStr + "'" + Vams_Pf_Cx + "',"; // 'DgSfTz(varchar(8), --灯光是否在线调整) ---作为VMAS 的车型
                SqlStr = SqlStr + "'" + v.JcxmHJ + "',"; // JcXm	varchar(24),	 --检测项目（检测项目组合：111111111111111111111)	
                                                         // ---外观、动态、底盘、车速、一轴、二轴、三轴、四轴、五轴、驻车、左灯、右灯、侧滑
                                                         // ----加载1，加载2，加载3，加载4，加载5，整备质量，外廓尺寸,行车路试，坡道路试，车速路试 
                SqlStr = SqlStr + "'" + "" + "',"; // JcJg  varchar(24),     ---检测结果 (结果的组(合 0000000000000000000 )  （1 代表合格，2代表不合格。0 代表没有检测）
                SqlStr = SqlStr + "'" + "" + "',"; // Jc_S	varchar(24),   ---检测状态 (检测状态组合000000000000000000) （0代表示检。1代表正在检测。2代表检测完毕，9代检测失败）
                SqlStr = SqlStr + "'" + v.Dly + "',"; // Dly(varchar(16),  ---登录人员)
                SqlStr = SqlStr + "'" + v.Hjdlsj + "',"; // InBz_01(varchar(128), ---备用信息字)
                if (v.JcxmHJ.IndexOf("F1") >= 0)
                    SqlStr = SqlStr + "'" + "0" + "',"; // GW_01 (状态1) 外观检测标志
                else
                    SqlStr = SqlStr + "'" + "2" + "',";// GW_01 (状态1) 外观检测标志
                if (v.JcxmHJ.IndexOf("PF") >= 0)
                    SqlStr = SqlStr + "'" + "0" + "',"; // GW_02(状态2)  线内检测标志
                else
                    SqlStr = SqlStr + "'" + "2" + "',";// GW_02(状态2)  线内检测标志
                if (v.JcxmHJ.IndexOf("C1") >= 0)
                    SqlStr = SqlStr + "'" + "0" + "',"; // GW_03(状态3)  底盘检测标志
                else
                    SqlStr = SqlStr + "'" + "2" + "',";// 
                if (v.JcxmHJ.IndexOf("OBD") >= 0)
                {
                    SqlStr = SqlStr + "'" + "0" + "',"; // GW_04(状态3)  OBD
                    SqlStr = SqlStr + "'" + "0" + "',"; // GW_10(状态3)  OBD
                }
                else
                {
                    SqlStr = SqlStr + "'" + "3" + "',"; // 
                    SqlStr = SqlStr + "'" + "1" + "',"; // GW_10(状态3)  OBD
                }
                SqlStr = SqlStr + "'" + v.Dly + "',"; // GW_05  暂存新系统标志
                if (v.Hjywlb == "2")
                    SqlStr = SqlStr + "'新车',"; // '         GW_06(varchar(8), ---轴数(  改成拍照车型种类 ))
                else
                    SqlStr = SqlStr + "'在用',";// '         GW_06(varchar(8), ---轴数(  改成拍照车型种类 ))
                SqlStr = SqlStr + "'1',";
                SqlStr = SqlStr + "'" + v.Hjywlb + "',"; // 'jclbStr
                SqlStr = SqlStr + "'" + "" + "',";
                SqlStr = SqlStr + "'" + v.Hjdlsj + "')";
                dbUtility.ExecuteNonQuery(SqlStr, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
