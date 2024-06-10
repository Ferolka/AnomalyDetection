using Common.Constantans;
using Common.Entities;
using Db.Csv.Models;
using Riok.Mapperly.Abstractions;

namespace Db.Csv.Mapperly
{
    [Mapper]
    internal static partial class ToxicWordCsvMapperly
    {
        [MapperIgnoreTarget(nameof(Phrase.Id))]
        [MapProperty(nameof(ToxicWordCsv.IsToxic), nameof(Phrase.PhraseType))]
        internal static partial Phrase Map(ToxicWordCsv source);

        internal static IEnumerable<Phrase> Map(IEnumerable<ToxicWordCsv> source)
        {
            foreach (var item in source)
            {
                yield return Map(item);
            }
        }

        internal static PhraseType MapStringToPhraseType(string phraseType)
        {
            return phraseType switch
            {
                "Toxic" => PhraseType.Toxic,
                _ => PhraseType.Normal,
            };
        }
    }
}