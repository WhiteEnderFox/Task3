//Rah5i  Mitchell Dreyer 18000499

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GADE_POE
{
    [Serializable]

    class Map
    {
        private Unit[] units;
        Random R = new Random();

        public Unit[] Units
        {
            get { return units; }
            set { units = value; }
        }

        //numUnits = total num of units in the map
        public Map(int maxX, int maxY, int numUnits, int numbuilds)
        {
            units = new Unit[numUnits];
            for(int i = 0; i < numUnits/2; i++)
            {


                MeleeUnit m = new MeleeUnit(R.Next(0, maxX),
                                            R.Next(0, maxY),
                                            100,
                                            10,
                                            1,
                                            1,
                                            i % 2,
                                            "M",
                                            "Knight");
                Units[i] = m;

               
            }

            for (int j = numUnits/2; j < numUnits; j++)
            {
                RangedUnit r = new RangedUnit(R.Next(0, maxX),
                                              R.Next(0, maxY),
                                              100,
                                              10,
                                              1,
                                              3,
                                              j % 2,
                                              "R",
                                              "Archer");
                Units[j] = r;
            }

        }
    }
}
