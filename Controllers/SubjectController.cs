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
    public class SubjectController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public SubjectController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<subject> Get()
        {
            return _MyContext.subject;
        }

        // POST: api/
        [HttpPost("AddSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSubject([FromBody] subject model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new subject
            {
                id = guid,
                name = model.name,
                image = model.image,
                numberOfLesson = model.numberOfLesson,
                type = model.type
            };
            var res = await _MyContext.teacher.Where(u => u.name == model.name).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "Subject is exist" });
            }
            else
            {
                _MyContext.subject.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetSubject")]
        public async Task<ActionResult<subject>> GetSubject(string name)
        {
            var m = await _MyContext.subject.Where(u => u.name == name).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        // GET: api/
        [HttpGet("GetSubjectByID")]
        public async Task<ActionResult<subject>> GetSubjectByID(string id)
        {
            var m = await _MyContext.subject.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        // GET: api/
        [HttpGet("GetTeachSubject")]
        public async Task<List<subject>> GetTeachSubject()
        {
            var m = await _MyContext.subject.Where(u => u.type == null).ToListAsync();
            return m;
        }

        // GET: api/
        [HttpGet("GetStudySubject")]
        public async Task<List<subject>> GetStudySubject()
        {
            var m = await _MyContext.subject.Where(u => u.type == "study").ToListAsync();
            return m;
        }

        [HttpPut("EditSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> EditSubject([FromBody] subject model)
        {
            var m = await _MyContext.subject.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteSubject")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            var m = await _MyContext.subject.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.subject.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
