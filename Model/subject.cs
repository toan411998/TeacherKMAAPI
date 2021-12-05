using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class subject
    {
        public string id { set; get; }
        public string? name { set; get; }
        public string? image { set; get; }
        public int? numberOfLesson { set; get; }
        public string? type { set; get; }
    }
}
