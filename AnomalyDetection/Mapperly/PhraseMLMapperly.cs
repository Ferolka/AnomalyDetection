using AnomalyDetection.Models;
using Common.Entities;
using Riok.Mapperly.Abstractions;

namespace AnomalyDetection.Mapperly
{
    [Mapper]
    internal static partial class PhraseMLMapperly
    {
        [MapperIgnoreSource(nameof(Phrase.Id))]
        internal static partial PhraseML Map(Phrase source);
    }
}