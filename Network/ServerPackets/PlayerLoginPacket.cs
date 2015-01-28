using JangadaServer.Creatures;
using JangadaServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ServerPackets
{
    public class PlayerLoginPacket
    {
        public PlayerDescription Player;

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.PlayerLogin);
            this.Player.Add(message);
        }

        public PlayerLoginPacket(Player player)
        {
            this.Player = new PlayerDescription(player);
        }
    }
}
