using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JangadaServer.Enums;

namespace JangadaServer.Network.ServerPackets
{
    public class PlayerLogoutPacket
    {
        public string Guid;

        public void Add(NetworkMessage message)
        {
            message.AddByte((byte)ServerMessageType.PlayerLogout);
            message.AddString(this.Guid);
        }

        public PlayerLogoutPacket(string guid)
        {
            this.Guid = guid;
        }
    }
}
