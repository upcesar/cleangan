using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cleangap.api.Models.Domain;
using cleangap.api.Domain;

namespace cleangap.api.Controllers
{
    [RoutePrefix("api/surveys"), Authorize]
    public class SurveyController : ApiController
    {
        [HttpGet, Route("questions/{PageNum}")]
        public SurveyModel ListQuestions(int PageNum = 1) {

            SurveysBO sBO = new SurveysBO();
            
            return sBO.ListQuestions(PageNum);
        }
    }
}
