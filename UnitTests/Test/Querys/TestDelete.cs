using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
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
            ITable t = db.GetTable("Table1");
            t.AddRow(t.CreateRowDefinition());
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Delete delete = CreateDelete(container,db.databaseName,t.tableName);
            delete.whereClause.AddCritery("Column3", t.GetColumn("Column3").dataType.GetDataTypeDefaultValue(), Operator.equal);
            Assert.IsTrue(delete.ValidateParameters());
            delete.ExecuteParticularQueryAction(t);
            Assert.IsTrue(delete.GetAfectedRowCount() > 0);

        }

        [TestMethod]
        public void DeleteRow_WrongArgumentsTableDoesntExist_NotifiedInResult()
        {
            Database db = new Database("db");
            ITable t = new Table("table");
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
            ITable t = db.GetTable("Table1");
            t.AddRow(t.CreateRowDefinition());
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            container.AddDatabase(db);
            Delete delete = CreateDelete(container, db.databaseName, t.tableName);
            delete.whereClause.AddCritery("x", "a", Operator.equal);
            Assert.IsFalse(delete.ValidateParameters());
            Assert.AreEqual(0, delete.GetAfectedRowCount());
        }

        [TestMethod]
        public void DeleteRow_TheRowIsReferedForAnotherRows_DontDeleteTheRowRefered() 
        {
            //Construction phase
            IDatabaseContainer container = ObjectConstructor.CreateDatabaseContainer();
            Database db = new Database("database1");
            ITable table1 = new Table("table1");
            Column column1t1 = new Column("c1t1", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table1.AddColumn(column1t1);
            table1.primaryKey.AddKey(column1t1);
            db.AddTable(table1);
            ITable table2 = new Table("table2");
            Column column1t2 = new Column("c1t2", DataTypesFactory.GetDataTypesFactory().GetDataType(TypesKeyConstants.IntTypeKey));
            table2.AddColumn(column1t2);
            table2.primaryKey.AddKey(column1t2);
            db.AddTable(table2);
            container.AddDatabase(db);            
            table2.foreignKey.AddForeignKey(column1t2, column1t1);
            //Insert some data
            Row rowT1;
            int limit = 10;
            for(int i = 0; i <= limit; i++) 
            {
                rowT1 = table1.CreateRowDefinition();
                rowT1.GetCell(column1t1.columnName).data = "" + i;
                table1.AddRow(rowT1);
            }
            Row rowT2 = table2.CreateRowDefinition();
            rowT2.GetCell(column1t2.columnName).data = "" + limit;
            table2.AddRow(rowT2);
            //Test
            int numberOfRows = table1.GetRowCount();
            Delete delete = CreateDelete(container, db.databaseName, table1.tableName);
            delete.whereClause.AddCritery(column1t1.columnName, "" + 11, Operator.less);
            Assert.IsTrue(delete.ValidateParameters());
            delete.Execute();
            Assert.AreEqual(table2.GetRowCount(), numberOfRows - delete.GetAfectedRowCount());
            Assert.IsTrue(table1.GetColumn(column1t1.columnName).ExistCells(rowT2.GetCell(column1t2.columnName).data));
        }

        public static Delete CreateDelete(IDatabaseContainer container, string databaseName, string tableName)
        {
            Delete delete = new Delete(container);
            delete.targetDatabase = databaseName;
            delete.targetTableName = tableName;
            return delete;
        }

    }
}
