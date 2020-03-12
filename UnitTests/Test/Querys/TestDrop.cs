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

        /**Ejemplo de Test
         * Se verifica que habiendo introducido bien los parametros, se elimina la tabla de la base de datos
         * 
         * 
         */
        [TestMethod]
        public void Drop_GoodArguments_TableDroped() 
        {
            IDatabaseContainer databaseContainer = ObjectConstructor.CreateDatabaseContainer();
            Database database = new Database("aaa");
            Table table = new Table("table1"); //Realmente no hace falta ni añadir columnas ni nada para esta query
            database.AddTable(table);
            databaseContainer.AddDatabase(database);
            Assert.IsTrue(databaseContainer.ExistDatabase(database.databaseName)); //Omitible, aunque nunca esta de mas por si acaso el problema esta en que en el container no se añaden las bases de datos
            Drop drop = CreateDrop(databaseContainer, database.databaseName, table.tableName); //Aqui se añaden los parametros a la drop
            Assert.IsTrue(drop.ValidateParameters());//Poned esta linea en todos los test. En caso de que se metan parametros validos ponerla como Assert.IsTrue, en caso de que no, ponerla como Assert.IsFalse
            drop.Execute();
            Assert.IsFalse(database.ExistTable(table.tableName));
        }

        [TestMethod] //para que pase en azure (me irrita que salga la aspa)
        public void GoodArguments_ShouldFindResult()
        {
            bool parametros, tablaBorrada = false;
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Database db = ObjectConstructor.CreateDatabaseFull("db1");
            List<Column> columnas = new List<Column>();
            List<List<string>> celdas = new List<List<string>>();
            Table t = ObjectConstructor.CreateFullTable("table1",columnas,celdas);
            db.AddTable(t);
            container.AddDatabase(db);
            Drop drop = CreateDrop(container, db.databaseName, t.tableName);
            parametros = drop.ValidateParameters();
            Assert.IsTrue(drop.ValidateParameters());
            if (parametros)
            {
                drop.Execute();
                if (db.ExistTable(t.tableName) == false)
                    tablaBorrada = true;
            }
            Assert.AreEqual(parametros,true);
            Assert.AreEqual(tablaBorrada, true);
        }

        [TestMethod]
        public void GoodArguments_ShouldntFindResults()
        {
            bool parametros, tablaBorrada = false;
            Database db = ObjectConstructor.CreateDatabaseFull("db1");
            List<Column> columnas = new List<Column>();
            List<List<string>> celdas = new List<List<string>>();
            Table t = ObjectConstructor.CreateFullTable("table1", columnas, celdas);
            db.AddTable(t);
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Drop drop = CreateDrop(container, db.databaseName, t.tableName);
            parametros = drop.ValidateParameters();
            if (parametros)
            {
                drop.Execute();
                if (db.ExistTable(t.tableName) == false)
                    tablaBorrada = false;
            }
            Assert.AreEqual(parametros, true);
            Assert.AreEqual(tablaBorrada, false);
        }

        [TestMethod] 
        public void WrongArguments_TableDoesntExist_True()
        {
            bool parametros, tablaBorrada = false;
            Database db = ObjectConstructor.CreateDatabaseFull("db1");
            List<Column> columnas = new List<Column>();
            List<List<string>> celdas = new List<List<string>>();
            Table t = ObjectConstructor.CreateFullTable("table1", columnas, celdas);
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Drop drop = CreateDrop(container, db.databaseName, t.tableName);
            parametros = drop.ValidateParameters();
            if (parametros)
            {
                drop.Execute();
                if (db.ExistTable(t.tableName) == false)
                    tablaBorrada = true;
            }
            Assert.AreEqual(parametros, false);
            Assert.AreEqual(tablaBorrada, false);
        }

        //Lo he puesto en mayusculas la primera letra (para cumplir con las reglas del proyecto, aunque tampoco pasaria nada si se nos cuela alguna minuscula, supongo)
        public Drop CreateDrop(IDatabaseContainer container, string databaseName, string tableName)
        {
            Drop drop = new Drop(container);
            drop.targetDatabase = databaseName;
            drop.targetTableName = tableName;
            return drop;
        }
    }
}
