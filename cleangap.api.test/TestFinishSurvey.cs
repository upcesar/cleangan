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
    public class TestFinishSurvey
    {
        [Test]
        public void TestBecomeToProject()
        {
            // TODO: Add your test code here
            SignatureModel sm = new SignatureModel()
            {
                DigitalSingature = "CESAR URDANETA",
                FullName = "CESAR URDANETA",
                SignDate = DateTime.Today,
            };

            SurveysBO surveyBO = new SurveysBO();
            bool closed = surveyBO.CloseSurvey("1", sm);

            Assert.IsTrue(closed);
        }
    }
}
