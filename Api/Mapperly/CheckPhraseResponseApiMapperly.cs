using AnomalyDetection.Models;
using Api.ApiModels;
using Riok.Mapperly.Abstractions;

namespace Api.Mapperly
{
    [Mapper]
    internal static partial class CheckPhraseResponseApiMapperly
    {
        internal static partial CheckPhraseResponseApiModel Map(CheckPhraseOut source);
    }
}