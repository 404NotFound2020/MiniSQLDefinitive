using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.ColumnBehaviours;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using MiniSQL.UbicationManagers;

namespace MiniSQL.Parsers
{
    public class XMLParserBuilder : ParserBuilder
    {

        public XMLParserBuilder()
        {
            XMLParser parser = new XMLParser();
            parser.GetPkFunction = document => {return PKNoActivated.GetPKNoActivated();};
            parser.GetFkFunction = document => {return FKNoActivated.GetFKNoActivated();};            
            this.SetParser(parser);
        }

        public override void SetDataFormatManager(string dataFormatManagerVersion)
        {
            
        }

        public override void SetIndexationVersion(string indexationVersion)
        {
            
        }

        public override void SetUbicationManager(string ubicationManager)
        {
            switch (ubicationManager) 
            {
                case UbicationVersions.FirstUbicationVersion:
                    this.GetParser().SetUbicationManager(FirstUbicationManager.GetFirstUbicationManager());
                    break;
            
            }

            
        }


    }
}
