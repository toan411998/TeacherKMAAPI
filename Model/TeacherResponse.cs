using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyGiangVien.Model
{
    public class TeacherResponse
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
        public string? role { set; get; }
        public string Token { get; set; }

        public TeacherResponse(string id, string username, string name, string gender, string majorsId, string image, int? numOfStudyLesson, int? numOfTeachingLesson, string date, string address, string phone, string mail, string password, string role, string token)
        {
            this.id = id;
            this.username = username;
            this.name = name;
            this.gender = gender;
            this.majorsId = majorsId;
            this.image = image;
            this.numOfStudyLesson = numOfStudyLesson;
            this.numOfTeachingLesson = numOfTeachingLesson;
            this.date = date;
            this.address = address;
            this.phone = phone;
            this.mail = mail;
            this.password = password;
            this.role = role;
            Token = token;
        }
        public TeacherResponse(teacher teacher, string token)
        {
            this.id = teacher.id;
            this.username = teacher.username;
            this.name = teacher.name;
            this.gender = teacher.gender;
            this.majorsId = teacher.majorsId;
            this.image = teacher.image;
            this.numOfStudyLesson = teacher.numOfStudyLesson;
            this.numOfTeachingLesson = teacher.numOfTeachingLesson;
            this.date = teacher.date;
            this.address = teacher.address;
            this.phone = teacher.phone;
            this.mail = teacher.mail;
            this.password = teacher.password;
            this.role = teacher.role;
            Token = token;
        }
    }
}
