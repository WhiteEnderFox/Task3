﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_POE
{
    class Factory_Methods : Building
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

        public Factory_Methods(int x, int y, int health, int faction, string symbol, int unitsspwn, int ticksperproduce, int spwnpoint)
        {
            XPos = x;
            YPos = y;
            Health = health;
            Faction = faction;
            Symbol = symbol;
            UnitsSpwn = unitsspwn;
            TicksPerProduce = ticksperproduce;
            SpwnPoint = spwnpoint;
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

        private int unitsspwn;

        public int UnitsSpwn
        {
            get { return unitsspwn; }
            set { unitsspwn = value; }
        }

        private int ticksperproduce;

        public int TicksPerProduce
        {
            get { return ticksperproduce; }
            set { ticksperproduce = value; }
        }

        private int spwnpoint;

        public int SpwnPoint
        {
            get { return spwnpoint; }
            set { spwnpoint = value; }
        }

    }
}
