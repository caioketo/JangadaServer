using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer.Content
{
    public class Terrain
    {
        int Id;
        float[,] HeightMapData;
        int Width;
        int Height;
        Vector3 Position;

        public Terrain(int id)
        {
            Id = id;
        }
    }
}
