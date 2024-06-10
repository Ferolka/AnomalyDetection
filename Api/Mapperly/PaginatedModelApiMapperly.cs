using AnomalyDetection.Models;
using Api.ApiModels;
using Riok.Mapperly.Abstractions;

namespace Api.Mapperly
{
    [Mapper]
    internal static partial class PaginatedModelApiMapperly
    {
        internal static partial PaginatedModel Map(PaginatedModelApiModel source);
    }
}
