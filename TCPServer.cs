using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class TcpServer
    {
        private const int SleepTimeout = 50;// miliseconds
        private static List<ClientConnection> Clients = new List<ClientConnection>();

        public static Task Run(int port, Action<byte[], ClientConnection> onReceived)
        {
            return Accept(port, (client) =>
                {
                    var receivedLines = new List<string>();

                    while(client.tcp.Connected)
                    {
                        var available = client.tcp.Available;

                        if (available == 0)
                        {
                            Thread.Sleep(SleepTimeout);
                            continue;
                        }

                        var buffer = new byte[available];
                        client.tcp.GetStream().Read(buffer, 0, available);
                        onReceived(buffer, client);
                    }

                    client.tcp.Close();
                });
        }

        private static Task Accept(int port, Action<ClientConnection> onClientAccepted)
        {
            var childTasks = new List<Task>();
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            listener.Start();

            return Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (!listener.Pending())
                        {
                            Thread.Sleep(SleepTimeout);
                            continue;
                        }

                        var client = new ClientConnection()
                        {
                            tcp = listener.AcceptTcpClient()
                        };
                        Clients.Add(client);
                        var childTask = new Task(() => onClientAccepted(client));
                        childTasks.Add(childTask);
                        childTask.ContinueWith(t => childTasks.Remove(t));
                        childTask.Start();
                    }
                }).ContinueWith(t =>
                    {
                        Task.WaitAll(childTasks.ToArray());
                        listener.Stop();
                    });
        }
    }
}
