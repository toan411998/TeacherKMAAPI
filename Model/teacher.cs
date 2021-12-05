using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class teacher
    {
        public string id { set; get; }
        public string? username { set; get; }
        public string? name { set; get; }
        public string? gender { set; get; }
        public string? majorsId { set; get; }
        public string? image { set; get; }
        public int? numOfStudyLesson { set; get; }
        public int? numOfTeachingLesson { set; get; }
        public string? date { set; get; }
        public string? address { set; get; }
        public string? phone { set; get; }
        public string? mail { set; get; }
        public string? password { set; get; }
        
    }
}
