using JangadaServer.Creatures;
using JangadaServer.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ServerPackets
{
    public class AreaDescriptionPacket
    {
        public int Id;
        public PlayerDescription Player;
        public List<PlayerDescription> Players = new List<PlayerDescription>();

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.AreaDescription);
            message.AddUInt16((ushort)this.Id);
            this.Player.Add(message);
            message.AddUInt16((ushort)this.Players.Count);
            foreach (PlayerDescription pd in this.Players)
            {
                pd.Add(message);
            }
        }

        public AreaDescriptionPacket(int id, Player player, List<Player> players)
        {
            this.Id = id;
            this.Player = new PlayerDescription(player);
            foreach (Player nPlayer in players)
            {
                this.Players.Add(new PlayerDescription(nPlayer));
            }
        }
    }
}
