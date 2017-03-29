using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class SurveyModel
    {
        public int id { get; set; }
        public int Page { get; set; }
        public int PageTotal { get; set; }

        public List<QuestionsModel> questions { get; set; }

        public SurveyModel()
        {
            questions = new List<QuestionsModel>();
        }

    }
}