using Common.Constantans;

namespace Common.Entities
{
    public class Phrase
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsToxic { get; set; }
    }
}