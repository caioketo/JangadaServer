using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JangadaServer.Enums;

namespace JangadaServer.Network.ServerPackets
{
    public class PlayerMovementPacket
    {
        public Vector3 newPosition;
        public Quaternion newRotation;

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.PlayerMovement);
            message.AddPosition(this.newPosition);
            message.AddQuaternion(this.newRotation);
        }

        public PlayerMovementPacket(Vector3 position, Quaternion rotation)
        {
            this.newPosition = position;
            this.newRotation = rotation;
        }
    }
}
