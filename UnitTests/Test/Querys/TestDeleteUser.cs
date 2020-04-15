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
    public class TestDeleteUser
    {
        [TestMethod]
        public void DeleteUser_TheUserDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName);
            Column column = table.GetColumn(SystemeConstants.UsersNameColumnName);
            string username = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(username))
            {
                username = VariousFunctions.GenerateRandomString(8);
            }
            DeleteUser deleteUser = CreateDeleteUser(databaseContainer);
            deleteUser.SetTargetUserName(username);
            Assert.IsFalse(deleteUser.ValidateParameters());
        }

        [TestMethod]
        public void DeleteUser_TheUserExist_DeleteUser()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName);
            Column column = table.GetColumn(SystemeConstants.UsersNameColumnName);
            string username = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(username))
            {
                username = VariousFunctions.GenerateRandomString(8);
            }
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = username;
            table.AddRow(row);
            DeleteUser deleteUser = CreateDeleteUser(databaseContainer);
            deleteUser.SetTargetUserName(username);
            Assert.IsTrue(deleteUser.ValidateParameters());
            deleteUser.Execute();
            Assert.IsFalse(column.ExistCells(username));
        }

        [TestMethod]
        public void DeleteUser_NotDeleteableUser_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            string username = SystemeConstants.AdminUser;
            DeleteUser deleteUser = CreateDeleteUser(databaseContainer);
            deleteUser.SetTargetUserName(username);
            Assert.IsFalse(deleteUser.ValidateParameters());
        }

        public static DeleteUser CreateDeleteUser(IDatabaseContainer container)
        {
            DeleteUser deleteUser = new DeleteUser(container);
            deleteUser.targetDatabase = SystemeConstants.SystemDatabaseName;
            deleteUser.targetTableName = SystemeConstants.UsersTableName;
            return deleteUser;
        }

    }
}

