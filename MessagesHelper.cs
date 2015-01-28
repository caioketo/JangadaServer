using Jangada;
using JangadaServer.Content;
using JangadaServer.Creatures;
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
            try
            {
                message.WriteTo(connection.tcp.GetStream());
            }
            catch (Exception)
            {
                Game.GetInstance().PlayerLogout(connection.player);
            }
        }

        public static void SendCharacterList(ClientConnection connection, List<Character> characters)
        {
            Networkmessage.Builder newMessage = Networkmessage.CreateBuilder();
            newMessage.CharactersPacket = CharactersPacket.CreateBuilder().AddRangeCharacterList(characters).Build();
            newMessage.Type = Networkmessage.Types.Type.CHARACTERS;
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(newMessage.Build()).Build();
            Send(messagesToSend, connection);
        }

        public static void SendCharacterPosition(ClientConnection connection, int mapIndex, Position position)
        {
            /*Networkmessage.Builder newMessage = Networkmessage.CreateBuilder();
            newMessage.SetType(Networkmessage.Types.Type.CHARACTER_POSITION);
            newMessage.CharacterPositionPacket = CharacterPositionPacket.CreateBuilder()
                .SetMapId(mapIndex)
                .SetPosition(position)
                .Build();
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(newMessage.Build()).Build();
            Send(messagesToSend, connection);*/
        }

        public static void SendPlayerMovement(ClientConnection connection)
        {
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(Networkmessage.CreateBuilder().SetType(Networkmessage.Types.Type.PLAYER_MOVEMENT)
                .SetPlayerMovementPacket(PlayerMovementPacket.CreateBuilder()
                    .SetNewPosition(Util.toPosition(connection.player.position))
                    .SetNewRotation(Util.toQuaternionMessage(connection.player.rotation))
                    .Build()).Build()).Build();
            Send(messagesToSend, connection);
        }

        public static void SendPlayerLogin(ClientConnection connection, Player player)
        {
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(Networkmessage.CreateBuilder().SetType(Networkmessage.Types.Type.PLAYER_LOGIN)
                .SetPlayerLoginPacket(PlayerLoginPacket.CreateBuilder().SetPlayer(PlayerDescription.CreateBuilder()
                .SetPlayerGuid(player.Guid)
                .SetPlayerPosition(player.GetPosition())
                .SetPlayerRotation(player.GetRotation())
                .Build()).Build()).Build()).Build();
            Send(messagesToSend, connection);
        }

        public static void SendInitialPacket(ClientConnection connection)
        {
            Area area = connection.player.area;
            PlayerDescription playerDesc = PlayerDescription.CreateBuilder()
                .SetPlayerGuid(connection.player.Guid)
                .SetPlayerPosition(connection.player.GetPosition())
                .SetPlayerRotation(connection.player.GetRotation())
                .Build();
            AreaDescriptionPacket.Builder areaDesc = AreaDescriptionPacket.CreateBuilder();
            areaDesc.SetAreaId(area.GetId());
            areaDesc.SetPlayer(playerDesc);
            foreach (Player player in area.GetPlayers())
            {
                if (!player.Guid.Equals(connection.player.Guid))
                {
                    areaDesc.AddPlayers(PlayerDescription.CreateBuilder()
                        .SetPlayerGuid(player.Guid)
                        .SetPlayerPosition(player.GetPosition())
                        .SetPlayerRotation(player.GetRotation())
                        .Build());
                }
            }
            Messages messagesToSend = Messages.CreateBuilder().AddNetworkmessage(Networkmessage.CreateBuilder()
                .SetType(Networkmessage.Types.Type.AREA_DESCRIPTION)
                .SetAreaDescriptionPacket(areaDesc.Build())).Build();
            Send(messagesToSend, connection);
        }

        internal static void SendCharacterMovement(Player player, ClientConnection connection)
        {
            Messages messagesToSend = Messages.CreateBuilder()
                .AddNetworkmessage(Networkmessage.CreateBuilder()
                .SetType(Networkmessage.Types.Type.CHARACTER_MOVEMENT)
                .SetCharacterMovementPacket(CharacterMovementPacket.CreateBuilder().SetPlayer(PlayerDescription.CreateBuilder()
                .SetPlayerGuid(player.Guid)
                .SetPlayerPosition(player.GetPosition())
                .SetPlayerRotation(player.GetRotation())
                .Build()).Build()).Build()).Build();

            Send(messagesToSend, connection);
        }

        internal static void SendPlayerLogout(Player player, ClientConnection connection)
        {
            Messages messagesToSend = Messages.CreateBuilder()
                .AddNetworkmessage(Networkmessage.CreateBuilder()
                .SetType(Networkmessage.Types.Type.PLAYER_LOGOUT)
                .SetPlayerLogoutPacket(PlayerLogoutPacket.CreateBuilder()
                .SetPlayerGuid(player.Guid)
                .Build())
                .Build())
                .Build();

            Send(messagesToSend, connection);
        }
    }
}
