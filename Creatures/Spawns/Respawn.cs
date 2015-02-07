using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace JangadaServer.Creatures.Spawns
{
    public class Respawn
    {
        public int RespawnTime;
        public List<int> CreaturesIdToRespawn = new List<int>();
        public List<int> CreaturesQtyToRespawn = new List<int>();
        public int AreaId;
        public Vector3 Q1;
        public Vector3 Q2;
        private Random random;
        private BackgroundWorker worker;

        public Respawn()
        {
            random = new Random();
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int totalCount = 0;
            //Check if still alive the monsters
            foreach (int creatureId in CreaturesIdToRespawn)
            {
                totalCount += Game.GetInstance().World.GetArea(AreaId).GetCreaturesCountInArea(creatureId, Q1, Q2);
            }


            if (!worker.IsBusy && totalCount <= 0)
                worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //RESPAWN CREATURES
            for (int i = 0; i < CreaturesIdToRespawn.Count; i++)
            {
                int creatureId = CreaturesIdToRespawn[i];
                int qty = CreaturesQtyToRespawn[i];
                for (int q = 0; q < qty; q++)
                {
                    Creature creatureToRespawn = new Creature(creatureId, RandomPosition());
                    Game.GetInstance().World.GetArea(AreaId).AddCreature(creatureToRespawn);
                }
                Console.WriteLine("Resp: " +  creatureId + " qty: " + qty);
            }
        }

        float NextDouble(float min, float max)
        {
            return (float)(min + (random.NextDouble() * (max - min)));
        }

        public Vector3 RandomPosition()
        {
            float x = NextDouble(Q1.X, Q2.X);
            float z = NextDouble(Q1.Z, Q2.Z);
            return new Vector3(x, Q1.Y, z);
        }

        public void Run()
        {
            Timer timer = new Timer(RespawnTime);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
    }
}
