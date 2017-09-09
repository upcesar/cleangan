using cleangap.api.DAL;
using cleangap.api.Models.Domain;
using cleangap.api.Models.Tables;
using cleangap.api.Services.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace cleangap.api.Domain
{
    public class QuestionOptionsBO
    {

        private List<QuestionOptionModel> _items = new List<QuestionOptionModel>();

        public List<QuestionOptionModel> Items { get { return _items; } }

        public QuestionOptionsBO(questions pQuestions)
        {
            List<QuestionOptionModel> OptionList = new List<QuestionOptionModel>();

            List<question_options> tblOptions = pQuestions.question_options
                                                          .OrderBy(x => x.order)
                                                          .ToList();

            int? idCustomer = AccountIdentity.GetCurrentUserInt();

            foreach (var item in tblOptions)
            {
                List<string> MultAnswers = item.answers.Where(x => x.id_customer == idCustomer)
                                                       .Select(x => x.answers_value)
                                                       .ToList<string>();

                OptionList.Add(new QuestionOptionModel()
                {
                    OptionId = item.id,
                    OptionText = item.option_text != null ? item.option_text.Trim() : null,
                    OptionType = item.input_type != null ? item.input_type.Trim() : null,
                    QuestionChoices = ListChoices(item),
                    HasMultipleAnswer = MultAnswers.Count > 1,
                    UniqueAnswer = MultAnswers.FirstOrDefault(),
                    MultipleAnswers = MultAnswers,
                });
            }

            _items = OptionList;
        }

        private List<QuestionChoicesModel> ListChoices(question_options pQuestionOption)
        {
            List<QuestionChoicesModel> listChoice = new List<QuestionChoicesModel>();

            if (!string.IsNullOrEmpty(pQuestionOption.values_list))
            {
                List<string> strChoiceValue = pQuestionOption.values_list.Split(',').ToList();


                foreach (var item in strChoiceValue)
                {
                    string pattern = "[\\s+]|[\\-+]";
                    string replacement = "_";
                    Regex rgx = new Regex(pattern);
                    string result_id = rgx.Replace(item, replacement).ToLower();

                    listChoice.Add(new QuestionChoicesModel()
                    {
                        id = result_id,
                        description = item,
                    });
                }

            }

            return listChoice;

        }

        
    }
}