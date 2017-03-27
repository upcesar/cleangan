using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleangap.api.Services.HttpClient;
using cleangap.api.Services.Security;

namespace cleangap.api.Test
{
    [TestFixture]
    public class TestGoogleCaptcha
    {
        #region Constants
        private const string RestMockURI = "https://api.spotify.com/";
        #endregion

        #region Private Test Objects Declaration
        private ApiCommand api;
        #endregion
        #region Private Test Object Initialization
        [OneTimeSetUp]
        public void Init()
        {
            api = new ApiCommand(RestMockURI);
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
            ApiCommand otherApi = new ApiCommand(FakeURI);

            Task<string> answer = otherApi.ExecuteGet("v1/search?q=kmfdm&type=artist");

            answer.Wait();

            Assert.IsFalse(api.IsSucess, string.Format("HTTP Status:, {0}", api.HttpCode.ToString()));
        }

        [Test]
        public void TestGoogleCaptchaWithoutErrors()
        {
            GoogleRecaptcha recaptcha = new GoogleRecaptcha("03AI-r6f5Gh8E2Vxo8VesV-0UHYXrvGVb4B7lxcxSGeRCiQdc2zIEENj7RyVFWIbgzrljbWlsUSaOpuzkTZ2okKDaodWstkD1__c4NIYPoK-kemGRRwh1bvWod3eXgmSuPSTA6jdj-jDoPNL9nnk3HgVS4qONQwzvZ-iMypsUlp3mi8oYStRS9XldaJm8zxc5G-6Kh1v5_jJi705VSFX5Ao0KrdKKvnSnpttvaumjtSyodvlGEFVWspVXD888o24iyy_y_2rQveXfadHpqVVwy_e5oJmSJVSlCsPH5kuzo5R9Q1rpwk6mU2C5PmanP7jKOF_LrQVm3N7GdXXx3eiQK3uW9oDKKyz_lC4qex4ZWLzGZ5PGC5jHTXGU");
            
            Assert.AreEqual(0, 
                            recaptcha.ErrorCodes.Count, 
                            string.Format("Error list: {0}", 
                                           string.Join(",", recaptcha.ErrorCodes)
                                          )
                );
        }


    }
}
