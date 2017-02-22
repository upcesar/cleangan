using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace cleangap.api.Models
{
    public class ApiResponse
    {
        public string HttpCode { get; set; }
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}