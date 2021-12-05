using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLyGiangVien.Context;
using QuanLyGiangVien.Model;
using Swagger.Demo.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyGiangVien.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PINTeacherController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public PINTeacherController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<PINTeacher> Get()
        {
            return _MyContext.PINTeacher;
        }

        // POST: api/
        [HttpPost("AddPIN")]
        [AllowAnonymous]
        public async Task<IActionResult> AddPIN([FromBody] PINTeacher model)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.pin));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        hexString += hash[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    hash_pass = hexString;
                }
            }

            var guid = Guid.NewGuid().ToString();

            var m = new PINTeacher
            {
                teacherId = model.teacherId,
                pin = hash_pass
            };
            var res = await _MyContext.PINTeacher.Where(u => u.teacherId == model.teacherId).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "Teacher's PIN is exist" });
            }
            else
            {
                _MyContext.PINTeacher.Add(m);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetPIN")]
        public async Task<ActionResult<PINTeacher>> GetPIN(string teacherId, string pin)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(pin));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        hexString += hash[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    hash_pass = hexString;
                }
            }

            var user = await _MyContext.PINTeacher.Where(u => u.teacherId == teacherId && u.pin == hash_pass).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

        [HttpPut("EditPIN")]
        [AllowAnonymous]
        public async Task<IActionResult> EditPIN([FromBody] PINTeacher model, string newPin)
        {
            var hash_pass = "";
            var new_pass = "";
            
            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.pin));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        hexString += hash[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    hash_pass = hexString;
                }

                var hashNew = sha2.ComputeHash(Encoding.Unicode.GetBytes(newPin));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hashNew.Length; i++)
                    {
                        hexString += hashNew[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    new_pass = hexString;
                }
            }

            var m = await _MyContext.PINTeacher.AsNoTracking().Where(u => u.teacherId == model.teacherId && u.pin == hash_pass).FirstOrDefaultAsync();
            //var user = await _MyContext.users.AsNoTracking().Where(u => u.username == model.username).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                model.pin = new_pass;
                m = model;

                _MyContext.Update(m);
                await _MyContext.SaveChangesAsync();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

        // POST: api/
        [HttpDelete("DeletePIN")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePIN([FromBody] PINTeacher model)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.pin));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        hexString += hash[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    hash_pass = hexString;
                }
            }

            var m = await _MyContext.PINTeacher.Where(u => u.teacherId == model.teacherId && u.pin == hash_pass).FirstOrDefaultAsync();

            if (m == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.PINTeacher.Remove(m);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }
    }
}
