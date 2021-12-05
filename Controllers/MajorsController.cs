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
    public class MajorsController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public MajorsController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<majors> Get()
        {
            return _MyContext.majors;
        }

        // POST: api/
        [HttpPost("AddMajors")]
        [AllowAnonymous]
        public async Task<IActionResult> AddMajors([FromBody] majors model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new majors
            {
                id = guid,
                name = model.name,
                description = model.description,
                image = model.image
            };
       
            _MyContext.majors.Add(m);
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
        [HttpGet("GetMajors")]
        public async Task<ActionResult<majors>> GetMajors(string id)
        {
            var m = await _MyContext.majors.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        [HttpPut("EditMajors")]
        [AllowAnonymous]
        public async Task<IActionResult> EditMajors([FromBody] majors model)
        {
            var m = await _MyContext.majors.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteMajors")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteMajors(string id)
        {
            var m = await _MyContext.majors.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.majors.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
