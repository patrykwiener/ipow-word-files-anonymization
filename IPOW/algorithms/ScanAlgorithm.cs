using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xceed.Words.NET;

namespace IPOW
{
    abstract class ScanAlgorithm
    {
        private const string SANITIZE_CHARACTERS = ".,;";
        protected string[] ExtractWords(DocX doc)
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

        protected string Sanitize(string input)
        {
            foreach (var replace in SANITIZE_CHARACTERS)
            {
                input = input.Replace(replace.ToString(), "");
            }

            return input;
        }

        protected List<string> ReadResourcesCSVFile(string filename)
        {
            return new List<string>(File.ReadAllText($"Resources/{filename}").Replace("\r", "").Split("\n"));
        }
    }
}
