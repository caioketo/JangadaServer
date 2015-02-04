using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JangadaServer.Content
{
    public class World
    {
        public List<Area> Areas = new List<Area>();

        public World()
        {
            Areas.Add(new Area(1));
        }

        public Area GetArea(int areaId)
        {
            foreach (Area area in Areas)
            {
                if (area.GetId() == areaId)
                {
                    return area;
                }
            }
            return null;
        }
    }
}
