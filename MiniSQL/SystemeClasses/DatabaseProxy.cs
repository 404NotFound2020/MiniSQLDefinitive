using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.SystemeClasses
{
    public class DatabaseProxy : IDatabase, IProxy
    {
        public IDatabase database;
        private List<IActiveSystemModule> afectedModules;

        public DatabaseProxy(IDatabase database, List<IActiveSystemModule> afectedModules)
        {
            this.database = database;
            this.afectedModules = afectedModules;
        }

        public override string databaseName { get => this.database.databaseName; set => this.database.databaseName = value; }

        public override void AddTable(ITable table)
        {
            this.database.AddTable(table);
            IEnumerator<IActiveSystemModule> moduleEnumerator = this.afectedModules.GetEnumerator();
            while (moduleEnumerator.MoveNext()) moduleEnumerator.Current.ActToAdd(this, table);
        }

        public override void DropTable(string tableName)
        {
            ITable table = this.database.GetTable(tableName);
            IEnumerator<IActiveSystemModule> moduleEnumerator = this.afectedModules.GetEnumerator();
            while (moduleEnumerator.MoveNext()) moduleEnumerator.Current.ActToRemove(this, table);
            this.database.DropTable(tableName);
        }

        public override bool ExistTable(string tableName)
        {
            return this.database.ExistTable(tableName);
        }

        public override ITable GetTable(string tableName)
        {
            return new TableProxy(this, this.database.GetTable(tableName), this.afectedModules);
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
