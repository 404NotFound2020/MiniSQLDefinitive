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
    public class TestRevokeDatabasePrivilege
    {
        [TestMethod]
        public void RevoqueDatabasePrivilege_ProfileDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Column profileNamesColumn = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while (profileNamesColumn.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            RevokeDatabasePrivilege revoqueDatabasePrivilege = CreateRevoqueDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            revoqueDatabasePrivilege.SetData(profileName, SystemeConstants.DefaultDatabaseName, SystemeConstants.CreatePrivilegeName);
            Assert.IsFalse(revoqueDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevoqueDatabasePrivilege_DatabaseDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string databaseName = VariousFunctions.GenerateRandomString(8);
            while(databaseContainer.ExistDatabase(databaseName)) databaseName = VariousFunctions.GenerateRandomString(8);
            RevokeDatabasePrivilege revoqueDatabasePrivilege = CreateRevoqueDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            revoqueDatabasePrivilege.SetData(SystemeConstants.DefaultProfile, databaseName, SystemeConstants.CreatePrivilegeName);
            Assert.IsFalse(revoqueDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevoqueDatabasePrivilege_PrivilegeDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Column databasePrivilegesColumn = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.DatabasesPrivilegesTableName).GetColumn(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName);
            string databasePrivilegeName = VariousFunctions.GenerateRandomString(8);
            while(databasePrivilegesColumn.ExistCells(databasePrivilegeName)) databasePrivilegeName = VariousFunctions.GenerateRandomString(8);
            RevokeDatabasePrivilege revoqueDatabasePrivilege = CreateRevoqueDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            revoqueDatabasePrivilege.SetData(SystemeConstants.DefaultProfile, SystemeConstants.DefaultDatabaseName, databasePrivilegeName);
            Assert.IsFalse(revoqueDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevoqueDatabasePrivilege_AllParamsExist_RowExist_DeleteRow()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data = SystemeConstants.DefaultProfile;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data = SystemeConstants.DefaultDatabaseName;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data = SystemeConstants.CreatePrivilegeName;
            table.AddRow(row);
            int rowNumber = table.GetRowCount();
            RevokeDatabasePrivilege revoqueDatabasePrivilege = CreateRevoqueDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            revoqueDatabasePrivilege.SetData(SystemeConstants.DefaultProfile, SystemeConstants.DefaultDatabaseName, SystemeConstants.CreatePrivilegeName);
            Assert.IsTrue(revoqueDatabasePrivilege.ValidateParameters());
            revoqueDatabasePrivilege.Execute();
            Assert.AreEqual(rowNumber - 1, table.GetRowCount()); 
        }

        public static RevokeDatabasePrivilege CreateRevoqueDatabasePrivilege(IDatabaseContainer container, string databaseName, string tableName)
        {
            RevokeDatabasePrivilege revoqueDatabasePrivilege = new RevokeDatabasePrivilege(container);
            revoqueDatabasePrivilege.targetDatabase = databaseName;
            revoqueDatabasePrivilege.targetTableName = tableName;
            return revoqueDatabasePrivilege;
        }

    }
}
