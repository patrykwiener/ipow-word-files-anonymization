using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xceed.Words.NET;

namespace IPOW
{

    class NameScanAlgorithm : ScanAlgorithm, IAlgorithm
    {
        private const string NAMES_FILENAME = "imiona_polskie.csv";
        private const string UPPERCASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly List<string> names;

        public NameScanAlgorithm()
        {
            names = ReadResourcesCSVFile(NAMES_FILENAME);
        }

        public void Anonymize(DocX doc)
        {
            var words = ExtractWords(doc);

            for (int i = 0; i < words.Length - 1; i++)
            {
                var currentWord = Sanitize(words[i]);
                var nextWord = Sanitize(words[i + 1]);

                if (IsName(currentWord))
                {
                    if (IsSurname(nextWord))
                    {
                        AnonymizeName(doc, currentWord, nextWord);
                    }
                    else if (i >= 1 && IsSurname(words[i - 1]))
                    {
                        AnonymizeName(doc, words[i - 1], currentWord);
                    }
                }
            }
        }
        private bool IsSurname(string nextWord)
        {
            if (nextWord.Length < 2)
            {
                return false;
            }

            var firstLetter = nextWord.Substring(0, 1);
            return UPPERCASE_LETTERS.Contains(firstLetter);
        }

        private bool IsName(string word)
        {
            return names.Contains(word);
        }

        private void AnonymizeName(DocX doc, string firstWord, string secondWord)
        {
            var replaceRegex = firstWord + "\\s+" + secondWord;
            var replacement = firstWord.Substring(0, 1) + "." + " " + secondWord.Substring(0, 1) + ".";
            doc.ReplaceText(replaceRegex, _ => replacement);
        }

    }
}
