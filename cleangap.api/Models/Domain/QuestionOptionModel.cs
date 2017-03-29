using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class QuestionOptionModel
    {
        public int id { get; set; }
        public string OptionText { get; set; }
        public string OptionType { get; set; }
        public List<QuestionChoicesModel> QuestionChoices { get; set; }
    }
}