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
    }
    public class FileUploadAPI
    {
        public IFormFile files { get; set; }

    }
}
