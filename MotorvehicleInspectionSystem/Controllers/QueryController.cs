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
using System.ServiceModel;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Controllers
{
    public class QueryController : Controller
    {
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="responseData">接口响应的参数实体类，可改变的</param>
        /// <returns>用户类结合</returns>
        public User[] LYYDJKR001(ResponseData responseData)
        {
            List<User> users = new List<User>();
            try
            {
                DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = string.Format("select *,STUFF((SELECT ',' + roleDm FROM Tb_UserRole WHERE username =a.UserName  FOR xml path('')),1,1,'') as RoleDm from Tab_UserInfo A");
                users = db.QueryForList<User>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException ex)
            {
                responseData.Code = "-99";
                responseData.Message = ex.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return users.ToArray();

        }
        /// <summary>
        /// 根据号牌号码（可选的）查询机动车队列
        /// </summary>
        /// <param name="requestData">接口请求的参数实体类</param>
        /// <param name="responseData">接口响应的参数实体类，可改变的</param>
        /// <returns>机动车队列集合</returns>
        public VehicleQueue[] LYYDJKR002(RequestData requestData, ResponseData responseData)
        {
            List<VehicleQueue> vehicleQueues = new List<VehicleQueue>();
            try
            {
                QueryVehicleCriteria queryVehQueueR002 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                string sql = "select t1.Lsh as Lsh,t2.hphm as Hphm ";
                sql += " ,(select mc from jscscode where dm = t2.hpzl and fl = '09') as HpzlCc ,t2.hpzl as Hpzl ";
                sql += " ,(select mc from jscscode where dm = t2.hpys and fl = '26') as HpysCc ,t2.hpys as Hpys ";
                sql += " ,convert(varchar(10), convert(datetime, t1.jcrq), 120) as Djrq ";
                sql += " ,(case when t1.jczt = '0' then '待检' when t1.jczt = '2' then '线上结束' when t1.jczt = '3' and(t1.shzt = '0' or t1.shzt is null)  then '过程结束' when t1.jczt = '3' and t1.shzt = '1' then '已审核' else '未知' end) as Jyzt ";
                sql += " ,(select mc from jscscode where dm=t1.ajywlb and fl='08') as ajywlbCc ,t1.ajywlb as ajywlb ";
                sql += " ,case t1.hjywlb when '-' then '-' else (select mc from jscscode where dm = t1.hjywlb and fl = '31') end as hjywlbCc ,t1.hjywlb as hjywlb ";
                sql += " ,(case when t1.ajywlb <> '-' and t1.hjywlb <> '-' then '同检' when t1.ajywlb <> '-' and t1.hjywlb = '-' then '安检' when t1.ajywlb = '-' and t1.hjywlb <> '-' then '环检' else '未知' end ) as Ywlb ";
                sql += " from QcyJcDateCover t1,BaseInfo_Hand t2 ";
                sql += " where t1.Lsh = t2.Lsh ";
                if (queryVehQueueR002.Hphm == "")
                {
                    sql += " and convert(varchar(10),convert(datetime, t1.jcrq),120) = convert(varchar(10), GETDATE(), 120) ";
                }
                else
                {
                    sql += " and t2.hphm like '%" + queryVehQueueR002.Hphm + "%' ";
                }
                sql += " order by t1.Jcrq desc, t1.JcTime ";
                DataTable dataTableAj = dbAj.ExecuteDataTable(sql, null);
                sql = " select t1.Lsh as Lsh,t2.hphm as Hphm ";
                sql += " ,(select mc from jscscode where dm = t2.hpzl and fl = '09') as HpzlCc ,t2.hpzl as Hpzl ";
                sql += " ,(select mc from jscscode where dm = t2.gcjk and fl = '36') as HpysCc ,t2.gcjk as Hpys ";
                sql += " ,convert(varchar(10), convert(datetime, t1.JcDate), 120) as Djrq ";
                sql += " ,'' as Jyzt ";
                sql += " ,'-' as ajywlbCc ,'-' as ajywlb ";
                sql += " ,(select mc from jscscode where dm = t1.Jclb and fl = '08') as hjywlbCc,t1.Jclb as hjywlb ";
                sql += " ,'环检' as ywlb ";
                sql += " from LY_Flow_Info t1,BaseInfo_Net t2 ";
                sql += " where t1.Lsh = t2.Lsh ";
                if (queryVehQueueR002.Hphm == "")
                {
                    sql += " and convert(varchar(10),convert(datetime, t1.JcDate),120) = convert(varchar(10), GETDATE(), 120) ";
                }
                else
                {
                    sql += " and t2.hphm like '%" + queryVehQueueR002.Hphm + "%' ";
                }
                sql += " order by t1.JcDate desc, t1.JcTime ";
                DataTable dataTableHj = dbHj.ExecuteDataTable(sql, null);
                if (dataTableAj.Rows.Count == 0 && dataTableHj.Rows.Count == 0)
                {
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                else
                {
                    //获取两个数据源的并集
                    var comparer = new CustomComparerR002();
                    IEnumerable<DataRow> QBJ = dataTableAj.AsEnumerable().Union(dataTableHj.AsEnumerable(), comparer);
                    //两个数据源的并集集合
                    DataTable DTBJ = QBJ.CopyToDataTable();
                    vehicleQueues = EntityReader.GetEntities<VehicleQueue>(DTBJ);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }

            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return vehicleQueues.ToArray();
        }
        /// <summary>
        /// 查询数据字典，以安检数据库为准
        /// </summary>
        /// <param name="requestData">接口请求的参数实体类</param>
        /// <param name="responseData">接口响应的参数实体类,可改变的</param>
        /// <returns></returns>
        public DataDictionary[] LYYDJKR003(RequestData requestData, ResponseData responseData)
        {
            List<DataDictionary> dataDictionaries = new List<DataDictionary>();
            try
            {
                QueryDataDR003 queryDataDR003 = JSONHelper.ConvertObject<QueryDataDR003>(requestData.Body[0]);

                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);

                string sql = "select * from JsCsCode ";

                sql += " where 1=1  ";
                if (queryDataDR003.Fl != "" && queryDataDR003.Fl != null)
                {
                    sql += " and fl = '" + queryDataDR003.Fl + "'";
                }
                if (queryDataDR003.Dm != "" && queryDataDR003.Dm != null)
                {
                    sql += " and dm = '" + queryDataDR003.Dm + "' ";
                }
                if (queryDataDR003.Mc != "" && queryDataDR003.Mc != null)
                {
                    sql += " and mc = '" + queryDataDR003.Mc + "' ";
                }
                dataDictionaries = dbAj.QueryForList<DataDictionary>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return dataDictionaries.ToArray();
        }
        /// <summary>
        /// 查询所有商品条目，供收费和开票使用
        /// </summary>
        /// <param name="requestData">接口请求的参数</param>
        /// <param name="responseData">接口响应的参数，可改变的</param>
        /// <returns>收费条目的集合</returns>
        public ChargeItem[] LYYDJKR004(RequestData requestData, ResponseData responseData)
        {
            List<ChargeItem> chargeItems = new List<ChargeItem>();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = "select * from JsCsCode where fl='99' ";
                chargeItems = dbAj.QueryForList<ChargeItem>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return chargeItems.ToArray();
        }
        /// <summary>
        /// 查询机动车详细信息
        /// </summary>
        /// <param name="requestData">接口请求的参数</param>
        /// <param name="responseData">接口响应的参数，可改变的</param>
        /// <returns></returns>
        public VehicleDetails[] LYYDJKR005(RequestData requestData, ResponseData responseData)
        {
            List<VehicleDetails> vehicleDetails = new List<VehicleDetails>();
            try
            {
                QueryVehicleCriteria queryVehicleDetailsR005 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if ((queryVehicleDetailsR005.Lsh == "" || queryVehicleDetailsR005.Lsh is null)
                    && (queryVehicleDetailsR005.Hpzl == "" || queryVehicleDetailsR005.Hphm == "" || queryVehicleDetailsR005.Hpzl is null || queryVehicleDetailsR005.Hphm is null)
                    && (queryVehicleDetailsR005.Clsbdh == "" || queryVehicleDetailsR005.Clsbdh is null)
                    && (queryVehicleDetailsR005.Xszbh == "" || queryVehicleDetailsR005.Xszbh is null))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空";
                }
                else
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                        //按流水号查询
                        string sql = "select * from BaseInfo_Net tn,BaseInfo_Hand th where tn.lsh=th.Lsh ";
                        if (queryVehicleDetailsR005.Lsh != "" && !(queryVehicleDetailsR005.Lsh is null))
                        {
                            sql += " and tn.Lsh ='" + queryVehicleDetailsR005.Lsh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //号牌号码，号牌种类
                        if (queryVehicleDetailsR005.Hphm != "" && queryVehicleDetailsR005.Hpzl != "" && !(queryVehicleDetailsR005.Hphm is null) && !(queryVehicleDetailsR005.Hpzl is null))
                        {
                            sql += "and tn.hphm ='" + queryVehicleDetailsR005.Hphm.Trim().Replace("'", "").Replace("-", "") + "' and tn.hpzl ='" + queryVehicleDetailsR005.Hpzl.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //车辆识别代号
                        if (queryVehicleDetailsR005.Clsbdh != "" && !(queryVehicleDetailsR005.Clsbdh is null))
                        {
                            sql += "and tn.clsbdh like '%" + queryVehicleDetailsR005.Clsbdh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //行驶证编号
                        if (queryVehicleDetailsR005.Xszbh != "" && !(queryVehicleDetailsR005.Xszbh is null))
                        {
                            sql += "and tn.xszbh ='" + queryVehicleDetailsR005.Xszbh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        vehicleDetails = dbAj.QueryForList<VehicleDetails>(sql, null);
                    }
                    //安检没查到，查环保
                    if (VehicleInspectionController.SyHj == "1" && vehicleDetails.Count == 0)
                    {
                        DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);

                    }
                    //安检查询到，再次确认环检业务类型
                    else if (VehicleInspectionController.SyHj == "1")
                    {

                    }
                    responseData.RowNum = vehicleDetails.Count;
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
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
            return vehicleDetails.ToArray();
        }
        /// <summary>
        /// 查询机动车检验项目情况
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public InspectionItemsR006[] LYYDJKR006(RequestData requestData, ResponseData responseData)
        {
            //List<InspectionItemCarInfo> inspectionItemCarInfos = new List<InspectionItemCarInfo>();
            //InspectionItemCarInfo inspectionItemCarInfo = new InspectionItemCarInfo();
            List<InspectionItemsR006> inspectionItemsR006s = new List<InspectionItemsR006>();
            try
            {
                QueryVehicleCriteria queryByLSH = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (queryByLSH.Lsh == "" || queryByLSH.Lsh is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空";
                    return inspectionItemsR006s.ToArray();
                }
                if (VehicleInspectionController.SyAj == "1")
                {
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    //按流水号查询
                    string sql = "";
                    sql = " select * from LY_Flow_Info where Lsh ='" + queryByLSH.Lsh + "'";
                    DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
                    if (dataTable.Rows.Count == 0)
                    {
                        responseData.Code = "-3";
                        responseData.Message = "不存在对应流水号的检测信息。";
                        return inspectionItemsR006s.ToArray();
                    }
                    //inspectionItemCarInfo.Lsh = dataTable.Rows[0]["lsh"].ToString();
                    //inspectionItemCarInfo.Jccs = dataTable.Rows[0]["jccs"].ToString();
                    //inspectionItemCarInfo.Ajywlb = dataTable.Rows[0]["ajywlb"].ToString();
                    //inspectionItemCarInfo.Hjywlb = dataTable.Rows[0]["hjywlb"].ToString();

                    string jccs = dataTable.Rows[0]["jccs"].ToString();
                    string ajywlb = dataTable.Rows[0]["ajywlb"].ToString();
                    string hjywlb = dataTable.Rows[0]["hjywlb"].ToString();

                    string[] jcxmZt = dataTable.Rows[0]["jyxmstatus"].ToString().Split(";");
                    if (jcxmZt.Length > 1)
                    {
                        int id = 0;
                        foreach (string jcxmZtS in jcxmZt)
                        {
                            id++;
                            string jcxmS = jcxmZtS.Split(":")[0];
                            if (jcxmS != "")
                            {
                                string ztS = jcxmZtS.Split(":")[1];
                                if (ztS == "1")
                                {
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006();
                                    switch (jcxmS)
                                    {
                                        case "NQ":
                                        case "UC":
                                        case "F1":
                                        case "C1":
                                        case "DC":
                                            sql = "select  ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                            sql += " , dalb as xmbh,(select mc from jscscode where fl='83' and dm=dalb ) as xmmc";
                                            sql += " ,lsh,'" + jccs + "' as jccs,'" + ajywlb + "' as ajywlb,'" + hjywlb + "' as hjywlb";
                                            sql += " ,jcxh";
                                            sql += " ,jcry_01";
                                            sql += " ,jcry_02";
                                            sql += " ,CONVERT(datetime, SUBSTRING(kstime, 1, 4) + '/' + SUBSTRING(kstime, 5, 2) + '/' + SUBSTRING(kstime, 7, 2) + ' ' + SUBSTRING(kstime, 9, 2) + ':' + SUBSTRING(kstime, 11, 2) + ':' + SUBSTRING(kstime, 13, 2), 120) as jckssj";
                                            sql += " ,CONVERT(datetime, SUBSTRING(jstime, 1, 4) + '/' + SUBSTRING(jstime, 5, 2) + '/' + SUBSTRING(jstime, 7, 2) + ' ' + SUBSTRING(jstime, 9, 2) + ':' + SUBSTRING(jstime, 11, 2) + ':' + SUBSTRING(jstime, 13, 2), 120) as jcjssj";
                                            sql += " ,case jcpj when '1' then '合格' when '-1' then '不合格' else '其他' end as  jcpj ";
                                            sql += " ,'完成' as jczt from JcData_RG";
                                            sql += " where lsh = '" + queryByLSH.Lsh + "'";
                                            sql += " and dalb = '" + jcxmS + "' ";
                                            inspectionItemsR006 = dbAj.QueryForObject<InspectionItemsR006>(sql, null);
                                            break;
                                        case "R1":
                                        case "R2":
                                            break;
                                    }
                                    inspectionItemsR006.ID = id;
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                                else if (ztS == "0")
                                {
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006();
                                    inspectionItemsR006.Lsh = queryByLSH.Lsh;
                                    inspectionItemsR006.Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                    inspectionItemsR006.Ajywlb = ajywlb;
                                    inspectionItemsR006.Hjywlb = hjywlb;
                                    inspectionItemsR006.Jczt = "未检";
                                    inspectionItemsR006.Xmbh = jcxmS;
                                    inspectionItemsR006.ID = id;
                                    inspectionItemsR006.Xmmc = dbAj.QueryForObject<DataDictionary>("select * from jscscode where fl='83' and dm='" + jcxmS + "'", null).Mc;
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                                else if (ztS == "2")
                                {
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006();
                                    inspectionItemsR006.Lsh = queryByLSH.Lsh;
                                    inspectionItemsR006.Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                    inspectionItemsR006.Jczt = "正在检测";
                                    inspectionItemsR006.Xmbh = jcxmS;
                                    inspectionItemsR006.Ajywlb = ajywlb;
                                    inspectionItemsR006.Hjywlb = hjywlb;
                                    inspectionItemsR006.ID = id;
                                    inspectionItemsR006.Xmmc = dbAj.QueryForObject<DataDictionary>("select * from jscscode where fl='83' and dm='" + jcxmS + "'", null).Mc;
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                //inspectionItemCarInfo.inspectionItems = inspectionItemsR006s.ToArray ();
                //inspectionItemCarInfos.Add(inspectionItemCarInfo);
                responseData.RowNum = inspectionItemsR006s.Count;
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
            return inspectionItemsR006s.ToArray();
        }
        /// <summary>
        /// 根据流水号查询检验照片，不限制检验测次数，默认查询相同照片的最大次数
        /// </summary>
        /// <param name="requestData">接口请求的参数</param>
        /// <param name="responseData">接口响应的参数，可改变的</param>
        /// <returns></returns>
        public UploadPic[] LYYDJKR007(RequestData requestData, ResponseData responseData)
        {
            List<UploadPic> uploadPics = new List<UploadPic>();
            try
            {
                QueryVehicleCriteria queryUploadPicR007 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum, *,(select Pic_TypeStr from [dbo].[Pic_Sj_Bt_Pic_Type] where Pic_Num=t1.Zpzl ) zpzlmc ";
                sql += " from UpLoad_Pic t1,(select zpzl, max(Jycs) jycs from UpLoad_Pic ";
                sql += " where lsh = '" + queryUploadPicR007.Lsh + "' ";
                sql += " group by zpzl) t2 ";
                sql += " where t1.lsh = '" + queryUploadPicR007.Lsh + "' ";
                sql += " and t1.Zpzl = t2.Zpzl and t1.Jycs = t2.jycs ";
                uploadPics = dbAj.QueryForList<UploadPic>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return uploadPics.ToArray();
        }
        /// <summary>
        /// 查询流水号，检测次数对应检验视频信息
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public UploadAVI[] LYYDJKR008(RequestData requestData, ResponseData responseData)
        {
            List<UploadAVI> uploadAVIs = new List<UploadAVI>();
            try
            {
                QueryVehicleCriteria queryUploadAVIR008 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum,* ,(select mc from jscscode where fl='84' and dm=xmbh ) spmc, jcrq +' '+ TimS  as jcsj  ";
                sql += " from UpLoad_AVI_XML where jcbh = '" + queryUploadAVIR008.Lsh + "'";
                if (queryUploadAVIR008.Jccs != 0)
                {
                    sql += " and jklx ='" + queryUploadAVIR008.Jccs.ToString() + "'";
                }
                sql += " order by JcJsSj ";
                uploadAVIs = dbAj.QueryForList<UploadAVI>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return uploadAVIs.ToArray();
        }
        /// <summary>
        /// 查询当天入场的车辆队列，包含预检状态
        /// </summary>
        /// <param name="requestData">接口请求的参数</param>
        /// <param name="responseData">接口响应的参数，可改变的</param>
        /// <returns></returns>
        public InCar[] LYYDJKR009(RequestData requestData, ResponseData responseData)
        {
            List<InCar> inCars = new List<InCar>();
            if (VehicleInspectionController.SyUb == "")
            {
                try
                {
                    DbUtility dbUB = new DbUtility(VehicleInspectionController.ConstrUB, DbProviderType.SqlServer);
                    string sql = "select carNumber hphm,addTime rcsj ";
                    sql += " ,case yjzt when '1' then '已预检' else '未预检' end as yjzt ";
                    sql += " from Tb_InCar ";
                    sql += " where convert(varchar(10), convert(datetime, addTime), 120) = convert(varchar(10), GETDATE(), 120) ";
                    sql += " order by addTime desc ";
                    inCars = dbUB.QueryForList<InCar>(sql, null);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                catch (ArgumentNullException)
                {
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                catch (Exception e)
                {
                    responseData.Code = "-99";
                    responseData.Message = e.Message;
                }
            }
            else
            {
                responseData.Code = "0";
                responseData.Message = "不具备预检功能！";
            }
            return inCars.ToArray();
        }

        /// <summary>
        /// 查询服务器时间（时间同步）
        /// </summary>
        /// <param name="responseData">接口响应的参数，可改变的</param>
        /// <returns></returns>
        public ServerTimeR011[] LYYDJKR011(ResponseData responseData)
        {
            List<ServerTimeR011> dateTimes = new List<ServerTimeR011>();
            try
            {
                ServerTimeR011 serverTimeR011 = new ServerTimeR011();
                string sql = string.Format("select GETDATE() as Sj");
                DateTime dateTime = DateTime.Now;
                if (VehicleInspectionController.SyAj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    dateTimes = db.QueryForList<ServerTimeR011>(sql, null);
                }
                else if (VehicleInspectionController.SyHj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    dateTimes = db.QueryForList<ServerTimeR011>(sql, null);
                }
                else if (VehicleInspectionController.SyUb == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrUB, DbProviderType.SqlServer);
                    dateTimes = db.QueryForList<ServerTimeR011>(sql, null);
                }
                else
                {
                    serverTimeR011.Sj = dateTime;
                    dateTimes.Add(serverTimeR011);
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
                responseData.RowNum = dateTimes.Count;
            }
            catch (ArgumentNullException ex)
            {
                responseData.Code = "-99";
                responseData.Message = ex.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return dateTimes.ToArray();

        }
        /// <summary>
        /// 查询数据库系统参数
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public SystemParameter[] LYYDJKR015(ResponseData responseData)
        {
            List<SystemParameter> systemParametes = new List<SystemParameter>();
            try
            {
                string sql = "";
                SystemParameter systemParameter;
                if (VehicleInspectionController.SyAj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    sql = "select top 1 Jyjgbh,Jcsjyxq,Jcsjbcnx,Web_Pass,Dw_Xkzh,Dw_Dhhm,Dw_mc,Dw_dz,SFF,KPF,LshSzm,'AJ' as sjlb from SystermCs_All ";
                    systemParameter = db.QueryForObject<SystemParameter>(sql, null);
                    systemParametes.Add(systemParameter)
;
                }
                if (VehicleInspectionController.SyHj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    sql = "select top 1 bz1 as Jyjgbh,JcDateYouXiao as Jcsjyxq,'' as Jcsjbcnx,bz2 as Web_Pass,Dw_Xkzh,Dw_Dhhm,Report_Head as Dw_mc,''as Dw_dz,''as SFF,'' as KPF,'' as LshSzm ,'HJ' as sjlb from SystermCs_All ";
                    systemParameter = db.QueryForObject<SystemParameter>(sql, null);
                    systemParametes.Add(systemParameter);
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
                responseData.RowNum = systemParametes.Count;
            }
            catch (ArgumentNullException ex)
            {
                responseData.Code = "-99";
                responseData.Message = ex.Message;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return systemParametes.ToArray();
        }

        /// <summary>
        /// 根据流水号查询机动车人工检验项目详细信息
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public ArtificialProjectR016[] LYYDJKR016(RequestData requestData, ResponseData responseData)
        {
            List<ArtificialProjectR016> artificialProjectR016s = new List<ArtificialProjectR016>();
            ArtificialProjectR016 artificialProjectR016 = new ArtificialProjectR016();
            string sql = "";
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (queryCriteria.Lsh == "" || queryCriteria.Lsh is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Lsh）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Jyxm == "" || queryCriteria.Jyxm is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Jyxm）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Ajywlb == "" || queryCriteria.Ajywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Hjywlb == "" || queryCriteria.Hjywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Hjywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                        //先查询外检车型
                        sql = " select wgchx,ajywlb  from BaseInfo_Hand where lsh='" + queryCriteria.Lsh + "'";
                        DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count == 0)
                        {
                            responseData.Code = "-3";
                            responseData.Message = "不存在对应流水号的检测车辆信息。（安检）";
                            return artificialProjectR016s.ToArray();
                        }
                        string wjcx = dataTable.Rows[0][0].ToString();
                        string ajywlb = dataTable.Rows[0][1].ToString();
                        if (wjcx == "")
                        {
                            responseData.Code = "-99";
                            responseData.Message = "对应流水号的车辆不存在外检车型信息。（安检）";
                            return artificialProjectR016s.ToArray();
                        }
                        //确定检验项目
                        //根据外检车型，检验项目查询人工项目详细--默认合格
                        switch (queryCriteria.Jyxm)
                        {
                            case "NQ":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                sql += " ,cartype ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm";
                                sql += " ,t1.itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from [dbo].[tb_cartypeanditemdm] t1,[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where cartype = '" + wjcx + "' and t1.itemdm = t2.Dm  and t2.Fl = '5' order by t1.itemdm";
                                break;
                            case "UC":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                sql += " ,cartype ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t1.itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from [dbo].[tb_cartypeanditemdm] t1,[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where cartype = '" + wjcx + "' and t1.itemdm = t2.Dm  and t2.Fl = '6' ";
                                if (ajywlb == "00")
                                {
                                    sql += "  and t1.itemdm <>'02'";
                                }
                                else
                                {
                                    sql += "  and t1.itemdm <>'04'";
                                }
                                sql += " order by t1.itemdm";
                                break;
                            case "F1":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                sql += " ,cartype ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t1.itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from [dbo].[tb_cartypeanditemdm] t1,[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where cartype = '" + wjcx + "' and t1.itemdm = t2.Dm  and t2.Fl = '1' order by t1.itemdm";
                                break;
                            case "C1":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                sql += " ,cartype ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t1.itemdm ";
                                sql += " ,mc as Xmms ,'-' as Sycx";
                                sql += " from [dbo].[tb_cartypeanditemdm] t1,[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where cartype = '" + wjcx + "' and t1.itemdm = t2.Dm  and t2.Fl = '2' order by t1.itemdm";
                                break;
                            case "DC":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                                sql += " ,cartype ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t1.itemdm ";
                                sql += " ,mc as Xmms ,'-' as Sycx";
                                sql += " from [dbo].[tb_cartypeanditemdm] t1,[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where cartype = '" + wjcx + "' and t1.itemdm = t2.Dm  and t2.Fl = '3' order by t1.itemdm";
                                break;
                            default:
                                responseData.Code = "-99";
                                responseData.Message = "不可识别的检验项目（Jyxm）。";
                                return artificialProjectR016s.ToArray();
                        }
                        artificialProjects = dbAj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016.ID = 1;
                        artificialProjectR016.Jyyw = "安检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return artificialProjectR016s.ToArray();
                    }
                }
                if (queryCriteria.Hjywlb != "-" && queryCriteria.Jyxm == "F1")
                {
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                        DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                        sql = "select zs from LY_Flow_Info where Lsh='" + queryCriteria.Lsh + "'";
                        DataTable dataTable = dbHj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count == 0)
                        {
                            responseData.Code = "-3";
                            responseData.Message = "不存在对应流水号的检测车辆信息。（环检）";
                            return artificialProjectR016s.ToArray();
                        }
                        string wjcx = dataTable.Rows[0][0].ToString();

                        if (wjcx == "")
                        {
                            responseData.Code = "-99";
                            responseData.Message = "对应流水号的车辆不存在外检车型信息。（环检）";
                            return artificialProjectR016s.ToArray();
                        }
                        sql = "SELECT ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                        sql += " ,'' as Cartype,'' as Wjcxmc, fl as Fldm,dm as ItemDm ,mc as Xmms ,'-' as Sycx ";
                        sql += " FROM QcyJcWgDpDmInfo WHERE 1 = 1 and Fl = '1' order by dm ";
                        artificialProjects = dbHj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016 = new ArtificialProjectR016();
                        if (artificialProjectR016s.Count == 0)
                        {
                            artificialProjectR016.ID = 1;
                        }
                        else
                        {
                            artificialProjectR016.ID = 2;
                        }
                        artificialProjectR016.Jyyw = "环检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return artificialProjectR016s.ToArray();
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
            responseData.Code = "1";
            responseData.Message = "SUCCESS";
            return artificialProjectR016s.ToArray();
        } 



        /// <summary>
        /// 查询机动车人工检验项目需要拍摄的照片
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public InspectionPhotoR017[] LYYDJKR017(RequestData requestData, ResponseData responseData)
        {
            List<InspectionPhotoR017> inspectionPhotoR017s = new List<InspectionPhotoR017>();
            string sql = "";
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (queryCriteria.Lsh == "" || queryCriteria.Lsh is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Lsh）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (queryCriteria.Jyxm == "" || queryCriteria.Jyxm is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Jyxm）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (queryCriteria.Ajywlb == "" || queryCriteria.Ajywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (queryCriteria.Hjywlb == "" || queryCriteria.Hjywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Hjywlb）";
                    return inspectionPhotoR017s.ToArray();
                }
                //第一种情况  只拍摄安检照片
                if (queryCriteria.Ajywlb != "-" && queryCriteria.Hjywlb == "-")
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                        //先查询外检车型
                        sql = " select wgchx,ajywlb  from BaseInfo_Hand where lsh='" + queryCriteria.Lsh + "'";
                        DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count == 0)
                        {
                            responseData.Code = "-3";
                            responseData.Message = "不存在对应流水号的检测车辆信息。（安检）";
                            return inspectionPhotoR017s.ToArray();
                        }
                        string wjcx = dataTable.Rows[0][0].ToString();
                        if (wjcx == "")
                        {
                            responseData.Code = "-99";
                            responseData.Message = "对应流水号的车辆不存在外检车型信息。（安检）";
                            return inspectionPhotoR017s.ToArray();
                        }
                        sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                        sql += " ,cartype as wjcxdm ,(select mc from jscscode where fl = '60' and dm = cartype ) as wjcxmc ";
                        sql += " ,t2.Pic_Num as zpdm,t2.Pic_TypeStr as zpmc" +
                            ",case  when Pic_AJ_DM is null then '-' when Pic_AJ_DM='NULL' then '-' when Pic_AJ_DM='0' then '-' else Pic_AJ_DM  end as zpajdm" +
                            ",case  when Pic_HJ_DM is null then '-' when Pic_HJ_DM='NULL' then '-' when Pic_HJ_DM='0' then '-' else Pic_HJ_DM  end as zphjdm" +
                            ",'1' as bcaj,'0' as bchj ";
                        sql += " from[dbo].[tb_cartypeandphotodm] t1,Pic_Sj_Bt_Pic_Type t2 ";
                        sql += " where cartype = '" + wjcx + "' and t1.photoDm = t2.Pic_Num ";
                        inspectionPhotoR017s = dbAj.QueryForList<InspectionPhotoR017>(sql, null);
                        responseData.Code = "1";
                        responseData.Message = "SUCCESS";
                        responseData.RowNum = inspectionPhotoR017s.Count;
                    }
                    else
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return inspectionPhotoR017s.ToArray();
                    }
                }
                //第二种情况  只拍摄环检照片
                if (queryCriteria.Ajywlb == "-" && queryCriteria.Hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                        sql = "select zs from LY_Flow_Info where Lsh='" + queryCriteria.Lsh + "'";
                        DataTable dataTable = dbHj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count == 0)
                        {
                            responseData.Code = "-3";
                            responseData.Message = "不存在对应流水号的检测车辆信息。（环检）";
                            return inspectionPhotoR017s.ToArray();
                        }
                        string wjcx = dataTable.Rows[0][0].ToString();

                        if (wjcx == "")
                        {
                            responseData.Code = "-99";
                            responseData.Message = "对应流水号的车辆不存在外检车型信息。（环检）";
                            return inspectionPhotoR017s.ToArray();
                        }
                        sql = "select Pic_Info  from [dbo].[WgXm_Show_Info]  where PaiZhao_Type ='" + wjcx + "'";
                        string picInfo = dbHj.ExecuteScalar(sql, null).ToString();
                        sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id , ";
                        sql += " '' as wjcxdm, '' as wjcxmc,Pic_DM as zpdm,Pic_TypeStr as zpmc,'-' as zpajdm,Pic_DM as zphjdm,'0' bcaj,'1'bchj ";
                        sql += " from Pic_Sj_Bt_Pic_Type where Pic_DM in (" + picInfo + ")";
                        inspectionPhotoR017s = dbHj.QueryForList<InspectionPhotoR017>(sql, null);
                        responseData.Code = "1";
                        responseData.Message = "SUCCESS";
                        responseData.RowNum = inspectionPhotoR017s.Count;
                    }
                    else
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return inspectionPhotoR017s.ToArray();
                    }
                }

                //第三种情况  安检，环检照片同时拍摄
                if (queryCriteria.Ajywlb != "-" && queryCriteria.Hjywlb != "-")
                {
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    //安检外检车型，照片代码
                    sql = " select wgchx  from BaseInfo_Hand where lsh='" + queryCriteria.Lsh + "'";
                    string ajwjcx = dbAj.ExecuteScalar(sql, null).ToString();
                    if (ajwjcx == "")
                    {
                        responseData.Code = "-99";
                        responseData.Message = "对应流水号的车辆不存在外检车型信息。（安检）";
                        return inspectionPhotoR017s.ToArray();
                    }
                    sql = " select stuff((SELECT ','+photoDm FROM tb_cartypeandphotodm where cartype='" + ajwjcx + "' and ajps='1' FOR XML PATH('')),1,1,'')";
                    string ajzpdm = dbAj.ExecuteScalar(sql, null).ToString();

                    //环检外检车型，照片代码
                    sql = "select zs from LY_Flow_Info where Lsh='" + queryCriteria.Lsh + "'";
                    string hjwjcx = dbHj.ExecuteScalar(sql, null).ToString();
                    if (hjwjcx == "")
                    {
                        responseData.Code = "-99";
                        responseData.Message = "对应流水号的车辆不存在外检车型信息。（环检）";
                        return inspectionPhotoR017s.ToArray();
                    }
                    sql = "select Pic_Info  from [dbo].[WgXm_Show_Info]  where PaiZhao_Type ='" + hjwjcx + "'";
                    string hjzpdm = dbHj.ExecuteScalar(sql, null).ToString();
                    //合并查询照片代码--从安检数据库
                    sql = " SELECT ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                    sql += " ,'" + ajwjcx + "' as wjcxdm,(select mc from jscscode where fl = '60' and dm = '" + ajwjcx + "' ) as wjcxmc ";
                    sql += "  ,Pic_Num as zpdm,Pic_TypeStr as zpmc,Pic_AJ_DM as zpajdm,Pic_HJ_DM as zphjdm ";
                    sql += " ,case when LEN(Pic_AJ_DM)= 4 then  '1' else '0' end as bcaj ";
                    sql += " ,case when LEN(Pic_HJ_DM)= 6 then  '1' else '0' end as bchj ";
                    sql += " FROM Pic_Sj_Bt_Pic_Type ";
                    sql += " WHERE CHARINDEX(Pic_Num, '" + ajzpdm + "') > 0 ";
                    sql += " or Pic_HJ_DM in (" + hjzpdm + ")";

                    inspectionPhotoR017s = dbAj.QueryForList<InspectionPhotoR017>(sql, null);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                    responseData.RowNum = inspectionPhotoR017s.Count;
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
            return inspectionPhotoR017s.ToArray();
        }

        /// <summary>
        /// 查询所有人工检验项目
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public ArtificialProjectR016[] LYYDJKR018(RequestData requestData, ResponseData responseData)
        {
            List<ArtificialProjectR016> artificialProjectR016s = new List<ArtificialProjectR016>();
            ArtificialProjectR016 artificialProjectR016 = new ArtificialProjectR016();
            string sql = "";
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);

                if (queryCriteria.Ajywlb == "" || queryCriteria.Ajywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Hjywlb == "" || queryCriteria.Hjywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Hjywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                        //确定检验项目
                        //根据检验项目查询人工项目详细
                        switch (queryCriteria.Jyxm)
                        {
                            case "NQ":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'' as cartype ,'' as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '5' order by itemdm ";
                                break;
                            case "UC":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'' as cartype ,'' as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '6' order by itemdm ";
                                break;
                            case "F1":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'' as cartype ,'' as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '1' order by itemdm ";
                                break;
                            case "C1":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'' as cartype ,'' as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms ,'-' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '2' order by itemdm ";
                                break;
                            case "DC":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'' as cartype ,'' as wjcxmc ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms,'-' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '3' order by itemdm ";
                                break;
                            default:
                                responseData.Code = "-99";
                                responseData.Message = "不可识别的检验项目（Jyxm）。";
                                return artificialProjectR016s.ToArray();
                        }
                        artificialProjects = dbAj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016.ID = 1;
                        artificialProjectR016.Jyyw = "安检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return artificialProjectR016s.ToArray();
                    }
                }
                if (queryCriteria.Hjywlb != "-" && queryCriteria.Jyxm == "F1")
                {
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                        DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);

                        sql = "SELECT ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                        sql += " ,'' as Cartype,'' as Wjcxmc, fl as Fldm,dm as ItemDm ,mc as Xmms  ,'-' as Sycx ";
                        sql += " FROM QcyJcWgDpDmInfo WHERE 1 = 1 and Fl = '1' order by dm ";
                        artificialProjects = dbHj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016 = new ArtificialProjectR016();
                        if (artificialProjectR016s.Count == 0)
                        {
                            artificialProjectR016.ID = 1;
                        }
                        else
                        {
                            artificialProjectR016.ID = 2;
                        }
                        artificialProjectR016.Jyyw = "环检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return artificialProjectR016s.ToArray();
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
            return artificialProjectR016s.ToArray();
        }
        /// <summary>
        /// 查询人工项目规定的最短检验时间
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public InspectionDurationR019[] LYYDJKR019(RequestData requestData, ResponseData responseData)
        {
            List<InspectionDurationR019> inspectionDurationR019s = new List<InspectionDurationR019>();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                string sql = "";
                if (queryCriteria.Jyxm != "" && !(queryCriteria.Jyxm is null))
                {
                    sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ,jcxm,Yqsc from ( " +
                   " select 'F1' as Jcxm,Yqsc = Time_Wg,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'C1' as Jcxm,Yqsc = Time_Dp,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'DC' as Jcxm,Yqsc = Time_Dt,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'B' as Jcxm,Yqsc = Time_Zd,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'H' as Jcxm,Yqsc = Time_Dg,CxmsDm from[dbo].[Time_Jc_CxCs] " +
                   " ) as t1 where CxmsDm = '" + queryCriteria.Ajcx + "' and jcxm='" + queryCriteria.Jyxm + "'";
                }
                else
                {
                    sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ,jcxm,Yqsc  from ( " +
                   " select 'F1' as Jcxm,Yqsc = Time_Wg,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'C1' as Jcxm,Yqsc = Time_Dp,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'DC' as Jcxm,Yqsc = Time_Dt,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'B' as Jcxm,Yqsc = Time_Zd,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'H' as Jcxm,Yqsc = Time_Dg,CxmsDm from[dbo].[Time_Jc_CxCs] " +
                   " ) as t1 where CxmsDm = '" + queryCriteria.Ajcx + "' ";
                }

                inspectionDurationR019s = dbAj.QueryForList<InspectionDurationR019>(sql, null);
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
            return inspectionDurationR019s.ToArray();
        }
        /// <summary>
        /// 查询所有人工检验项目 包含默认合格
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public ArtificialProjectR016[] LYYDJKR020(RequestData requestData, ResponseData responseData)
        {
            List<ArtificialProjectR016> artificialProjectR016s = new List<ArtificialProjectR016>();
            ArtificialProjectR016 artificialProjectR016 = new ArtificialProjectR016();
            string sql = "";
            string mrhgStr;
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                
                if (queryCriteria.Ajywlb == "" || queryCriteria.Ajywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Hjywlb == "" || queryCriteria.Hjywlb is null)
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Hjywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (queryCriteria.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();

                        //先查询外检车型
                        sql = " select wgchx,ajywlb  from BaseInfo_Hand where lsh='" + queryCriteria.Lsh + "'";
                        DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count == 0)
                        {
                            responseData.Code = "-3";
                            responseData.Message = "不存在对应流水号的检测车辆信息。（安检）";
                            return artificialProjectR016s.ToArray();
                        }
                        string wjcx = dataTable.Rows[0][0].ToString();
                        //确定检验项目
                        //根据检验项目查询人工项目详细
                        switch (queryCriteria.Jyxm)
                        {
                            case "NQ":
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'"+wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms,'1' as Sycx ";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '5' order by Sycx desc, itemdm ";
                                break;
                            case "UC":
                                mrhgStr = "02,03,05,06";
                                if (queryCriteria.Ajywlb == "00")
                                {
                                    mrhgStr = "03,04,05,06";
                                }
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'" + wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms";
                                sql += " ,case when t2.dm in (" + mrhgStr + ") then '1' else  '0' end  as Sycx";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '6' order by Sycx desc, itemdm ";
                                break;
                            case "F1":
                                sql = " select stuff((SELECT ','+itemdm FROM [dbo].[tb_cartypeanditemdm] where cartype='"+wjcx +"' and fldm='1'  FOR XML PATH('')),1,1,'')";
                                mrhgStr = dbAj.ExecuteScalar(sql , null).ToString();
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'" + wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms ";
                                sql += " ,case when t2.dm in (" + mrhgStr + ") then '1' else  '0' end  as Sycx";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '1' order by Sycx desc, itemdm ";
                                break;
                            case "C1":
                                sql = " select stuff((SELECT ','+itemdm FROM [dbo].[tb_cartypeanditemdm] where cartype='" + wjcx + "' and fldm='2'  FOR XML PATH('')),1,1,'')";
                                mrhgStr = dbAj.ExecuteScalar(sql, null).ToString();
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'" + wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms ";
                                sql += " ,case when t2.dm in (" + mrhgStr + ") then '1' else  '0' end  as Sycx";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '2' order by Sycx desc, itemdm ";
                                break;
                            case "DC":
                                sql = " select stuff((SELECT ','+itemdm FROM [dbo].[tb_cartypeanditemdm] where cartype='" + wjcx + "' and fldm='3'  FOR XML PATH('')),1,1,'')";
                                mrhgStr = dbAj.ExecuteScalar(sql, null).ToString();
                                sql = " select ROW_NUMBER()OVER(ORDER BY (select 0)) as id  ";
                                sql += " ,'" + wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
                                sql += " ,case t2.Fl when '1' then '外观' when '2' then '底盘' when '3' then '底盘动态' when '5' then '联网查询' when '6' then '唯一性检查' else t2.Fl end as Fldm ";
                                sql += " ,t2.dm as itemdm ";
                                sql += " ,mc as Xmms ";
                                sql += " ,case when t2.dm in (" + mrhgStr + ") then '1' else  '0' end  as Sycx";
                                sql += " from[dbo].[QcyJcWgDpDmInfo] t2 ";
                                sql += " where t2.Fl = '3' order by Sycx desc, itemdm ";
                                break;
                            default:
                                responseData.Code = "-99";
                                responseData.Message = "不可识别的检验项目（Jyxm）。";
                                return artificialProjectR016s.ToArray();
                        }
                        artificialProjects = dbAj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016.ID = 1;
                        artificialProjectR016.Jyyw = "安检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return artificialProjectR016s.ToArray();
                    }
                }
                if (queryCriteria.Hjywlb != "-" && queryCriteria.Jyxm == "F1")
                {
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                        DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);

                        sql = "SELECT ROW_NUMBER()OVER(ORDER BY (select 0)) as id ";
                        sql += " ,'' as Cartype,'' as Wjcxmc, fl as Fldm,dm as ItemDm ,mc as Xmms ,'1' as Sycx ";
                        sql += " FROM QcyJcWgDpDmInfo WHERE 1 = 1 and Fl = '1' order by dm ";
                        artificialProjects = dbHj.QueryForList<ArtificialProject>(sql, null);
                        artificialProjectR016 = new ArtificialProjectR016();
                        if (artificialProjectR016s.Count == 0)
                        {
                            artificialProjectR016.ID = 1;
                        }
                        else
                        {
                            artificialProjectR016.ID = 2;
                        }
                        artificialProjectR016.Jyyw = "环检";
                        artificialProjectR016.Xmlb = artificialProjects.ToArray();
                        artificialProjectR016.Xmsl = artificialProjects.Count;
                        artificialProjectR016s.Add(artificialProjectR016);
                    }
                    else
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return artificialProjectR016s.ToArray();
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
            return artificialProjectR016s.ToArray();
        }

        //public string QueryTime()
        //{
        //    BasicHttpBinding binding2 = new BasicHttpBinding();
        //    EndpointAddress address2 = new EndpointAddress("http://localhost:8072/ConnDBWebService.asmx");
        //    //ConnDBWebServiceSoapClient client2 = new ConnDBWebServiceSoapClient(binding2, address2);
        //    //DateTime dt = client2.getTimeFromServer();
        //    //return dt.ToString();
        //    //http://localhost:8072/HCNETWebService.asmx
        //    address2 = new EndpointAddress("http://localhost:8072/HCNETWebService.asmx");
        //    HCNETWebServiceSoapClient client3 = new HCNETWebServiceSoapClient(binding2, address2);
        //    Task<HelloWorldResponse> helloWorldResponse = client3.HelloWorldAsync();
        //    HelloWorldResponse helloWorldResponse1 = helloWorldResponse.Result;
        //    //return helloWorldResponse1.Body.HelloWorldResult ;
        //    Task<ShutterResponse> shutter = client3.ShutterAsync("", "", "", "", "", "", "", "", "", "");
        //    string a = shutter.Result.Body.ShutterResult;
        //    return a;

        //    a.Substring(0, 120);
        //}
    }
}
