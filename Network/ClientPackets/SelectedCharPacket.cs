using JangadaServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ClientPackets
{
    public class SelectedCharPacket
    {
        public int Id;

        public static SelectedCharPacket Parse(NetworkMessage message)
        {
            SelectedCharPacket packet = new SelectedCharPacket();
            packet.Id = message.GetUInt16();
            return packet;
        }
    }
}
