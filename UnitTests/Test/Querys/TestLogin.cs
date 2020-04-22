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
    public class TestLogin
    {
        [TestMethod]
        public void Login_UserDoenstExist_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IUserThread userThread = ObjectConstructor.GetFakeUserThread();
            string doenstExistUsername = ShittyUtilities.GetDoesntExistUserName(databaseContainer);
            Login login = CreateLogin(databaseContainer, userThread, SystemeConstants.SystemDatabaseName, SystemeConstants.UsersTableName);
            login.SetData(doenstExistUsername, "aaaa");
            Assert.IsFalse(login.ValidateParameters());
        }

        [TestMethod]
        public void Login_UserExistButPasswordDontMatch_NoticeInValidate()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IUserThread userThread = ObjectConstructor.GetFakeUserThread();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName);
            string newUsername = ShittyUtilities.GetDoesntExistUserName(databaseContainer);
            string password = "aaa";
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = newUsername;
            row.GetCell(SystemeConstants.UsersPasswordColumnName).data = password;
            table.AddRow(row);
            Login login = CreateLogin(databaseContainer, userThread, SystemeConstants.SystemDatabaseName, SystemeConstants.UsersTableName);
            login.SetData(newUsername, password + "a");
            Assert.IsFalse(login.ValidateParameters());
        }

        [TestMethod]
        public void LoginUserExist_Login()
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            IUserThread userThread = ObjectConstructor.GetFakeUserThread();
            ITable table = databaseContainer.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName);
            string newUsername = ShittyUtilities.GetDoesntExistUserName(databaseContainer);
            string password = "aaa";
            Row row = table.CreateRowDefinition();
            row.GetCell(SystemeConstants.UsersNameColumnName).data = newUsername;
            row.GetCell(SystemeConstants.UsersPasswordColumnName).data = password;
            table.AddRow(row);
            Login login = CreateLogin(databaseContainer, userThread, SystemeConstants.SystemDatabaseName, SystemeConstants.UsersTableName);
            login.SetData(newUsername, password);
            Assert.IsTrue(login.ValidateParameters());
        }

        public static Login CreateLogin(IDatabaseContainer container, IUserThread thread, string databaseName, string tableName)
        {
            Login login = new Login(container, thread);
            login.targetDatabase = databaseName;
            login.targetTableName = tableName;
            return login;
        }


    }
}