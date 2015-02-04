using JangadaServer.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            Bitmap heightMap = Resources.HM2;
            Width = heightMap.Width;
            Height = heightMap.Height;
            HeightMapData = new float[Width, Height];
            for (int x = 0; x < heightMap.Width; x++)
            {
                for (int y = 0; y < heightMap.Height; y++)
                {
                    System.Drawing.Color color = heightMap.GetPixel(x, y);
                    byte r = heightMap.GetPixel(x, y).R;
                    byte g = heightMap.GetPixel(x, y).G;
                    byte b = heightMap.GetPixel(x, y).B;
                    float br = heightMap.GetPixel(x, y).GetBrightness();
                    HeightMapData[x, y] = heightMap.GetPixel(x,y).R / 5.0f;
                }
            }
        }


        public float GetHeightAt(Vector3 heightmapPosition)
        {
            Vector3 positionOnHeightmap = heightmapPosition;
            float fX, fY;
            fX = positionOnHeightmap.X + (Width / 2);
            fY = positionOnHeightmap.Z + (Height / 2);
            float fTX, fTY;
            float fSampleH1, fSampleH2, fSampleH3, fSampleH4;
            int x, y;
            double fFinalHeight;
            x = (int)Math.Floor(fX);
            y = (int)Math.Floor(fY);
            fTX = fX - x;
            fTY = fY - y;
            if (x >= 255)
            {
                x = 254;
            }
            if (y >= 255)
            {
                y = 254;
            }
            fSampleH1 = HeightMapData[x, y];
            fSampleH2 = HeightMapData[x + 1, y];
            fSampleH3 = HeightMapData[x, y + 1];
            fSampleH4 = HeightMapData[x + 1, y + 1];
            fFinalHeight = (fSampleH1 * (1.0 - fTX) + fSampleH2 * fTX) * (1.0 - fTY) + (fSampleH3 * (1.0 - fTX) + fSampleH4 * fTX) * (fTY);
            Console.WriteLine(HeightMapData[x, y].ToString());
            x = (int)(heightmapPosition.X);
            int z = (int)(heightmapPosition.Z);

            int xPlusOne = x + 1;
            int zPlusOne = z + 1;

            float triZ0 = (this.HeightMapData[x, z]);
            float triZ1 = (this.HeightMapData[xPlusOne, z]);
            float triZ2 = (this.HeightMapData[x, zPlusOne]);
            float triZ3 = (this.HeightMapData[xPlusOne, zPlusOne]);

            float height = 0.0f;
            float sqX = (heightmapPosition.X) - x;
            float sqZ = (heightmapPosition.Z) - z;
            if ((sqX + sqZ) < 1)
            {
                height = triZ0;
                height += (triZ1 - triZ0) * sqX;
                height += (triZ2 - triZ0) * sqZ;
            }
            else
            {
                height = triZ3;
                height += (triZ1 - triZ3) * (1.0f - sqZ);
                height += (triZ2 - triZ3) * (1.0f - sqX);
            }
            Console.WriteLine(height.ToString());
            return height;
        }
    }
}
