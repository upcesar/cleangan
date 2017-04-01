using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleangap.api.Domain;
using cleangap.api.Models.Domain;

namespace cleangap.api.test
{
    [TestFixture]
    public class TestSurveys
    {

        [Test]
        public void TestListQuestions()
        {
            // TODO: Add your test code here
            //Assert.Pass("Your first passing test");

            ISurveysBO qsBO = new SurveysBO();
            var x = qsBO.ListQuestions(1);

            Assert.IsInstanceOf<SurveyModel>(x);
        }
        [Test]
        public void TestListAnswers()
        {
            ISurveysBO qsBO = new SurveysBO();
            var x = qsBO.HasAnswer(1);

            Assert.IsTrue(x);
        }

        [Test]
        public void TestListFirstAnswers()
        {
            ISurveysBO qsBO = new SurveysBO();

            var x = qsBO.ListQuestions(1);

            Assert.AreEqual("My ERP is TOTVS", x.questions.FirstOrDefault().QuestionOption.FirstOrDefault().UniqueAnswer);

        }

        [Test]
        public void TestSetAnswer()
        {
            ISurveysBO qsBO = new SurveysBO();

            AnswersModel answerModel = new AnswersModel()
            {
                QuestionOptionId = 1,
                UniqueValue = "ERP Sigue Cloud"
            };

            string userId = "1"; // Customer Id.

            var x = qsBO.SaveAnswer(answerModel, userId);

            Assert.IsTrue(x);

        }
        [Test]
        public void TestSetMultipleAnswer()
        {
            ISurveysBO qsBO = new SurveysBO();

            AnswersModel answerModel = new AnswersModel()
            {
                QuestionOptionId = 17,
                HasMultipleValue = true,
                MultipleValues = new List<string>() { "email", "phone" }
            };

            string userId = "1"; // Customer Id.

            var x = qsBO.SaveAnswer(answerModel, userId);

            Assert.IsTrue(x);
        }
    }
}
