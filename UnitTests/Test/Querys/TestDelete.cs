using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Interfaces;
using MiniSQL.Querys;
using UnitTests.Test.TestObjectsContructor;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestDelete
    {

        //Sorry but i will put this in spanish 
        /**Usad una query del tipo select para seleccionar las rows que se quieren eliminar, una vez seleccionadas ejecutar el delete con la misma where que el select
         * tras eso, comparar que las rows de la select y las de la delete (porque ambas clases almacenan una lista de row que fueron seleccionadas o borradas) son las mismas
         * (usad el comparador), y finalmente, volver a aplicar otra select con la misma where y verificar que las rows afectadas fueron 0
        **/
        [TestMethod]
        public void DeleteRow_GodArguments_RowsDeleted()
        {
            Database db = ObjectConstructor.CreateDatabaseFull("db");
            Table t = db.GetTable("Table1");
            t.AddRow(t.CreateRowDefinition());
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Delete delete = CreateDelete(container,db.databaseName,t.tableName);
            delete.whereClause.AddCritery(new Tuple<string, string>("Column3", t.GetColumn("Column3").dataType.GetDataTypeDefaultValue()), Operator.equal);
            Assert.IsTrue(delete.ValidateParameters());
            delete.ExecuteParticularQueryAction(t);
            Assert.IsTrue(delete.GetAfectedRowCount() > 0);

        }

        [TestMethod]
        public void DeleteRow_WrongArgumentsTableDoesntExist_NotifiedInResult()
        {
            Database db = new Database("db");
            Table t = new Table("table");
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Delete delete = CreateDelete(container, db.databaseName, t.tableName);
            Assert.IsFalse(delete.ValidateParameters());
            Assert.AreEqual(0, delete.GetAfectedRowCount());
        }

        [TestMethod]
        public void DeleteRow_BadArgumentsInWhere_NotifiedInResult()
        {
            Database db = ObjectConstructor.CreateDatabaseFull("db");
            Table t = db.GetTable("Table1");
            t.AddRow(t.CreateRowDefinition());
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Delete delete = CreateDelete(container, db.databaseName, t.tableName);
            delete.whereClause.AddCritery(new Tuple<string, string>("x", "a"), Operator.equal);
            Assert.IsFalse(delete.ValidateParameters());
            Assert.AreEqual(0, delete.GetAfectedRowCount());
        }

        public Delete CreateDelete(IDatabaseContainer container, string databaseName, string tableName)
        {
            Delete delete = new Delete(container);
            delete.targetDatabase = databaseName;
            delete.targetTableName = tableName;
            return delete;
        }

    }
}
