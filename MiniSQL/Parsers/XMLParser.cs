using MiniSQL.Classes;
using MiniSQL.ColumnBehaviours;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MiniSQL.Parsers
{
    class XMLParser : AbstractParser
    {

        public Func<XmlDocument, IPK> GetPkFunction;
        public Func<XmlDocument, IFK> GetFkFunction;

        public XMLParser() 
        {
            //this.GetPkFunction = document => {return PKNoActivated.GetPKNoActivated();};
            //this.GetFkFunction = document => {return FKNoActivated.GetFKNoActivated();};
        }

        public override bool DeleteDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteTable(string databaseName, string tableName)
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
            throw new NotImplementedException();
        }

        public override bool SaveTable(Database database, Table table)
        {

            return false;
        }

        private void CreateColumnStructNodes(XmlDocument structureXML, XmlElement xmlElement, Column column) 
        {
            //Escap the strings.
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
