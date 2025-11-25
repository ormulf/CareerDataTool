using CareerDataTool.Domain.Job;

namespace CareerDataTool.Domain.Enterprises
{
    public class Enterprise
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public List<JobVacancy> JobVacancies { get; set; }
    }
}
