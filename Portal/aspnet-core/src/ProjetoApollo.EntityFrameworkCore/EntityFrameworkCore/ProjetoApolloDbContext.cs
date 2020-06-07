using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ProjetoApollo.Authorization.Roles;
using ProjetoApollo.Authorization.Users;
using ProjetoApollo.MultiTenancy;
using Abp.Localization;

namespace ProjetoApollo.EntityFrameworkCore
{
    public class ProjetoApolloDbContext : AbpZeroDbContext<Tenant, Role, User, ProjetoApolloDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public ProjetoApolloDbContext(DbContextOptions<ProjetoApolloDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Apollo");
            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); // any integer that is smaller than 10485760
        }
    }
}
