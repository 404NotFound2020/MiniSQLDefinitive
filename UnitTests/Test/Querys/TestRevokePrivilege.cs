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
    public class TestRevokePrivilege
    {
        [TestMethod]
        public void RevokePrivilege_ProfileDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string profileName = ShittyUtilities.GetDoenstExistProfileName(databaseContainer);
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(profileName, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName, SystemeConstants.InsertPrivilegeName);
            Assert.IsFalse(revoquePrivilege.ValidateParameters());            
        }

        [TestMethod]
        public void RevokePrivilege_DatabaseDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string databaseName = VariousFunctions.GenerateRandomString(8);
            while(databaseContainer.ExistDatabase(databaseName)) databaseName = VariousFunctions.GenerateRandomString(8);
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(SystemeConstants.DefaultProfile, databaseName, SystemeConstants.ProfilesTableName, SystemeConstants.InsertPrivilegeName);
            Assert.IsFalse(revoquePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevokePrivilege_TableDoesntExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase database = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName);
            string tableName = VariousFunctions.GenerateRandomString(8);
            while (database.ExistTable(tableName)) tableName = VariousFunctions.GenerateRandomString(8);
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(SystemeConstants.DefaultProfile, SystemeConstants.SystemDatabaseName, tableName, SystemeConstants.InsertPrivilegeName);
            Assert.IsFalse(revoquePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevokePrivilege_PrivilegeDoesntExist_NoticeInValidate() {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Column privilegesColumn = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesTableName).GetColumn(SystemeConstants.PrivilegesPrivilegeNameColumnName);
            string privilegeName = VariousFunctions.GenerateRandomString(8);
            while(privilegesColumn.ExistCells(privilegeName)) privilegeName = VariousFunctions.GenerateRandomString(8);
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(SystemeConstants.DefaultProfile, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName, privilegeName);
            Assert.IsFalse(revoquePrivilege.ValidateParameters());        
        }

        [TestMethod]
        public void RevokePrivilege_AllParamsExistsButTheCombinationIsNotValid_NoticeInValidate()
        {
            
            //Prefase
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            CreateSecurityProfile createSecurityProfile = TestCreateSecurityProfile.CreateCreateSecurityProfile(databaseContainer);
            Column profileNamesColumn = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            string profileName = VariousFunctions.GenerateRandomString(8);
            while (profileNamesColumn.ExistCells(profileName)) profileName = VariousFunctions.GenerateRandomString(8);
            createSecurityProfile.SetProfileName(profileName);
            createSecurityProfile.ValidateParameters();
            createSecurityProfile.Execute();
            //TEST
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(profileName, SystemeConstants.SystemDatabaseName, SystemeConstants.ProfilesTableName, SystemeConstants.InsertPrivilegeName);
            Assert.IsFalse(revoquePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void RevokePrivilege_AllOK_RevokePrivilege()
        {
            //Prefase            
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IDatabase database = databaseContainer.GetDatabase(SystemeConstants.DefaultDatabaseName);
            Table table = new Table("table");
            database.AddTable(table);
            string profileName = SystemeConstants.DefaultProfile;
            string privilegeName = SystemeConstants.InsertPrivilegeName;
            GrantPrivilege grantPrivilege = TestGrantPrivilege.CreateGrantPrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            grantPrivilege.SetData(privilegeName, profileName, database.databaseName, table.tableName);
            grantPrivilege.ValidateParameters();
            grantPrivilege.Execute();
            ITable profilesPrivilegesTable = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            int rowCounts = profilesPrivilegesTable.GetRowCount();
            //Test
            RevoquePrivilege revoquePrivilege = CreateRevokePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnTablesTableName);
            revoquePrivilege.SetData(profileName, database.databaseName, table.tableName, privilegeName);
            Assert.IsTrue(revoquePrivilege.ValidateParameters());
            revoquePrivilege.Execute();
            Assert.AreEqual(rowCounts - 1, profilesPrivilegesTable.GetRowCount());
        }

        public static RevoquePrivilege CreateRevokePrivilege(IDatabaseContainer container, string databaseName, string tableName)
        {
            RevoquePrivilege revoquePrivilege = new RevoquePrivilege(container);
            revoquePrivilege.targetDatabase = databaseName;
            revoquePrivilege.targetTableName = tableName;
            return revoquePrivilege;
        }

    }
}
