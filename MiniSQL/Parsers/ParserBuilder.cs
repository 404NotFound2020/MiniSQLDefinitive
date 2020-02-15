using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Parsers
{
    public abstract class ParserBuilder
    {
        private AbstractParser parser;
        
        public abstract void SetDataFormatManager(string dataFormatManagerVersion);
        public abstract void SetUbicationManager(string ubicationManager);
        
        protected void SetParser(AbstractParser parser) 
        { 
        
        }

        public AbstractParser GetParser() 
        {
            return null;
        }


    }
}
