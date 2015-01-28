using Jangada;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer
{
    public class Util
    {
        public static Position toPosition(Vector3 pos)
        {
            return Position.CreateBuilder()
                .SetX(pos.X)
                .SetY(pos.Y)
                .SetZ(pos.Z)
                .Build();
        }

        public static QuaternionMessage toQuaternionMessage(Quaternion qua)
        {
            return QuaternionMessage.CreateBuilder()
                .SetX(qua.X)
                .SetY(qua.Y)
                .SetZ(qua.Z)
                .SetW(qua.W)
                .Build();
        }

        public static string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
