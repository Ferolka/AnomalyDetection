using Microsoft.AspNetCore.Mvc;

namespace Api.ApiModels
{
    public class PaginatedModelApiModel
    {
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;


        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 100;
    }
}
