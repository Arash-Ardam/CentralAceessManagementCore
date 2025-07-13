using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace ReadDbContext;

internal class ReadDataAccess : IReadDataAccess
{

    private readonly ReadDbContextConfigEntry? _readConfigs;

    public ReadDataAccess(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var config = configuration.GetRequiredSection(nameof(ReadDbContextConfigEntry));

        _readConfigs = config.Get<ReadDbContextConfigEntry>(); 
    }


    public async Task<IEnumerable<T>> LoadData<T, UParams>(
        string storedProcedure,
        UParams parameters
        )
    {
        using IDbConnection connection = new SqlConnection(_readConfigs.dapper.ConnectionString);

        return await connection.QueryAsync<T>(storedProcedure,
                                              parameters,
                                              commandType: CommandType.StoredProcedure);

    }

    public async Task SaveData<UParams>(
        string storedProcedure,
        UParams parameters
        )
    {
        using IDbConnection connection = new SqlConnection(_readConfigs.dapper.ConnectionString);

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
