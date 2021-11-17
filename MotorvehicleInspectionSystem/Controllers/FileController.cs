using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorvehicleInspectionSystem.Data;
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

        [HttpPost]
        public string UploadFile([FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                   
                    string pathF  = "D:\\Upload\\";
                    string path = pathF + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + objFile.files.FileName.Split("_")[0] + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filesStream = System.IO.File.Create(path + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(filesStream);
                        filesStream.Flush();
                        return path + objFile.files.FileName;
                    }
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }


        [HttpPost]
        [DisableRequestSizeLimit]
        public string UploadFile2([FromForm] IFormFile objFile)
        {
            try
            {
                if (objFile.Length > 0)
                {

                    string pathF = "D:\\Upload\\";
                    string path = pathF + "\\" + DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" + DateTime.Now.Day.ToString() + "\\" + objFile.FileName.Split("_")[0] + "\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filesStream = System.IO.File.Create(path + objFile.FileName))
                    {
                        objFile.CopyTo(filesStream);
                        filesStream.Flush();
                        return path + objFile.FileName;
                    }
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }


        [HttpPost]
        public string PostFile([FromForm] IFormCollection formCollection)
        {
            string result = "";
            string webRootPath = "D:\\Upload\\";

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            foreach (IFormFile file in filelist)
            {
                String Tpath = "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                string name = file.FileName;
                string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string FilePath = webRootPath + Tpath;

                string type = System.IO.Path.GetExtension(name);
                DirectoryInfo di = new DirectoryInfo(FilePath);
                if (!di.Exists) { di.Create(); }
                using (FileStream fs = System.IO.File.Create(FilePath + FileName + type))
                {
                    // 复制文件
                    file.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }
                result = "1";
            }
            return result;
        }
    }
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }

    }

}

