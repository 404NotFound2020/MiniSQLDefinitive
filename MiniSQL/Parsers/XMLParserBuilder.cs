using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.Constants;
using MiniSQL.Interfaces;

namespace MiniSQL.Parsers
{
    public class XMLParserBuilder : ParserBuilder
    {

        public XMLParserBuilder()
        {      
            this.SetParser(new XMLParser());
        }

        public override void SetIndexationVersion(string indexationVersion)
        {
            XMLParser xmlParser = (XMLParser)this.GetParser();
            switch (indexationVersion) 
            {
                case ParserVersions.IndexationVersion:
                    xmlParser.savePrimaryKeyFunction = xmlParser.CreateTableLevelPrimaryKeyElement;
                    xmlParser.loadTableWithPrimaryKeysFunction = xmlParser.LoadTableLevelPrimaryKey;
                    break;
                case ParserVersions.NoIndexationVersion: //Its not a good idea use this version, because you will lost any key information of your tables
                    xmlParser.savePrimaryKeyFunction = (table, document) => { };
                    xmlParser.loadTableWithPrimaryKeysFunction = (table, document) => { };
                    break;
            }
        }


    }
}
