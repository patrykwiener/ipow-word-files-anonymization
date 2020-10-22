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

        [Test, TestCaseSource("AnonymizationTestSource")]
        public void ShouldAnonymizeContent(string content, List<string> unwantedTokens)
        {
            DocumentAnonymizer anonymizer = new DocumentAnonymizer();

            string anonymizedContent = anonymizer.anonymize(content);

            assertDoesNotContain(anonymizedContent, unwantedTokens);
        }

        private void assertDoesNotContain(string anonymizedContent, List<string> unwantedTokens)
        {
            unwantedTokens.ForEach(token =>
            {
                Assert.IsFalse(anonymizedContent.Contains(token));
            });
        }

        public static IEnumerable<TestCaseData> AnonymizationTestSource()
        {
            yield return new TestCaseData("content1", new List<string>(new string[] { "token1", "token2" }));
            yield return new TestCaseData("content2", new List<string>(new string[] { "token3", "token4" }));
            yield return new TestCaseData("content3", new List<string>(new string[] { "token5", "token6" }));
        }
    }
}