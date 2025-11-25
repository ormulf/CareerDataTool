namespace CareerDataTool.Domain.Words
{
    public class KeyWordVariation
    {
        public int KeyWordVariationId { get; set; }
        public int KeyWordId { get; set; }
        public KeyWord ParentWord { get; set; }
        public string Word { get; set; }
    }
}
