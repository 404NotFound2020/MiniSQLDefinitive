using System;
using System.Collections.Generic;

namespace MiniSQL.Classes
{
	public class Database
	{

		public string databaseName;
		private Dictionary<string, Table> tables;
		public string user;
		public string password;

		public Database(string databaseName, string user, string password)
		{

		}

		public void AddTable(Table table)
		{

		}


		public Table GetTable()
		{
			return null;
		}

		public bool ExistTable(string tableName)
		{
			return false;
		}



	}
}
