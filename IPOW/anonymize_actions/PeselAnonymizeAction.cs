using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IPOW.anonymize_actions
{
    class PeselAnonymizeAction
    {
        private string pattern;
        public PeselAnonymizeAction()
        {
            pattern = @"[0-9]{4}[0-3]{1}[0-9]{6}";
        }

        public string execute(string content, string replacement)
        {
            return Regex.Replace(content, pattern, replacement);
        }

    }
}
