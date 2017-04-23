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
        public List<QuestionOptionModel> QuestionOption { get; set; }
        public QuestionsModel DependentQuestion { get; set; }
        public int? DependentQuestionId { get; set; }
        public string DependentAnswerValue { get; set; }
        public bool DependentSelected { get; set; }
        public QuestionsModel()
        {
            QuestionOption = new List<QuestionOptionModel>();
        }
    }

    
}