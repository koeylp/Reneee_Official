using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Reneee.Persistence.Extensions;

public class DatabaseOptionSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(DatabaseOptions options)
    {
        _configuration.GetSection("DatabaseOptions").Bind(options);
    }
}

