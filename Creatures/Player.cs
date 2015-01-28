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
    public class Player
    {
        public ClientConnection connection;
        public string Guid;
        public Vector3 position = new Vector3(0, 0, 0);
        public Quaternion rotation = Quaternion.Identity;
        public Area area;

        public Position GetPosition()
        {
            return Util.toPosition(position);
        }

        public QuaternionMessage GetRotation()
        {
            return Util.toQuaternionMessage(rotation);
        }

        public Player(Vector3 position, ClientConnection connection)
        {
            this.connection = connection;
            this.position = position;
            this.Guid = Util.generateID();
        }

        public void MoveForward(float speed)
        {
            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, 1), this.rotation);
            this.position += addVector * speed;
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
    }
}
