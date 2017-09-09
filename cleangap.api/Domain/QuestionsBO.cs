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
            QuestionOptionsBO qoBO = new QuestionOptionsBO(pQuestions);

            return qoBO.Items;
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
        private void PopulateQuestions(List<QuestionsModel> qList, List<questions> tblQuestion)
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
                var tblQuestion = db.questions.Where(x => x.id_subsection == pSubSectionId && x.page == pageNum).ToList();
                PopulateQuestions(qList, tblQuestion);
            }

            return qList;
        }

        public List<QuestionsModel> GetByPageNum(int? pageNum = 1)
        {

            List<QuestionsModel> qList = new List<QuestionsModel>();

            using (var db = new CleanGapDataContext())
            {
                var tblQuestion = db.questions.Where(q => q.page == pageNum).ToList();
                _maxPage = db.questions.Max(x => x.page);

                PopulateQuestions(qList, tblQuestion);
            }

            return qList;
        }
    }
}