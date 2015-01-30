using Jangada;
using JangadaServer.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer.Creatures
{
    public class Player : Creature
    {
        public ClientConnection connection;
        
        public Player(Vector3 position, ClientConnection connection) : base(position)
        {
            this.connection = connection;
            this.position = position;
            this.Guid = Util.generateID();
            this.Stats = new Stats();
        }

    }
}
