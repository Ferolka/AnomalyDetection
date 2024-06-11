using Common.Entities;
using Db.Csv.Models;
using Riok.Mapperly.Abstractions;

namespace Db.Csv.Mapperly
{
    [Mapper]
    internal static partial class ToxicWordCsvMapperly
    {
        [MapperIgnoreTarget(nameof(Phrase.Id))]
        internal static partial Phrase Map(ToxicWordCsv source);

        internal static IEnumerable<Phrase> Map(IEnumerable<ToxicWordCsv> source)
        {
            foreach (var item in source)
            {
                yield return Map(item);
            }
        }
    }
}