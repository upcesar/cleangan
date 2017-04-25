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
        public int? ParentQuestionId { get; set; }
        public string ParentAnswerValue { get; set; }
        public bool ParentSelected { get; set; }
        public List<QuestionsModel> ChildrenQuestion { get; set; }
        public QuestionsModel()
        {
            QuestionOption = new List<QuestionOptionModel>();
        }
    }

    
}