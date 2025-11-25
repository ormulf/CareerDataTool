using CareerDataTool.Domain.Enum;

namespace CareerDataTool.Domain.Words
{
    public class WordToIgnore
    {
        public int WordToIgnoreId { get; set; }
        public string Word { get; set; }
        public Language WordLanguage { get; set; }
    }
}
