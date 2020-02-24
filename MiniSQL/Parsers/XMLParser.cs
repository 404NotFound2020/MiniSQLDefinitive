using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MiniSQL.Parsers
{
    class XMLParser : AbstractParser
    {
        private string[] xmlDeclaration;

        public XMLParser() 
        {
            this.xmlDeclaration = new string[] { "1.0", "ISO-8859-1", null };
        }

        public override bool DeleteDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteTable(string databaseName, string tableName)
        {
            throw new NotImplementedException();
        }

        public override bool ExistDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override bool ExistTable(string databaseName, string tableName)
        {
            throw new NotImplementedException();
        }

        public override Database LoadDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override Table LoadTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public override bool SaveDatabase(Database database)
        {
            IUbicationManager ubicationManager = this.GetUbicationManager();
            string databasePath = ubicationManager.GetDatabaseFilePath(database.databaseName);
            if (!Directory.Exists(databasePath)) Directory.CreateDirectory(databasePath);
            IEnumerator<KeyValuePair<string, Table>> enumerator = database.ReadTables().GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                this.SaveTable(database, enumerator.Current.Value);
            }           
            return false;
        }

        public override bool SaveTable(Database database, Table table)
        {
            this.SaveTableStruct(table).Save(this.GetUbicationManager().GetTableStructureFilePath(database.databaseName, table.tableName) + ".xml");
            this.SaveTableData(table).Save(this.GetUbicationManager().GetTableDataFilePath(database.databaseName, table.tableName) + ".xml");
            return false;
        }

        private XmlDocument SaveTableStruct(Table table) 
        {
            XmlDocument tableStructureXML = new XmlDocument();            
            XmlElement tableElement = tableStructureXML.CreateElement("table");
            IEnumerator<Column> enumerator = table.GetColumnList().GetEnumerator();
            XmlElement column;
            while (enumerator.MoveNext())
            {
                column = tableStructureXML.CreateElement("column");
                column.AppendChild(this.CreateSimpleNode(tableStructureXML, "columnName", enumerator.Current.columnName));
                column.AppendChild(this.CreateSimpleNode(tableStructureXML, "columnDataType", enumerator.Current.dataType.GetSimpleTextValue()));
                tableElement.AppendChild(column);
            }
            tableStructureXML.AppendChild(tableElement);
            tableStructureXML.InsertBefore(tableStructureXML.CreateXmlDeclaration(this.xmlDeclaration[0], this.xmlDeclaration[1], this.xmlDeclaration[2]), tableElement);
            return tableStructureXML;
        }

        private XmlDocument SaveTableData(Table table) 
        {
            XmlDocument tableData = new XmlDocument();
            XmlElement tableElement = tableData.CreateElement("data");
            XmlElement rowElement;
            XmlElement cellElement;
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            IEnumerator<Cell> cellEnumerator;
            while (rowEnumerator.MoveNext()) 
            {
                rowElement = tableData.CreateElement("row");
                cellEnumerator = rowEnumerator.Current.GetCellEnumerator();
                while (cellEnumerator.MoveNext()) 
                {
                    cellElement = this.CreateSimpleNode(tableData, "cell", cellEnumerator.Current.column.dataType.ParseToSaveData(cellEnumerator.Current.data));
                    cellElement.SetAttribute("columnName", cellEnumerator.Current.column.columnName);
                    rowElement.AppendChild(cellElement);
                }
                tableElement.AppendChild(rowElement);
            }
            tableData.AppendChild(tableElement);
            tableData.InsertBefore(tableData.CreateXmlDeclaration(this.xmlDeclaration[0], this.xmlDeclaration[1], this.xmlDeclaration[2]), tableElement);
            return tableData;
        }

        private XmlElement CreateSimpleNode(XmlDocument xmlDocument, string nodeName, string nodeText) 
        {
            XmlElement element = xmlDocument.CreateElement(nodeName);
            XmlText textNode = xmlDocument.CreateTextNode(nodeText);
            element.AppendChild(textNode);
            return element;
        }

    }
}
