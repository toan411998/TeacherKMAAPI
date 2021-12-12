using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class teacherSubject
    {
        public string id { set; get; }
        public string? teacherId { set; get; }
        public string? subjectId { set; get; }
        public string? year { set; get; }
        public int? semester { set; get; }
        public string? room { set; get; }
    }
}
