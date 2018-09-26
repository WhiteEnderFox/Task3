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
    public partial class Form1 : Form
    {
        Map map = new Map(20, 20, 20, 20);
        const int START_X = 20;
        const int START_Y = 20;
        const int SPACING = 10;
        const int SIZE = 20;
        Random R = new Random();
        int turn = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DisplayMap()
        {
            groupBox1.Controls.Clear();
            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(MeleeUnit))// display for melee unit
                {
                    int start_x, start_y;
                    start_x = groupBox1.Location.X;
                    start_y = groupBox1.Location.Y;
                    MeleeUnit m = (MeleeUnit)u;
                    Button b = new Button();
                    b.Size = new Size(SIZE, SIZE);
                    b.Location = new Point(start_x + (m.XPos * SIZE), start_y + (m.YPos * SIZE));
                    b.Text = m.Symbol;
                    if (m.Faction == 1)
                    {
                        b.ForeColor = Color.Blue;
                    }
                    else
                    {
                        b.ForeColor = Color.Orange;
                    }

                    if(m.IsDead())
                    {
                        b.ForeColor = Color.Black;
                    }
                    b.Click += new EventHandler(buttn_click);
                    groupBox1.Controls.Add(b);
                }

                if (u.GetType() == typeof(lich))// display for lich unit
                {
                    int start_x, start_y;
                    start_x = groupBox1.Location.X;
                    start_y = groupBox1.Location.Y;
                    lich l = (lich)u;
                    Button b = new Button();
                    b.Size = new Size(SIZE, SIZE);
                    b.Location = new Point(start_x + (l.XPos * SIZE), start_y + (l.YPos * SIZE));
                    b.Text = l.Symbol;
                    if (l.Faction == 1)
                    {
                        b.ForeColor = Color.Blue;
                    }
                    else
                    {
                        b.ForeColor = Color.Orange;
                    }

                    if (l.IsDead())
                    {
                        b.ForeColor = Color.Black;
                    }
                    b.Click += new EventHandler(buttn_click);
                    groupBox1.Controls.Add(b);
                }

                foreach (Unit Y in map.Units)
                {if (Y.GetType() == typeof(RangedUnit))// display for ranged unit
                    {
                        int start_x, start_y;
                        start_x = groupBox1.Location.X;
                        start_y = groupBox1.Location.Y;
                        RangedUnit r = (RangedUnit)Y;
                        Button b = new Button();
                        b.Size = new Size(SIZE, SIZE);
                        b.Location = new Point(start_x + (r.XPos * SIZE), start_y + (r.YPos * SIZE));
                        b.Text = r.Symbol;
                        if (r.Faction == 1)
                        {
                            b.ForeColor = Color.Blue;
                        }
                        else
                        {
                            b.ForeColor = Color.Orange;
                        }

                        if (r.IsDead())
                        {
                            b.ForeColor = Color.Black;
                        }
                        b.Click += new EventHandler(buttn_click);
                        groupBox1.Controls.Add(b);
                    }
                }
                if (u.GetType() == typeof(Rouge))// display for rouge unit
                {
                    int start_x, start_y;
                    start_x = groupBox1.Location.X;
                    start_y = groupBox1.Location.Y;
                    Rouge v = (Rouge)u;
                    Button b = new Button();
                    b.Size = new Size(SIZE, SIZE);
                    b.Location = new Point(start_x + (v.XPos * SIZE), start_y + (v.YPos * SIZE));
                    b.Text = v.Symbol;
                    if (v.Faction == 1)
                    {
                        b.ForeColor = Color.Green;
                    }

                    if (v.IsDead())
                    {
                        b.ForeColor = Color.Black;
                    }
                    b.Click += new EventHandler(buttn_click);
                    groupBox1.Controls.Add(b);
                }

            }
        }

        private void UpdateMap()
        {

            foreach (Unit u in map.Units)
            {

                if (u.IsDead() == false)
                {
                    if (u.GetType() == typeof(MeleeUnit))
                    {
                        int start_x, start_y;
                        start_x = groupBox1.Location.X;
                        start_y = groupBox1.Location.Y;
                        MeleeUnit m = (MeleeUnit)u;
                        Button b = new Button();
                        b.Size = new Size(SIZE, SIZE);
                        b.Location = new Point(start_x + (m.XPos * SIZE), start_y + (m.YPos * SIZE));
                        b.Text = m.Symbol;
                        if (m.Faction == 1)
                        {
                            b.ForeColor = Color.Blue;
                        }
                        else
                        {
                            b.ForeColor = Color.Orange;
                        }

                        if (m.IsDead())
                        {
                            b.ForeColor = Color.Black;
                        }
                        b.Click += new EventHandler(buttn_click);

                        
                        if (m.Health < 15)// running away
                        {
                            switch (R.Next(0, 4))
                            {
                                case 0: m.Move(Direction.North); break;
                                case 1: m.Move(Direction.East); break;
                                case 2: m.Move(Direction.South); break;
                                case 3: m.Move(Direction.West); break;
                            }
                        }
                        else if (m.Health < 5) // change faction
                        {
                            if (m.Faction == 0)
                            {
                                b.ForeColor = Color.Blue;
                                m.Faction = 1;
                                m.Health = 6;
                            }
                            else
                            {
                                b.ForeColor = Color.Orange;
                                m.Faction = 0;
                                m.Health = 6;
                            }
                        }
                        else if (m.IsDead() == true)
                        {

                        }
                        else // in combat or moving toward
                        {
                            bool inCombat = false;
                            foreach (Unit e in map.Units)
                            {

                                if (u.inRange(e)) // in combat
                                {
                                    u.Combat(e);
                                    inCombat = true;
                                }
                            }

                            if (inCombat)
                            {
                                Unit c = m.Closest(map.Units);
                                m.Move(m.DirectionTo(c));
                            }
                        }
                    }
                }
                if (u.IsDead() == false)
                {
                    if (u.GetType() == typeof(lich))
                    {
                        int start_x, start_y;
                        start_x = groupBox1.Location.X;
                        start_y = groupBox1.Location.Y;
                        lich l = (lich)u;
                        Button b = new Button();
                        b.Size = new Size(SIZE, SIZE);
                        b.Location = new Point(start_x + (l.XPos * SIZE), start_y + (l.YPos * SIZE));
                        b.Text = l.Symbol;
                        if (l.Faction == 1)
                        {
                            b.ForeColor = Color.Blue;
                        }
                        else
                        {
                            b.ForeColor = Color.Orange;
                        }

                        if (l.IsDead())
                        {
                            b.ForeColor = Color.Black;
                        }
                        b.Click += new EventHandler(buttn_click);


                        if (l.Health < 10)// running away
                        {
                            switch (R.Next(0, 4))
                            {
                                case 0: l.Move(Direction.North); break;
                                case 1: l.Move(Direction.East); break;
                                case 2: l.Move(Direction.South); break;
                                case 3: l.Move(Direction.West); break;
                            }
                        }
                        else if (l.Health < 5) // change faction
                        {
                            if (l.Faction == 0)
                            {
                                b.ForeColor = Color.Blue;
                                l.Faction = 1;
                                l.Health = 6;
                            }
                            else
                            {
                                b.ForeColor = Color.Orange;
                                l.Faction = 0;
                                l.Health = 6;
                            }
                        }
                        else if (l.IsDead() == true)
                        {

                        }
                        else // in combat or moving toward
                        {
                            bool inCombat = false;
                            foreach (Unit e in map.Units)
                            {

                                if (u.inRange(e)) // in combat
                                {
                                    u.Combat(e);
                                    inCombat = true;
                                }
                            }

                            if (inCombat)
                            {
                                Unit c = l.Closest(map.Units);
                                l.Move(l.DirectionTo(c));
                            }
                        }
                    }
                }
                if (u.IsDead() == false)
                {
                    if (u.GetType() == typeof(Rouge))
                    {

                        Rouge v = (Rouge)u;
                        
                        if (v.Health < 7)// running away
                        {
                            switch (R.Next(0, 4))
                            {
                                case 0: v.Move(Direction.North); break;
                                case 1: v.Move(Direction.East); break;
                                case 2: v.Move(Direction.South); break;
                                case 3: v.Move(Direction.West); break;
                            }
                        }
                       
                        else if (v.IsDead() == true)
                        {

                        }
                        else // in combat or moving toward
                        {
                            bool inCombat = false;
                            foreach (Unit e in map.Units)
                            {

                                if (u.inRange(e)) // in combat
                                {
                                    u.Combat(e);
                                    inCombat = true;
                                }
                            }

                            if (inCombat)
                            {
                                Unit c = v.Closest(map.Units);
                                v.Move(v.DirectionTo(c));
                            }
                        }
                    }
                }
                    foreach (Unit Y in map.Units)
                {
                    if (Y.IsDead() == false)
                    {
                        if (Y.GetType() == typeof(RangedUnit))
                        {
                            int start_x, start_y;
                            start_x = groupBox1.Location.X;
                            start_y = groupBox1.Location.Y;
                            RangedUnit r = (RangedUnit)Y;
                            Button b = new Button();
                            b.Size = new Size(SIZE, SIZE);
                            b.Location = new Point(start_x + (r.XPos * SIZE), start_y + (r.YPos * SIZE));
                            b.Text = r.Symbol;
                            if (r.Faction == 1)
                            {
                                b.ForeColor = Color.Blue;
                            }
                            else
                            {
                                b.ForeColor = Color.Orange;
                            }

                            if (r.IsDead())
                            {
                                b.ForeColor = Color.Black;
                            }
                            b.Click += new EventHandler(buttn_click);
                            
                            if (r.Health < 25)// running away
                            {
                                switch (R.Next(0, 4))
                                {
                                    case 0: r.Move(Direction.North); break;
                                    case 1: r.Move(Direction.East); break;
                                    case 2: r.Move(Direction.South); break;
                                    case 3: r.Move(Direction.West); break;
                                }
                            }
                            else if (r.Health < 5)  // change faction
                            {
                                if (r.Faction == 0)
                                {
                                    b.ForeColor = Color.Blue;
                                    r.Faction = 1;
                                    r.Health = 6;
                                }
                                else
                                {
                                    b.ForeColor = Color.Orange;
                                    r.Faction = 0;
                                    r.Health = 6;
                                }
                            }
                            else if (r.IsDead() == true)
                            {

                            }
                            else // in combat or moving toward
                            {
                                bool inCombat = false;
                                foreach (Unit e in map.Units)
                                {

                                    if (Y.inRange(e)) // in combat
                                    {
                                        Y.Combat(e);
                                        inCombat = true;
                                    }
                                }

                                if (inCombat)
                                {
                                    Unit c = r.Closest(map.Units);
                                    r.Move(r.DirectionTo(c));
                                }
                            }
                        }
                    }
                }
            }
        }
            private void timer1_Tick(object sender, EventArgs e)
            {
                UpdateMap();
                DisplayMap();
            txtTurn.Text = (++turn).ToString();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void buttn_click(object sender, EventArgs e) // display unit stats page
        {
            int x = (((Button)sender).Location.X - groupBox1.Location.X) / SIZE;
            int y = (((Button)sender).Location.Y - groupBox1.Location.Y) / SIZE;
            txtInfo.Text = x + " " + y;
            foreach (Unit u in map.Units)
            {
                if (u.GetType() == typeof(MeleeUnit))
                {
                    MeleeUnit m = (MeleeUnit)u;
                    if (m.XPos == x && m.YPos == y)
                    {
                        txtInfo.Text = "Unit stats: " + m.ToString();
                    }
                }

                 else if (u.GetType() == typeof(RangedUnit))
                    {
                        RangedUnit r = (RangedUnit)u;
                        if (r.XPos == x && r.YPos == y)
                        {
                            txtInfo.Text = "Unit stats: " + r.ToString();
                        }
                    }

                else if (u.GetType() == typeof(Rouge))
                {
                    Rouge v = (Rouge)u;
                    if (v.XPos == x && v.YPos == y)
                    {
                        txtInfo.Text = "Unit stats: " + v.ToString();
                    }
                }
                else if (u.GetType() == typeof(lich))
                {
                    lich l = (lich)u;
                    if (l.XPos == x && l.YPos == y)
                    {
                        txtInfo.Text = "Unit stats: " + l.ToString();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)// save button
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsout = new FileStream("Map.dat", FileMode.Create, FileAccess.Write, FileShare.None);


            try
            {
                using (fsout)
                {
                    bf.Serialize(fsout, map);
                    MessageBox.Show("info Saved");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred" + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)// load button
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsin = new FileStream("Map.dat", FileMode.Open, FileAccess.Read, FileShare.None);

            try
            {
                using (fsin)
                {
                    map = (Map)bf.Deserialize(fsin);
                    MessageBox.Show("info Loaded");
                }
                DisplayMap();
            }
            catch
            {
                MessageBox.Show("Error Occurred");
            }
        }
    }
}
