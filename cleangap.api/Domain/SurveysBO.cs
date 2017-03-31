using cleangap.api.DAL;
using cleangap.api.Models.Domain;
using cleangap.api.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace cleangap.api.Domain
{

    public interface ISurveysBO
    {
        SurveyModel ListQuestions(int? pageNum);
        bool HasAnswer(int pageNum);
        bool SaveAnswer(List<AnswersModel> answer);
    }

    public class SurveysBO : ISurveysBO
    {
        private int intQtySubSection;

        public SurveysBO()
        {
            intQtySubSection = 1;
        }
        public SurveysBO(int qtySubSections)
        {
            intQtySubSection = qtySubSections;
        }
        private SurveyModel AddQuestion(SurveyModel s, List<questions> tblQuestion)
        {

            foreach (var item in tblQuestion)
            {
                
                List<QuestionOptionModel> options = AddQuestionOptions(item);

                s.questions.Add(new QuestionsModel()
                {
                    id = item.id,
                    description = item.description,
                    QuestionOption = options,
                });
            }

            return s;
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
        private List<QuestionOptionModel> AddQuestionOptions(questions pQuestions)
        {
            List<QuestionOptionModel> OptionList = new List<QuestionOptionModel>();

            List<question_options> tblOptions = pQuestions.question_options
                                                          .OrderBy(x => x.order)
                                                          .ToList();


            foreach (var item in tblOptions)
            {
                OptionList.Add(new QuestionOptionModel()
                {
                    OptionId = item.id,
                    OptionText = item.option_text != null ? item.option_text.Trim() : null,
                    OptionType = item.input_type != null ? item.input_type.Trim() : null,
                    QuestionChoices = ListChoices(item),
                    HasMultipleAnswer = item.answers.Count > 1,
                    UniqueAnswer = item.answers.Select(x=>x.answers_value).FirstOrDefault(),
                    MultipleAnswers = item.answers.Select(x => x.answers_value).ToList<string>()
                });
            }

            return OptionList;
        }
        public SurveyModel ListQuestions(int? pageNum)
        {
            if (pageNum != null)
            {
                SurveyModel s = new SurveyModel();
                using (var db = new CleanGapDataContext())
                {
                    var tblQuestion = db.questions.Where(q => q.page == pageNum).ToList();
                    var maxPage = db.questions.Max(x => x.page);

                    if (maxPage != null && tblQuestion.Count > 0)
                    {
                        s.Page = (int)pageNum;
                        s.PageTotal = (int)maxPage;

                        s = AddQuestion(s, tblQuestion);
                    }

                    return s;

                }

            }
            throw new NullReferenceException("Page Num cannot be empty");
        }
        public bool HasAnswer(int pageNum)
        {
            SurveyModel s = ListQuestions(pageNum);

            return s.questions.Any(x => x.QuestionOption.Any(y => !string.IsNullOrEmpty(y.UniqueAnswer)));
        }
        public bool SaveAnswer(List<AnswersModel> answer)
        {
            return true;
        }
    }
}