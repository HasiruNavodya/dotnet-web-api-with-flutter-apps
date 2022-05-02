﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLBFE_API.Controllers
{
    [Route("documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DocumentsController(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPut,Route("upload")]
        public ActionResult SaveFile(int NIC, string documentType)
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/FileStorage/"+filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return Ok("Document Uploaded Succesfully");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpGet("download")]
        public async Task<ActionResult> DownloadFile(int NIC, string documentType)
        {
            var filePath = $"FileStorage/JobSeekers/{NIC}/Documents/{NIC}SampleDoc.pdf";

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }

        [HttpGet("profilepic")]
        public async Task<ActionResult> DownloadProfilePicture(int NIC)
        {
            var filePath = _webHostEnvironment.ContentRootPath + $"/FileStorage/JobSeekers/{NIC}/ProfilePicture/propic.jpg";

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
            //return Ok(filePath);
        }
    }
}
