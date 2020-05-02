using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtilities.Interfaces
{
    public interface IMessage
    {
        string[] GetElementsContentByTagName(string tagName);
    }
}
