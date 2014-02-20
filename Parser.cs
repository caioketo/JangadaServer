using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class Parser
    {
        public static bool Parse(NetworkMessage.Types.Type type, NetworkMessage message)
        {
            switch (type)
            {
                case NetworkMessage.Types.Type.LOGIN:
                    break;
                case NetworkMessage.Types.Type.LOGOUT:
                    break;
                case NetworkMessage.Types.Type.CHAT:
                    Console.WriteLine(message.ChatPacket.Message);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
