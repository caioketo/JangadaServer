using JangadaServer.Creatures;
using JangadaServer.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ServerPackets
{
    public class PlayerDescription
    {
        public string Guid;
        public Vector3 Position;
        public Quaternion Rotation;

        public void Add(NetworkMessage message)
        {
            message.AddString(this.Guid);
            message.AddPosition(this.Position);
            message.AddQuaternion(this.Rotation);
        }

        public PlayerDescription(string guid, Vector3 position, Quaternion rotation)
        {
            this.Guid = guid;
            this.Position = position;
            this.Rotation = rotation;
        }

        public PlayerDescription(Player player)
        {
            this.Guid = player.Guid;
            this.Position = player.position;
            this.Rotation = player.rotation;
        }
    }
}
