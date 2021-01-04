using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xceed.Words.NET;

namespace IPOW
{

    class NameScanAlgorithm : IAlgorithm
    {
        private const string NAMES_FILENAME = "imiona_polskie.csv";
        private const string UPPERCASE_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SANITIZE_CHARACTERS = ".,;";

        private readonly List<string> names;

        public NameScanAlgorithm()
        {
            names = new List<string>(File.ReadAllText($"Resources/{NAMES_FILENAME}").Replace("\r", "").Split("\n"));
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

        private string[] ExtractWords(DocX doc)
        {
            List<string> words = new List<string>();

            foreach (var element in doc.Xml.Elements())
            {
                var wordsInElement = element.Value.Split((char[])null)
                    .Where(w => w.Trim().Length > 0);
                words.AddRange(wordsInElement);
            }

            return words.ToArray();
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

        private string Sanitize(string input)
        {   
            foreach (var replace in SANITIZE_CHARACTERS)
            {
                input = input.Replace(replace.ToString(), "");
            }

            return input;
        }
    }
}
