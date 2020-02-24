using MiniSQL.Classes;
using MiniSQL.ColumnBehaviours;
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
            XmlDocument tableStruct = SaveTableStruct(table);
            tableStruct.Save(this.GetUbicationManager().GetTableStructureFilePath(database.databaseName, table.tableName) + ".xml");
            return false;
        }

        private XmlDocument SaveTableStruct(Table table) 
        {
            XmlDocument tableStructureXML = new XmlDocument();
            //tableStructureXML.InsertBefore(tableStructureXML.CreateXmlDeclaration(this.xmlDeclaration[0], this.xmlDeclaration[1], this.xmlDeclaration[2]), rootElement);
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
            return tableStructureXML;
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
