using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_POE
{
    class Resource_methods : Building
    {
       
        public int XPos
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public int YPos
        {
            get { return yPos; }
            set { yPos = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int Faction
        {
            get { return faction; }
            set { faction = value; }
        }
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public Resource_methods(int x, int y, int health, int faction, string symbol, int resource, int resourcepertick, int resourceleft)
        {
            XPos = x;
            YPos = y;
            Health = health;
            Faction = faction;
            Symbol = symbol;
            Resource = resource;
            ResourcePerTick = resourcepertick;
            ResourceLeft = resourceleft;
        }

        public override void builds()
        {
            throw new NotImplementedException();
        }

        public override void Udeath()
        {
            throw new NotImplementedException();
        }

        public override void Tostring()
        {
            throw new NotImplementedException();
        }


        private int resource;

        public int Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        private int resourcepertick;

        public int ResourcePerTick
        {
            get { return resourcepertick; }
            set { resourcepertick = value; }
        }

        private int resourceleft;

        public int ResourceLeft
        {
            get { return resourceleft; }
            set { resourceleft = value; }
        }

    }
}
