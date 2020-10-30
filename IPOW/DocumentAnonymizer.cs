using IPOW.anonymize_actions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPOW
{
    public class DocumentAnonymizer
    {

        public string anonymize(string content, string replacement)
        {
            content = anonymizePesel(content, replacement);
            return content;
        }

        private string anonymizePesel(string content, string replacement)
        {
            return new PeselAnonymizeAction().execute(content, replacement);
        }

    }
}
