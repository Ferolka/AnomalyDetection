using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Extensions
{
    public static class ResultExtensions
    {
        public static IResult MapToIResult(this ResultModel resultModel)
        {
            if (!resultModel.IsSuccess)
            {
                return MapException(resultModel);
            }

            return Results.NoContent();
        }
        public static IResult MapToIResult<T, TApi>(
            this ResultModel<T> resultModel,
            Func<T, TApi> mapper)
        {
            if (!resultModel.IsSuccess)
            {
                return MapException(resultModel);
            }

            return Results.Ok(mapper.Invoke(resultModel.Entity!));
        }
        private static IResult MapException(ResultModel resultModel) 
        {
            return resultModel.Status switch
            {
                Common.Constantans.ResultType.NotFound => Results.NotFound(),
                Common.Constantans.ResultType.DatabaseError => Results.Problem(resultModel.Exception!.Message),
                Common.Constantans.ResultType.UnexpectedError => Results.Problem(resultModel.Exception!.Message),
                _ => Results.Problem(resultModel.Exception!.Message)
            };
        }
    }
}
