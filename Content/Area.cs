using JangadaServer.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer.Content
{
    public class Area
    {
        List<Player> Players = new List<Player>();
        public Terrain Terrain;
        int Id;

        public Area(int id)
        {
            Id = id;
            Terrain = new Terrain(id);
        }


        public int GetId()
        {
            return Id;
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public void AddPlayer(Player player)
        {
            player.area = this;
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public bool IsPlayerInArea(Player player)
        {
            return Players.Contains(player);
        }
    }
}
