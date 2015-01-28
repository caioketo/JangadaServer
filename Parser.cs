using Jangada;
using JangadaServer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class Parser
    {
        public static bool Parse(byte type, Network.NetworkMessage message, ClientConnection connection)
        {
            switch ((ClientMessageType)type)
            {
                case ClientMessageType.Login:
                    Network.ClientPackets.LoginPacket loginPacket = Network.ClientPackets.LoginPacket.Parse(message);
                    Console.WriteLine("Login: " + loginPacket.Login);
                    Console.WriteLine("Password: " + loginPacket.Password);
                    List<Character> chars = new List<Character>();
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto")
                        .SetInfo("Level: 1")
                        .Build());
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto2")
                        .SetInfo("Level: 100")
                        .Build());
                    MessagesHelper.SendCharacterList(connection, chars);
                    break;
                case ClientMessageType.SelectedChar:
                    Network.ClientPackets.SelectedCharPacket selectedCharPacket = Network.ClientPackets.SelectedCharPacket.Parse(message);
                    Game.GetInstance().OnPlayerLogin(connection);
                    Console.WriteLine("Selected char id = " + selectedCharPacket.Id.ToString());
                    break;
                case ClientMessageType.RequestMovement:
                    Network.ClientPackets.RequestMovementPacket requestMovementPacket = Network.ClientPackets.RequestMovementPacket.Parse(message);
                    Game.GetInstance().OnPlayerMove(requestMovementPacket.Type, requestMovementPacket.Amount, connection);
                    break;
                default:
                    break;
            }
            return true;
        }
        public static bool Parse(Networkmessage.Types.Type type, Networkmessage message, ClientConnection connection)
        {
            switch (type)
            {
                case Networkmessage.Types.Type.LOGIN:
                    Console.WriteLine("Login: " + message.LoginPacket.Login);
                    Console.WriteLine("Password: " + message.LoginPacket.Password);
                    List<Character> chars = new List<Character>();
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto")
                        .SetInfo("Level: 1")
                        .Build());
                    chars.Add(Character.CreateBuilder()
                        .SetId(1)
                        .SetName("Keto2")
                        .SetInfo("Level: 100")
                        .Build());
                    MessagesHelper.SendCharacterList(connection, chars);
                    break;
                case Networkmessage.Types.Type.SELECTEDCHAR:
                    Game.GetInstance().OnPlayerLogin(connection);
                    Console.WriteLine("Selected char id = " + message.SelectCharacterPacket.Id.ToString());
                    break;
                case Networkmessage.Types.Type.REQUEST_MOVEMENT:
                    Game.GetInstance().OnPlayerMove(message.RequestMovementPacket.MovementType,
                        message.RequestMovementPacket.Ammount, connection);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
