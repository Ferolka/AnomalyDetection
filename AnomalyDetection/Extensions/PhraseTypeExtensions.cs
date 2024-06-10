using Common.Constantans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetection.Extensions
{
    internal static class PhraseTypeExtensions
    {
        internal static PhraseType GetPhraseType(this string stringValue)
        {
            var phraseType = (PhraseType)Enum.Parse(typeof(PhraseType), stringValue);

            return phraseType;
        }
    }
}
