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
    [Serializable]
    class MeleeUnit : Unit
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

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
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

       
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public MeleeUnit(int x, int y, int health, int attack, int speed, int range, int faction, string symbol, string name)
        {
            XPos = x;
            YPos = y;
            Health = health;
            Attack = attack;
            Speed = speed;
            Range = range;
            Faction = faction;
            Symbol = symbol;
            Name = name;
        }

        public override void Move(Direction d)
        {
            switch (d)
            {
                case Direction.North:
                    {
                        YPos -= Speed;
                        break;
                    }
                case Direction.East:
                    {
                        XPos += Speed;
                        break;
                    }
                case Direction.South:
                    {
                        YPos += Speed;
                        break;
                    }
                case Direction.West:
                    {
                        XPos -= Speed;
                        break;
                    }
            }
        }
        public override void Combat(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnit))
            {
                Health -= ((MeleeUnit)u).Attack;
            }
            //else if (u.GetType() == typeof(RangedUnit))
            //{
            //    Health -= ((RangedUnit)u).Attack;
            //}
        }
        public override bool IsDead()
        {
            if(Health <= 0)
            {
               return true;
            }
            else
            {
               return false;
            }
        }
        public override Unit Closest(Unit[] units)
        {
            Unit closest = this;
            int closestDistance = 50;
            foreach (Unit u in units)
            {
                if (u.GetType() == typeof(MeleeUnit))
                {
                    if (((MeleeUnit)u).Faction != Faction && ((MeleeUnit)u).IsDead() == false)
                        if (DistanceTo(u) < closestDistance)
                        {
                            closest = u;
                            closestDistance = DistanceTo(u);
                        }
                }
                //if (u.GetType() == typeof(RangedUnit))
                //{
                //    if (((RangedUnit)u).Faction != Faction)
                //        if (DistanceTo(u) < closestDistance)
                //    {
                //        closest = u;
                //        closestDistance = DistanceTo(u);
                //    }
                //}
            }
            return closest;
        }
        public override bool inRange(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit m = (MeleeUnit)u;

                if (DistanceTo(u) <= Range)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public override string ToString()
        {
            return Name + ", " + XPos + ", " + YPos + ", " + Health;
        }
        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        private int DistanceTo(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit m = (MeleeUnit)u;
                int d = Math.Abs(XPos - m.XPos) + Math.Abs(YPos - m.YPos);
                return d;

            }
            else
            {
                return 0;
            }
        }

        public Direction DirectionTo(Unit u)
        {
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit m = (MeleeUnit)u;
                if (m.XPos < XPos)
                {
                    return Direction.North;
                }
                else if (m.YPos > YPos)
                {
                    return Direction.East;
                }
                else if (m.XPos > XPos)
                {
                    return Direction.South;
                }
                else
                {
                    return Direction.West;
                }
            }
            else
            {
                return Direction.North;
            }
        }
    }
}
