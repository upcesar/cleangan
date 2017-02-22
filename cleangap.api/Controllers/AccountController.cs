using cleangap.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cleangap.api.Domain;
using cleangap.api.Models.Domain;

namespace cleangap.api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpPost, AllowAnonymous, Route("register")]
        public ApiResponse Register(CustomerModel data)
        {
            CustomersBO customerBO = new CustomersBO();

            bool registered = customerBO.Register(data);
            string msg = registered ? "Registration successful" : "E-Mail was registered in our database";

            return new ApiResponse()
            {
                HttpCode = Ok().ToString(),
                IsSuccess = registered,
                Message = msg
            };
        }
    }
}
