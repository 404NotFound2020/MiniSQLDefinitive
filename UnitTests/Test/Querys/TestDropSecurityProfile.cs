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
    public class TestDropSecurityProfile
    {
        [TestMethod]
        public void DropSecurityProfile_TheProfileDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Column column = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while(column.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            DropSecurityProfile dropSecurityProfile = CreateDropSecurityProfile(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName);
            dropSecurityProfile.SetTargetSecurityProfile(profileName);
            Assert.IsFalse(dropSecurityProfile.ValidateParameters());
        }

        [TestMethod]
        public void DropSecurityProfile_TheProfileCannotDrop_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            DropSecurityProfile dropSecurityProfile = CreateDropSecurityProfile(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName);
            dropSecurityProfile.SetTargetSecurityProfile(SystemeConstants.DefaultProfile);
            Assert.IsFalse(dropSecurityProfile.ValidateParameters());
        }

        [TestMethod]
        public void DropSecurity_TheProfileCanDrop_DropTheProfile()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName);
            Column column = table.GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while(column.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.ProfileNameColumn).data = profileName;
            table.AddRow(row);
            int numberOfRow = table.GetRowCount();
            DropSecurityProfile dropSecurityProfile = CreateDropSecurityProfile(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName);
            dropSecurityProfile.SetTargetSecurityProfile(profileName);
            Assert.IsTrue(dropSecurityProfile.ValidateParameters());
            dropSecurityProfile.Execute();
            Assert.AreEqual(numberOfRow - 1, table.GetRowCount());
        }

        public static DropSecurityProfile CreateDropSecurityProfile(IDatabaseContainer container, string databaseName, string tableName)
        {
            DropSecurityProfile dropSecurityProfile = new DropSecurityProfile(container);
            dropSecurityProfile.targetDatabase = databaseName;
            dropSecurityProfile.targetTableName = tableName;
            return dropSecurityProfile;
        }

    }
}
