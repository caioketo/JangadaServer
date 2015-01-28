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
    }
}
