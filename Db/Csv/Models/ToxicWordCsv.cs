using CsvHelper.Configuration.Attributes;

namespace Db.Csv.Models
{
    public class ToxicWordCsv
    {
        [Name("text")]
        public string? Text { get; set; }

        [Name("is_toxic")]
        [BooleanTrueValues("Toxic")]
        [BooleanFalseValues("Not Toxic")]
        public bool IsToxic { get; set; }
    }
}