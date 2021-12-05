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
    public class MajorsImageController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public MajorsImageController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<majorsImage> Get()
        {
            return _MyContext.majorsImage;
        }

        // POST: api/
        [HttpPost("AddMajorsImage")]
        [AllowAnonymous]
        public async Task<IActionResult> AddMajorsImage([FromBody] majorsImage model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new majorsImage
            {
                id = guid,
                majorsId = model.majorsId,
                image = model.image
            };
            
            _MyContext.majorsImage.Add(m);
            

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
        [HttpGet("GetMajorsImage")]
        public async Task<ActionResult<majorsImage>> GetMajorsImage(string id)
        {
            var m = await _MyContext.majorsImage.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        [HttpPut("EditMajorsImage")]
        [AllowAnonymous]
        public async Task<IActionResult> EditMajorsImage([FromBody] majorsImage model)
        {
            var m = await _MyContext.majorsImage.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteMajorsImage")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteMajorsImage(string id)
        {
            var m = await _MyContext.majorsImage.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.majorsImage.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
