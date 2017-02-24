using cleangap.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cleangap.api.Domain;
using cleangap.api.Models.Domain;
using cleangap.api.Services.Security;
using System.Threading.Tasks;

namespace cleangap.api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<ApiResponse> Register(CustomerModel data)
        {
            string msg = string.Empty;
            bool registered = false;
            CustomersBO customerBO = new CustomersBO();
            GoogleRecaptcha recaptchaSrvc = new GoogleRecaptcha(data.recaptcha);

            bool validRecaptcha = await recaptchaSrvc.Validate();

            if (validRecaptcha && recaptchaSrvc.Success)
            {
                registered = customerBO.Register(data);
                msg = registered ? "Registration successful" : "E-Mail was previously registered in our database";
            }
            else
            {
                msg = "Failure on validating Google Recaptcha";
            }

            return new ApiResponse()
            {
                HttpCode = validRecaptcha ? Ok().ToString() : InternalServerError().ToString(),
                IsSuccess = registered,
                Message = msg
            };
        }
    }
}
