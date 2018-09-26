//Rah5i  Mitchell Dreyer 18000499

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GADE_POE
{
    public enum Direction { North, East ,South , West}

    [Serializable]
    public abstract class Unit
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected int speed;
        protected int attack;
        protected int range;
        protected int faction;
        protected string symbol;
        protected string name;


        abstract public void Move(Direction direction);
        abstract public void Combat(Unit u);
        abstract public bool inRange(Unit u);
        abstract public Unit Closest(Unit[] units);
        abstract public bool IsDead();
        //abstract public void tostring();
    }
}
