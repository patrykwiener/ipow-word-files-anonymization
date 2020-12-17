using System;
using System.Collections.Generic;
using System.Text;
using Xceed.Words.NET;

namespace IPOW
{
    class AnonymizationManager
    {
        public static void AnonymizeWithSave(string filepath, IAlgorithm algorithm, string outputFilePath)
        {
            using (DocX document = DocX.Load(filepath))
            {
                algorithm.Anonymize(document);
                document.SaveAs(outputFilePath);
            }
        }
    }
}
