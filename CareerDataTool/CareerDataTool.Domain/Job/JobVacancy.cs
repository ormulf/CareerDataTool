using CareerDataTool.Domain.Enterprises;
using CareerDataTool.Domain.Location;
using CareerDataTool.Domain.Enum;
using CareerDataTool.Domain.Job.Enum;
using CareerDataTool.Domain.Words;
namespace CareerDataTool.Domain.Job
{
    public class JobVacancy
    {
        public int JobVacancyId { get; set; }
        public int EnterpriseId { get; set; }
        public Enterprise JobEnterprise { get; set; }
        public string IdFromSite { get; set; }
        public string UrlFromSite { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Place JobPlace { get; set; }
        public DateOnly WhenWasPublishedInSite { get; set; }
        public DateOnly WhenWasRegister { get; set; }
        public Language JobLanguage { get; set; }
        public WorkMode ?JobWorkMode { get; set; }
        public Role JobRole { get; set; }
        public RoleLevel ?JobRoleLevel { get; set; }
        public List<KeyWord> KeyWords { get; set; }

    }
}
