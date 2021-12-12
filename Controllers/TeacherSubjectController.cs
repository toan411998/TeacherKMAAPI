using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuanLyGiangVien.Model;
using Swagger.Demo.Context;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherSubjectController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public TeacherSubjectController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }
        // GET: api/
        [HttpGet]
        public IEnumerable<teacherSubject> Get()
        {
            return _MyContext.teacherSubject;
        }

        // POST: api/
        [HttpPost("AddTeacherSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> AddTeacherSubject([FromBody] teacherSubject model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new teacherSubject
            {
                id = guid,
                teacherId = model.teacherId,
                subjectId = model.subjectId,
                year = model.year,
                semester = model.semester,
                room = model.room
            };
            var res = await _MyContext.teacherSubject.Where(u => u.teacherId == model.teacherId 
                && u.subjectId == model.subjectId 
                && u.year == model.year
                && u.semester == model.semester
                && u.room == model.room
            ).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "TeacherSubject is exist" });
            }
            else
            {
                _MyContext.teacherSubject.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetTeacherSubject")]
        public async Task<ActionResult<teacherSubject>> GetTeacherSubject(string id)
        {
            var m = await _MyContext.teacherSubject.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        // GET: api/
        [HttpGet("GetTeacherSubjectByTeacherID")]
        public async Task<List<teacherSubject>> GetTeacherSubjectByTeacherID(string teacherId)
        {
            var m = await _MyContext.teacherSubject.Where(u => u.teacherId == teacherId).ToListAsync();
            return m;
        }

        [HttpPut("EditTeacherSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> EditTeacherSubject([FromBody] teacherSubject model)
        {
            var m = await _MyContext.teacherSubject.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteTeacherSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTeacherSubject(string id)
        {
            var m = await _MyContext.teacherSubject.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.teacherSubject.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

    }
}
