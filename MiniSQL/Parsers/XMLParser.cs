using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using System;
using System.Collections;
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
        private const string extension = ".xml";
        public Action<Table, XmlDocument> savePrimaryKeyFunction;
        public Action<Table, XmlDocument> loadTableWithPrimaryKeysFunction;

        public XMLParser() 
        {
            this.xmlDeclaration = new string[] { "1.0", null, null };
            this.savePrimaryKeyFunction = this.CreateTableLevelPrimaryKeyElement;
            this.loadTableWithPrimaryKeysFunction = this.LoadTableLevelPrimaryKey;
        }

        public override void DeleteDatabase(string databaseName)
        {
            if (this.ExistDatabase(databaseName))
            {
                Directory.Delete(this.GetUbicationManager().GetDatabaseFilePath(databaseName), true);
            }
            else
            {
                throw new Exception("U mongol");
            }
            
        }

        public override void DeleteTable(string databaseName, string tableName)
        {
            string propertiesPath = this.GetUbicationManager().GetDatabasePropertiesFilePath(databaseName) + extension;
            XmlDocument databaseProperties = new XmlDocument();
            databaseProperties.Load(propertiesPath);
            XmlNode tablePropertiesNode = databaseProperties.SelectSingleNode("//" + XMLTagsConstants.DatabasePropertiesTableElementTag_WR + "[" + XMLTagsConstants.DatabasePropertiesTableElementNameTag_WR + "='" + tableName + "']");
            if(tablePropertiesNode != null) 
            {
                IUbicationManager ubicationManager = this.GetUbicationManager();
                databaseProperties.DocumentElement.RemoveChild(tablePropertiesNode);
                File.Delete(ubicationManager.GetTableDataFilePath(databaseName, tableName) + extension);
                File.Delete(ubicationManager.GetTableStructureFilePath(databaseName, tableName) + extension);
                databaseProperties.Save(propertiesPath);
            }
            else
            {
                throw new Exception("U retarded exception"); //probably, the own system will throw the exception, however, we dont know what type it will be, thats why we decide what exception will be thrown                         
            }
        }

        public override bool ExistDatabase(string databaseName)
        {
            return Directory.Exists(this.GetUbicationManager().GetDatabaseFilePath(databaseName));
        }

        public override bool ExistTable(string databaseName, string tableName)
        {
            XmlDocument databaseProperties = new XmlDocument();
            databaseProperties.Load(this.GetUbicationManager().GetDatabasePropertiesFilePath(databaseName) + extension);
            return databaseProperties.SelectSingleNode("//" + XMLTagsConstants.DatabasePropertiesTableElementTag_WR + "[" + XMLTagsConstants.DatabasePropertiesTableElementNameTag_WR + "='" + tableName + "']") != null;
        }

        public override Database LoadDatabase(string databaseName)
        {
            Database database = new Database(databaseName);
            XmlDocument databaseProperties = new XmlDocument();
            databaseProperties.Load(this.GetUbicationManager().GetDatabasePropertiesFilePath(databaseName) + extension);
            XmlNodeList tableNodes = databaseProperties.GetElementsByTagName(XMLTagsConstants.DatabasePropertiesTableElementTag_WR);
            IEnumerator tableNodesEnumerator = tableNodes.GetEnumerator();
            while (tableNodesEnumerator.MoveNext())
            {
                database.AddTable(this.LoadTable(database.databaseName, ((XmlNode)tableNodesEnumerator.Current).SelectSingleNode(XMLTagsConstants.DatabasePropertiesTableElementNameTag_WR).InnerText));
            }
            return database;
        }

        public override Table LoadTable(string databaseName, string tableName)
        {
            Table table = this.LoadTableStructure(databaseName, tableName);
            this.LoadTableData(databaseName, table);
            return table;            
        }

        private Table LoadTableStructure(string databaseName, string tableName)
        {
            Table table = new Table(tableName);
            XmlDocument tableStructDocument = new XmlDocument();
            tableStructDocument.Load(this.GetUbicationManager().GetTableStructureFilePath(databaseName, tableName) + extension);
            IEnumerator enumerator = tableStructDocument.GetElementsByTagName(XMLTagsConstants.TableStructureColumnElementTag_WR).GetEnumerator();
            XmlNode xmlNode;
            while (enumerator.MoveNext())
            {
                xmlNode = (XmlNode)enumerator.Current;
                table.AddColumn(new Column(xmlNode.SelectSingleNode(XMLTagsConstants.TableStructureColumnNameTag_WR).InnerText, DataTypesFactory.GetDataTypesFactory().GetDataType(xmlNode.SelectSingleNode(XMLTagsConstants.TableStructureColumnDataTypeTag_WR).InnerText)));
            }
            this.loadTableWithPrimaryKeysFunction.Invoke(table, tableStructDocument);
            return table;
        }

        public void LoadTableLevelPrimaryKey(Table table, XmlDocument xmlDocument) {
            Console.WriteLine("bbbb");
            XmlNode primaryKeyNode = xmlDocument.GetElementsByTagName(XMLTagsConstants.PrimaryKeyElementTag_WR)[0];
            IEnumerator columnEnumerator = primaryKeyNode.SelectNodes(XMLTagsConstants.TableStructureColumnNameTag_WR).GetEnumerator();
            while (columnEnumerator.MoveNext()) 
            {
                Console.WriteLine("aaa");
                table.primaryKey.AddKey(table.GetColumn(((XmlNode)columnEnumerator.Current).InnerText));
            }        
        }

        private void LoadTableData(string databaseName, Table table)
        {
            XmlDocument tableDataDocument = new XmlDocument();
            tableDataDocument.Load(this.GetUbicationManager().GetTableDataFilePath(databaseName, table.tableName) + extension);
            IEnumerator rowEnumerator = tableDataDocument.GetElementsByTagName(XMLTagsConstants.TableDataRowElementTag_WR).GetEnumerator();
            IEnumerator cellEnumerator;
            XmlNode xmlCellNode;
            Row row;
            Cell cell;
            while (rowEnumerator.MoveNext())
            {
                row = table.CreateRowDefinition();
                cellEnumerator = ((XmlNode)rowEnumerator.Current).SelectNodes(XMLTagsConstants.TableDataCellElementTag_WR).GetEnumerator();
                while (cellEnumerator.MoveNext())
                {
                    xmlCellNode = (XmlNode)cellEnumerator.Current;
                    cell = row.GetCell(xmlCellNode.Attributes.GetNamedItem(XMLTagsConstants.TableDataCellColumnNameAtributeTag_WR).InnerText);
                    cell.data = cell.column.dataType.ParseLoadData(xmlCellNode.InnerText);
                }
                table.AddRow(row);
            }
        }

        public override void SaveDatabase(Database database)
        {
            XmlDocument databaseProperties = new XmlDocument();
            XmlElement databaseElement = databaseProperties.CreateElement(XMLTagsConstants.DatabasePropertiesRootElementTag_WR);
            IUbicationManager ubicationManager = this.GetUbicationManager();
            string databasePath = ubicationManager.GetDatabaseFilePath(database.databaseName);
            if (!Directory.Exists(databasePath)) Directory.CreateDirectory(databasePath);
            IEnumerator<Table> enumerator = database.GetTableEnumerator();
            while (enumerator.MoveNext()) 
            {
                this.SaveTable(database, enumerator.Current, databaseProperties, databaseElement);
            }
            databaseProperties.AppendChild(databaseElement);
            databaseProperties.InsertBefore(databaseProperties.CreateXmlDeclaration(this.xmlDeclaration[0], this.xmlDeclaration[1], this.xmlDeclaration[2]), databaseElement);
            databaseProperties.Save(ubicationManager.GetDatabasePropertiesFilePath(database.databaseName) + extension);
        }

        public override void SaveTable(Database database, Table table)
        {
            XmlDocument databaseProperties = new XmlDocument();
            IUbicationManager ubicationManager = this.GetUbicationManager();
            databaseProperties.Load(ubicationManager.GetDatabasePropertiesFilePath(database.databaseName) + extension);
            this.SaveTable(database, table, databaseProperties, databaseProperties.DocumentElement);
            databaseProperties.Save(ubicationManager.GetDatabasePropertiesFilePath(database.databaseName) + extension);
        }

        protected void SaveTable(Database database, Table table, XmlDocument databaseProperties, XmlElement databaseElement) 
        {
            XmlElement tableElement = databaseProperties.CreateElement(XMLTagsConstants.DatabasePropertiesTableElementTag_WR);
            tableElement.AppendChild(this.CreateSimpleNode(databaseProperties, XMLTagsConstants.DatabasePropertiesTableElementNameTag_WR, table.tableName));
            databaseElement.AppendChild(tableElement);
            this.SaveTableStruct(table).Save(this.GetUbicationManager().GetTableStructureFilePath(database.databaseName, table.tableName) + extension);
            this.SaveTableData(table).Save(this.GetUbicationManager().GetTableDataFilePath(database.databaseName, table.tableName) + extension);
        }

        private XmlDocument SaveTableStruct(Table table) 
        {
            XmlDocument tableStructureXML = new XmlDocument();            
            XmlElement tableElement = tableStructureXML.CreateElement(XMLTagsConstants.TableStructureRootElementTag_WR);
            IEnumerator<Column> enumerator = table.GetColumnEnumerator();
            XmlElement column;
            while (enumerator.MoveNext())
            {
                column = tableStructureXML.CreateElement(XMLTagsConstants.TableStructureColumnElementTag_WR);
                column.AppendChild(this.CreateSimpleNode(tableStructureXML, XMLTagsConstants.TableStructureColumnNameTag_WR, enumerator.Current.columnName));
                column.AppendChild(this.CreateSimpleNode(tableStructureXML, XMLTagsConstants.TableStructureColumnDataTypeTag_WR, enumerator.Current.dataType.GetSimpleTextValue()));
                tableElement.AppendChild(column);
            }
            tableStructureXML.AppendChild(tableElement);
            this.savePrimaryKeyFunction(table, tableStructureXML);
            tableStructureXML.InsertBefore(tableStructureXML.CreateXmlDeclaration(this.xmlDeclaration[0], this.xmlDeclaration[1], this.xmlDeclaration[2]), tableElement);
            return tableStructureXML;
        }

        public void CreateTableLevelPrimaryKeyElement(Table table, XmlDocument xmlDocument)
        {
            XmlElement primaryKeyElement = xmlDocument.CreateElement(XMLTagsConstants.PrimaryKeyElementTag_WR);
            IEnumerator<Column> primaryKeyEnumerator = table.primaryKey.GetKeyEnumerator();
            while (primaryKeyEnumerator.MoveNext())
            {
                primaryKeyElement.AppendChild(this.CreateSimpleNode(xmlDocument, XMLTagsConstants.TableStructureColumnNameTag_WR, primaryKeyEnumerator.Current.columnName));
            }
            xmlDocument.DocumentElement.AppendChild(primaryKeyElement);
        }

        private XmlDocument SaveTableData(Table table) 
        {
            XmlDocument tableData = new XmlDocument();
            XmlElement tableElement = tableData.CreateElement(XMLTagsConstants.TableDataRootElementTag_WR);
            XmlElement rowElement;
            XmlElement cellElement;
            IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            IEnumerator<Cell> cellEnumerator;
            while (rowEnumerator.MoveNext()) 
            {
                rowElement = tableData.CreateElement(XMLTagsConstants.TableDataRowElementTag_WR);
                cellEnumerator = rowEnumerator.Current.GetCellEnumerator();
                while (cellEnumerator.MoveNext()) 
                {
                    cellElement = this.CreateSimpleNode(tableData, XMLTagsConstants.TableDataCellElementTag_WR, cellEnumerator.Current.column.dataType.ParseToSaveData(cellEnumerator.Current.data));
                    cellElement.SetAttribute(XMLTagsConstants.TableStructureColumnNameTag_WR, cellEnumerator.Current.column.columnName);
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

        public override string[] GetDatabasesNames()
        {            
            IEnumerable<string> databasesPaths = Directory.EnumerateDirectories(this.GetUbicationManager().GetRootDirectoryPath());
            string[] names = new string[databasesPaths.Count()];
            IEnumerator<string> enumerator = databasesPaths.GetEnumerator();
            int i = 0;
            string[] split;            
            while (enumerator.MoveNext()) 
            {
                split = enumerator.Current.Split('\\');
                names[i] = split[split.Length - 1];
                i = i + 1;
            }
            return names;
        }
    }
}

//Delimitator version