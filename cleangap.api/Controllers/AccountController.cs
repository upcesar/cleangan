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
        private CustomersBO customerBO = new CustomersBO();

        //POST api/account/register
        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<ApiResponse> Register(CustomerModel data)
        {
            string msg = string.Empty;
            bool registered = false;
            
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

        //POST api/account/send-password-recovery
        [HttpPost, AllowAnonymous, Route("send-password-recovery")]
        public ApiResponse SendPasswordRecovery(CustomerModel data)
        {

            bool userFound = customerBO.SendTokenEmail(data);
            string strMsg = userFound ? "E-Mail sent" : "E-Mail not found";

            return new ApiResponse()
            {
                HttpCode = Ok().ToString(),
                IsSuccess = userFound,
                Message = strMsg

            };
        }
    }
}
