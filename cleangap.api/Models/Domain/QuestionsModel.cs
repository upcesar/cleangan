using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class QuestionsModel
    {
        public int id { get; set; }
        public string description { get; set; }

        public int section_id { get; set; }
        public string section_name { get; set; }
        public List<QuestionOptionModel> QuestionOption { get; set; }
        public QuestionsModel()
        {
            section_id = 0;
            section_name = "Uncategorized";
            QuestionOption = new List<QuestionOptionModel>();
        }
    }

    
}