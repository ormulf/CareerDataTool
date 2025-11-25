namespace CareerDataTool.Domain.Words
{
    public class KeyWordCategory
    {
        public int WordCategoryId { get; set; }
        public string Name { get; set; }
        public List<KeyWord> KeyWords { get; set; }
    }
}
