using System;
using System.Collections.Generic;
using System.Text;
using Xceed.Words.NET;

namespace IPOW
{
    class AnonymizationManager
    {
        public static void AnonymizeWithSave(string filepath, List<string> patterns, string outputFilePath)
        {
            using (DocX document = DocX.Load(filepath))
            {
                DocumentAnonymizer documentAnonymizer = new DocumentAnonymizer(document);
                documentAnonymizer.AnonymizeWithPatterns(patterns);
                document.SaveAs(outputFilePath);
            }
        }
    }
}
