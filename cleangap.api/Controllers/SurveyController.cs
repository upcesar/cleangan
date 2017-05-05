using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cleangap.api.Models.Domain;
using cleangap.api.Domain;
using cleangap.api.Models;
using cleangap.api.Services.Security;

namespace cleangap.api.Controllers
{
    [RoutePrefix("api/surveys"), Authorize]
    public class SurveyController : ApiController
    {
        /// <summary>
        /// List question structure, given a page number
        /// </summary>
        /// <param name="PageNum">Question's Page / Ster number</param>
        /// <returns>Questions list with options</returns>
        [HttpGet, Route("questions/{PageNum}")]
        public SurveyModel ListQuestions(int PageNum = 1)
        {

            SurveysBO sBO = new SurveysBO();

            return sBO.ListQuestions(PageNum);
        }

        /// <summary>
        /// Get last section when user logins back
        /// </summary>
        /// <returns>Questions list with options</returns>
        [HttpGet, Route("resume")]
        public SurveyModel GetLastQuestions()
        {
            
            SurveysBO sBO = new SurveysBO();            

            return sBO.ResumeLast();
        }

        /// <summary>
        /// Save answer
        /// </summary>
        /// <param name="pAnswer">Answer model</param>
        /// <returns>Response with operation status</returns>
        [HttpPost, Route("answers")]
        public ApiResponse AnswerQuestions(AnswersModel pAnswer)
        {
            SurveysBO sBO = new SurveysBO();
            string CurrentUserId = AccountIdentity.GetCurrentUser();

            bool saved = sBO.SaveAnswer(pAnswer, CurrentUserId);

            return new ApiResponse()
            {
                HttpCode = saved ? Ok().ToString() : InternalServerError().ToString(),
                IsSuccess = saved,
                Message = saved ? "Answers saved successfully" : "Failure on saving answers"
            };
        }
    }

}
