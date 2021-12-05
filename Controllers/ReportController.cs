using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyGiangVien.Model;
using Swagger.Demo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public ReportController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<report> Get()
        {
            return _MyContext.report;
        }

        // POST: api/
        [HttpPost("AddReport")]
        [AllowAnonymous]
        public async Task<IActionResult> AddReport([FromBody] report model)
        {
            var guid = Guid.NewGuid().ToString();

            var m = new report
            {
                id = guid,
                title = model.title,
                content = model.content,
                date = model.date
            };

            _MyContext.report.Add(m);
            var x = _MyContext.SaveChanges();
            if (x != null) {
                return new OkObjectResult(new { Message = "Success" });
            }
            else
            {
                return new OkObjectResult(new { Message = "Fail" });
            }
        }

        // GET: api/
        [HttpGet("GetReport")]
        public async Task<ActionResult<report>> GetReport(string id)
        {
            var m = await _MyContext.report.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        [HttpPut("EditReport")]
        [AllowAnonymous]
        public async Task<IActionResult> EditReport([FromBody] report model)
        {
            var m = await _MyContext.report.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                model.id = m.id;
                m = model;
                _MyContext.Update(m);
                await _MyContext.SaveChangesAsync();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

        // POST: api/
        [HttpDelete("DeleteReport")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteReport(string id)
        {
            var m = await _MyContext.report.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.report.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

    }
}
