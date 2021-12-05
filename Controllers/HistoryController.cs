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
    public class HistoryController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public HistoryController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<history> Get()
        {
            return _MyContext.history;
        }

        // POST: api/
        [HttpPost("AddHistory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddHistory([FromBody] history model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new history
            {
                id = guid,
                action = model.action,
                date = model.date,
                time = model.time,
                teacherId = model.teacherId
            };
            
            _MyContext.history.Add(m);
            
            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetHistory")]
        public async Task<ActionResult<history>> GetHistory(string id)
        {
            var m = await _MyContext.history.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        // POST: api/
        [HttpDelete("DeleteHistory")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteHistory(string id)
        {
            var m = await _MyContext.history.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.history.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
