using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random numgen = new Random();
        int direction = 0;
        int tail_count = 1;
        bool can_use_keys = true;
        int color = 0;
        bool inc = false;

        public void spawn()
        {
            PictureBox a = new PictureBox();
            a.BackColor = Color.Red;
            a.Height = 25;
            a.Width = 25;

            mark1:

            int xx = numgen.Next(0, 17) * 25;
            int yy = numgen.Next(0, 17) * 25;

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (item.Location.X == xx && item.Location.Y == yy) goto mark1;
            }

            a.Tag = "food";
            a.Location = new Point(xx, yy);

            Controls.Add(a);
        }

        public void add_tail(int xxx, int yyy)
        {
            tail_count++;
            PictureBox a = new PictureBox();


            if (color == 0) inc = false;
            if (color == 250) inc = true;

            if (inc == false) color += 10;
            else color -= 10;

            a.BackColor = Color.FromArgb(0, color, color);


            a.Height = 25;
            a.Width = 25;
            a.Location = new Point(xxx, yyy);
            a.Tag = tail_count;

            Controls.Add(a);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            spawn();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = tail_count; i > 0; i--)
            {
                foreach (PictureBox item in Controls.OfType<PictureBox>())
                {
                    /*
                    //šablona
                    if (item.Tag.ToString() == tail_count.ToString())
                    {
                        item.Location = new Point(pictureBox1.Location.X,pictureBox1.Location.Y);
                    }
                    */
                    if (item.Tag.ToString() == i.ToString())
                    {
                        foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                        {
                            if (item2.Tag.ToString() == (i + 1).ToString())
                            {
                                item2.Location = new Point(item.Location.X, item.Location.Y);
                            }
                        }
                    }
                }
            }

            if (direction == 0)
            {
                if (pictureBox1.Location.X == 0) pictureBox1.Location = new Point(pictureBox1.Location.X + 400, pictureBox1.Location.Y);
                else pictureBox1.Location = new Point(pictureBox1.Location.X - 25, pictureBox1.Location.Y);
            }
            if (direction == 1)
            {
                if (pictureBox1.Location.Y == 400) pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 400);
                else pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 25);
            }
            if (direction == 2)
            {
                if (pictureBox1.Location.X == 400) pictureBox1.Location = new Point(pictureBox1.Location.X - 400, pictureBox1.Location.Y);
                else pictureBox1.Location = new Point(pictureBox1.Location.X + 25, pictureBox1.Location.Y);
            }
            if (direction == 3)
            {
                if (pictureBox1.Location.Y == 0) pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 400);
                else pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 25);
            }

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (item.Tag != null && item.Tag.ToString() != "food" && item.Tag.ToString() != "1")
                {
                    if (pictureBox1.Location == item.Location)
                    {
                        timer1.Stop();
                        MessageBox.Show("You Lost! Score " + tail_count + "!");
                        Application.Exit();
                    }
                }
            }


            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (item.Tag == "food")
                {
                    if (item.Location == pictureBox1.Location)
                    {

                        add_tail(item.Location.X, item.Location.Y);
                        Controls.Remove(item);
                        spawn();
                    }
                }
            }




            can_use_keys = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && can_use_keys && direction != 2) { direction = 0; timer1.Start(); can_use_keys = false; }
            if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down) && can_use_keys && direction != 3) { direction = 1; timer1.Start(); can_use_keys = false; }
            if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && can_use_keys && direction != 0) { direction = 2; timer1.Start(); can_use_keys = false; }
            if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up) && can_use_keys && direction != 1) { direction = 3; timer1.Start(); can_use_keys = false; }
        }


    }
}
