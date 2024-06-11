using AnomalyDetection.Interfaces;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class MLEndpoints
    {
        public static IEndpointRouteBuilder MapMLEndpoints(
            this IEndpointRouteBuilder endpointRouteBuilder)
        {

            endpointRouteBuilder.MapGet("/train", async (
                [FromServices] ITrainUseCase trainUseCase,
                HttpContext httpContext) =>
            {
                var result = await trainUseCase.ExecuteAsync(httpContext.RequestAborted);
                return result.MapToIResult();
            });

            return endpointRouteBuilder;
        }
    }
}
