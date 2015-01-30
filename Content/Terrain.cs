using JangadaServer.Properties;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            LoadHeightData();
        }

        private void LoadHeightData()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            Bitmap heightMap = Resources.HM1;
            Width = heightMap.Width;
            Height = heightMap.Height;
            HeightMapData = new float[Width, Height];
            System.Drawing.Color[] heightMapColors = new System.Drawing.Color[Width * Height];
            for (int x = 0; x < heightMap.Width; x++)
            {
                for (int y = 0; y < heightMap.Height; y++)
                {
                    HeightMapData[x, y] = heightMap.GetPixel(x,y).R / 5.0f;
                }
            }
        }

        public float GetHeightAt(Vector3 position)
        {
            //Determining the cell the player is positioned in      
            int left, top;
            left = (int)position.X;
            if (left < 0) left *= -1;
            top = (int)position.Z;
            if (top < 0) top *= -1;

            //Working out our position within the cell
            float xNormalized = position.X;
            float zNormalized = position.Z;

            //Calculating height
            float topHeight = MathHelper.Lerp(HeightMapData[left, top],
                            HeightMapData[left + 1, top],
                            xNormalized);

            float bottomHeight = MathHelper.Lerp(HeightMapData[left, top + 1],
                            HeightMapData[left + 1, top + 1],
                            xNormalized);

            //Finding exact height
            return MathHelper.Lerp(topHeight, bottomHeight, zNormalized);
        }
    }
}
