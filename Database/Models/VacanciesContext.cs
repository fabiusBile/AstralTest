using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    
    
    public partial class VacanciesContext : DbContext
    {
        private readonly string _dbPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
        private readonly string _dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
        
        public VacanciesContext()
        {
        }

        public VacanciesContext(DbContextOptions<VacanciesContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($@"Server={_dbServer};Persist Security Info=True;User ID=SA;Password={_dbPassword};");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
    }
}