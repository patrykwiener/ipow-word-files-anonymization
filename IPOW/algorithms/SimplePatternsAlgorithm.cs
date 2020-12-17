using System.Collections.Generic;
using System.Diagnostics;
using Xceed.Words.NET;

namespace IPOW
{
    class SimplePatternsAlgorithm : IAlgorithm
    {
        private string replacement;
        private List<string> patterns;

        public SimplePatternsAlgorithm(List<string> patterns, string replacement = "***")
        {
            this.patterns = patterns;
            this.replacement = replacement;
        }

        public void Anonymize(DocX doc)
        {
            foreach (string pattern in patterns)
            {
                doc.ReplaceText(pattern, _ => replacement);
            }
        }
    }
}
