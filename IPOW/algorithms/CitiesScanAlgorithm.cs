using System.Collections.Generic;
using System.IO;
using Xceed.Words.NET;
using System.Linq;
using System.Diagnostics;

namespace IPOW
{
    class CitiesScanAlgorithm : ScanAlgorithm, IAlgorithm
    {
        private const string CITIES_FILENAME = "miasta_polskie.csv";

        private readonly List<string> cities;

        public CitiesScanAlgorithm()
        {
            cities = ReadResourcesCSVFile(CITIES_FILENAME);
        }

        public void Anonymize(DocX doc)
        {
            var words = ExtractWords(doc);

            for (int i = 0; i < words.Length - 1; i++)
            {
                var word = Sanitize(words[i]);

                if (IsCity(word))
                {
                    AnonymizeCity(doc, word);
                }
            }
        }

        private bool IsCity(string word)
        {
            return cities.Contains(word);
        }

        private void AnonymizeCity(DocX doc, string word)
        {
            var replaceRegex = word;
            var replacement = "[...]";
            doc.ReplaceText(replaceRegex, _ => replacement);
        }

    }
}
