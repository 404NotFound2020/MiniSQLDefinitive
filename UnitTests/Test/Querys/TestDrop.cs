using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestDrop
    {
        [TestMethod]
        public void GoodArguments_ShouldFindResult()
        {
            bool parametros = false, tablaBorrada = false;
            Database db = ObjectConstructor.CreateDatabaseFull("db1");
            List<Column> columnas = new List<Column>();
            List<List<string>> celdas = new List<List<string>>();
            Table t = ObjectConstructor.CreateFullTable("table1",columnas,celdas);
            db.AddTable(t);
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Drop drop = createDrop(container);
            parametros = drop.ValidateParameters();
            if (parametros)
            {
                drop.Execute();
                if (db.ExistTable(t.tableName) == false)
                    tablaBorrada = true;
            }
            Assert.AreEqual(parametros,true);
            Assert.AreEqual(tablaBorrada, true);
        }

        public void GoodArguments_ShouldntFindResults()
        {
            
        }

        public void WrongArguments_TableDoesntExist_True()
        {
            
        }

        public Drop createDrop(IDatabaseContainer container)
        {
            return new Drop(container);
        }
    }
}
