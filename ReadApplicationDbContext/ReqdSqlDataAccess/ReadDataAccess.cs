using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace ReadApplicationDbContext.ReqdSqlDataAccess;

internal class ReadDataAccess : IReadDataAccess
{
    private readonly IOptions<ReadDbContextConfigEntry> _options;

    private readonly ReadDbContextConfigEntry _readConfigs;

    public ReadDataAccess(IOptions<ReadDbContextConfigEntry> options)
    {
        _options = options;
        _readConfigs = _options.Value;
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
