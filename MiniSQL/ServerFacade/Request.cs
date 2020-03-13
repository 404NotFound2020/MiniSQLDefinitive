using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MiniSQL.ServerFacade
{
    public class Request
    {
        private XmlDocument xmlRequest;

        public Request(string flatXml) 
        {
            this.xmlRequest = new XmlDocument();
            this.xmlRequest.LoadXml(flatXml);        
        }


    }
}
