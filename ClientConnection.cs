using JangadaServer.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class ClientConnection
    {
        public TcpClient tcp;
        public Player player;
        public int userId;
        
        public ClientConnection()
        {
            player = new Player(new Microsoft.Xna.Framework.Vector3(8, 6.5f, -3), this);
        }
    }
}
