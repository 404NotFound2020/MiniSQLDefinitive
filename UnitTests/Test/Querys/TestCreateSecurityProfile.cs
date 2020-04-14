using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestCreateSecurityProfile
    {
        [TestMethod]
        public void CreateSecurityProfile_ProfileDoenstExits_CreateProfile()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            CreateSecurityProfile query = CreateCreateSecurityProfile(databaseContainer);
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName);
            Column column = table.GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            query.SetProfileName(profileName);
            Assert.IsTrue(query.ValidateParameters());
            query.Execute();
            Assert.IsTrue(column.ExistCells(profileName));
        }

        [TestMethod]
        public void CreateSecurityProfile_ProfileExits_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            CreateSecurityProfile query = CreateCreateSecurityProfile(databaseContainer);
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName);
            Column column = table.GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            query.SetProfileName(profileName);
            query.Execute();

            query = CreateCreateSecurityProfile(databaseContainer);
            query.SetProfileName(profileName);
            Assert.IsFalse(query.ValidateParameters());
        }

        public static CreateSecurityProfile CreateCreateSecurityProfile(IDatabaseContainer container)
        {
            CreateSecurityProfile createSecurityProfile = new CreateSecurityProfile(container);
            createSecurityProfile.targetDatabase = SystemeConstants.SystemDatabaseName;
            createSecurityProfile.targetTableName = SystemeConstants.ProfilesTableName;
            return createSecurityProfile;
        }

    }
}

