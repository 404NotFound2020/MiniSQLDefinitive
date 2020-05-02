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
        private static string sizeTag = "totalSize";
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

        public static string Protocole(string rootTag, string message)
        {
            string finalMessage = "";
            int size = message.Length + GetSizeTag(rootTag) + GetSizeTextTag(sizeTag);
            size = size + (size + size.ToString().Length).ToString().Length;
            finalMessage = "<" + rootTag + ">" + CreateTextNode(sizeTag, size.ToString()) + message + "</" + rootTag + ">";
            return finalMessage;
        }

        public static string CreateTextNode(string tagName, string text)
        {
            return "<" + tagName + "><![CDATA[" + text + "]]></" + tagName + ">";
        }
        
        public static int GetSizeTag(string tag) {
            return (tag.Length + 2) * 2 + 1; 
        }

        public static int GetSizeTextTag(string tag)
        {
            return GetSizeTag(tag) + 12;
        }

        public static int GetPackageSize(string message)
        {
            string[] chains = message.Split(new string[] { sizeTag }, StringSplitOptions.RemoveEmptyEntries);
            return int.Parse(chains[1].Substring(10, chains[1].Length-15));
        }

    }
}
