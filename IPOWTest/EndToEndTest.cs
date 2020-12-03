using NUnit.Framework;
using IPOW;
using System.Collections.Generic;
using System;

namespace IPOWTest
{
    public class EndToEndTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test, TestCaseSource("AnonymizationTestSource")]
        //public void ShouldAnonymizeContent(string content, string replacement, List<string> unwantedTokens)
        //{
        //    DocumentAnonymizer anonymizer = new DocumentAnonymizer();

        //    string anonymizedContent = anonymizer.anonymize(content, replacement);

        //    assertDoesNotContain(anonymizedContent, unwantedTokens);
        //}

        //private void assertDoesNotContain(string anonymizedContent, List<string> unwantedTokens)
        //{
        //    unwantedTokens.ForEach(token =>
        //    {
        //        Assert.IsFalse(anonymizedContent.Contains(token));
        //    });
        //}


        public static IEnumerable<TestCaseData> AnonymizationTestSource()
        {
            yield return new TestCaseData("some_content 02070803625", "", new List<string>(new string[] { "02070803625", "token2" }));
            yield return new TestCaseData("02070803625 97031004567", "", new List<string>(new string[] { "02070803625", "97031004567" }));
            yield return new TestCaseData("content3", "", new List<string>(new string[] { "token5", "token6" }));
        }
    }
}