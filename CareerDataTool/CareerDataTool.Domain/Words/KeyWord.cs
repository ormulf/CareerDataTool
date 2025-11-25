using CareerDataTool.Domain.Job;

namespace CareerDataTool.Domain.Words
{
    public class KeyWord
    {
        public int KeyWordId { get; set; }
        public int WordCategoryId { get; set; }
        public KeyWordCategory Category { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }
        public List<KeyWordVariation> Variations { get; set; }
        public List<JobVacancy> jobVacancies { get; set; }
    }
}
