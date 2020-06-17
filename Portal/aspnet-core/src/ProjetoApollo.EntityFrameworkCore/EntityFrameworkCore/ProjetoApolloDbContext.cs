using Abp.Localization;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoApollo.Apollo.Core;
using ProjetoApollo.Authorization.Roles;
using ProjetoApollo.Authorization.Users;
using ProjetoApollo.MultiTenancy;

namespace ProjetoApollo.EntityFrameworkCore
{
    public class ProjetoApolloDbContext : AbpZeroDbContext<Tenant, Role, User, ProjetoApolloDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Institution> Institution { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Speciality> Speciality { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Historic> Historic { get; set; }
        public DbSet<Questionary> Questionary { get; set; }
        public DbSet<QuestionaryAnswers> QuestionaryAnswers { get; set; }
        public DbSet<MedicalConsultation> MedicalConsultation { get; set; }

        public ProjetoApolloDbContext(DbContextOptions<ProjetoApolloDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Contact> (e => {
            //     e.HasOne (r => r.Institution).WithOne (u => u.TechnicalContact);
            // });

            modelBuilder.HasDefaultSchema("Apollo");
            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); // any integer that is smaller than 10485760
        }
    }
}