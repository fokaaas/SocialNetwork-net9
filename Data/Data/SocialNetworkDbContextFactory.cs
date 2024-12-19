using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Data;

public class SocialNetworkDbContextFactory : IDesignTimeDbContextFactory<SocialNetworkDbContext>
{
    public SocialNetworkDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<SocialNetworkDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SocialNetworkDb"));

        return new SocialNetworkDbContext(optionsBuilder.Options);
    }
}