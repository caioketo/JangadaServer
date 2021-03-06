﻿using Jangada;
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

        private static StatsDescription AddStatsDescription(Creature creature)
        {
            return StatsDescription.CreateBuilder()
                .SetCONS(creature.Stats.CONS)
                .SetDEX(creature.Stats.DEX)
                .SetINT(creature.Stats.INT)
                .SetWIS(creature.Stats.WIS)
                .SetSTR(creature.Stats.STR)
                .SetHP(creature.HP)
                .SetMP(creature.MP)
                .Build();
        }

        private static SkillsDescription AddSkillsDescription(Skill skill)
        {
            return SkillsDescription.CreateBuilder()
                .SetName(skill.Name)
                .SetTextureId(skill.TextureId)
                .SetCoolDown(skill.CoolDown)
                .SetDistance(skill.Distance)
                .SetAutoCast(skill.AutoCast)
                .Build();
        }

        public static void SendInitialPacket(ClientConnection connection)
        {
            Area area = connection.player.area;
            PlayerDescription.Builder playerDescBuilder = PlayerDescription.CreateBuilder()
                .SetPlayerGuid(connection.player.Guid)
                .SetPlayerPosition(connection.player.GetPosition())
                .SetPlayerRotation(connection.player.GetRotation())
                .SetStats(AddStatsDescription(connection.player));

            foreach (Skill skill in connection.player.Skills)
            {
                playerDescBuilder.AddSkills(AddSkillsDescription(skill));
            }

            PlayerDescription playerDesc = playerDescBuilder.Build();

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
                        .SetStats(AddStatsDescription(player))
                        .Build());
                }
            }
            foreach (Creature creature in area.GetCreatures())
            {
                areaDesc.AddCreatures(CreatureDescription.CreateBuilder()
                    .SetCreatureGuid(creature.Guid)
                    .SetModelId(creature.ModelId)
                    .SetCreaturePosition(creature.GetPosition())
                    .SetCreatureRotation(creature.GetRotation())
                    .SetStats(AddStatsDescription(creature))
                    .Build());
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

        internal static void SendCreatureRespawn(ClientConnection connection, Creature creature)
        {
            Messages messagesToSend = Messages.CreateBuilder()
                .AddNetworkmessage(Networkmessage.CreateBuilder()
                .SetType(Networkmessage.Types.Type.CREATURE_RESPAWN)
                .SetCreatureRespawnPacket(CreatureRespawnPacket.CreateBuilder()
                .SetCreatureDescription(CreatureDescription.CreateBuilder()
                .SetCreatureGuid(creature.Guid)
                .SetModelId(creature.ModelId)
                .SetCreaturePosition(creature.GetPosition())
                .SetCreatureRotation(creature.GetRotation())
                .SetStats(AddStatsDescription(creature))
                .Build())
                .Build())
                .Build())
                .Build();
            Send(messagesToSend, connection);
        }
    }
}
