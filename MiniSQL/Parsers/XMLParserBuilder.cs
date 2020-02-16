using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.ColumnBehaviours;
using MiniSQL.Constants;
using MiniSQL.Interfaces;

namespace MiniSQL.Parsers
{
    public class XMLParserBuilder : ParserBuilder
    {

        public XMLParserBuilder() {
            this.SetParser(new XMLParser());
        }

        public override void SetDataFormatManager(string dataFormatManagerVersion)
        {
            
        }

        public override void SetUbicationManager(string ubicationManager)
        {
            
        }

        public override void SetPKActivationState(string state) 
        {
            if(state == ParserVersions.PKsIsActivated)
            {

            }
        }
        public override void SetFKActivationState(string state) 
        {
            if (state == ParserVersions.FKsIsActivated)
            {

            }

        }
}
