using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    /// <summary>
    /// Summary Model with all questions answered
    /// </summary>
    public class SummaryModel
    {
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }

        public int NumPages { get; set; }

        public List<SurveyModel> SurveysItems { get; set; }
    }
}