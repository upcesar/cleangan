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
        /// <param name="BoundToMax">Navigation is going backward</param>
        /// <returns>Questions list with options</returns>
        [HttpGet, Route(""), Route("questions/{PageNum?}")]
        public SurveyModel ListQuestions(int PageNum = 1, string BoundToMax = "")
        {

            SurveysBO sBO = new SurveysBO();

            return sBO.ListQuestions(PageNum, BoundToMax.ToLower() == "true");
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

        /// <summary>
        /// Check Status for Surveys / GAP Document
        /// </summary>
        /// <returns>Response with operation status</returns>
        [HttpGet, Route("status")]
        public ApiResponse Status()
        {
            SurveysBO sBO = new SurveysBO();
            string CurrentUserId = AccountIdentity.GetCurrentUser();

            bool isClosedSurvey = sBO.CheckClosedSurvey(CurrentUserId);

            return new ApiResponse()
            {
                HttpCode = Ok().ToString(),
                IsSuccess = !isClosedSurvey,
                Message = isClosedSurvey ? "Customer has finished the GAP document." : "Customer's GAP document is still open."
            };
        }

        /// <summary>
        /// Close Survey
        /// </summary>
        /// <param name="pSignature">Signature object with Full Name, Date and Digital Signature</param>
        /// <returns>Response with operation status</returns>
        [HttpPost, Route("close")]
        public ApiResponse Close(SignatureModel pSignature)
        {
            SurveysBO sBO = new SurveysBO();
            string CurrentUserId = AccountIdentity.GetCurrentUser();

            bool saved = sBO.CloseSurvey(CurrentUserId, pSignature);

            return new ApiResponse()
            {
                HttpCode = saved ? Ok().ToString() : InternalServerError().ToString(),
                IsSuccess = saved,
                Message = saved ? "Survey closed successfully" : "Failure on closing survey"
            };
        }

        /// <summary>
        /// Get All questions answered by the user
        /// </summary>
        /// <returns>List of questions answered</returns>
        [HttpGet, Route("summary/{initialPage?}/{offsetPages?}")]
        public SummaryModel Summary(int initialPage = 1, int offsetPages = 5)
        {
            SurveysBO sBO = new SurveysBO();

            return sBO.ListSummary(initialPage, offsetPages);
        }
    }

}
