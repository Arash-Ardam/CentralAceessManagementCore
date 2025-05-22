
namespace Domain.DataModels
{
    public class DataCenter
    {
        public string Name { get; set; } = string.Empty;

        public List<DatabaseEngine> DatabaseEngines { get; set; } = new List<DatabaseEngine>();

        public List<Access> Accesses { get; set; } = new List<Access>();

        private DataCenter()
        {

        }

        // null pattern
        private static readonly DataCenter _empty  = new DataCenter();
        public static DataCenter Empty { get { return _empty; } }

        public static DataCenter CreateByName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) ?
                new DataCenter() { Name = name }
                : throw new Exception("Name cannot be null !!!");
        }

        public void UpdateName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            else throw new Exception("name cannot be null or whitespace");
        }

        #region Access Specs

        public void AddAccess(Access access)
        {
            if(access != Access.Empty)
                this.Accesses.Add(access);
        }

        public void RemoveAccess(Access access)
        {
            if(access != Access.Empty)
                this.Accesses.Remove(access);
        }

        #endregion

        #region DataBase Engine Specs
        public string GetNameByDatabaseEngine(DatabaseEngine engine)
        {
            if (DatabaseEngines.Contains(engine))
            {
                return Name;
            }
            else
                throw new Exception("the DbEngine Entry is not exist");
        }

        public void AddDatabaseEngine(DatabaseEngine databaseEngine)
        {
            if (databaseEngine != DatabaseEngine.Empty)
                DatabaseEngines.Add(databaseEngine);
            else if (databaseEngine == default)
            {
                throw new Exception("DCEngine cannot be Empty");
            }
            else
                throw new Exception("DatabaseEngine with null params is not acceptable !!!");
        }

        public DatabaseEngine? GetDatabaseEngineByAddress(string address)
        {
            return DatabaseEngines.FirstOrDefault(engine => engine.Address == address) ?? DatabaseEngine.Empty;
        }

        public DatabaseEngine? GetDatabaseEngineByName(string name)
        {
            return DatabaseEngines.FirstOrDefault(engine => engine.Name == name) ?? DatabaseEngine.Empty;
        }

        public void DeleteDatabaseEngineByName(string name)
        {
            var toRemoveDatabaseEngine = GetDatabaseEngineByName(name);

            if (toRemoveDatabaseEngine != DatabaseEngine.Empty)
                DatabaseEngines.Remove(toRemoveDatabaseEngine!);
            else
                throw new Exception($"There is no Entity with name :{name} to remove");
        }

        public void DeleteDatabaseEngineByAddress(string address)
        {
            var toRemoveDatabaseEngine = GetDatabaseEngineByAddress(address);

            if (toRemoveDatabaseEngine != DatabaseEngine.Empty)
                DatabaseEngines.Remove(toRemoveDatabaseEngine!);
            else
                throw new Exception($"There is no Entity with address : {address} to remove");
        }

        #endregion

    }
}
