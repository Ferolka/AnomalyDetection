using AnomalyDetection.Interfaces;
using Api.ApiModels;
using Api.Extensions;
using Api.Mapperly;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class PhraseEndpoints
    {
        public static IEndpointRouteBuilder MapPhraseEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/check", async (
                [FromBody] CheckPhraseApiModel checkPhraseApiModel,
                [FromServices] ICheckPhraseUseCase checkPhraseUseCase,
                HttpContext httpContext) =>
                {
                    var phraseML = CheckPhraseApiMapperly.Map(checkPhraseApiModel);

                    var checkPhraseOutResult = await checkPhraseUseCase.ExecuteAsync(phraseML, httpContext.RequestAborted);

                    return checkPhraseOutResult.MapToIResult(CheckPhraseResponseApiMapperly.Map);
                })
                .Produces<CheckPhraseResponseApiModel>();

            endpointRouteBuilder.MapGet("/predictedPhrases", async (
                [AsParameters] PaginatedModelApiModel paginatedModelApiModel,
                [FromServices] IPredictedPhraseUseCase predictedPhraseUseCase,
                HttpContext httpContext) =>
                {
                    var paginatedModel = PaginatedModelApiMapperly.Map(paginatedModelApiModel);

                    var predictedPhraseResult = await predictedPhraseUseCase
                        .GetUncheckedPredictedPhrases(paginatedModel, httpContext.RequestAborted);

                    return predictedPhraseResult.MapToIResult(
                        x => x.ConvertAll(PredictedPhraseApiMapperly.Map));
                })
                .Produces<List<PredictedPhraseApiModel>>();

            endpointRouteBuilder.MapPut("/verify", async (
                [FromBody] List<PredictedPhraseApiModel> predictedPhraseApiModels,
                [FromServices] IPredictedPhraseUseCase predictedPhraseUseCase,
                HttpContext httpContext) =>
                {
                    var predictedPhraseList = predictedPhraseApiModels.ConvertAll(PredictedPhraseApiMapperly.Map);

                    var predictedPhraseResult = await predictedPhraseUseCase
                        .VerifyPredictedPhrases(predictedPhraseList, httpContext.RequestAborted);

                    return predictedPhraseResult.MapToIResult();
                });

            return endpointRouteBuilder;
        }
    }
}