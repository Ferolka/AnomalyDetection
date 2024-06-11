using AnomalyDetection.Models;
using Api.ApiModels;
using Riok.Mapperly.Abstractions;

namespace Api.Mapperly
{
    [Mapper]
    internal static partial class CheckPhraseApiMapperly
    {
        [MapperIgnoreTarget(nameof(PhraseML.IsToxic))]
        [MapProperty(nameof(CheckPhraseApiModel.Phrase), nameof(PhraseML.Text))]
        internal static partial PhraseML Map(CheckPhraseApiModel source);
    }
}