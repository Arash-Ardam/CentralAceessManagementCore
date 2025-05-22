using Domain.DataModels;
using Domain.Enums;
using Newtonsoft.Json;

namespace DomainTest
{
    public class DataCenterTest
    {
        [Fact]
        public void CreateByName_Should_Work()
        {
            //Arrange 

            //Act
            DataCenter newDataCenter = DataCenter.CreateByName("DCTest_1");

            //Assert
            Assert.True(newDataCenter != null);
        }

        [Fact]
        public void CreateByName_With_NullArgs_Should_Return_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<Exception>(() => DataCenter.CreateByName(" "));
        }

        [Fact]
        public void UpdateName_should_work()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDC");

            //Act
            dataCenter.UpdateName("newTestDC");

            //Assert
            Assert.True(dataCenter.Name == "newTestDC");
        }

        [Fact]
        public void UpdateName_with_nullOrwhitespace_should_throw_exception()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDC");

            //Act

            //Assert
            Assert.Throws<Exception>(() => dataCenter.UpdateName(" "));
        }

        [Fact]
        public void Add_DatabaseEngine_Should_Work()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDC");
            DatabaseEngine newDatabaseEngine = DatabaseEngine.CreateByNameAndAddress("TPG_Dev_Db", "172.16.27.13");

            //Act

            dataCenter.AddDatabaseEngine(newDatabaseEngine);

            //Assert
            Assert.True(dataCenter.DatabaseEngines.Contains(newDatabaseEngine));
        }

        [Fact]
        public void Add_DatabaseEngine_With_NullData_SHould_Throw_Exception()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDC");
            DatabaseEngine emptyDatabaseEngine = DatabaseEngine.Empty;

            //Act

            //Assert
            Assert.Throws<Exception>(() => dataCenter.AddDatabaseEngine(emptyDatabaseEngine));
        }

        [Fact]
        public void Get_DatabaseEngine_With_ExistedAddress_Should_work()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            dataCenter.AddDatabaseEngine(databaseEngine);

            //Act
            var actualResult = dataCenter.GetDatabaseEngineByAddress("189.26.25.13");

            //Assert
            Assert.Equal(databaseEngine, actualResult);
        }

        [Fact]
        public void Get_DatabaseEngine_with_NotExistedAddress_Should_Return_EmptyDatabase()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            dataCenter.AddDatabaseEngine(databaseEngine);

            //Act
            var actualResult = dataCenter.GetDatabaseEngineByAddress("189.26.25.14");

            //Assert
            Assert.Equal(DatabaseEngine.Empty, actualResult);
        }

        [Fact]
        public void Get_DatabaseEngine_With_ExistedName_Should_work()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            dataCenter.AddDatabaseEngine(databaseEngine);

            //Act
            var actualResult = dataCenter.GetDatabaseEngineByName("testDbEngine");

            //Assert
            Assert.Equal(databaseEngine, actualResult);
        }

        [Fact]
        public void Get_DatabaseEngine_with_NotExistedName_Should_Return_EmptyDatabase()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            dataCenter.AddDatabaseEngine(databaseEngine);

            //Act
            var actualResult = dataCenter.GetDatabaseEngineByName("ErrorName");

            //Assert
            Assert.Equal(DatabaseEngine.Empty, actualResult);
        }

        [Fact]
        public void Delete_DatabaseEngine_with_wxisted_entry_should_work()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            var databaseEngine2 = DatabaseEngine.CreateByNameAndAddress("testDbEngine2", "189.26.25.14");
            dataCenter.AddDatabaseEngine(databaseEngine);
            dataCenter.AddDatabaseEngine(databaseEngine2);

            //Act
            dataCenter.DeleteDatabaseEngineByName("testDbEngine");
            dataCenter.DeleteDatabaseEngineByAddress("189.26.25.14");

            //Assert
            Assert.True(dataCenter.DatabaseEngines.Count() == 0);
        }

        [Fact]
        public void Delete_DatabaseEngine_with_notexistedData_should_throw_exception()
        {
            //Arrange
            DataCenter dataCenter = DataCenter.CreateByName("TestDc");
            var databaseEngine = DatabaseEngine.CreateByNameAndAddress("testDbEngine", "189.26.25.13");
            var databaseEngine2 = DatabaseEngine.CreateByNameAndAddress("testDbEngine2", "189.26.25.14");
            dataCenter.AddDatabaseEngine(databaseEngine);
            dataCenter.AddDatabaseEngine(databaseEngine2);

            //Act

            //Assert
            Assert.Throws<Exception>(() => dataCenter.DeleteDatabaseEngineByName("errortest"));
            Assert.Throws<Exception>(() => dataCenter.DeleteDatabaseEngineByAddress("1.1.1.1"));
        }


        [Fact]
        public void Create_inbound_Access_with_trueData_should_work()
        {
            //Arrange
            DatabaseEngine databaseEngine1 = DatabaseEngine.CreateByNameAndAddress("DCEngine1", "1.1.1.1");
            string jsonSource = JsonConvert.SerializeObject(databaseEngine1);
            DatabaseEngine databaseEngine2 = DatabaseEngine.CreateByNameAndAddress("DCEngine2", "8.8.8.8");
            string jsonDestination = JsonConvert.SerializeObject(databaseEngine2);

            //Act
            Access newAccess = new Access.Create()
                .AddSource(jsonSource)
                .AddDestination(jsonDestination)
                .AddPort(80)
                .SetDirection(DatabaseDirection.InBound)
                .Build();
                                        
            //Assert
            newAccess.Direction = DatabaseDirection.InBound;
        }

        [Fact]
        public void Create_outbound_Access_with_trueData_sould_work()
        {
            //Arrange
            DatabaseEngine databaseEngine1 = DatabaseEngine.CreateByNameAndAddress("DCEngine1", "1.1.1.1");
            string jsonSource = JsonConvert.SerializeObject(databaseEngine1);
            DatabaseEngine databaseEngine2 = DatabaseEngine.CreateByNameAndAddress("DCEngine2", "8.8.8.8");
            string jsonDestination = JsonConvert.SerializeObject(databaseEngine2);

            //Act
            Access newAccess = new Access.Create()
                .AddSource(jsonSource)
                .AddDestination(jsonDestination)
                .AddPort(80)
                .SetDirection(DatabaseDirection.OutBound)
                .Build();

            //Assert
            newAccess.Direction = DatabaseDirection.OutBound;
        }

    }

}