using System;
using System.Text;
using Xceed.Words.NET;

namespace IPOW
{
    interface IAlgorithm
    {
        void Anonymize(DocX doc);
    }
}
