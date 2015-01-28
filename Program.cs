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
        static TcpServer _server;
        static void Main(string[] args)
        {
            TcpServer.Run(7777, (bytes, client) =>
            {
                if (Game.GetInstance().useProto)
                {
                    Messages messages = Messages.CreateBuilder().MergeFrom(bytes).Build();
                    foreach (Networkmessage message in messages.NetworkmessageList)
                    {
                        if (!Parser.Parse(message.Type, message, client))
                        {
                            //Disconnect
                        }
                    }
                }
                else
                {
                    Network.NetworkMessage inMessage = new Network.NetworkMessage(bytes);
                    int size = (int)BitConverter.ToUInt32(inMessage.Buffer, 0) + 4;
                    inMessage.Length = size;
                    inMessage.PrepareToRead();

                    while (inMessage.Position < inMessage.Length - 1)
                    {
                        byte type = inMessage.GetByte();
                        if (!Parser.Parse(type, inMessage, client))
                        {
                            //Disconnect
                        }
                    }
                }
            });
            while (true)
            {
            }
        }
    }
}
