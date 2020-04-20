using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
   // [TestClass]
    public class TestCreateUser
    {
        //[TestMethod]
        public void CreateUser_UserExist_NoticeInValidate()
        {
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            CreateUser createUser = CreateCreateUser(container, SystemeConstants.SystemDatabaseName, SystemeConstants.UsersTableName);
            createUser.SetUser(SystemeConstants.AdminUser, "asadsa");
            Assert.IsFalse(createUser.ValidateParameters());
            Console.WriteLine(createUser.GetResult());
        }

       // [TestMethod]
        public void CreateUser_UserDoesntExist_CreateUser()
        {
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Column column = container.GetDatabase(SystemeConstants.SystemDatabaseName).GetTable(SystemeConstants.UsersTableName).GetColumn(SystemeConstants.UsersNameColumnName);
            string userName = VariousFunctions.GenerateRandomString(8);
            while (column.ExistCells(userName)) userName = VariousFunctions.GenerateRandomString(8);
            CreateUser createUser = CreateCreateUser(container, SystemeConstants.SystemDatabaseName, SystemeConstants.UsersTableName);
        }

        public static CreateUser CreateCreateUser(IDatabaseContainer container, string databaseName, string tableName)
        {
            CreateUser createUser = new CreateUser(container);
            createUser.targetDatabase = databaseName;
            createUser.targetTableName = tableName;
            return createUser;
        }


    }
}