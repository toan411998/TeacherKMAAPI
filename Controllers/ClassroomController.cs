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
    public class ClassroomController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public ClassroomController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<classroom> Get()
        {
            return _MyContext.classroom;
        }

        // POST: api/
        [HttpPost("AddClassroom")]
        [AllowAnonymous]
        public async Task<IActionResult> AddClassroom([FromBody] classroom model)
        {            
            var guid = Guid.NewGuid().ToString();

            var m = new classroom
            {
                id = guid,
                name = model.name,
                description = model.description
            };
            var res = await _MyContext.classroom.Where(u => u.name == model.name).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "Classroom is exist" });
            }
            else
            {
                _MyContext.classroom.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetClassroom")]
        public async Task<ActionResult<classroom>> GetClassroom(string id)
        {          
            var user = await _MyContext.classroom.Where(u => u.id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("EditClassroom")]
        [AllowAnonymous]
        public async Task<IActionResult> EditClassroom([FromBody] classroom model)
        {            
            var m = await _MyContext.classroom.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();
            //var user = await _MyContext.users.AsNoTracking().Where(u => u.username == model.username).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {            
                _MyContext.Update(model);
                await _MyContext.SaveChangesAsync();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

        // POST: api/
        [HttpDelete("DeleteClassroom")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteClassroom(string id)
        {
            var m = await _MyContext.classroom.Where(u => u.id == id).FirstOrDefaultAsync();
            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.classroom.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
