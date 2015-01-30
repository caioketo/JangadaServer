using Jangada;
using JangadaServer.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer.Creatures
{
    public class Creature
    {
        public string Guid;
        public Vector3 position = new Vector3(0, 0, 0);
        public Quaternion rotation = Quaternion.Identity;
        public Area area;
        public Stats Stats;
        public int HP = 500;
        public int MP = 500;

        public Creature(Vector3 position)
        {
            this.position = position;
            this.Guid = Util.generateID();
            this.Stats = new Stats();
        }


        #region Utils
        public void MoveForward(float speed)
        {
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, 1), this.rotation);
            this.position += addVector * speed;
            if (this.position.Y < area.Terrain.GetHeightAt(this.position))
            {
                this.position.Y = area.Terrain.GetHeightAt(this.position);
            }
        }

        public void MoveBackward(float speed)
        {
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), this.rotation);
            this.position += addVector * speed;
        }

        public void Yaw(float amount)
        {
            Quaternion qua = Quaternion.CreateFromYawPitchRoll(MathHelper.ToRadians(amount), 0, 0);
            this.rotation *= qua;
        }
        #endregion

        #region Getters
        public Position GetPosition()
        {
            return Util.toPosition(position);
        }

        public QuaternionMessage GetRotation()
        {
            return Util.toQuaternionMessage(rotation);
        }

        public int GetMaxHealth()
        {
            return (this.Stats.CONS * 100);
        }

        public int GetMaxMana()
        {
            return (this.Stats.INT * 100);
        }
        #endregion
    }
}
