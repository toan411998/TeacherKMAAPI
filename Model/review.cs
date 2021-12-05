using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class review
    {
        public string id { set; get; }
        public string? teacherId { set; get; }
        public string? teacherName { set; get; }
        public string? majorsId { set; get; }
        public string? majorsName { set; get; }
        public int? year { set; get; }
        public int? semester { set; get; }
        public int? state { set; get; }
    }
}
