using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_POE
{
    abstract class Building
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int faction;
        protected string symbol;

        public abstract void builds();

        public abstract void Udeath();

        public abstract void Tostring();

        public Building()
        {

        }
    }
}
