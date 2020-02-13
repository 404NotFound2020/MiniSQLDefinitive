using System;
using System.Collections.Generic;

public class Table
{
	private string tableName;
	private Dictionary<string, Column> columns;
	private Dictionary<string, Row> rows; 

	public Table()
	{

	}

	public bool insert(string column, string d)
	{
		return false;
	}

	public bool delete(string column, string d)
	{
		return false;
	}
	public bool update(string column, string d)
	{
		return false;
	}
	public List<Cell> select(string column, string d)
	{
		List<Cell> result = new List<Cell>();

		return result;
	}

}
