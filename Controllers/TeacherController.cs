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
    public class TeacherController : Controller
    {
        private readonly MyContext _MyContext;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public TeacherController(MyContext myContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _MyContext = myContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        // GET: api/
        [HttpGet]
        public IEnumerable<teacher> Get()
        {
            return _MyContext.teacher;
        }

        private string generateJwtToken(teacher user)
        {
            // generate token that is valid for 30 minute
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.id)
                    /*,
                    new Claim("username", user.username),
                    new Claim("password", user.password),
                    new Claim("name", user.name),
                    new Claim("phone", user.phone),
                    new Claim("date", user.date),
                    new Claim("address", user.address),
                    
                    new Claim("mail", user.mail),*/
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        // POST: api/
        [HttpPost("AddTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> AddTeacher([FromBody] teacher model)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.password));
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

            var user = new teacher
            {
                id = guid,
                username = model.username,
                name = model.name,
                gender = model.gender,
                majorsId = model.majorsId,
                image = model.image,
                numOfStudyLesson = model.numOfStudyLesson,
                numOfTeachingLesson = model.numOfTeachingLesson,
                date = model.date,
                address = model.address,
                phone = model.phone,
                mail = model.mail,
                password = hash_pass
            };
            var res = await _MyContext.teacher.Where(u => u.username == model.username).FirstOrDefaultAsync();

            if (res != null)
            {
                return new OkObjectResult(new { Message = "Account is exist" });
            }
            else
            {
                _MyContext.teacher.Add(user);
            }

            _MyContext.SaveChanges();

            return new OkObjectResult(new { Message = "Success" });
        }

        // GET: api/
        [HttpGet("GetTeacher")]
        public async Task<ActionResult<teacher>> GetTeacher(string username, string password)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(password));
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

            var user = await _MyContext.teacher.Where(u => u.username == username && u.password == hash_pass).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("EditTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> EditTeacher([FromBody] teacher model, string newPass)
        {
            var hash_pass = "";
            var new_pass = "";
            var username = model.username;

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.password));
                {
                    string hexString = string.Empty;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        hexString += hash[i].ToString("X2");
                    }
                    Console.WriteLine(hexString);
                    hash_pass = hexString;
                }

                var hashNew = sha2.ComputeHash(Encoding.Unicode.GetBytes(newPass));
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

            var user = await _MyContext.teacher.AsNoTracking().Where(u => u.username == model.username && u.password == hash_pass).FirstOrDefaultAsync();
            //var user = await _MyContext.users.AsNoTracking().Where(u => u.username == model.username).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                model.id = user.id;
                model.password = new_pass;
                user = model;
                /*var checkUser = await _MyContext.teacher.AsNoTracking().Where(u => u.username == model.username).FirstOrDefaultAsync();
                if (checkUser == null)
                {
                    _MyContext.Update(user);
                    await _MyContext.SaveChangesAsync();
                }
                else
                {
                    return new OkObjectResult(new { Message = "Account is exist" });
                }*/

                _MyContext.Update(user);
                await _MyContext.SaveChangesAsync();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

        // POST: api/
        [HttpDelete("DeleteTeacher")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteTeacher([FromBody] teacher model)
        {
            var hash_pass = "";

            using (var sha2 = SHA256.Create())
            {
                var hash = sha2.ComputeHash(Encoding.Unicode.GetBytes(model.password));
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

            var user = await _MyContext.teacher.Where(u => u.username == model.username && u.password == hash_pass).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _MyContext.teacher.Remove(user);
                _MyContext.SaveChanges();
            }

            return new OkObjectResult(new { Message = "Success" });
        }

    }
}
