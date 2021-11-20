using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpressController : ControllerBase
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ExpressController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Administrador,Owner,Collaborator,Reader")]
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFiles(IFormCollection files)
        {
            try
            {
                foreach (var file in files.Files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Express/DownloadFile/ASSAA12SASA
        [HttpGet("[action]/{file}")]
        public async Task<byte[]> DownloadFile([FromRoute] string file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", file);
            var stream = new FileStream(path, FileMode.Open);
            using (var ms = new MemoryStream(2048))
            {
                await stream.CopyToAsync(ms);
                stream.Close();
                return ms.ToArray();
            }
        }

        [Authorize(Roles = "Administrador,Owner,Collaborator,Reader")]
        [HttpDelete("[action]/{file}")]
        public IActionResult DeleteFile([FromRoute] string file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", file);
            var fileInfo = new FileInfo(path);
            fileInfo.Delete();
            return Ok();
        }

    }
}
