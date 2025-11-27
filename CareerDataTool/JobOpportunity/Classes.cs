using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobOpportunity
{
    public class KeyWordsWithVariations
    {
        public string KeyWord { get; set; }
        public List<string> Variations { get; set; }
    }
    public class KeyWordsWithVariationsFromChratGPT
    {
        public string canonical { get; set; }
        public List<string> variants { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string fine_category { get; set; }
    }
    public class WordVariation
    {
        public int IdVariation { get; set; }
        public int IdWord { get; set; }
        public string Word { get; set; }
    }
    public class KeyWordsAllInfo
    {
        public int IdWord { get; set; }
        public string KeyWord { get; set; }
        public List<string> Variations { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string FineCategory { get; set; }

        public void PrintInfo()
        {
            Console.WriteLine($"KeyWord: {KeyWord}");
            Console.WriteLine($"Variations: {string.Join(", ", Variations)}");
            Console.WriteLine($"Description: {Description.Substring(0, Description.Length / 5)}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"FineCategory: {FineCategory}");
        }
        public string GetSQLInsertCommand()
        {
            var variationsString = string.Join(";", Variations).Replace("'", "''");
            var descriptionString = Description.Replace("'", "''");
            var categoryString = Category.Replace("'", "''");
            var fineCategoryString = FineCategory;
            return $"INSERT INTO KeyWords (word,variations,description,category,fineCategory) VALUES ('{KeyWord}', '{variationsString}', '{descriptionString}', '{categoryString}', '{fineCategoryString}');";
        }
    }
    public class JobFullInfo
    {
        public int IdJob { get; set; }
        public string JobId { get; set; }
        public string UrlJob { get; set; }
        public string[] KeyWords { get; set; }
        public string Language { get; set; }
        public string Role { get; set; }
        public string RoleLevel { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string JobLocal { get; set; }
        public string JobWhen { get; set; }
        public string JobCandidatesGross { get; set; }
        public string JobDescription { get; set; }
        public JobFullInfo()
        {

        }
        public JobFullInfo(JobItem jobItem, JobItemAddedInfo jobItemAddedInfo)
        {
            JobId = jobItem.JobId;
            UrlJob = jobItem.UrlJob;
            KeyWords = jobItemAddedInfo.KeyWords;
            Language = jobItemAddedInfo.Language;
            Role = jobItemAddedInfo.Role;
            RoleLevel = jobItemAddedInfo.RoleLevel;
            Company = jobItem.Company;
            JobTitle = jobItem.JobTitle;
            JobLocal = jobItem.JobLocal;
            JobWhen = jobItem.JobWhen;
            ParseJobWhenToDate();
            JobCandidatesGross = jobItem.JobCandidatesGross;
            ParseJobCandidatesGross();
            JobDescription = jobItem.JobDescription;
        }
        private void ParseJobCandidatesGross()
        {
            int qtdCandidates = -1;
            if (JobCandidatesGross == "Mais de 100 pessoas clicaram em Candidatar-se" || JobCandidatesGross == "Mais de 100 candidaturas")
            {
                qtdCandidates = 101;
            }
            else if (JobCandidatesGross.Contains(" candidaturas"))
            {
                qtdCandidates = int.Parse(JobCandidatesGross.Split(' ')[0]);
            }
            else if (JobCandidatesGross == "pessoas clicaram em Candidatar-se")
            {
                qtdCandidates = int.Parse(JobCandidatesGross.Split(' ')[0]);
            }
            JobCandidatesGross = qtdCandidates.ToString();
        }
        private void ParseJobWhenToDate()
        {
            //dias,dia,semnas,semana,meses,mês
            var date = DateTime.Now;
            if (JobWhen == " · ")
            {
                JobWhen = date.ToString("yyyy-MM-dd");
            }
            else
            {
                try
                {
                    var parts = JobWhen.Split(' ');
                    int number = int.Parse(parts[parts.Length - 2]);
                    string period = parts[parts.Length - 1];

                    switch (period.ToLower())
                    {
                        case "dias":
                        case "dia":
                            date = date.AddDays(-number);
                            break;
                        case "semanas":
                        case "semana":
                        case "semanas,":
                            date = date.AddDays(-number * 7);
                            break;
                        case "meses":
                        case "mês":
                            date = date.AddMonths(-number);
                            break;
                        default:
                            break;
                    }
                    JobWhen = date.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    JobWhen = date.ToString("yyyy-MM-dd");
                }

            }
        }

        public void PrintShortInfo()
        {
            Console.WriteLine($"Company: {Company}, Job Title: {JobTitle}");
            Console.WriteLine($"JobWhen: {JobWhen}, JobCandidatesGross: {JobCandidatesGross}");
        }


    }
    public class JobItemAddedInfo
    {
        [JsonPropertyName("urlJob")]
        public string UrlJob { get; set; }
        [JsonPropertyName("keyWords")]
        public string[] KeyWords { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("roleLevel")]
        public string RoleLevel { get; set; }

        public string JobId => GetCurrentJobIdFromUrlJob();

        private string GetCurrentJobIdFromUrlJob()
        {
            var parameters = UrlJob.Split('?')[1];
            var paramList = parameters.Split('&');
            foreach (var param in paramList)
            {
                var keyValue = param.Split('=');
                if (keyValue[0] == "currentJobId")
                {
                    return keyValue[1];
                }
            }
            return string.Empty;
        }
    }

    public class WordsToClassify
    {
        [JsonPropertyName("word")]
        public string Word { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("compound")]
        public bool Compound { get; set; }

        [JsonPropertyName("ignorelist")]
        public bool IgnoreList { get; set; }
    }

    public class JobData
    {
        [JsonPropertyName("items")]
        public List<JobItem> JobFromJson { get; set; }
    }

    public class JobItem
    {
        [JsonPropertyName("urlJob")]
        public string UrlJob { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("jobTitle")]
        public string JobTitle { get; set; }

        [JsonPropertyName("jobLocal")]
        public string JobLocal { get; set; }

        [JsonPropertyName("jobWhen")]
        public string JobWhen { get; set; }

        [JsonPropertyName("jobCandidatesGross")]
        public string JobCandidatesGross { get; set; }

        [JsonPropertyName("jobDescription")]
        public string JobDescription { get; set; }
        public string JobId => GetCurrentJobIdFromUrlJob();

        private string GetCurrentJobIdFromUrlJob()
        {
            var parameters = UrlJob.Split('?')[1];
            var paramList = parameters.Split('&');
            foreach (var param in paramList)
            {
                var keyValue = param.Split('=');
                if (keyValue[0] == "currentJobId")
                {
                    return keyValue[1];
                }
            }
            return string.Empty;
        }
        public void PrintShortInfo()
        {
            Console.WriteLine($"Company: {Company}, Job Title: {JobTitle}");
        }
        public void PrintJobInfo()
        {
            Console.WriteLine($"URL: {UrlJob}");
            Console.WriteLine($"Company: {Company}");
            Console.WriteLine($"Job Title: {JobTitle}");
            Console.WriteLine($"Location: {JobLocal}");
            Console.WriteLine($"When: {JobWhen}");
            Console.WriteLine($"Candidates Gross: {JobCandidatesGross}");
            Console.WriteLine($"Description: {JobDescription}");
        }
    }
}
