using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Parsers
{
    public class ParserBuilderFactory
    {
        private static ParserBuilderFactory parserBuilderFactory;

        private ParserBuilderFactory() 
        { 
        
        }

        public static ParserBuilderFactory GetParserBuilderFactory() 
        { 
            if(parserBuilderFactory == null) 
            {
                parserBuilderFactory = new ParserBuilderFactory();
            }
            return parserBuilderFactory;
        }

        public ParserBuilder GetParserBuilder(string parserVersion) 
        {
            ParserBuilder parserBuilder = null;
            switch (parserVersion) 
            {
                case ParserVersions.XMLParserVersion:
                    parserBuilder = new XMLParserBuilder();
                    break;           
            }
            return parserBuilder;
        
        }


    }
}
