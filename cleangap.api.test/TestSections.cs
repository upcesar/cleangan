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
    public class TestSections
    {
        [Test]
        public void TestGetInstanceSubSections()
        {
            SectionBO objSection = new SectionBO(4);

            var x = objSection.GetChildren();

            Assert.IsInstanceOf<List<SectionModel>>(x);
        }

        [Test]
        public void TestGetCountSubSections()
        {
            SectionBO objSection = new SectionBO(4);

            var x = objSection.GetChildren();

            Assert.Greater(x.Count, 0);
        }
    }
}
