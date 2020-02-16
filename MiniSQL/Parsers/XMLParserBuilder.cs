using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQL.Interfaces;

namespace MiniSQL.Parsers
{
    public class XMLParserBuilder : ParserBuilder
    {

        public XMLParserBuilder() {
            
        }

        public override void SetDataFormatManager(string dataFormatManagerVersion)
        {
            throw new NotImplementedException();
        }

        public override void SetUbicationManager(string ubicationManager)
        {
            throw new NotImplementedException();
        }
    }
}
