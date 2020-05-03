using NetworkUtilities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtilities.Transactions
{
    public class SendAndReceive
    {

        public static string ReceiveMessage(NetworkStream stream, int bufferSize)
        {
            Byte[] data = new Byte[bufferSize];
            Int32 bytes;
            string responseData = "";
            bytes = stream.Read(data, 0, data.Length);
            responseData = responseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            int packageSize = XmlMessage.GetPackageSize(responseData);
            int actualBufferPosition = bufferSize;
            while (actualBufferPosition < packageSize)
            {
                bytes = stream.Read(data, 0, data.Length);
                responseData = responseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                actualBufferPosition = actualBufferPosition + bufferSize;
            }
            return responseData;
        }

        public static void SendMessage(NetworkStream stream, string message)
        {          
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(XmlMessage.Protocole("message", message));
            stream.Write(msg, 0, msg.Length);
        }


    }
}
