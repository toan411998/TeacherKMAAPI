using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class history
    {
        public string id { set; get; }
        public string? action { set; get; }
        public string? date { set; get; }
        public string? time { set; get; }
        public string? teacherId { set; get; }
    }
}
