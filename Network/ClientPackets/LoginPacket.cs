using JangadaServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JangadaServer.Network.ClientPackets
{
    public class LoginPacket
    {
        public string Login;
        public string Password;

        public static LoginPacket Parse(NetworkMessage message)
        {
            LoginPacket packet = new LoginPacket();
            packet.Login = message.GetString();
            packet.Password = message.GetString();
            return packet;
        }
    }
}
