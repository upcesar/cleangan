using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleangap.api.Services.HttpClient;

namespace cleangap.api.Test
{
    [TestFixture]
    public class TestClass
    {
        #region Constants
        private const string RestMockURI = "https://api.spotify.com/";
        #endregion

        #region Private Test Objects Declaration
        private ApiCommands api;
        #endregion
        #region Private Test Object Initialization
        [OneTimeSetUp]
        public void Init()
        {
            api = new ApiCommands(RestMockURI);
        }
        [OneTimeTearDown]
        public void Dispose()
        {
            api = null;
        }
        #endregion

        [Test]
        public void TestRestTaskGetNotEmpty()
        {
            Task<string> answer = api.ExecuteGet("v1/search?q=kmfdm&type=artist");

            answer.Wait();

            Assert.That(answer.Result, Is.Not.Empty);
        }

        [Test]
        public void TestNotFoundURI()
        {
            string FakeURI = "https://api.spotify2.com/";
            ApiCommands otherApi = new ApiCommands(FakeURI);

            Task<string> answer = otherApi.ExecuteGet("v1/search?q=kmfdm&type=artist");

            answer.Wait();

            Assert.IsFalse(api.IsSucess, string.Format("HTTP Status:, {0}", api.HttpCode.ToString()));
        }
    }
}
