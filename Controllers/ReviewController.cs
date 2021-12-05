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
    public class ReviewController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public ReviewController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<review> Get()
        {
            return _MyContext.review;
        }

        // POST: api/
        [HttpPost("AddReview")]
        [AllowAnonymous]
        public async Task<IActionResult> AddReview([FromBody] review model)
        {

            var guid = Guid.NewGuid().ToString();

            var m = new review
            {
                id = guid,
                teacherId = model.teacherId,
                teacherName = model.teacherName,
                majorsId = model.majorsId,
                majorsName = model.majorsName,
                year = model.year,
                semester = model.semester,
                state = model.state
            };
            var res = await _MyContext.review.Where(u => u.teacherId == model.teacherId 
                && u.year == model.year
                && u.semester == model.semester
            ).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "Subject is exist" });
            }
            else
            {
                _MyContext.review.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetReview")]
        public async Task<ActionResult<review>> GetReview(string id)
        {
            var m = await _MyContext.review.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }

            return m;
        }

        [HttpPut("EditReview")]
        [AllowAnonymous]
        public async Task<IActionResult> EditReview([FromBody] review model)
        {
            var m = await _MyContext.review.AsNoTracking().Where(u => u.id == model.id).FirstOrDefaultAsync();

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
        [HttpDelete("DeleteReview")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteReview(string id)
        {
            var m = await _MyContext.review.Where(u => u.id == id).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.review.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
