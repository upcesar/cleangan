using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class AnswersModel
    {
        public int QuestionOptionId { get; set; }
        public string UniqueValue { get; set; }
        public List<string> MultipleValues { get; set; }
        public bool HasMultipleValue { get; set; }

        public AnswersModel()
        {
            HasMultipleValue = false;
            MultipleValues = new List<string>();
        }
    }
}