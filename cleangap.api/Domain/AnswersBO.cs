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
    #region INTERFACES
    /// <summary>
    /// AnswersBO interface
    /// </summary>
    public interface IAnswersBO
    {
        /// <summary>
        /// Save Unique Answer from the Model
        /// </summary>
        /// <returns></returns>
        bool SaveSingleAnswer();
        /// <summary>
        /// Save Multiple Answers
        /// </summary>
        /// <returns>True if it was saved successfully. Otherwise, false.</returns>
        bool SaveMultipleAnswer();

    }
    #endregion
    /// <summary>
    /// AnswersBO interface
    /// </summary>
    public class AnswersBO : IAnswersBO
    {
        #region PRIVATE MEMBERS
        private AnswersModel _answer;
        private string _currentCustomerId;
        #endregion

        public AnswersBO()
        {
            throw new Exception("Pass parameter Answer and User ID on constructor");
        }

        public AnswersBO(AnswersModel pAnswer, string pCurrentCustomerId)
        {
            _answer = pAnswer;
            _currentCustomerId = pCurrentCustomerId;
        }

        #region PRIVATE METHODS
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

                    if (listAnswerId.Count > 0)
                    {
                        var queryAnswer = db.answers.Where(x => listAnswerId.Contains(x.id_question_option));

                        db.answers.RemoveRange(queryAnswer);
                        db.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Save the multiple answer in the list
        /// </summary>
        /// <returns>Boolean: True if it was saved successfully. Otherwise, false.</returns>
        public bool SaveMultipleAnswer()
        {
            int intCustomerId = 0;
            bool saved = false;

            if (int.TryParse(_currentCustomerId, out intCustomerId))
            {
                using (var db = new CleanGapDataContext())
                {
                    var queryAnswer = db.answers.Where(x => x.id_customer == intCustomerId
                                                         && x.id_question_option == _answer.QuestionOptionId);

                    db.answers.RemoveRange(queryAnswer);

                    List<answers> tblAnswer = new List<answers>();

                    foreach (var item in _answer.MultipleValues)
                    {
                        tblAnswer.Add(new answers()
                        {
                            answers_value = item,
                            id_customer = intCustomerId,
                            id_question_option = _answer.QuestionOptionId
                        });
                    }

                    db.answers.AddRange(tblAnswer);

                    saved = db.SaveChanges() > 0;
                }
            }

            return saved;
        }

        /// <summary>
        /// Save Multiple Answers
        /// </summary>
        /// <returns>True if it was saved successfully. Otherwise, false.</returns>
        public bool SaveSingleAnswer()
        {
            int intCustomerId = 0;
            bool saved = false;

            if (int.TryParse(_currentCustomerId, out intCustomerId))
            {
                using (var db = new CleanGapDataContext())
                {
                    var tblanswer = db.answers.Where(
                                        x => x.id_question_option == _answer.QuestionOptionId
                                        && x.id_customer == intCustomerId
                                        && (_answer.IndexRepeater == null || x.index_repeater == _answer.IndexRepeater)
                                    ).FirstOrDefault();

                    if (tblanswer != null)
                    {
                        //Edit current answer
                        tblanswer.answers_value = _answer.UniqueValue;
                        tblanswer.id_customer = intCustomerId;
                        tblanswer.id_question_option = _answer.QuestionOptionId;
                        tblanswer.index_repeater = _answer.IndexRepeater;

                        db.Entry(tblanswer).State = EntityState.Modified;
                    }
                    else
                    {
                        //Add new answer
                        tblanswer = new answers()
                        {
                            answers_value = _answer.UniqueValue,
                            id_customer = intCustomerId,
                            id_question_option = _answer.QuestionOptionId,
                            index_repeater = _answer.IndexRepeater
                        };

                        db.answers.Add(tblanswer);
                    }

                    saved = db.SaveChanges() > 0;
                    ExcludeAnswerRadio(_answer, _currentCustomerId);
                }
            }

            return saved;
        }
        #endregion
    }
}