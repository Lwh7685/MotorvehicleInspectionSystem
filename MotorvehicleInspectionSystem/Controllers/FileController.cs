using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
using MotorvehicleInspectionSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MotorvehicleInspectionSystem.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// 上传文件，返回地址
        /// </summary>
        /// <param name="objFile">文件</param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        public string UploadFile([FromForm] IFormFile objFile)
        {
            try
            {
                if (objFile.Length > 0)
                {
                    SystemParameterAj systemParameterAj = SystemParameterAj.m_instance;
                    string pathF = "D:\\Upload";
                    if (!string.IsNullOrEmpty(systemParameterAj.Spbcdz))
                    {
                        pathF = systemParameterAj.Spbcdz;
                    }
                    string path = pathF + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + objFile.FileName.Split("_")[0] + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string aa = objFile.FileName.Split(".")[0].Split("-")[0] + "." + objFile.FileName.Split(".")[1];
                    using (FileStream filesStream = System.IO.File.Create(path + aa))
                    {
                        objFile.CopyTo(filesStream);
                        filesStream.Flush();
                        return "1"+ path + aa;
                    }
                }
                else
                {
                    return "0false";
                }
            }
            catch (Exception ex)
            {
                return "0"+ex.Message;
            }
        }

        /// <summary>
        /// 文件流的方式输出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DownloadFile()
        {
            var addrUrl = Path.Combine($"{Environment.CurrentDirectory}", @"apks\app-release.apk");
            var stream = System.IO.File.OpenRead(addrUrl);
            return File(stream, "application/vnd.android.package-archive", Path.GetFileName(addrUrl));
        }

    }
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }

    }

}

