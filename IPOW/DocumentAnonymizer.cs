using System.Collections.Generic;
using Xceed.Words.NET;

namespace IPOW
{
    public class DocumentAnonymizer
    {
        private DocX doc;
        private string replacement;

        public DocumentAnonymizer(DocX doc, string replacement = "***")
        {
            this.doc = doc;
            this.replacement = replacement;
        }

        public void AnonymizeWithPattern(string pattern)
        {
            this.doc.ReplaceText(pattern, _ => replacement);
        }
        
        public void AnonymizeWithPatterns(List<string> patterns)
        {
            foreach (string pattern in patterns)
            {
                this.AnonymizeWithPattern(pattern);
            }
        }

    }
}
