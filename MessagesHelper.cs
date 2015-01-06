using Jangada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class MessagesHelper
    {
        public static void Send(Messages message, ClientConnection connection)
        {
            message.WriteTo(connection.tcp.GetStream());
        }

        public static void SendCharacterList(ClientConnection connection, List<Character> characters)
        {
            Networkmessage.Builder newMessage = Networkmessage.CreateBuilder();
            newMessage.CharactersPacket = CharactersPacket.CreateBuilder().AddRangeCharacterList(characters).Build();
            newMessage.Type = Networkmessage.Types.Type.CHARACTERS;
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(newMessage.Build()).Build();
            Send(messagesToSend, connection);
        }
    }
}
