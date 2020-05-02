using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer.Drugs
{
    public class UserThread : IUserThread
    {
        public string username { get; set; }
        private TcpClient client;
        private Thread thread;
        private bool continueThread;

        public UserThread(TcpClient client) {
            this.client = client;
            this.username = SystemeConstants.AnonimousUser;
        }

        public void Run()
        {
            thread = new Thread(StayListen);
            continueThread = true;
            thread.Start();
        }

        private void StayListen()
        {
            Byte[] data = new Byte[256];
            Int32 bytes;
            string responseData;
            NetworkStream stream = client.GetStream();
            while ((continueThread))
            {
                responseData = "";
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    responseData = responseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                }
                while (stream.DataAvailable);
                this.SendMessage(TrueServer.ReceiveRequest(this, responseData));
            }
            client.Close();
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = client.GetStream();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes("<message>" + message + "</message>");
            stream.Write(msg, 0, msg.Length);
        }

        public void Close()
        {
            continueThread = false;
        }
    }
}
