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

    public interface ISurveysBO
    {
        SurveyModel ListQuestions(int? pageNum);
        bool HasAnswer(int pageNum);
        bool SaveAnswer(AnswersModel pAnswer, string currentUserId);
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
        private bool ShowParentQuestion(questions depItem, string dependentAnswerValue)
        {
            List<QuestionOptionModel> options = AddQuestionOptions(depItem);

            if (depItem != null)
                return options.Where(x => x.UniqueAnswer == dependentAnswerValue).Any();

            return true;

        }

        private List<QuestionsModel> GetChilderQuestions(ICollection<questions> childrenQuestions)
        {
            // item.children_question.Select(x => x.id).ToList<int>()
            if (childrenQuestions != null)
            {
                List<QuestionsModel> childrenQuestionList = new List<QuestionsModel>();

                foreach (var item in childrenQuestions)
                {
                    childrenQuestionList.Add(new QuestionsModel()
                    {
                        id = item.id,
                        ParentAnswerValue = item.parent_answer_value,
                    });
                }

                return childrenQuestionList;

            }


            return null;
        }

        private SurveyModel AddQuestion(SurveyModel s, List<questions> tblQuestion)
        {

            foreach (var item in tblQuestion)
            {

                List<QuestionOptionModel> options = AddQuestionOptions(item);

                QuestionsModel q = new QuestionsModel()
                {
                    id = item.id,
                    description = item.description,
                    ParentAnswerValue = item.parent_answer_value,
                    QuestionOption = options,
                    ParentSelected = true,
                    ChildrenQuestion = GetChilderQuestions(item.children_question),
                };

                if (item.parent_question != null)
                {
                    q.ParentQuestionId = item.parent_question.id;
                    q.ParentSelected = ShowParentQuestion(item.parent_question, item.parent_answer_value);
                }

                s.questions.Add(q);
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

            return OptionList;
        }
        private void SetQuestionSection(SurveyModel s, List<questions> tblQuestion)
        {
            var objSection = tblQuestion.FirstOrDefault();

            if (objSection != null)
            {
                s.section_id = objSection.question_sections.id;
                s.section_name = objSection.question_sections.name;
            }
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

                        SetQuestionSection(s, tblQuestion);

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

        private void ExcludeAnswerRadio(AnswersModel pAnswer, string currentCustomerId)
        {

            using (var db = new CleanGapDataContext())
            {
                // Get Question ID
                var intQuestionID = db.question_options
                                      .Where(x => x.id == pAnswer.QuestionOptionId)
                                      .Select(x => x.id_question)
                                      .FirstOrDefault();

                if (intQuestionID != null)
                {
                    var query = from qo in db.question_options
                                join a in db.answers on qo.id equals a.id_question_option
                                where qo.id_question == intQuestionID && qo.id != pAnswer.QuestionOptionId && qo.input_type.ToLower().Trim() == "radio"
                                select a.id_question_option;

                    var listAnswerId = query.ToList();

                    if(listAnswerId.Count > 0)
                    {
                        var queryAnswer = db.answers.Where(x => listAnswerId.Contains(x.id_question_option));

                        db.answers.RemoveRange(queryAnswer);
                        db.SaveChanges();
                    }
                }                
            }
        }

        private bool SaveSingleAnswer(AnswersModel pAnswer, string currentCustomerId)
        {
            int intCustomerId = 0;
            bool saved = false;

            if (int.TryParse(currentCustomerId, out intCustomerId))
            {
                using (var db = new CleanGapDataContext())
                {

                    answers tblanswer = db.answers.Where
                                            (x => x.id_question_option == pAnswer.QuestionOptionId
                                            && x.id_customer == intCustomerId
                                                ).FirstOrDefault();

                    if (tblanswer != null)
                    {
                        //Edit current answer
                        tblanswer.answers_value = pAnswer.UniqueValue;
                        tblanswer.id_customer = intCustomerId;
                        tblanswer.id_question_option = pAnswer.QuestionOptionId;

                        db.Entry(tblanswer).State = EntityState.Modified;
                    }
                    else
                    {
                        //Add new answer
                        tblanswer = new answers()
                        {
                            answers_value = pAnswer.UniqueValue,
                            id_customer = intCustomerId,
                            id_question_option = pAnswer.QuestionOptionId
                        };

                        db.answers.Add(tblanswer);
                    }

                    saved = db.SaveChanges() > 0;
                }
            }

            return saved;
        }

        private bool SaveMultipleAnswer(AnswersModel pAnswer, string currentCustomerId)
        {
            int intCustomerId = 0;
            bool saved = false;

            if (int.TryParse(currentCustomerId, out intCustomerId))
            {
                using (var db = new CleanGapDataContext())
                {
                    var queryAnswer = db.answers.Where(x => x.id_customer == intCustomerId
                                                         && x.id_question_option == pAnswer.QuestionOptionId);

                    db.answers.RemoveRange(queryAnswer);

                    List<answers> tblAnswer = new List<answers>();


                    foreach (var item in pAnswer.MultipleValues)
                    {
                        tblAnswer.Add(new answers()
                        {
                            answers_value = item,
                            id_customer = intCustomerId,
                            id_question_option = pAnswer.QuestionOptionId
                        });
                    }

                    db.answers.AddRange(tblAnswer);

                    saved = db.SaveChanges() > 0;
                }
            }

            return saved;
        }

        public bool SaveAnswer(AnswersModel pAnswer, string currentUserId)
        {
            bool saved = false;

            if (pAnswer.HasMultipleValue)
            {
                saved = SaveMultipleAnswer(pAnswer, currentUserId);
            }
            else
            {
                saved = SaveSingleAnswer(pAnswer, currentUserId);
                ExcludeAnswerRadio(pAnswer, currentUserId);

            }

            return saved;
        }
    }
}