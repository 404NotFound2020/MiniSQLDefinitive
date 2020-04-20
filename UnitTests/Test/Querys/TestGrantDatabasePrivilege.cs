using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestGrantDatabasePrivilege
    {
        [TestMethod]
        public void GrantDatabasePrivilege_DatabaseDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string randomDatabaseName = VariousFunctions.GenerateRandomString(8);
            while (databaseContainer.ExistDatabase(randomDatabaseName))
            {
                randomDatabaseName = VariousFunctions.GenerateRandomString(8);
            }
            GrantDatabasePrivilege grantDatabasePrivilege = CreateGrantDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            grantDatabasePrivilege.SetData(SystemeConstants.CreatePrivilegeName, SystemeConstants.DefaultProfile, randomDatabaseName);
            Assert.IsFalse(grantDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantDatabasePrivilege_PrivilegeDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string randomPrivilegeName = VariousFunctions.GenerateRandomString(8);
            Column column = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.DatabasesPrivilegesTableName).GetColumn(SystemeConstants.DatabasesPrivilegesPrivilegeNameColumnName);
            while (column.ExistCells(randomPrivilegeName))
            {
                randomPrivilegeName = VariousFunctions.GenerateRandomString(8);
            }
            GrantDatabasePrivilege grantDatabasePrivilege = CreateGrantDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            grantDatabasePrivilege.SetData(randomPrivilegeName, SystemeConstants.DefaultProfile, SystemeConstants.DefaultDatabaseName);
            Assert.IsFalse(grantDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantDatabasePrivilege_ProfileDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string randomProfileName = VariousFunctions.GenerateRandomString(8);
            Column column = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.ProfilesTableName).GetColumn(SystemeConstants.ProfileNameColumn);
            while (column.ExistCells(randomProfileName))
            {
                randomProfileName = VariousFunctions.GenerateRandomString(8);
            }
            GrantDatabasePrivilege grantDatabasePrivilege = CreateGrantDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            grantDatabasePrivilege.SetData(SystemeConstants.CreatePrivilegeName, randomProfileName, SystemeConstants.DefaultDatabaseName);
            Assert.IsFalse(grantDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrandDatabasePrivilege_PKViolated_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data = SystemeConstants.DefaultProfile;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data = SystemeConstants.DefaultDatabaseName;
            row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data = SystemeConstants.CreatePrivilegeName;
            table.AddRow(row);
            GrantDatabasePrivilege grantDatabasePrivilege = CreateGrantDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, table.tableName);
            grantDatabasePrivilege.SetData(row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName).data, row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName).data, row.GetCell(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName).data);
            Assert.IsFalse(grantDatabasePrivilege.ValidateParameters());
        }

        [TestMethod]
        public void GrantDatabasePrivilege_AllParametersAreGood_GrantDatabasePrivilege()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            GrantDatabasePrivilege grantDatabasePrivilege = CreateGrantDatabasePrivilege(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName);
            grantDatabasePrivilege.SetData(SystemeConstants.CreatePrivilegeName, SystemeConstants.DefaultProfile, SystemeConstants.DefaultDatabaseName);
            Assert.IsTrue(grantDatabasePrivilege.ValidateParameters());
            grantDatabasePrivilege.Execute();
            Select select = TestSelect.CreateSelect(databaseContainer, SystemeConstants.SystemDatabaseName, SystemeConstants.PrivilegesOfProfilesOnDatabasesTableName, true);
            select.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnDatabasesProfileColumnName, SystemeConstants.DefaultProfile, Operator.equal);
            select.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnDatabasesDatabaseNameColumnName, SystemeConstants.DefaultDatabaseName, Operator.equal);
            select.whereClause.AddCritery(SystemeConstants.PrivilegesOfProfilesOnDatabasesPrivilegeColumnName, SystemeConstants.CreatePrivilegeName, Operator.equal);
            select.ValidateParameters();
            select.Execute();
            Assert.AreEqual(1, select.GetAfectedRowCount());
        }

        public static GrantDatabasePrivilege CreateGrantDatabasePrivilege(IDatabaseContainer databaseContainer, string databaseName, string tableName)
        {
            GrantDatabasePrivilege grantDatabasePrivilege = new GrantDatabasePrivilege(databaseContainer);
            grantDatabasePrivilege.targetDatabase = databaseName;
            grantDatabasePrivilege.targetTableName = tableName;
            return grantDatabasePrivilege;
        }
    }
}
