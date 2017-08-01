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
        /// <summary>
        /// Page / Step of the survey
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Number of steps / pages
        /// </summary>
        public int PageTotal { get; set; }
        /// <summary>
        /// When user is authenticated, rediret to a summery or not whether all questions are answered
        /// </summary>
        public bool RedirectSummary { get; set; }
        /// <summary>
        /// Section ID
        /// </summary>
        public int section_id { get; internal set; }
        /// <summary>
        /// Section Name
        /// </summary>
        public string section_name { get; internal set; }
        /// <summary>
        /// Subsection's List related to the main section
        /// </summary>
        public List<SectionModel> subsection { get; set; }
        /// <summary>
        /// Question's list for surveys
        /// </summary>
        public List<QuestionsModel> questions { get; set; }
        /// <summary>
        /// SurveyModel Construction for setting up initial values
        /// </summary>
        public SurveyModel()
        {
            section_id = 0;
            section_name = "Uncategorized";
            RedirectSummary = false;
            subsection = new List<SectionModel>();
        }

    }
}