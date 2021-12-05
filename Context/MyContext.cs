using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyGiangVien.Model;

namespace Swagger.Demo.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<teacher> teacher { get; set; }
        public DbSet<subject> subject { get; set; }
        public DbSet<teacherSubject> teacherSubject { get; set; }
        public DbSet<review> review { get; set; }
        public DbSet<report> report { get; set; }
        public DbSet<majorsTeacher> majorsTeacher { get; set; }
        public DbSet<majorsImage> majorsImage { get; set; }
        public DbSet<majors> majors { get; set; }
        public DbSet<history> history { get; set; }
        public DbSet<dailyWork> dailyWork { get; set; }
        public DbSet<PINTeacher> PINTeacher { get; set; }

    }

}
