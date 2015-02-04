using Jangada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.GetInstance();
            TcpServer.Run(7777, (bytes, client) =>
            {
                Messages messages = Messages.CreateBuilder().MergeFrom(bytes).Build();
                foreach (Networkmessage message in messages.NetworkmessageList)
                {
                    if (!Parser.Parse(message.Type, message, client))
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
