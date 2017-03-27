using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleangap.api.Domain;

namespace cleangap.api.test
{
    [TestFixture]
    public class TestQuestionSections
    {
        [Test]
        public void TestLoad()
        {
            // TODO: Add your test code here
            //Assert.Pass("Your first passing test");

            IQuestionSectionsBO qsBO = new QuestionSectionsBO();

            var x = qsBO.ToList();

            Assert.Pass("Your first passing test");

        }
    }
}
