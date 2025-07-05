
namespace ReadApplicationDbContext.ReqdSqlDataAccess;

public interface IReadDataAccess
{
    Task<IEnumerable<T>> LoadData<T, UParams>(string storedProcedure, UParams parameters);
    Task SaveData<UParams>(string storedProcedure, UParams parameters);
}