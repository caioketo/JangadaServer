using Jangada;
using JangadaServer.Content;
using JangadaServer.Creatures;
using JangadaServer.Creatures.Spawns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class Game
    {
        static Game instance;
        public static Game GetInstance()
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }

        public bool useProto = true;
        public World World;
        List<Respawn> Respawns = new List<Respawn>();

        public Game()
        {
            World = new World();
            LoadRespawns();
        }

        private void LoadRespawns()
        {
            Respawn resp = new Respawn();
            resp.AreaId = 1;
            resp.CreaturesIdToRespawn.Add(1);
            resp.CreaturesQtyToRespawn.Add(5);
            resp.Q1 = new Microsoft.Xna.Framework.Vector3(1, 1, 1);
            resp.Q2 = new Microsoft.Xna.Framework.Vector3(4, 1, 4);
            resp.RespawnTime = 10000;
            Respawns.Add(resp);
            foreach (Respawn respawn in Respawns)
            {
                respawn.Run();
            }
        }

        public void OnPlayerLogin(ClientConnection connection)
        {
            World.Areas[0].AddPlayer(connection.player);
            MessagesHelper.SendInitialPacket(connection);
            foreach (Player player in connection.player.area.GetPlayers())
            {
                if (!player.Guid.Equals(connection.player.Guid))
                {
                    MessagesHelper.SendPlayerLogin(player.connection, connection.player);
                }
            }
        }
        internal void OnPlayerMove(Jangada.RequestMovementPacket.Types.MovementType movementType, float amount, ClientConnection connection)
        {
            switch (movementType)
            {
                case RequestMovementPacket.Types.MovementType.FORWARD:
                    connection.player.MoveForward(amount);
                    break;
                case RequestMovementPacket.Types.MovementType.BACKWARD:
                    connection.player.MoveBackward(amount);
                    break;
                case RequestMovementPacket.Types.MovementType.YAW:
                    connection.player.Yaw(amount);
                    break;
                default:
                    break;
            }
            MessagesHelper.SendPlayerMovement(connection);
            foreach (Player player in connection.player.area.GetPlayers())
            {
                if (!player.Guid.Equals(connection.player.Guid))
                {
                    MessagesHelper.SendCharacterMovement(connection.player, player.connection);
                }
            }
        }

        internal void PlayerLogout(Player player)
        {
            foreach (Player nplayer in player.area.GetPlayers())
            {
                if (!nplayer.Guid.Equals(player.Guid))
                {
                    MessagesHelper.SendPlayerLogout(player, nplayer.connection);
                }
            }
        }
    }
}
