using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    class Program
    {
        static TcpServer _server;
        static void Main(string[] args)
        {            
            TcpServer.Run(7777, (bytes, client) =>
            {
                Console.Write(bytes);
                Messages messages = Messages.CreateBuilder().MergeFrom(bytes).Build();
                foreach (NetworkMessage message in messages.NetworkmessageList)
                {
                    if (!Parser.Parse(message.Type, message))
                    {
                        //Disconnect
                    }
                }
            });
            while (true)
            {
            }
        }
    }
}
