using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    /// <summary>
    /// Question Model for business logic
    /// </summary>
    public class QuestionsModel
    {
        /// <summary>
        /// Question Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Question Description
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Question Options List
        /// </summary>
        public List<QuestionOptionModel> QuestionOption { get; set; }
        /// <summary>
        /// Parent Question Id for questions related with logic
        /// </summary>
        public int? ParentQuestionId { get; set; }
        /// <summary>
        /// Parent Question's answer value that triggers the related questions 
        /// </summary>
        public string ParentAnswerValue { get; set; }
        /// <summary>
        /// Set If Parent Question has the answers that triggers the related questions
        /// </summary>
        public bool ParentSelected { get; set; }
        /// <summary>
        /// Check if current question should use a repeater control
        /// </summary>
        public bool HasRepeater { get; set; }
        /// <summary>
        /// List of Children Quesiton 
        /// </summary>
        public List<QuestionsModel> ChildrenQuestion { get; set; }
        /// <summary>
        /// Queston initialization (constructor)
        /// </summary>
        public QuestionsModel()
        {
            QuestionOption = new List<QuestionOptionModel>();
        }
    }

    
}