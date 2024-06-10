namespace AnomalyDetection.Models
{
    public class PaginatedModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}