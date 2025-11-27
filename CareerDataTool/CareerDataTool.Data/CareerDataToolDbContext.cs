using CareerDataTool.Domain.Enterprises;
using CareerDataTool.Domain.Job;
using CareerDataTool.Domain.Location;
using CareerDataTool.Domain.Words;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CareerDataTool.Data
{
    public class CareerDataToolDbContext : DbContext
    {
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<KeyWordCategory> KeyWordCategories { get; set; }
        public DbSet<KeyWordVariation> KeyWordVariations { get; set; }
        public DbSet<WordToIgnore> WordsToIgnore { get; set; }

        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = "BRUNOPLAY",
            UserID = "sa",
            Password = "SenhaSql",
            InitialCatalog = "CareerDataTool",
            TrustServerCertificate = true
        };
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }
}
