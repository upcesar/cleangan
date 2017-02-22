using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class LoginResponseModel
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}