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



    }
}
