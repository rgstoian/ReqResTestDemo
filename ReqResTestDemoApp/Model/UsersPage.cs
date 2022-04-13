using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqResTestDemoApp.Model
{
    public class UsersPage
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<UserData>? data { get; set; }
        public Support? support { get; set; }
    }

    public class Support
    {
        public string? url { get; set; }
        public string? text { get; set; }
    }

    public class UserData
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? avatar { get; set; }
    }

    public class SingleUser
    {
        public UserData data { get; set; }
        public Support support { get; set; }
    }


    public class CreatedUser
    {
        public string name { get; set; }
        public string job { get; set; }
        public string? id { get; set; }
        public DateTime createdAt { get; set; }
    }


    public class UpdatedUser
    {
        public string name { get; set; }
        public string job { get; set; }
        public DateTime updatedAt { get; set; }
    }


    public class RegisteredUser
    {
        public int id { get; set; }
        public string token { get; set; }
    }



}
