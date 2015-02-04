using JangadaServer.Creatures;
using Microsoft.Xna.Framework;
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
        List<Creature> Creatures = new List<Creature>();
        public Terrain Terrain;
        int Id;

        public Area(int id)
        {
            Id = id;
            Terrain = new Terrain(id);
        }

        public int GetCreaturesCount(int creatureId)
        {
            int count = 0;
            foreach (Creature creature in Creatures)
            {
                if (creature.CreatureId == creatureId)
                {
                    count++;
                }
            }

            return count;
        }

        public int GetCreaturesCountInArea(int creatureId, Vector3 Q1, Vector3 Q2)
        {
            int count = 0;
            foreach (Creature creature in Creatures)
            {
                if (creature.CreatureId == creatureId &&
                    ((creature.position.X >= Q1.X && creature.position.X <= Q2.X) &&
                    (creature.position.Z >= Q1.Z && creature.position.Z <= Q2.Z)))
                {
                    count++;
                }
            }

            return count;
        }

        public int GetId()
        {
            return Id;
        }

        public List<Creature> GetCreatures()
        {
            return Creatures;
        }

        public void AddCreature(Creature creature)
        {
            creature.area = this;
            Creatures.Add(creature);
        }

        public void RemvoeCreature(Creature creature)
        {
            Creatures.Remove(creature);
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
