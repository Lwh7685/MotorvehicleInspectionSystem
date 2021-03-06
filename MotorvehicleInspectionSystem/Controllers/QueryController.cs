using HCNETWebService;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models;
using MotorvehicleInspectionSystem.Models.ChargePayment;
using MotorvehicleInspectionSystem.Models.Invoice;
using MotorvehicleInspectionSystem.Models.Request;
using MotorvehicleInspectionSystem.Models.Response;
using MotorvehicleInspectionSystem.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

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
                if (VehicleInspectionController.SyAj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    string sql = string.Format("select *,STUFF((SELECT ',' + roleDm FROM Tb_UserRole WHERE username =a.UserName  FOR xml path('')),1,1,'') as RoleDm from Tab_UserInfo A");
                    users = db.QueryForList<User>(sql, null);
                }
                else if (VehicleInspectionController.SyHj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    string sql = "select RySFZ as SfzInfoID ,GongHao,RyName as 'username',RyName as 'turename',RyPassWord as 'PassWord' ,getdate() as 'AddDate',RyRight as 'RoleDm' from [dbo].[TbSystemUserInfo]";
                    users = db.QueryForList<User>(sql, null);
                }
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
            DataTable dataTableAj = new DataTable();
            DataTable dataTableHj = new DataTable();
            string sql = "";
            try
            {
                QueryVehicleCriteria queryVehQueueR002 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                if (VehicleInspectionController.SyAj == "1")
                {
                    sql = "select t1.Lsh as Lsh,t1.Lsh as ajlsh,t2.hphm as Hphm,t1.Jccs,t1.ajjccs ";
                    sql += " ,(select mc from jscscode where dm = t2.hpzl and fl = '09') as HpzlCc ,t2.hpzl as Hpzl ";
                    sql += " ,(select mc from jscscode where dm = t3.cllx and fl = '07') as CllxCc ,t3.cllx as Cllx ";
                    sql += " ,(select mc from jscscode where dm = t2.hpys and fl = '26') as HpysCc ,t2.hpys as Hpys ";
                    sql += " ,convert(varchar(19), convert(datetime, t1.AddRq ), 120) as Djrq ";
                    sql += " ,(case t1.isonline when '0' then '未上线' when '1' then '线上检验' when '2' then '线上结束' end ) as Jyzt  ";
                    sql += " ,(select mc from jscscode where dm=t1.ajywlb and fl='08') as ajywlbCc ,t1.ajywlb as ajywlb ";
                    sql += " ,'-' as hjywlbCc ,'-' as hjywlb,'-' as hjlsh ";
                    sql += " ,'安检' as Ywlb ,'0' as hjjccs,t2.sfjf as sfsf,t2.sfkp as sfkp ";
                    sql += " from LY_Flow_Info t1,BaseInfo_Hand t2,baseinfo_net t3 ";
                    sql += " where t1.Lsh = t2.Lsh and t1.lsh=t3.lsh  and (t2.tb <>'1' or t2.tb is null)";
                    if (queryVehQueueR002.Hphm == "")
                    {
                        sql += " and convert(varchar(10),convert(datetime, t1.AddRq),120) = convert(varchar(10), GETDATE(), 120) ";
                    }
                    else
                    {
                        sql += " and t2.hphm like '%" + queryVehQueueR002.Hphm + "%' ";
                    }
                    sql += " and t1.isonline in ('0','1','2') order by t1.AddRq desc ";
                    dataTableAj = dbAj.ExecuteDataTable(sql, null);
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        sql = " select t1.Lsh as Lsh,t2.hphm as Hphm,t1.Jccs as hjjccs ";
                        sql += " ,(select mc from jscscode where dm = t2.hpzl and fl = '09') as HpzlCc ,t2.hpzl as Hpzl ";
                        sql += " ,(select mc from jscscode where dm = t2.gcjk and fl = '36') as HpysCc ,t2.gcjk as Hpys ";
                        sql += " ,(select mc from jscscode where dm = t2.cllx and fl = '07') as CllxCc ,t2.cllx as Cllx ";
                        sql += " ,convert(varchar(10), convert(datetime, t1.Jcdate ), 120) +' '+ t1.Jctime  as Djrq  ";
                        sql += " ,'' as Jyzt ";
                        sql += " ,(select mc from jscscode where dm = t1.Jclb and fl = '08') as hjywlbCc,t1.Jclb as hjywlb ";
                        sql += " ,'环检' as ywlb ,'1' as sfsf,'1' as sfkp";
                        sql += " from LY_Flow_Info t1,BaseInfo_Net t2 ";
                        sql += " where t1.Lsh = t2.Lsh  and (t1.GW_01 ='0' or t1.GW_03 ='0')";
                        sql += " and convert(varchar(10),convert(datetime, t1.JcDate),120) = convert(varchar(10), GETDATE(), 120) ";
                        sql += " and t2.hphm like '%" + queryVehQueueR002.Hphm + "%' ";
                        sql += " order by t1.JcDate desc, t1.JcTime ";
                        dataTableHj = dbHj.ExecuteDataTable(sql, null);
                    }

                    DataTable dtAll = UniteDataTableLYYDJKR002(dataTableAj, dataTableHj, "dtAll");
                    vehicleQueues = EntityReader.GetEntities<VehicleQueue>(dtAll);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                else if (VehicleInspectionController.SyHj == "1")
                {
                    sql = " select t1.Lsh as Lsh ,t1.Lsh as hjlsh,t2.hphm as Hphm,t1.Jccs as jccs,t1.Jccs as hjjccs ";
                    sql += " ,(select mc from jscscode where dm = t2.hpzl and fl = '09') as HpzlCc ,t2.hpzl as Hpzl ";
                    sql += " ,(select mc from jscscode where dm = t2.cllx and fl = '07') as CllxCc ,t2.cllx as Cllx ";
                    sql += " ,(select mc from jscscode where dm = t2.gcjk and fl = '36') as HpysCc ,t2.gcjk as Hpys ";
                    sql += " ,convert(varchar(10), convert(datetime, t1.Jcdate ), 120) +' '+ t1.Jctime  as Djrq  ";
                    sql += " ,'' as Jyzt ";
                    sql += " ,'-' as ajywlbCc ,'-' as ajywlb ,'-' as ajlsh,'0' as ajjccs ";
                    sql += " ,(select mc from jscscode where dm = t1.Jclb and fl = '08') as hjywlbCc,t1.Jclb as hjywlb ";
                    sql += " ,'环检' as ywlb ,'1' as sfsf,'1' as sfkp";
                    sql += " from LY_Flow_Info t1,BaseInfo_Net t2 ";
                    sql += " where t1.Lsh = t2.Lsh  and (t1.GW_01 ='0' or t1.GW_03 ='0')";
                    sql += " and convert(varchar(10),convert(datetime, t1.JcDate),120) = convert(varchar(10), GETDATE(), 120) ";
                    sql += " and t2.hphm like '%" + queryVehQueueR002.Hphm + "%' ";
                    sql += " order by t1.JcDate desc, t1.JcTime ";
                    dataTableHj = dbHj.ExecuteDataTable(sql, null);
                    vehicleQueues = EntityReader.GetEntities<VehicleQueue>(dataTableHj);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                //if (dataTableAj.Rows.Count == 0 && dataTableHj.Rows.Count == 0)
                //{
                //    responseData.Code = "1";
                //    responseData.Message = "SUCCESS";
                //}
                //else
                //{
                //    //获取两个数据源的并集
                //    var comparer = new CustomComparerR002();
                //    IEnumerable<DataRow> QBJ = dataTableAj.AsEnumerable().Union(dataTableHj.AsEnumerable(), comparer);
                //    //两个数据源的并集集合
                //    DataTable DTBJ = QBJ.CopyToDataTable();
                //    vehicleQueues = EntityReader.GetEntities<VehicleQueue>(DTBJ);
                //    responseData.Code = "1";
                //    responseData.Message = "SUCCESS";
                //}
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
        /// 合并安检环检的信息
        /// </summary>
        /// <param name="Aj">安检数据表</param>
        /// <param name="Hj">环检数据表</param>
        /// <param name="DTName">新表名称</param>
        /// <returns></returns>
        private DataTable UniteDataTableLYYDJKR002(DataTable Aj, DataTable Hj, string DTName)
        {
            DataTable dt3 = Aj.Clone();
            object[] obj = new object[dt3.Columns.Count];

            for (int i = 0; i < Aj.Rows.Count; i++)
            {
                Aj.Rows[i].ItemArray.CopyTo(obj, 0);
                dt3.Rows.Add(obj);
            }
            for (int i = 0; i < Hj.Rows.Count; i++)
            {
                var rows = dt3.AsEnumerable()
                            .Where(p => p.Field<string>("hphm") == Hj.Rows[i]["hphm"].ToString() && p.Field<string>("hpzl") == Hj.Rows[i]["hpzl"].ToString());

                if (rows.Count() > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        dr["hjlsh"] = Hj.Rows[i]["lsh"].ToString();
                        dr["hjywlb"] = Hj.Rows[i]["hjywlb"].ToString();
                        dr["hjjccs"] = Hj.Rows[i]["hjjccs"].ToString();
                        dr["hjywlbCc"] = Hj.Rows[i]["hjywlbCc"].ToString();
                        dr["ywlb"] = "同检";
                    }
                }
                else
                {
                    DataRow dr = dt3.NewRow();
                    dr["hphm"] = Hj.Rows[i]["hphm"].ToString();
                    dr["hpzl"] = Hj.Rows[i]["hpzl"].ToString();
                    dr["hpzlCc"] = Hj.Rows[i]["hpzlCc"].ToString();
                    dr["Cllx"] = Hj.Rows[i]["Cllx"].ToString();
                    dr["CllxCc"] = Hj.Rows[i]["CllxCc"].ToString();
                    dr["hjlsh"] = Hj.Rows[i]["lsh"].ToString();
                    dr["lsh"] = Hj.Rows[i]["lsh"].ToString();
                    dr["hjywlb"] = Hj.Rows[i]["hjywlb"].ToString();
                    dr["hjjccs"] = Hj.Rows[i]["hjjccs"].ToString();
                    dr["jccs"] = Hj.Rows[i]["hjjccs"].ToString();
                    dr["hjywlbCc"] = Hj.Rows[i]["hjywlbCc"].ToString();
                    dr["Djrq"] = Hj.Rows[i]["Djrq"].ToString();
                    dr["Hpys"] = Hj.Rows[i]["Hpys"].ToString();
                    dr["HpysCc"] = Hj.Rows[i]["HpysCc"].ToString();
                    dr["ajlsh"] = "-";
                    dr["ajywlb"] = "-";
                    dr["ajywlbCc"] = "-";
                    dr["ajjccs"] = 0;
                    //ywlb
                    dr["ywlb"] = "环检";
                    dt3.Rows.Add(dr);
                }
            }
            dt3.TableName = DTName; //设置DT的名字
            return dt3;
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
                if (!string.IsNullOrEmpty(queryDataDR003.Fl))
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
        public ChargeItem[] LYYDJKR004(ResponseData responseData)
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
            VehicleDetails vehicleDetails1 = new VehicleDetails();
            bool ajNull = false;
            string sql = "";
            try
            {
                DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                QueryVehicleCriteria queryVehicleDetailsR005 = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (string.IsNullOrEmpty(queryVehicleDetailsR005.Ajlsh) && string.IsNullOrEmpty(queryVehicleDetailsR005.Hjlsh)
                    && (string.IsNullOrEmpty(queryVehicleDetailsR005.Hpzl) || string.IsNullOrEmpty(queryVehicleDetailsR005.Hphm))
                    && string.IsNullOrEmpty(queryVehicleDetailsR005.Clsbdh)
                    && string.IsNullOrEmpty(queryVehicleDetailsR005.Xszbh))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空";
                }
                else
                {
                    if (VehicleInspectionController.SyAj == "1")
                    {
                        //按流水号查询
                        sql = "select *,'-' as Hjdlsj,tn.lsh as ajlsh,'-' as hjlsh from BaseInfo_Net tn,BaseInfo_Hand th where tn.lsh=th.Lsh ";
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Ajlsh))
                        {
                            sql += " and tn.Lsh ='" + queryVehicleDetailsR005.Ajlsh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //号牌号码，号牌种类
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Hphm) && !string.IsNullOrEmpty(queryVehicleDetailsR005.Hpzl))
                        {
                            sql += "and tn.hphm ='" + queryVehicleDetailsR005.Hphm.Trim().Replace("'", "").Replace("-", "") + "' and tn.hpzl ='" + queryVehicleDetailsR005.Hpzl.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //车辆识别代号
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Clsbdh))
                        {
                            //在安检的时候，车辆识别代号不为空的时候，从监管平台获取，不再从数据库查询
                            sql += "and tn.clsbdh like '%" + queryVehicleDetailsR005.Clsbdh.Trim().Replace("'", "").Replace("-", "") + "'";


                        }
                        //行驶证编号
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Xszbh))
                        {
                            sql += "and tn.xszbh ='" + queryVehicleDetailsR005.Xszbh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        try
                        {
                            vehicleDetails1 = dbAj.QueryForObject<VehicleDetails>(sql, null);
                        }
                        catch (ArgumentNullException)
                        {
                            //安检数据库没有数据
                            ajNull = true;
                        }
                    }
                    //安检查询到，再次确认环检业务类型
                    if (VehicleInspectionController.SyHj == "1")
                    {
                        sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id, t1.*";
                        sql += " ,'' as sfmj,'-' as ajywlb ,'-' as zjywlb,t2.Jclb as hjywlb ,t2.JcXm as hjxm ,'-' as Aj_Veh_Type,'-' as Zj_Veh_Type,'-' as Wgchx";
                        sql += " ,t2.jcdate,t2.jctime,'0' as Zhchzhsh,'' as Zhchzhw,'0' as Zhzhsh,'' as Qdzhw,'0' as SFKZ,t1.YHXZ as Qgs,t1.YYZHDM as rygg,t1.Bz03 as lsdh";
                        sql += " ,t1.zzxzqh as lxdz,t1.zzg as hpys,t1.bz02 as edzhs,'' as Bzhzhw,'' as Jdchsshlb,'' as Jcxlb,'0' as Qzhsh,'0' as Zhxzhsh,'' as Zjjylb,'' as Yyzhh";
                        sql += " ,'' as Zjlsh,'' as Dzss,'' as Kqxjzw,t1.hbdbqk as Pfjd,t1.zsxzqh as pqgsh,WRZY as Jqfs,ZJDW as Dws,xzqh as Gyfs,'-' as sjr,t1.Bz03 as Sjrdh,'' as Sjrsfzh";
                        sql += " ,t2.InBz_01 as Hjdlsj ,t2.lsh as hjlsh,'-' as ajlsh,'0' as Chych,'0' as qdzhsh";
                        sql += " from BaseInfo_Net t1,LY_Flow_Info t2 where t1.Lsh = t2.Lsh ";
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Hjlsh))
                        {
                            sql += " and t2.Lsh ='" + queryVehicleDetailsR005.Hjlsh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //号牌号码，号牌种类
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Hphm) && !string.IsNullOrEmpty(queryVehicleDetailsR005.Hpzl))
                        {
                            sql += "and t2.hphm ='" + queryVehicleDetailsR005.Hphm.Trim().Replace("'", "").Replace("-", "") + "' and t2.hpzl ='" + queryVehicleDetailsR005.Hpzl.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //车辆识别代号
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Clsbdh))
                        {
                            sql += "and t1.clsbdh like '%" + queryVehicleDetailsR005.Clsbdh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        //行驶证编号
                        if (!string.IsNullOrEmpty(queryVehicleDetailsR005.Xszbh))
                        {
                            sql += "and t2.xszbh ='" + queryVehicleDetailsR005.Xszbh.Trim().Replace("'", "").Replace("-", "") + "'";
                        }
                        if (ajNull)
                        {
                            vehicleDetails = dbHj.QueryForList<VehicleDetails>(sql, null);
                        }
                        else
                        {
                            try
                            {
                                VehicleDetails vehicleDetails2 = dbHj.QueryForObject<VehicleDetails>(sql, null);
                                vehicleDetails1.Hjywlb = vehicleDetails2.Hjywlb;
                                vehicleDetails1.Hjlsh = vehicleDetails2.Hjlsh;
                                vehicleDetails.Add(vehicleDetails1);
                            }
                            catch (ArgumentNullException)
                            {
                                //环检也没有数据,直接返回吧
                                vehicleDetails.Add(vehicleDetails1);
                            }
                        }
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
            string sql = "";
            int id = 0;
            try
            {
                QueryVehicleCriteria queryByLSH = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (string.IsNullOrEmpty(queryByLSH.Ajlsh) || string.IsNullOrEmpty(queryByLSH.Hjlsh))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空(Ajlsh/Hjlsh)";
                    return inspectionItemsR006s.ToArray();
                }
                if (string.IsNullOrEmpty(queryByLSH.Ajywlb) || string.IsNullOrEmpty(queryByLSH.Hjywlb))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空(Ajywlb/Hjywlb)";
                    return inspectionItemsR006s.ToArray();
                }
                if (VehicleInspectionController.SyAj == "1" && queryByLSH.Ajywlb != "-")
                {
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    //按流水号查询
                    sql = " select * from LY_Flow_Info where Lsh ='" + queryByLSH.Ajlsh + "'";
                    DataTable dataTable = dbAj.ExecuteDataTable(sql, null);
                    if (dataTable.Rows.Count == 0)
                    {
                        responseData.Code = "-3";
                        responseData.Message = "不存在对应流水号的检测信息。";
                        return inspectionItemsR006s.ToArray();
                    }
                    string jccs = dataTable.Rows[0]["jccs"].ToString();
                    string ajywlb = dataTable.Rows[0]["ajywlb"].ToString();
                    string[] jcxmZt = dataTable.Rows[0]["jyxmstatus"].ToString().Split(";");
                    if (jcxmZt.Length > 1)
                    {

                        foreach (string jcxmZtS in jcxmZt)
                        {
                            id++;
                            string jcxmS = jcxmZtS.Split(":")[0];
                            if (jcxmS != "NQ" && jcxmS != "UC" && jcxmS != "F1" && jcxmS != "C1" && jcxmS != "DC" && jcxmS.IndexOf("R") < 0)
                            {
                                continue;
                            }
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
                                            sql += " , dalb as jcxm,(select mc from jscscode where fl='83' and dm=dalb ) as xmmc";
                                            sql += " ,lsh,lsh as ajlsh,'" + jccs + "' as jccs,'" + jccs + "' as ajjccs,'" + ajywlb + "' as ajywlb,'-' as hjywlb,'-' as hjlsh,'0' as hjjccs";
                                            sql += " ,jcxh";
                                            sql += " ,jcry_01";
                                            sql += " ,jcry_02";
                                            sql += " ,CONVERT(datetime, SUBSTRING(kstime, 1, 4) + '/' + SUBSTRING(kstime, 5, 2) + '/' + SUBSTRING(kstime, 7, 2) + ' ' + SUBSTRING(kstime, 9, 2) + ':' + SUBSTRING(kstime, 11, 2) + ':' + SUBSTRING(kstime, 13, 2), 120) as jckssj";
                                            sql += " ,CONVERT(datetime, SUBSTRING(jstime, 1, 4) + '/' + SUBSTRING(jstime, 5, 2) + '/' + SUBSTRING(jstime, 7, 2) + ' ' + SUBSTRING(jstime, 9, 2) + ':' + SUBSTRING(jstime, 11, 2) + ':' + SUBSTRING(jstime, 13, 2), 120) as jcjssj";
                                            sql += " ,case jcpj when '1' then '合格' when '-1' then '不合格' else '其他' end as  jcpj ";
                                            sql += " ,'完成' as jczt from JcData_RG";
                                            sql += " where lsh = '" + queryByLSH.Ajlsh + "'";
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
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Ajlsh,
                                        Ajlsh = queryByLSH.Ajlsh,
                                        Ajjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Ajywlb = ajywlb,
                                        Jczt = "未检",
                                        Jcxm = jcxmS,
                                        ID = id,
                                        Hjlsh = "-",
                                        Hjjccs = 0,
                                        Hjywlb = "-",
                                        Xmmc = dbAj.QueryForObject<DataDictionary>("select * from jscscode where fl='83' and dm='" + jcxmS + "'", null).Mc
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                                else if (ztS == "2")
                                {
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Ajlsh,
                                        Ajlsh = queryByLSH.Ajlsh,
                                        Ajjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Jczt = "正在检测",
                                        Jcxm = jcxmS,
                                        Ajywlb = ajywlb,
                                        ID = id,
                                        Hjlsh = "-",
                                        Hjjccs = 0,
                                        Hjywlb = "-",
                                        Xmmc = dbAj.QueryForObject<DataDictionary>("select * from jscscode where fl='83' and dm='" + jcxmS + "'", null).Mc
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    if (dataTable.Rows[0]["JcXm"].ToString().IndexOf("B") >= 0 || dataTable.Rows[0]["JcXm"].ToString().IndexOf("H") >= 0 || dataTable.Rows[0]["JcXm"].ToString().IndexOf("A1") >= 0 || (dataTable.Rows[0]["JcXm"].ToString().IndexOf("Z1") >= 0 && dataTable.Rows[0]["zbzlstatus"].ToString() == "0"))
                    {
                        id++;
                        string jczt = "未检";
                        if (dataTable.Rows[0]["isonline"].ToString() == "0")
                        {
                            jczt = "未检";
                        }
                        if (dataTable.Rows[0]["isonline"].ToString() == "1")
                        {
                            jczt = "线上";
                        }
                        if (dataTable.Rows[0]["isonline"].ToString() == "2")
                        {
                            jczt = "完成";
                        }
                        if (dataTable.Rows[0]["isonline"].ToString() == "3")
                        {
                            jczt = "结束";
                        }
                        InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                        {
                            Lsh = queryByLSH.Ajlsh,
                            Ajlsh = queryByLSH.Ajlsh,
                            Ajjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                            Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                            Ajywlb = ajywlb,
                            Jczt = jczt,
                            Jcxm = "YQ",//仪器设备
                            ID = id,
                            Hjlsh = "-",
                            Hjjccs = 0,
                            Hjywlb = "-",
                            Jcxh = dataTable.Rows[0]["SB_TD"].ToString(),
                            Jckssj = Convert.ToDateTime(string.IsNullOrEmpty(dataTable.Rows[0]["sxsj"].ToString()) ? DateTime.Now.ToString() : dataTable.Rows[0]["sxsj"].ToString()),
                            Jcjssj = Convert.ToDateTime(string.IsNullOrEmpty(dataTable.Rows[0]["onlinetime"].ToString()) ? DateTime.Now.ToString() : dataTable.Rows[0]["onlinetime"].ToString()),
                            Jcry_01 = dataTable.Rows[0]["Ry_05"].ToString(),
                            Xmmc = "安检上线"
                        };
                        inspectionItemsR006s.Add(inspectionItemsR006);
                    }
                }

                if (VehicleInspectionController.SyHj == "1")
                {
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    if (queryByLSH.Hjywlb != "-")
                    {
                        sql = " select * from LY_Flow_Info where Lsh ='" + queryByLSH.Hjlsh + "'";
                        DataTable dataTable = dbHj.ExecuteDataTable(sql, null);
                        if (dataTable.Rows.Count > 0)
                        {
                            //外观
                            if (dataTable.Rows[0]["GW_01"].ToString() == "0" || dataTable.Rows[0]["GW_01"].ToString() == "1")
                            {
                                var itemF1Arr = inspectionItemsR006s.Where(i => (i.Jczt == "未检" || i.Jczt == "正在检测") && i.Jcxm == "F1").OrderBy(i => i.Jcxm).ToList();
                                if (itemF1Arr.Any())
                                {
                                    var itemF1 = itemF1Arr.First();
                                    itemF1.Hjlsh = dataTable.Rows[0]["lsh"].ToString();
                                    itemF1.Hjywlb = dataTable.Rows[0]["jclb"].ToString();
                                    itemF1.Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                }
                                else
                                {
                                    id++;
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Hjlsh,
                                        Ajlsh = "-",
                                        Ajjccs = 0,
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Ajywlb = "-",
                                        Jczt = (dataTable.Rows[0]["GW_01"].ToString() == "0" ? "未检" : "正在检测"),
                                        Jcxm = "F1",
                                        ID = id,
                                        Hjlsh = dataTable.Rows[0]["lsh"].ToString(),
                                        Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Hjywlb = dataTable.Rows[0]["jclb"].ToString(),
                                        Xmmc = "外观"
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                            }
                            else if (dataTable.Rows[0]["GW_01"].ToString() == "2")
                            {
                                var itemF1Arr = inspectionItemsR006s.Where(i => i.Jczt == "完成" && i.Jcxm == "F1").OrderBy(i => i.Jcxm).ToList();
                                if (itemF1Arr.Any())
                                {
                                    var itemF1 = itemF1Arr.First();
                                    itemF1.Hjlsh = dataTable.Rows[0]["lsh"].ToString();
                                    itemF1.Hjywlb = dataTable.Rows[0]["jclb"].ToString();
                                    itemF1.Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                }
                                else
                                {
                                    id++;
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Hjlsh,
                                        Ajlsh = "-",
                                        Ajjccs = 0,
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Ajywlb = "-",
                                        Jczt = "完成",
                                        Jcxm = "F1",
                                        ID = id,
                                        Hjlsh = dataTable.Rows[0]["lsh"].ToString(),
                                        Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Hjywlb = dataTable.Rows[0]["jclb"].ToString(),
                                        Xmmc = "外观"
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                            }
                            //底盘
                            if (dataTable.Rows[0]["GW_03"].ToString() == "0" || dataTable.Rows[0]["GW_03"].ToString() == "1")
                            {
                                var itemF1Arr = inspectionItemsR006s.Where(i => (i.Jczt == "未检" || i.Jczt == "正在检测") && i.Jcxm == "C1").OrderBy(i => i.Jcxm).ToList();
                                if (itemF1Arr.Any())
                                {
                                    var itemF1 = itemF1Arr.First();
                                    itemF1.Hjlsh = dataTable.Rows[0]["lsh"].ToString();
                                    itemF1.Hjywlb = dataTable.Rows[0]["jclb"].ToString();
                                    itemF1.Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                }
                                else
                                {
                                    id++;
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Hjlsh,
                                        Ajlsh = "-",
                                        Ajjccs = 0,
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Ajywlb = "-",
                                        Jczt = (dataTable.Rows[0]["GW_03"].ToString() == "0" ? "未检" : "正在检测"),
                                        Jcxm = "C1",
                                        ID = id,
                                        Hjlsh = dataTable.Rows[0]["lsh"].ToString(),
                                        Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Hjywlb = dataTable.Rows[0]["jclb"].ToString(),
                                        Xmmc = "底盘"
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
                                }
                            }
                            else if (dataTable.Rows[0]["GW_03"].ToString() == "2")
                            {
                                var itemF1Arr = inspectionItemsR006s.Where(i => i.Jczt == "完成" && i.Jcxm == "C1").OrderBy(i => i.Jcxm).ToList();
                                if (itemF1Arr.Any())
                                {
                                    var itemF1 = itemF1Arr.First();
                                    itemF1.Hjlsh = dataTable.Rows[0]["lsh"].ToString();
                                    itemF1.Hjywlb = dataTable.Rows[0]["jclb"].ToString();
                                    itemF1.Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString());
                                }
                                else
                                {
                                    id++;
                                    InspectionItemsR006 inspectionItemsR006 = new InspectionItemsR006
                                    {
                                        Lsh = queryByLSH.Hjlsh,
                                        Ajlsh = "-",
                                        Ajjccs = 0,
                                        Jccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Ajywlb = "-",
                                        Jczt = "完成",
                                        Jcxm = "C1",
                                        ID = id,
                                        Hjlsh = dataTable.Rows[0]["lsh"].ToString(),
                                        Hjjccs = Convert.ToInt32(dataTable.Rows[0]["jccs"].ToString()),
                                        Hjywlb = dataTable.Rows[0]["jclb"].ToString(),
                                        Xmmc = "底盘"
                                    };
                                    inspectionItemsR006s.Add(inspectionItemsR006);
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
                //查安检
                if (queryUploadPicR007.Ajywlb != "-")
                {
                    if (VehicleInspectionController.SyAj != "1")
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return uploadPics.ToArray();
                    }
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum, *,(select Pic_TypeStr from [dbo].[Pic_Sj_Bt_Pic_Type] where Pic_Num=t1.Zpzl ) zpzlmc ";
                    sql += " from UpLoad_Pic t1,(select zpzl, max(Jycs) jycs from UpLoad_Pic ";
                    sql += " where lsh = '" + queryUploadPicR007.Ajlsh + "' ";
                    sql += " group by zpzl) t2 ";
                    sql += " where t1.lsh = '" + queryUploadPicR007.Ajlsh + "' ";
                    sql += " and t1.Zpzl = t2.Zpzl and t1.Jycs = t2.jycs ";
                    try
                    {
                        uploadPics = dbAj.QueryForList<UploadPic>(sql, null);
                    }
                    catch (ArgumentNullException)
                    {

                    }

                }
                //查环保
                if (queryUploadPicR007.Hjywlb != "-")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return uploadPics.ToArray();
                    }
                    List<UploadPic> uploadPics2 = new List<UploadPic>();
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum,jylsh as lsh,BzO1 as bz01,BzO2 as bz02,BzO3 as bz03, *,(select Pic_TypeStr from [dbo].[Pic_Sj_Bt_Pic_Type] where Pic_Num=t1.Zpzl ) zpzlmc ";
                    sql += " from UpLoad_Wg_Pic t1,(select zpzl, max(Jycs) jycs from UpLoad_Wg_Pic ";
                    sql += " where jylsh = '" + queryUploadPicR007.Hjlsh + "' ";
                    sql += " group by zpzl) t2 ";
                    sql += " where t1.jylsh = '" + queryUploadPicR007.Hjlsh + "' ";
                    sql += " and t1.Zpzl = t2.Zpzl and t1.Jycs = t2.jycs ";
                    try
                    {
                        uploadPics2 = dbHj.QueryForList<UploadPic>(sql, null);
                    }
                    catch (ArgumentNullException)
                    {

                    }
                    uploadPics = uploadPics.Concat(uploadPics2).ToList<UploadPic>();
                }
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
                if (queryUploadAVIR008.Ajlsh != "" && string.IsNullOrEmpty(queryUploadAVIR008.Ajlsh))
                {
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum,* ,(select mc from jscscode where fl='84' and dm=xmbh ) spmc, jcrq +' '+ TimS  as jcsj,REPLACE (InBz_02,'D:\','lxdz\') as lxdz  ";
                    sql += " from UpLoad_AVI_XML where jcbh = '"+queryUploadAVIR008.Ajlsh +"'";//+ queryUploadAVIR008.Lsh +
                    //if (queryUploadAVIR008.Jccs != 0)
                    //{
                    //    sql += " and jklx ='" + queryUploadAVIR008.Jccs.ToString() + "'";
                    //}
                    sql += " order by JcJsSj ";
                    try
                    {
                        uploadAVIs = dbAj.QueryForList<UploadAVI>(sql, null);
                    }
                    catch (ArgumentNullException)
                    {
                        
                    }
                }
                if(queryUploadAVIR008.Hjlsh !="" && string.IsNullOrEmpty (queryUploadAVIR008.Hjlsh))
                {
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    string sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as rownum,* ,(select mc from jscscode where fl='84' and dm=xmbh ) spmc, jcrq +' '+ TimS  as jcsj,REPLACE (InBz_02,'D:\','lxdz\') as lxdz  ";
                    sql += " from UpLoad_AVI_XML where jcbh = '"+queryUploadAVIR008.Hjlsh +"'";//+ queryUploadAVIR008.Lsh +
                    //if (queryUploadAVIR008.Jccs != 0)
                    //{
                    //    sql += " and jklx ='" + queryUploadAVIR008.Jccs.ToString() + "'";
                    //}
                    sql += " order by JcJsSj ";
                    List<UploadAVI> uploadAVIs2 = new List<UploadAVI>();
                    try
                    {
                        uploadAVIs2 = dbHj.QueryForList<UploadAVI>(sql, null);
                    }
                    catch (ArgumentNullException)
                    {

                    }
                    uploadAVIs = uploadAVIs.Concat(uploadAVIs2).ToList<UploadAVI>();
                }
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
        public InCar[] LYYDJKR009(ResponseData responseData)
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
        /// 查询安检预约情况
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public AppointmentEntityAj.ResponseAppointmentAjR010[] LYYDJKR010(ResponseData responseData)
        {
            List<AppointmentEntityAj.ResponseAppointmentAjR010> responseAppointmentAjR010s = new List<AppointmentEntityAj.ResponseAppointmentAjR010>();
            try
            {

                AppointmentEntityAj.ResponseAppointmentAjR010 responseAppointment = new AppointmentEntityAj.ResponseAppointmentAjR010();
                List<AppointmentEntityAj.ResponseAppointmentAjR010> responseAppointments = new List<AppointmentEntityAj.ResponseAppointmentAjR010>();
                responseAppointment.Clsbdh = "LS1D221B2H0601122";
                responseAppointment.Hphm = "鄂HF39D2";
                responseAppointment.Hpzl = "02";
                responseAppointment.Id = 0;
                responseAppointment.Sjr = "张三";
                responseAppointment.Sjrsfzh = "123123123123";
                responseAppointment.Sjrdh = "1561156231561123561";
                responseAppointments.Add(responseAppointment);
                responseAppointment = new AppointmentEntityAj.ResponseAppointmentAjR010();
                responseAppointment.Clsbdh = "LA9940C38K0AZY386";
                responseAppointment.Hphm = "鄂HB552";
                responseAppointment.Hpzl = "15";
                responseAppointment.Id = 1;
                responseAppointment.Sjr = "李四";
                responseAppointment.Sjrsfzh = "312312325918524358";
                responseAppointment.Sjrdh = "12359421";
                responseAppointments.Add(responseAppointment);


                //SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                //Dictionary<string, string> @params = new Dictionary<string, string>();
                //@params.Add("jyjgbh", systemParameterAj.Jyjgbh);
                //string methordName = "/check-station/api/actived-appointment";
                //string retrunStr = AppointmentAj(systemParameterAj.AppointmentURL + methordName, @params);
                //JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
                //jsonSetting.NullValueHandling = NullValueHandling.Ignore;
                //AppointmentEntityAj.ACTIVEDRETURN aCTIVEDRETURN = JsonConvert.DeserializeObject<AppointmentEntityAj.ACTIVEDRETURN>(retrunStr, jsonSetting);
                //responseAppointmentAjR010s = aCTIVEDRETURN.data.ToList<AppointmentEntityAj.ResponseAppointmentAjR010>();
                responseAppointmentAjR010s = responseAppointments;
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
            return responseAppointmentAjR010s.ToArray();
        }
        /// <summary>
        /// 查询安检预约情况
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string AppointmentAj(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if ((i > 0))
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i += 1;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            // '添加参数
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                // '获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
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
        /// 查询待审核队列
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public ModerationQueueR013[] LYYDJKR013(RequestData requestData, ResponseData responseData)
        {
            List<ModerationQueueR013> moderationQueueR013s = new List<ModerationQueueR013>();
            DataTable dataTableAj = new DataTable();
            DataTable dataTableHj = new DataTable();
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                //安检审核查询
                if (queryCriteria.Shyw == "1" || queryCriteria.Shyw == "0")
                {
                    if (VehicleInspectionController.SyAj != "1")
                    {
                        responseData.Code = "-9";
                        responseData.Message = "检测站不包含安检业务";
                        return moderationQueueR013s.ToArray();
                    }
                    DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                    string sql = "select t1.lsh as ajlsh,'-' as hjlsh,t1.hpzl,t1.hphm,t2.clsbdh ,t2.cllx,t3.Jccs as ajjccs,'0' as hjjccs ,t3.ajywlb as ajywlb,'-' as hjywlb,t1.sqr as ajsqr ";
                    sql += " ,'-' as hjsqr, t1.sqsj as ajsqsj,'-' as hjsqsj,convert(varchar(10), t3.Jcrq) + ' ' + convert(varchar(10), t3.JcTime) as ajjcsj,'-' as hjjcsj ";
                    sql += " from T_AuditStatus t1,BaseInfo_Net t2, QcyJcDateCover t3 ";
                    sql += " where t1.Lsh = t2.Lsh and t1.lsh = t3.lsh ";
                    if (!string.IsNullOrEmpty(queryCriteria.Hphm))
                    {
                        sql += " and t1.hphm like '%" + queryCriteria.Hphm + "%'";
                    }
                    else
                    {
                        sql += " and Convert(date,sqsj)='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                    }
                    sql += " and t1.shzt='1'";
                    //moderationQueueR013s = dbAj.QueryForList<ModerationQueueR013>(sql, null);
                    dataTableAj = dbAj.ExecuteDataTable(sql, null);
                }
                if (queryCriteria.Shyw == "0" || queryCriteria.Shyw == "2")
                {
                    if (VehicleInspectionController.SyHj != "1")
                    {
                        responseData.Code = "-10";
                        responseData.Message = "检测站不包含环检业务";
                        return moderationQueueR013s.ToArray();
                    }
                    DbUtility dbHj = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    string sql = "select t1.lsh as hjlsh,'-' as ajlsh,t1.hpzl,t1.hphm,t2.clsbdh ,t2.cllx,'0' as ajjccs,t3.Jccs as hjjccs ";
                    sql += " ,'-' as ajywlb,t3.Jclb as hjywlb,'-' as ajsqr,t1.sqr as hjsqr, '-' as ajsqsj,t1.sqsj as hjsqsj ";
                    sql += " ,convert(varchar(10), t3.JcDate) + ' ' + convert(varchar(10), t3.JcTime) as hjjcsj ,'-' as ajjcsj ";
                    sql += " from T_AuditStatus t1,BaseInfo_Net t2, LY_Flow_Info  t3 ";
                    sql += " where t1.Lsh = t2.Lsh and t1.lsh = t3.lsh ";
                    if (!string.IsNullOrEmpty(queryCriteria.Hphm))
                    {
                        sql += " and t1.hphm like '%" + queryCriteria.Hphm + "%'";
                    }
                    else
                    {
                        sql += " and Convert(date,sqsj)='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                    }
                    sql += " and t1.shzt='1'";
                    dataTableHj = dbHj.ExecuteDataTable(sql, null);
                }
                //合并两个表
                if (dataTableAj.Rows.Count == 0)
                {
                    moderationQueueR013s = EntityReader.GetEntities<ModerationQueueR013>(dataTableHj);
                }
                else
                {
                    DataTable dtAll = UniteDataTableLYYDJKR013(dataTableAj, dataTableHj, "dtAll");
                    moderationQueueR013s = EntityReader.GetEntities<ModerationQueueR013>(dtAll);
                }
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
                responseData.RowNum = moderationQueueR013s.Count;
            }
            catch (ArgumentNullException ex)
            {
                responseData.Code = "1";
                responseData.Message = ex.Message;
                responseData.RowNum = moderationQueueR013s.Count;
            }
            catch (Exception e)
            {
                responseData.Code = "-99";
                responseData.Message = e.Message;
            }
            return moderationQueueR013s.ToArray();
        }
        /// <summary>
        /// 合并安检环检表
        /// </summary>
        /// <param name="Aj"></param>
        /// <param name="Hj"></param>
        /// <param name="DTName"></param>
        /// <returns></returns>
        private DataTable UniteDataTableLYYDJKR013(DataTable Aj, DataTable Hj, string DTName)
        {
            DataTable dt3 = Aj.Clone();
            object[] obj = new object[dt3.Columns.Count];

            for (int i = 0; i < Aj.Rows.Count; i++)
            {
                Aj.Rows[i].ItemArray.CopyTo(obj, 0);
                dt3.Rows.Add(obj);
            }
            for (int i = 0; i < Hj.Rows.Count; i++)
            {
                var rows = dt3.AsEnumerable()
                            .Where(p => p.Field<string>("hphm") == Hj.Rows[i]["hphm"].ToString() && p.Field<string>("hpzl") == Hj.Rows[i]["hpzl"].ToString());

                if (rows.Count() > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        dr["hjlsh"] = Hj.Rows[i]["hjlsh"].ToString();
                        dr["hjywlb"] = Hj.Rows[i]["hjywlb"].ToString();
                        dr["hjjccs"] = Hj.Rows[i]["hjjccs"].ToString();
                        dr["Hjsqr"] = Hj.Rows[i]["Hjsqr"].ToString();
                        dr["Hjsqsj"] = Hj.Rows[i]["Hjsqsj"].ToString();
                        dr["Hjjcsj"] = Hj.Rows[i]["Hjjcsj"].ToString();
                    }
                }
                else
                {
                    DataRow dr = dt3.NewRow();
                    dr["ajlsh"] = "-";
                    dr["hjlsh"] = Hj.Rows[i]["hjlsh"].ToString();
                    dr["hphm"] = Hj.Rows[i]["hphm"].ToString();
                    dr["hpzl"] = Hj.Rows[i]["hpzl"].ToString();
                    dr["Clsbdh"] = Hj.Rows[i]["Clsbdh"].ToString();
                    dr["Cllx"] = Hj.Rows[i]["Cllx"].ToString();
                    dr["ajjccs"] = 0;
                    dr["hjjccs"] = Hj.Rows[i]["hjjccs"].ToString();
                    dr["ajywlb"] = "-";
                    dr["hjywlb"] = Hj.Rows[i]["hjywlb"].ToString();
                    dr["Ajsqr"] = "-";
                    dr["Hjsqr"] = Hj.Rows[i]["Hjsqr"].ToString();
                    dr["Hjsqsj"] = Hj.Rows[i]["Hjsqsj"].ToString();
                    dr["Hjjcsj"] = Hj.Rows[i]["Hjjcsj"].ToString();

                    dt3.Rows.Add(dr);
                }
            }
            dt3.TableName = DTName; //设置DT的名字
            return dt3;
        }
        /// <summary>
        /// 查询收费情况
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        public ChargePayment[] LYYDJKR014(RequestData requestData, ResponseData responseData)
        {
            List<ChargePayment> chargePayments = new List<ChargePayment>();
            try
            {
                string sql = "";
                QueryVehicleCriteria vehicleCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                DbUtility db = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                if (!string.IsNullOrEmpty(vehicleCriteria.Oid))
                {
                    string sqlStr = "select count(*) from [dbo].[tb_ChargeTransactionResults] where bizseq ='" + vehicleCriteria.Oid + "' and trxstatus ='0000' ";
                    int countR1 = Convert.ToInt32(db.ExecuteScalar(sqlStr, null));
                    sqlStr = "select count(*) from [dbo].[tb_ChargeTransactionReturn] where orderid ='" + vehicleCriteria.Oid + "' and trxstatus ='0000'";
                    int countR2 = Convert.ToInt32(db.ExecuteScalar(sqlStr, null));
                    if (countR1 > 0 | countR2 > 0)
                    {
                        responseData.Code = "1";
                        responseData.Message = "SUCCESS";
                        //sql = "select top 1 *,lsh as ajlsh from [dbo].[tb_ChargePaymentOrders] where oid ='" + vehicleCriteria.Oid  + "' order by addtime desc";
                        //ChargeOrder chargeOrder = db.QueryForObject<ChargeOrder>(sql, null);
                        //sql = "select * from [dbo].[tb_ChargePaymentDetail] where orderno ='" + chargeOrder.Oid + "'";
                        //List<ChargeDetail> chargeDetails = db.QueryForList<ChargeDetail>(sql, null);
                        //ChargePayment chargePayment = new ChargePayment()
                        //{
                        //    chargeDetails = chargeDetails.ToArray(),
                        //    chargeOrder = chargeOrder
                        //};
                        //chargePayments.Add(chargePayment);
                    }
                    else
                    {
                        responseData.Code = "1";
                        responseData.Message = "waiting...";

                        //sql = "select top 1 *,lsh as ajlsh from [dbo].[tb_ChargePaymentOrders] where oid ='" + vehicleCriteria.Oid + "' order by addtime desc";
                        //ChargeOrder chargeOrder = db.QueryForObject<ChargeOrder>(sql, null);
                        //sql = "select orderno as Orderno,detailno as Detailno,goodsno as Goodsno,goodsname as Goodsname,num as Num,price as Price,zje as Zje  from [dbo].[tb_ChargePaymentDetail] where orderno ='" + chargeOrder.Oid + "'";
                        //List<ChargeDetail> chargeDetails = db.QueryForList<ChargeDetail>(sql, null);
                        //ChargePayment chargePayment = new ChargePayment()
                        //{
                        //    chargeDetails = chargeDetails.ToArray(),
                        //    chargeOrder = chargeOrder
                        //};
                        //chargePayments.Add(chargePayment);
                    }
                }
                if (!string.IsNullOrEmpty(vehicleCriteria.Ajlsh))
                {
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                    sql = "select * from baseinfo_hand where lsh='" + vehicleCriteria.Ajlsh + "' and sfjf='1'";
                    DataTable dt = db.ExecuteDataTable(sql, null);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "select top 1 *,lsh as ajlsh from [dbo].[tb_ChargePaymentOrders] where lsh ='" + vehicleCriteria.Ajlsh + "' order by addtime desc";
                        ChargeOrder chargeOrder = db.QueryForObject<ChargeOrder>(sql, null);
                        sql = "select * from [dbo].[tb_ChargePaymentDetail] where orderno ='" + chargeOrder.Oid + "'";
                        List<ChargeDetail> chargeDetails = db.QueryForList<ChargeDetail>(sql, null);
                        ChargePayment chargePayment = new ChargePayment()
                        {
                            chargeDetails = chargeDetails.ToArray(),
                            chargeOrder = chargeOrder
                        };
                        chargePayments.Clear();
                        chargePayments.Add(chargePayment);
                    }
                    else
                    {
                        responseData.Code = "0";
                        responseData.Message = "查询无数据";
                    }
                }
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
            return chargePayments.ToArray();
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
                    sql = "select top 1 Jyjgbh,Jcsjyxq,Jcsjbcnx,Web_Pass,Dw_Xkzh,Dw_Dhhm,Dw_mc,Dw_dz,SFF,KPF,LshSzm,'AJ' as sjlb ,t2.appid,t2.md5key,t2.c ,t2.c1  from SystermCs_All,tb_ChargeSystemCS t2";
                    systemParameter = db.QueryForObject<SystemParameter>(sql, null);
                    systemParametes.Add(systemParameter)
        ;
                }
                if (VehicleInspectionController.SyHj == "1")
                {
                    DbUtility db = new DbUtility(VehicleInspectionController.ConstrHj, DbProviderType.SqlServer);
                    sql = "select top 1 bz1 as Jyjgbh,JcDateYouXiao as Jcsjyxq,'' as Jcsjbcnx,bz2 as Web_Pass,Dw_Xkzh,Dw_Dhhm,Report_Head as Dw_mc,''as Dw_dz,''as SFF,'' as KPF,'' as LshSzm ,'HJ' as sjlb ,t2.appid,t2.md5key,t2.c ,t2.c1  from SystermCs_All,tb_ChargeSystemCS t2 ";
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
            string sql;
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
            string sql;
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (string.IsNullOrEmpty(queryCriteria.Ajlsh) || string.IsNullOrEmpty(queryCriteria.Hjlsh))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajlsh/Hjlsh）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (string.IsNullOrEmpty(queryCriteria.Jyxm))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Jyxm）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (string.IsNullOrEmpty(queryCriteria.Ajywlb))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return inspectionPhotoR017s.ToArray();
                }
                if (string.IsNullOrEmpty(queryCriteria.Hjywlb))
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
                        sql = " select wgchx,ajywlb  from BaseInfo_Hand where lsh='" + queryCriteria.Ajlsh + "'";
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
                        sql = "select zs from LY_Flow_Info where Lsh='" + queryCriteria.Hjlsh + "'";
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
                    sql = " select wgchx  from BaseInfo_Hand where lsh='" + queryCriteria.Ajlsh + "'";
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
                    sql = "select zs from LY_Flow_Info where Lsh='" + queryCriteria.Hjlsh + "'";
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
            string sql;
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
                    sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ,jyxm,Yqsc from ( " +
                   " select 'F1' as Jyxm,Yqsc = Time_Wg,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'C1' as Jyxm,Yqsc = Time_Dp,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'DC' as Jyxm,Yqsc = Time_Dt,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'B' as Jyxm,Yqsc = Time_Zd,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'H' as Jyxm,Yqsc = Time_Dg,CxmsDm from[dbo].[Time_Jc_CxCs] " +
                   " ) as t1 where CxmsDm = '" + queryCriteria.Ajcx + "' and jyxm='" + queryCriteria.Jyxm + "'";
                }
                else
                {
                    sql = "select ROW_NUMBER()OVER(ORDER BY (select 0)) as id ,jyxm,Yqsc  from ( " +
                   " select 'F1' as Jyxm,Yqsc = Time_Wg,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'C1' as Jyxm,Yqsc = Time_Dp,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'DC' as Jyxm,Yqsc = Time_Dt,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'B' as Jyxm,Yqsc = Time_Zd,CxmsDm from[dbo].[Time_Jc_CxCs] union all " +
                   " select 'H' as Jyxm,Yqsc = Time_Dg,CxmsDm from[dbo].[Time_Jc_CxCs] " +
                   " ) as t1 where CxmsDm = '" + queryCriteria.Ajcx + "' ";
                }

                inspectionDurationR019s = dbAj.QueryForList<InspectionDurationR019>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";

            }
            catch (ArgumentNullException)
            {
                responseData.Code = "-100";
                responseData.Message = "没有查询到符合条件的数据信息！";
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
            string sql;
            string mrhgStr;
            try
            {
                QueryVehicleCriteria queryCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);

                if (string.IsNullOrEmpty(queryCriteria.Ajywlb))
                {
                    responseData.Code = "-1";
                    responseData.Message = "查询条件不能全部为空（Ajywlb）";
                    return artificialProjectR016s.ToArray();
                }
                if (string.IsNullOrEmpty(queryCriteria.Hjywlb))
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
                        sql = " select wgchx,ajywlb  from BaseInfo_Hand where lsh='" + queryCriteria.Ajlsh + "'";
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
                                sql += " ,'" + wjcx + "' as cartype ,(select mc from jscscode where fl = '60' and dm = '" + wjcx + "' ) as wjcxmc  ";
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
                                sql = " select stuff((SELECT ','+itemdm FROM [dbo].[tb_cartypeanditemdm] where cartype='" + wjcx + "' and fldm='1'  FOR XML PATH('')),1,1,'')";
                                mrhgStr = dbAj.ExecuteScalar(sql, null).ToString();
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
                else
                {
                    artificialProjectR016.ID = 1;
                    artificialProjectR016.Jyyw = "安检";
                    List<ArtificialProject> artificialProjects = new List<ArtificialProject>();
                    artificialProjectR016.Xmlb = artificialProjects.ToArray();
                    artificialProjectR016s.Add(artificialProjectR016);
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
        /// <summary>
        /// 查询APK版本号
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public AppVersion[] LYYDJKR021(ResponseData responseData)
        {
            List<AppVersion> appVersions = new List<AppVersion>();
            try
            {
                AppVersion appVersion = new AppVersion();
                ApkDecoder apkDecoder = new ApkDecoder();
                appVersion.Version = apkDecoder.AppVersion;
                appVersion.VersionCode = apkDecoder.AppVersionCode;
                appVersions.Add(appVersion);
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
            return appVersions.ToArray();
        }
        /// <summary>
        /// 机动车信息联网查询(安检平台)
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public VehicleDetails[] LYYDJKR022(RequestData requestData, ResponseData responseData)
        {
            List<VehicleDetails> vehicleDetails = new List<VehicleDetails>();
            SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
            string code = "-1";
            try
            {
                NetworkQueryR022 networkQueryR022 = JSONHelper.ConvertObject<NetworkQueryR022>(requestData.Body[0]);
                string xmlDocStr = XMLHelper.XmlSerializeStr<NetworkQueryR022>(networkQueryR022, "Query");
                //xmlDocStr = HttpUtility.UrlEncode(xmlDocStr);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlDocStr);
                xmlDocument.Save(@"D:\TestXml\18C49_S.xml");
                //调用接口
                //string resultXml = CallingSecurityInterface.WriteObjectOutNew("18", systemParameterAj.Jkxlh , "18C49", xmlDocStr);
                //resultXml = HttpUtility.UrlEncode(resultXml);
                ////分析返回结果
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(resultXml);
                //doc.Save(@"D:\TestXml\18C49_R.xml");

                XmlDocument doc = new XmlDocument();
                doc.Load(@"D:\TestXml\18C49_R.xml");
                code = XMLHelper.GetNodeValue(doc, "code");
                if (code == "1")
                {
                    VehicleDetails o = XMLHelper.DESerializer<VehicleDetails>(XMLHelper.GetNodeXML(doc, "body"));
                    vehicleDetails.Add(o);
                    responseData.Code = "1";
                    responseData.Message = "SUCCESS";
                }
                else
                {
                    responseData.Code = code;
                    responseData.Message = XMLHelper.GetNodeValue(doc, "Message");
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
        /// 查询行政区划
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public AdministrativeRegionR023[] LYYDJKR023(ResponseData responseData)
        {
            List<AdministrativeRegionR023> administrativeRegionR023s = new List<AdministrativeRegionR023>();
            string sql;
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                sql = "select * from tb_xzqh ";
                administrativeRegionR023s = dbAj.QueryForList<AdministrativeRegionR023>(sql, null);
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
            return administrativeRegionR023s.ToArray();
        }
        /// <summary>
        /// 查询机动车线上检验进度
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public InspectionProgressR024[] LYYDJKR024(RequestData requestData, ResponseData responseData)
        {
            List<InspectionProgressR024> inspectionProgressR024s = new List<InspectionProgressR024>();
            InspectionProgressR024 inspectionProgressR024 = new InspectionProgressR024();
            try
            {
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                QueryVehicleCriteria vehicleCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (string.IsNullOrEmpty(vehicleCriteria.Ajlsh))
                {
                    responseData.Code = "-8";
                    responseData.Message = "参数不能为空:Ajlsh";
                    return inspectionProgressR024s.ToArray();
                }
                string sql = "select * from LY_Flow_Info where lsh='" + vehicleCriteria.Ajlsh + "'";
                DataTable dtLy = dbAj.ExecuteDataTable(sql, null);
                if (dtLy.Rows.Count <= 0)
                {
                    responseData.Code = "-3";
                    responseData.Message = "不存在对应流水号的检测车辆信息:" + vehicleCriteria.Ajlsh;
                    return inspectionProgressR024s.ToArray();
                }
                //检测线号
                string jcxh = dtLy.Rows[0]["SB_TD"].ToString();
                //线号不为空
                if (string.IsNullOrEmpty(jcxh) || jcxh == "-")
                {
                    responseData.Code = "-1";
                    responseData.Message = "机动车还没有上线:" + vehicleCriteria.Ajlsh;
                    return inspectionProgressR024s.ToArray();
                }
                //查询工位数目
                sql = "select count(*) from [dbo].[tb_workspaceinformation] where jcxh='" + jcxh + "'";
                int gws = Convert.ToInt32(dbAj.ExecuteScalar(sql, null));
                if (gws <= 0)
                {
                    responseData.Code = "-1";
                    responseData.Message = "检测线没有工位设置，线号：" + gws;
                    return inspectionProgressR024s.ToArray();
                }
                //设置工位
                List<StationStatus> stationStatuses = new List<StationStatus>();
                for (int i = 1; i <= gws; i++)
                {
                    StationStatus stationStatus = new StationStatus();
                    switch (i)
                    {
                        case 1:
                            stationStatus.Gwmc = "一工位";
                            stationStatus.Gwzt = dtLy.Rows[0]["GW_01"].ToString();
                            break;
                        case 2:
                            stationStatus.Gwmc = "二工位";
                            stationStatus.Gwzt = dtLy.Rows[0]["GW_02"].ToString();
                            break;
                        case 3:
                            stationStatus.Gwmc = "三工位";
                            stationStatus.Gwzt = dtLy.Rows[0]["GW_03"].ToString();
                            break;
                    }
                    stationStatuses.Add(stationStatus);
                }
                inspectionProgressR024.Ajlsh = dtLy.Rows[0]["lsh"].ToString();
                inspectionProgressR024.Jcgw = stationStatuses.ToArray();
                inspectionProgressR024.Xszt = dtLy.Rows[0]["isonline"].ToString();
                inspectionProgressR024s.Add(inspectionProgressR024);
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
            return inspectionProgressR024s.ToArray();
        }
        /// <summary>
        /// 查询开票参数
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public InvoiceParameters[] LYYDJKR025(ResponseData responseData)
        {
            List<InvoiceParameters> invoiceParameters = new List<InvoiceParameters>();
            try
            {
                DbUtility dnAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = "select Cs_01 as Skdwsbh,Cs_02 as Shr,Cs_03 as Skdwmc,Cs_04 as Bmdm,Cs_05 as Spbm,Cs_08 as Dh,Cs_09 as Dz,Cs_10 as Khh,Cs_12 as Fpjksfsbm,'0.06' as Sl from FP_SysCs";
                invoiceParameters = dnAj.QueryForList<InvoiceParameters>(sql, null);
                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "-1";
                responseData.Message = "没有查到参数！";
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
            return invoiceParameters.ToArray();
        }
        /// <summary>
        /// 查询客户单位开票信息
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="responseData"></param>
        /// <returns></returns>
        public EInvoiceBuyer[] LYYDJKR026(RequestData requestData, ResponseData responseData)
        {
            List<EInvoiceBuyer> invoiceBuyers = new List<EInvoiceBuyer>();
            try
            {
                QueryVehicleCriteria queryVehicleCriteria = JSONHelper.ConvertObject<QueryVehicleCriteria>(requestData.Body[0]);
                if (string.IsNullOrEmpty(queryVehicleCriteria.Buyername))
                {
                    responseData.Code = "-8";
                    responseData.Message = "参数不能为空:Buyername";
                    return invoiceBuyers.ToArray();
                }
                DbUtility dbAj = new DbUtility(VehicleInspectionController.ConstrAj, DbProviderType.SqlServer);
                string sql = "select * from T_EInvoiceBuyer where buyername='" + queryVehicleCriteria.Buyername + "'";
                invoiceBuyers = dbAj.QueryForList<EInvoiceBuyer>(sql, null);

                responseData.Code = "1";
                responseData.Message = "SUCCESS";
            }
            catch (ArgumentNullException)
            {
                responseData.Code = "0";
                responseData.Message = "没有查到参数！";
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
            return invoiceBuyers.ToArray();
        }
    }
}
