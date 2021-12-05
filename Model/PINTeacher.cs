using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class PINTeacher
    {
        [Key]
        public string teacherId { set; get; }
        public string? pin { set; get; }
    }
}
