using AnomalyDetection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class MLEndpoints
    {
        public static IEndpointRouteBuilder MapMLEndpoints(
            this IEndpointRouteBuilder endpointRouteBuilder)
        {

            endpointRouteBuilder.MapGet("/train", (
                [FromServices] ITrainUseCase trainUseCase,
                HttpContext httpContext) =>
            {
                return trainUseCase.ExecuteAsync(httpContext.RequestAborted);
            });

            return endpointRouteBuilder;
        }
    }
}
