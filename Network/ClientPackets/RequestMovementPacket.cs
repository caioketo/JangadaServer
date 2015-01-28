using JangadaServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ClientPackets
{
    public class RequestMovementPacket
    {
        public MovementType Type;
        public float Amount;

        public static RequestMovementPacket Parse(NetworkMessage message)
        {
            RequestMovementPacket packet = new RequestMovementPacket();
            packet.Type = (MovementType)message.GetUInt16();
            packet.Amount = message.GetDouble();
            return packet;
        }
    }
}
