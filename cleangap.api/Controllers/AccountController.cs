﻿using cleangap.api.Models;
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
            string strMsg;
            bool userFound = false;

            try
            {
                userFound = customerBO.SendTokenEmail(data);
                strMsg = userFound ? "E-Mail sent" : "E-Mail not found";
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;                
            }

            return new ApiResponse()
            {
                HttpCode = Ok().ToString(),
                IsSuccess = userFound,
                Message = strMsg

            };
        }

        //POST api/account/send-password-recovery
        [HttpPost, AllowAnonymous, Route("reset-password")]
        public ApiResponse ResetPassword(ResetPasswordModel data)
        {
            bool passwordResetted = customerBO.ResetPassword(data);
            string strMsg = passwordResetted ? "New password created successfully" : "Failure on creating a new password";

            return new ApiResponse()
            {
                HttpCode = Ok().ToString(),
                IsSuccess = passwordResetted,
                Message = strMsg

            };
        }
    }
}
