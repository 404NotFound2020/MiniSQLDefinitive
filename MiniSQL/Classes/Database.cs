﻿using System;
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

		public bool ExistTable(string tableName)
		{
			return false;
		}

		public void AddTable(Table table)
		{

		}

		public Table GetTable(string tableName)
		{
			return null;
		}

	



	}
}