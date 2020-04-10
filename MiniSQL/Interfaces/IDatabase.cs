using MiniSQL.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Interfaces
{
    public abstract class IDatabase
    {
        public abstract string databaseName { get; set; }
        public abstract bool ExistTable(string tableName);
        public abstract void DropTable(string tableName);
        public abstract void AddTable(ITable table);
        public abstract ITable GetTable(string tableName);
        public abstract IEnumerator<ITable> GetTableEnumerator();
        protected abstract Dictionary<string, ITable> GetTables();

		public static IEqualityComparer<IDatabase> GetDatabaseComparer()
		{
			return new DatabaseComparer();
		}

		private class DatabaseComparer : IEqualityComparer<IDatabase>
		{
			public bool Equals(IDatabase x, IDatabase y)
			{
				if (!x.databaseName.Equals(y.databaseName))
					return false;
				return new DictionaryComparer<string, ITable>(ITable.GetTableComparer()).Equals(x.GetTables(), y.GetTables());
			}

			public int GetHashCode(IDatabase obj)
			{
				throw new NotImplementedException();
			}
		}

	}
}
