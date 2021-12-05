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
    public class MajorsTeacherController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public MajorsTeacherController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<majorsTeacher> Get()
        {
            return _MyContext.majorsTeacher;
        }

        // POST: api/
        [HttpPost("AddMajorsTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> AddMajorsTeacher([FromBody] majorsTeacher model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new majorsTeacher
            {
                id = guid,
                majorsId = model.majorsId,
                teacherId = model.teacherId,
                role = model.role
            };
            var res = await _MyContext.majorsTeacher.Where(u => u.majorsId == model.majorsId && u.teacherId == model.teacherId).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "MajorsTeacher is exist" });
            }
            else
            {
                _MyContext.majorsTeacher.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetMajorsTeacher")]
        public async Task<ActionResult<majorsTeacher>> GetMajorsTeacher(string id)
        {
            var m = await _MyContext.majorsTeacher.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        [HttpPut("EditMajorsTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> EditMajorsTeacher([FromBody] majorsTeacher model)
        {
            var m = await _MyContext.majorsTeacher.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteMajorsTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteMajorsTeacher(string id)
        {
            var m = await _MyContext.majorsTeacher.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.majorsTeacher.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
