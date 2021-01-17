using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext: DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations;
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications;
        public DbSet<ApplicantProfilePoco> ApplicantProfiles;
        public DbSet<ApplicantResumePoco> ApplicantResumes;
        public DbSet<ApplicantSkillPoco> ApplicantSkills;
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories;
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions;
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions;
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations;
        public DbSet<CompanyJobPoco> CompanyJobs;
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills;
        public DbSet<CompanyLocationPoco> CompanyLocations;
        public DbSet<CompanyProfilePoco> CompanyProfiles;
        public DbSet<SecurityLoginPoco> SecurityLogins;
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs;
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles;
        public DbSet<SecurityRolePoco> SecurityRoles;
        public DbSet<SystemCountryCodePoco> SystemCountryCodes;
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes;
                
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();

            string _connectionString = root
                                        .GetSection("ConnectionStrings")
                                        .GetSection("DataConnection").Value;

            optionsBuilder.UseSqlServer(_connectionString);
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Applicant_Educations
            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(ap => ap.ApplicantProfile)
                .WithMany(ae => ae.ApplicantEducations)
                .HasForeignKey(ae => ae.Applicant);

            //Applicant_Job_Applications
            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(ap => ap.ApplicantProfile)
                .WithMany(aja => aja.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(cj => cj.CompanyJob)
                .WithMany(aja => aja.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Job);

            //Applicant_Profiles
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(sl => sl.SecurityLogins)
                .WithMany(ap => ap.ApplicantProfiles)
                .HasForeignKey(ap => ap.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(scc => scc.SystemCountryCode)
                .WithMany(ap => ap.ApplicantProfile)
                .HasForeignKey(ap => ap.Country);

            //Applicant_Resumes
            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(ap => ap.ApplicantProfile)
                .WithMany(ar => ar.ApplicantResumes)
                .HasForeignKey(ar => ar.Applicant);

            //Applicant_Skills
            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(ap => ap.ApplicantProfile)
                .WithMany(asp => asp.ApplicantSkills)
                .HasForeignKey(asp => asp.Applicant);

            //Applicant_Work_History
            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(ap => ap.ApplicantProfile)
                .WithMany(awh => awh.ApplicantWorkHistorys)
                .HasForeignKey(awh => awh.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(sc => sc.SystemCountryCode)
                .WithMany(awh => awh.ApplicantWorkHistory)
                .HasForeignKey(awh => awh.CountryCode);

            //Company_Description
            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cp => cp.CompanyProfile)
                .WithMany(cd => cd.CompanyDescriptions)
                .HasForeignKey(cd => cd.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(slc => slc.SystemLanguageCode)
                .WithMany(cd => cd.CompanyDescriptions)
                .HasForeignKey(cd => cd.LanguageId);

            //Company_Job_Education
            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(cj => cj.CompanyJob)
                .WithMany(cje => cje.CompanyJobEducations)
                .HasForeignKey(cje => cje.Job);

            //Company_Job_Skill
            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(cj => cj.CompanyJob)
                .WithMany(cjs => cjs.CompanyJobSkills)
                .HasForeignKey(cjs => cjs.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(cp => cp.CompanyProfile)
                .WithMany(cj => cj.CompanyJobs)
                .HasForeignKey(cj => cj.Company);

            //Company_Job_Description
            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(cj => cj.CompanyJob)
                .WithMany(cjd => cjd.CompanyJobDescriptions)
                .HasForeignKey(cjd => cjd.Job);

            //Company_Location
            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cp => cp.CompanyProfile)
                .WithMany(cl => cl.CompanyLocations)
                .HasForeignKey(cl => cl.Company);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(sl => sl.SecurityLogin)
                .WithMany(sll => sll.SecurityLoginsLogs)
                .HasForeignKey(sll => sll.Login);

            //Security_Logins_Role
            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(sl => sl.SecurityLogin)
                .WithMany(slr => slr.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(sr => sr.SecurityRole)
                .WithMany(slr => slr.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Role);



            base.OnModelCreating(modelBuilder);
        }
    }
}
