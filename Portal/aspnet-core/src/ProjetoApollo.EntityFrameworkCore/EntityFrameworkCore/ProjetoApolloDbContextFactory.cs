using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ProjetoApollo.Configuration;
using ProjetoApollo.Web;

namespace ProjetoApollo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ProjetoApolloDbContextFactory : IDesignTimeDbContextFactory<ProjetoApolloDbContext>
    {
        public ProjetoApolloDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProjetoApolloDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ProjetoApolloDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ProjetoApolloConsts.ConnectionStringName));

            return new ProjetoApolloDbContext(builder.Options);
        }
    }
}
