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
        //XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        private string[] xmlDeclarationParams;
        public Func<XmlDocument, IPK> GetPkFunction;
        public Func<XmlDocument, IFK> GetFkFunction;

        public XMLParser() 
        {
            this.xmlDeclarationParams = new string[3];
            this.xmlDeclarationParams[0] = "1.0";
            this.xmlDeclarationParams[1] = "UTF-8";
            this.xmlDeclarationParams[2] = null;
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
            IEnumerator<KeyValuePair<string,Table>> tableEnumerator = database.ReadTables().GetEnumerator();
            if (!Directory.Exists(this.GetUbicationManager().GetDatabaseFilePath(database.databaseName))){
                Directory.CreateDirectory(this.GetUbicationManager().GetDatabaseFilePath(database.databaseName));
            }
            
            while (tableEnumerator.MoveNext()) 
            {
                this.SaveTable(database, tableEnumerator.Current.Value);
            }

            return false;
        }

        public override bool SaveTable(Database database, Table table)
        {
            IUbicationManager ubicationManager = this.GetUbicationManager();
            string tableStructureFilePath = ubicationManager.GetDatabaseFilePath(database.databaseName) + "\\" + ubicationManager.GetTableStructureFilePath(table.tableName);
            XmlDocument tableStructXml = CreateTableStructDocument(table);
            tableStructXml.Save(tableStructureFilePath + ".xml");

            return false;
        }

        private XmlDocument CreateTableStructDocument(Table table) 
        {
            XmlDocument tableStructDocument = new XmlDocument();
            XmlNode masterNode = tableStructDocument.CreateElement("table");
            //nodoMaster.AppendChild(tableStructDocument.CreateElement("tableName").AppendChild(tableStructDocument.CreateTextNode(table.tableName)));
            IEnumerator<Column> columnEnumerator = table.GetColumnList().GetEnumerator();
            XmlNode columnNode;
            while (columnEnumerator.MoveNext())
            {
                columnNode = tableStructDocument.CreateElement("column");
                columnNode.AppendChild(tableStructDocument.CreateElement("columnName").AppendChild(tableStructDocument.CreateTextNode(columnEnumerator.Current.columnName)));
                columnNode.AppendChild(tableStructDocument.CreateElement("columnDataType").AppendChild(tableStructDocument.CreateTextNode(columnEnumerator.Current.dataType.GetSimpleTextValue())));
                masterNode.AppendChild(columnNode);
            }

            return tableStructDocument;
        }



       


        /**
         * <table>
                <column>
	            <columnName></columnName>
	            <columnDataType></columnType>
	            <unique></unique>
	            <references>

	            </references>
            </column>


        </table>
    **/

    }
}
