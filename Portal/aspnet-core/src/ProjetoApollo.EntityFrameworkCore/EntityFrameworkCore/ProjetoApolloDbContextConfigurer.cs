using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ProjetoApollo.EntityFrameworkCore
{
    public static class ProjetoApolloDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ProjetoApolloDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ProjetoApolloDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
