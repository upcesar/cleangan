using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    /// <summary>
    /// Survey's Model
    /// </summary>
    public class SurveyModel
    {
        public int id { get; set; }
        public int Page { get; set; }
        public int PageTotal { get; set; }
        public bool RedirectSummary { get; set; }

        public List<QuestionsModel> questions { get; set; }
        public int section_id { get; internal set; }
        public string section_name { get; internal set; }

        public SurveyModel()
        {
            section_id = 0;
            section_name = "Uncategorized";
            RedirectSummary = false;
            questions = new List<QuestionsModel>();
        }

    }
}