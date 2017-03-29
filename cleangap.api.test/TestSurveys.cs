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
            var x = qsBO.ListQuestions(2);
            
            Assert.IsInstanceOf<SurveyModel>(x);
        }
    }
}
