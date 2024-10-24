using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex
{
    public partial class Form1 : Form
    {
        private int corX;
        private int corY;
        int flag=0;
        List<List<bool>> space;
        int m=18;
        int n=30;
       
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            corX = m/2;
            corY = n-5;
            panel1.Height = n * 10;
            panel1.Width = m * 10;
            //space = new List<List<bool>> (n);
            space = Enumerable.Repeat(new List<bool>(Enumerable.Repeat(false, m)), n).ToList();
            space[0][0] = true;
            //for (int i = 0; i < n; i++)
            //{
            //    space.Add(new List<bool>(m));
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics Draw_Space = e.Graphics;
            HatchBrush hBrush = new HatchBrush(HatchStyle.Cross, Color.Red,Color.Red);
            HatchBrush hBrush2 = new HatchBrush(HatchStyle.Cross, Color.Gray, Color.Gray);
            Pen pen = new Pen(Color.Black, 2);
            Draw_Space.DrawLine(pen, new Point(0, 0), new Point(panel1.Width,0));
            Draw_Space.DrawLine(pen, new Point(0, 0), new Point(0, panel1.Height));
            Draw_Space.DrawLine(pen, new Point(0, panel1.Height), new Point(panel1.Width,panel1.Height));
            Draw_Space.DrawLine(pen, new Point(panel1.Width, panel1.Height), new Point(panel1.Width,0));
            Draw_Space.FillRectangle(hBrush, corX*10,corY*10, 10, 10);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (space[i][j] == true)
                    {
                        Draw_Space.FillRectangle(hBrush2, 10 * j, 10 * i, 10, 10);
                    }
                }
            }
            if (flag == 2)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        space[i][j] = false;
                    }
                }
                corX = m / 2;
                corY = n - 5;
                flag = 0;
                panel1.Refresh();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
            flag = 1;
            timer1.Start();
        }
        private List<bool> Rand()
        {
            Random rnd = new Random();
            int count = rnd.Next(1,m/3);
            List<bool> tmp;
            tmp = Enumerable.Repeat(false, m).ToList();
            for (int i = 0; i < count; i++)
            {
                int id = rnd.Next(0, m-1);
                tmp[id] = true;
            }
            return tmp;
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (flag == 1)
            {
                if (e.KeyCode == Keys.A)
                {
                    corX -= 1;
                }
                if (e.KeyData == Keys.D)
                {
                    corX += 1;
                }
                if (e.KeyCode == Keys.W)
                {
                   
                    for(int i = n-1;i>0; i--)
                    {
                        space[i] = space[i - 1];
                    }
                    space[0]=Rand();
                    //corY -= 1;
                }
                if (space[corY][corX] == true)
                {
                    
                    timer1.Stop();
                    MessageBox.Show("Game Over");
                    flag = 2;
                }
                panel1.Refresh();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = n - 1; i > 0; i--)
            {
                space[i] = space[i - 1];
            }
            space[0] = Rand();
            if (space[corY][corX] == true)
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                flag = 2;
            }
            panel1.Refresh();
        }
    }
}
