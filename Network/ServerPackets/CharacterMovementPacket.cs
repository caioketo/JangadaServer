using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JangadaServer.Enums;
using JangadaServer.Creatures;

namespace JangadaServer.Network.ServerPackets
{
    public class CharacterMovementPacket
    {
        public PlayerDescription Player;

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.CharacterMovement);
            this.Player.Add(message);
        }

        public CharacterMovementPacket(Player player)
        {
            this.Player = new PlayerDescription(player);
        }
    }
}
