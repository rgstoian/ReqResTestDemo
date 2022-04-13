﻿using ReqResTestDemoApp.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReqResTestDemoApp
{
    public class Helpers<T> : APICalls
    {
       public dynamic ResponseClass(RestResponse input)
        {
            return JsonSerializer.Deserialize<T>(input.Content);
        }

        public int TotalUsers()
        {
            UsersPage users = ResponseClass(GetUserPage());
            int maxpages = users.total_pages;

            int result = 0;

            for (int i = 1; i <= maxpages; i++)
            {
                UsersPage userlist = ResponseClass(GetUserPage(i));
                result+=userlist.data.Count;
            }

            return result;
        }
    }
}
