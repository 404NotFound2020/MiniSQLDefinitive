using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Initializer
{
    public class DatabaseProxy : IDatabase
    {
        public IDatabase database;
        private ISysteme system;

        public DatabaseProxy(IDatabase database, ISysteme system)
        {
            this.database = database;
            this.system = system;
        }

        public override string databaseName { get => this.database.databaseName; set => this.database.databaseName = value; }

        public override void AddTable(ITable table)
        {
            this.database.AddTable(table);
            this.system.SaveTable(database, table);
        }

        public override void DropTable(string tableName)
        {
            ITable table = this.database.GetTable(tableName);
            this.system.RemoveTable(this.database, table);
            this.database.DropTable(tableName);
        }

        public override bool ExistTable(string tableName)
        {
            return this.database.ExistTable(tableName);
        }

        public override ITable GetTable(string tableName)
        {
            return new TableProxy(this, this.database.GetTable(tableName), this.system);
        }

        public override IEnumerator<ITable> GetTableEnumerator()
        {
            return this.database.GetTableEnumerator();
        }

        protected override Dictionary<string, ITable> GetTables()
        {
            Dictionary<string, ITable> dictionary = new Dictionary<string, ITable>();
            IEnumerator<ITable> enumerator = this.database.GetTableEnumerator();
            while (enumerator.MoveNext())
            {
                dictionary.Add(enumerator.Current.tableName, enumerator.Current);
            }
            return dictionary;
        }
    }
}
