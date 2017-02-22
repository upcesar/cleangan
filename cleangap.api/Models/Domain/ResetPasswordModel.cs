using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}