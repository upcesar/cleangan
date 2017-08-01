using cleangap.api.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cleangap.api.Models.Domain;
using cleangap.api.Models.Tables;
using cleangap.api.Services.Security;

namespace cleangap.api.Domain
{
    /// <summary>
    /// Business rule's interface for Questions
    /// </summary>
    public interface IQuestionsBO
    {
        List<QuestionsModel> GetByPageNum(int? pageNum = 1);
        List<QuestionsModel> GetBySubSection(int? pSubSectionId = null, int? pageNum = 1);
    }

    /// <summary>
    /// Business rules for Questions
    /// </summary>
    public class QuestionsBO : IQuestionsBO
    {
        private int? _maxPage;

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
                    //QuestionChoices = ListChoices(item),
                    HasMultipleAnswer = MultAnswers.Count > 1,
                    UniqueAnswer = MultAnswers.FirstOrDefault(),
                    MultipleAnswers = MultAnswers,
                });
            }

            return OptionList;
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

        private bool ShowParentQuestion(questions depItem, string dependentAnswerValue)
        {
            List<QuestionOptionModel> options = AddQuestionOptions(depItem);

            if (depItem != null)
                return options.Where(x => x.UniqueAnswer == dependentAnswerValue).Any();

            return true;

        }
        private void PopulateQuestions(List<QuestionsModel> qList, IQueryable<questions> tblQuestion)
        {
            foreach (var item in tblQuestion)
            {
                List<QuestionOptionModel> options = AddQuestionOptions(item);

                QuestionsModel q = new QuestionsModel()
                {
                    id = item.id,
                    description = item.description,
                    ParentAnswerValue = item.parent_answer_value,
                    HasRepeater = item.has_repeater,
                    QuestionOption = options,
                    ParentSelected = true,
                    ChildrenQuestion = GetChilderQuestions(item.children_question),
                };

                if (item.parent_question != null)
                {
                    q.ParentQuestionId = item.parent_question.id;
                    q.ParentSelected = ShowParentQuestion(item.parent_question, item.parent_answer_value);
                }

                qList.Add(q);
            }
        }

        public int? MaxPage { get { return _maxPage;  } }
        

        public List<QuestionsModel> GetBySubSection(int? pSubSectionId = null, int? pageNum = 1)
        {

            List<QuestionsModel> qList = new List<QuestionsModel>();

            using (var db = new CleanGapDataContext())
            {
                var tblQuestion = db.questions.Where(x => x.id_subsection == pSubSectionId && x.page == pageNum);
                PopulateQuestions(qList, tblQuestion);
            }

            return qList;
        }

        public List<QuestionsModel> GetByPageNum(int? pageNum = 1)
        {

            List<QuestionsModel> qList = new List<QuestionsModel>();

            using (var db = new CleanGapDataContext())
            {
                var tblQuestion = db.questions.Where(q => q.page == pageNum);
                _maxPage = db.questions.Max(x => x.page);

                PopulateQuestions(qList, tblQuestion);
            }

            return qList;
        }


    }
}