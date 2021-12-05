using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class dailyWork
    {
        public string id { set; get; }
        public string? teacherId { set; get; }
        public string? subjectId { set; get; }
        public string? subjectName { set; get; }
        public string? date { set; get; }
        public int? lesson { set; get; }
        public string? room { set; get; }
        public int? state { set; get; }
        public string? startTime { set; get; }
        public string? endTime { set; get; }
    }
}
