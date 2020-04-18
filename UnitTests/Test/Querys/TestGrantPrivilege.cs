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
    public class TestGrantPrivilege
    {
        [TestMethod]
        public void GrantPrivilege_DatabaseDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string randomDatabaseName = VariousFunctions.GenerateRandomString(8);
            while (databaseContainer.ExistDatabase(randomDatabaseName)) randomDatabaseName = VariousFunctions.GenerateRandomString(8);
            GrantPrivilege grantPrivilege = CreateGrantPrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(SystemeConstants.InsertPrivilegeName, SystemeConstants.DefaultProfile, randomDatabaseName, "aaa");
            Assert.IsFalse(grantPrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantPrivilege_TableDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase database = databaseContainer.GetDatabase(SystemeConstants.DefaultDatabaseName);
            string randomTableName = VariousFunctions.GenerateRandomString(8);
            while (database.ExistTable(randomTableName)) randomTableName = VariousFunctions.GenerateRandomString(8);
            GrantPrivilege grantPrivilege = CreateGrantPrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(SystemeConstants.InsertPrivilegeName, SystemeConstants.DefaultProfile, database.databaseName, randomTableName);
            Assert.IsFalse(grantPrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantPrivilege_PrivilegeDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase targetDatabase = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName);
            Column column = targetDatabase.GetTable(SystemeConstants.PrivilegesTableName).GetColumn(SystemeConstants.PrivilegesPrivilegeNameColumnName);
            string privilegeName = VariousFunctions.GenerateRandomString(8);
            while(column.ExistCells(privilegeName)) privilegeName = VariousFunctions.GenerateRandomString(8);
            GrantPrivilege grantPrivilege = CreateGrantPrivilege(databaseContainer, targetDatabase.databaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(privilegeName, SystemeConstants.DefaultProfile, targetDatabase.databaseName, SystemeConstants.PrivilegesTableName);
            Assert.IsFalse(grantPrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantPrivilege_ProfileDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase targetDatabase = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName);
            Column column = targetDatabase.GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while(column.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            GrantPrivilege grantPrivilege = CreateGrantPrivilege(databaseContainer, targetDatabase.databaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(SystemeConstants.InsertPrivilegeName, profileName, targetDatabase.databaseName, SystemeConstants.PrivilegesTableName);
            Assert.IsFalse(grantPrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantPrivilege_AllParametersAreOK_GrantThePrivilege()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase database = databaseContainer.GetDatabase(SystemeConstants.DefaultDatabaseName);
            Table table = new Table("table");
            database.AddTable(table);
            GrantPrivilege grantPrivilege = CreateGrantPrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(SystemeConstants.InsertPrivilegeName, SystemeConstants.DefaultProfile, database.databaseName, table.tableName);
            Assert.IsTrue(grantPrivilege.ValidateParameters());
        }

        public static GrantPrivilege CreateGrantPrivilege(IDatabaseContainer databaseContainer, string databaseName, string tableName)
        {
            GrantPrivilege grantPrivilege = new GrantPrivilege(databaseContainer);
            grantPrivilege.targetDatabase = databaseName;
            grantPrivilege.targetTableName = tableName;
            return grantPrivilege;
        }
    }
}
