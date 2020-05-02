using NetworkUtilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NetworkUtilities.Requests
{
    public class XmlMessage : IMessage
    {
        private XmlDocument xmlRequest;

        public XmlMessage(string flatXml)
        {
            this.xmlRequest = new XmlDocument();
            this.xmlRequest.LoadXml(flatXml);
        }

        public string[] GetElementsContentByTagName(string tagName)
        {
            XmlNodeList nodeList = this.xmlRequest.GetElementsByTagName(tagName);
            string[] text = new string[nodeList.Count];
            for (int i = 0; i < nodeList.Count; i++)
            {
                text[i] = nodeList[i].InnerText;
            }
            return text;
        }
    }
}
