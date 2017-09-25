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
    #region Interfaces
    public interface ISurveysBO
    {
        SurveyModel ListQuestions(int? pageNum, bool BoundToMax = false);
        SurveyModel ResumeLast();
        bool HasAnswer(int pageNum);
        bool SaveAnswer(AnswersModel pAnswer, string currentUserId);
        int LastSectionId { get; }

        bool CloseSurvey(string CustomerId, SignatureModel objSignature);

        bool CheckClosedSurvey(string CustomerId);

        SummaryModel ListSummary(int initialPage = 1, int offsetPages = 5);

    }
    #endregion
    public class SurveysBO : ISurveysBO
    {
        private QuestionsBO _questionBO = new QuestionsBO();
        private int intQtySubSection;


        public int LastSectionId
        {
            get
            {
                int maxPage = 1;
                int? idCustomer = AccountIdentity.GetCurrentUserInt();
                using (var db = new CleanGapDataContext())
                {
                    var query = from a in db.answers
                                join qo in db.question_options on a.id_question_option equals qo.id
                                join q in db.questions on qo.id_question equals q.id
                                where a.id_customer == idCustomer
                                select new { LastPage = q.page };

                    maxPage = query.Max(f => f.LastPage) ?? 1;
                }

                return maxPage;
            }
        }
        #region Constructors
        public SurveysBO()
        {
            intQtySubSection = 1;
        }
        public SurveysBO(int qtySubSections)
        {
            intQtySubSection = qtySubSections;
        }
        #endregion
        #region Private Methods
        private SurveyModel AddDefaultQuestion(SurveyModel s, List<QuestionsModel> listQuestion)
        {

            if (s.subsection.Count == 0)
            {
                s.subsection.Add(new SectionModel()
                {
                    id = 0,
                    name = string.Empty,
                    questions = listQuestion
                });
            }

            return s;
        }
        private void SetQuestionSection(SurveyModel s, List<QuestionsModel> listQuestion)
        {
            QuestionsModel objQuestion = listQuestion.FirstOrDefault();
            SectionBO _sectionBO = new SectionBO(objQuestion.id);
            SectionModel objSection = _sectionBO.GetByQuestion();

            if (objSection != null)
            {
                s.section_id = objSection.id;
                s.section_name = objSection.name;
            }
        }
        private void SetSubSection(SurveyModel s)
        {
            SectionBO section = new SectionBO(s.section_id);
            section.PageNum = s.Page;
            s.subsection = section.GetChildren();

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
        private int? CheckPageSection(CleanGapDataContext db, int? pageNum, bool BoundToMax)
        {
            if (BoundToMax)
            {
                int? idCustomer = AccountIdentity.GetCurrentUserInt();
                bool hasAnswer = db.answers.Where(x => x.id_customer == idCustomer).Any();
                int varSectionPage = LastSectionId;

                if (hasAnswer && pageNum >= varSectionPage)
                {
                    return varSectionPage;
                }
                return 1;
            }

            return pageNum;

        }
        private bool GoToSummary()
        {
            int? idCustomer = AccountIdentity.GetCurrentUserInt();
            bool goSummary = false;
            using (var db = new CleanGapDataContext())
            {
                var rowCustomer = db.customers.Find(idCustomer);
                if (rowCustomer != null && rowCustomer.id > 0)
                {
                    goSummary = rowCustomer.redirect_summary;
                }
            }

            return goSummary;
        }
        private void SetUserSummary(CleanGapDataContext db, int CountList)
        {
            int? idCustomer = AccountIdentity.GetCurrentUserInt();
            var tbCustomer = db.customers.Find(idCustomer);

            if (tbCustomer != null && tbCustomer.id > 0 && CountList > 0)
            {
                tbCustomer.redirect_summary = true;
                db.Entry(tbCustomer).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        #endregion
        #region Public Methods
        public SurveyModel ResumeLast()
        {
            int PageNum = LastSectionId;
            bool boolGoToSummary = GoToSummary();
            if (boolGoToSummary)
            {
                return new SurveyModel() { RedirectSummary = true };
            }

            return ListQuestions(PageNum);
        }
        public SurveyModel ListQuestions(int? pageNum, bool BoundToMax = false)
        {
            if (pageNum != null)
            {
                SurveyModel s = new SurveyModel();

                using (var db = new CleanGapDataContext())
                {
                    pageNum = CheckPageSection(db, pageNum, BoundToMax);

                    /*
                    var tblQuestion = db.questions.Where(q => q.page == pageNum).ToList();
                    var maxPage = db.questions.Max(x => x.page);
                    */
                    var listQuestions = _questionBO.GetByPageNum(pageNum);
                    var maxPage = _questionBO.MaxPage;

                    if (maxPage != null && listQuestions.Count > 0)
                    {
                        s.Page = (int)pageNum;
                        s.PageTotal = (int)maxPage;

                        SetQuestionSection(s, listQuestions);

                        SetSubSection(s);

                        s = AddDefaultQuestion(s, listQuestions);
                    }

                    return s;

                }

            }
            throw new NullReferenceException("Page Num cannot be empty");
        }
        public SummaryModel ListSummary(int Page = 1, int offsetPages = 5)
        {
            Page = Page < 1 ? 1 : Page; // InitialPage must not be less than the first page.

            int initialPage = ((Page - 1) * offsetPages) + 1;   
            SummaryModel objSummary = new SummaryModel() { PreviousPage = Page - 1, NextPage = Page + 1, SurveysItems = new List<SurveyModel>() };

            using (var db = new CleanGapDataContext())
            {
                var maxPage = db.questions.Max(x => x.page);
                int finalPage = initialPage + offsetPages - 1;

                for (int i = initialPage; i <= finalPage; i++)
                {
                    var survey = ListQuestions(i);
                    if (survey.Page > 0)
                    {
                        if (objSummary.NumPages == 0)
                        {
                            double numPages = (double)survey.PageTotal / offsetPages;
                            objSummary.NumPages = (int)Math.Ceiling(numPages);
                        }

                        objSummary.SurveysItems.Add(survey);
                    }                        
                    else
                        break;
                }
                SetUserSummary(db, objSummary.SurveysItems.Count);
            }

            if(objSummary.SurveysItems.Count == 0)
            {
                objSummary.PreviousPage = null;
                objSummary.NextPage = null;
            }

            objSummary.PreviousPage = objSummary.PreviousPage < 1 ? null : objSummary.PreviousPage;
            objSummary.NextPage = objSummary.NextPage > objSummary.NumPages ? null : objSummary.NextPage;

            return objSummary;
        }
        public bool HasAnswer(int pageNum)
        {
            SurveyModel s = ListQuestions(pageNum);

            return s.questions.Any(x => x.QuestionOption.Any(y => !string.IsNullOrEmpty(y.UniqueAnswer)));
        }
        public bool SaveAnswer(AnswersModel pAnswer, string currentUserId)
        {
            AnswersBO answerBO = new AnswersBO(pAnswer, currentUserId);

            if (pAnswer.HasMultipleValue)
            {
                return answerBO.SaveMultipleAnswer();
            }

            return answerBO.SaveSingleAnswer();
            
        }
        public bool CheckClosedSurvey(string CustomerId)
        {
            int intCustomerId = 0;
            bool FoundProject = false, FoundOpenAnswer = false, HasAnswers = false;

            if (int.TryParse(CustomerId, out intCustomerId))
            {
                using (var db = new CleanGapDataContext())
                {
                    FoundProject = db.projects.Where(p => p.id_customer == intCustomerId).Any();
                    FoundOpenAnswer = db.answers.Where(a => a.id_customer == intCustomerId && a.id_project != null).Any();
                    HasAnswers = db.answers.Where(a => a.id_customer == intCustomerId).Any();
                }
            }
            return FoundOpenAnswer && (FoundProject || !HasAnswers);

        }
        public bool CloseSurvey(string CustomerId, SignatureModel objSignature)
        {
            bool FoundProject = CheckClosedSurvey(CustomerId);
            bool saved = false;
            if (!FoundProject)
            {
                int intCustomerId = 0;
                if (int.TryParse(CustomerId, out intCustomerId))
                {
                    using (var db = new CleanGapDataContext())
                    {
                        // Gets open answers by customer

                        projects rowProject = new projects()
                        {
                            id_customer = intCustomerId,
                            answers = db.answers.Where(a => a.id_customer == intCustomerId).ToList(),
                            is_open = false,
                            project_status = null,
                            full_name = objSignature.FullName.ToUpper(),
                            sign_date = objSignature.SignDate,
                            digital_signature = objSignature.DigitalSingature.ToUpper(),
                            creation_date = DateTime.Now,
                        };
                        db.projects.Add(rowProject);
                        saved = db.SaveChanges() > 0;

                        if (saved)
                        {
                            CustomersBO custBO = new CustomersBO();
                            custBO.SendWelcomeEMail(intCustomerId);
                        }
                    }
                }
            }
            return saved;
        }
        #endregion
    }
}