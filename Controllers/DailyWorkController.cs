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
    public class DailyWorkController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public DailyWorkController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<dailyWork> Get()
        {
            return _MyContext.dailyWork;
        }

        // POST: api/
        [HttpPost("AddDailyWork")]
        [AllowAnonymous]
        public async Task<IActionResult> AddDailyWork([FromBody] dailyWork model)
        {
            var res = await _MyContext.teacher.Where(u => u.id == model.teacherId).FirstOrDefaultAsync();
            if (res == null) {
                return new OkObjectResult(new { Message = "Not found teacher" });
            }

            var guid = Guid.NewGuid().ToString();

            var m = new dailyWork
            {
                id = guid,
                teacherId = model.teacherId,
                subjectId = model.subjectId,
                subjectName = model.subjectName,
                date = model.date,
                lesson = model.lesson,
                room = model.room,
                state = model.state,
                startTime = model.startTime,
                endTime = model.endTime
            };

            m.state = 0;
            
            _MyContext.dailyWork.Add(m);
            

            var x = _MyContext.SaveChanges();

            if (x != null)
            {
                return new OkObjectResult(new { Message = "Success" });
            }
            else
            {
                return new OkObjectResult(new { Message = "Fail" });
            }
        }

        // GET: api/
        [HttpGet("GetDailyWork")]
        public async Task<ActionResult<dailyWork>> GetDailyWork(string id)
        {
            var m = await _MyContext.dailyWork.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        // GET: api/
        [HttpGet("GetDailyWorkByTeacherId")]
        public async Task<List<dailyWork>> GetDailyWorkByTeacherId(string teacherId)
        {
            var m = await _MyContext.dailyWork.Where(u => u.teacherId == teacherId).ToListAsync();

            return m;
        }

        [HttpPut("EditDailyWork")]
        [AllowAnonymous]
        public async Task<IActionResult> EditDailyWork([FromBody] dailyWork model)
        {
            var m = await _MyContext.dailyWork.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteDailyWork")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDailyWork(string id)
        {
            var m = await _MyContext.dailyWork.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.dailyWork.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
