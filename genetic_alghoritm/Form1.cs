using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace genetic_alghoritm
{
    public partial class Form1 : Form
    {
        int x = 64;
        int y = 32;
        Bot[] bots;
        int[] sprites;
        Random rand;
        Pen[] brushes = new Pen[4] { new Pen(Color.Black, 1), new Pen(Color.Red, 1), new Pen(Color.Green, 1), new Pen(Color.Orange, 1) };
        Pen myPen = new Pen(Color.Silver, 1);
        Pen grPen = new Pen(Color.Gray, 1);
        Pen whitePen = new Pen(Color.White, 1);
        public Form1()
        {
            InitializeComponent();
            rand = new Random();
            bots = new Bot[64];
            for (int i = 0; i<64;i++)
            {
                int loc = rand.Next(0, 63);
                bots[i] = new Bot(50,new int[64], loc);
                for(int j= 0; j < 64;j++)
                {
                    bots[i].Genom[j] = rand.Next(0,63);
                }
            }
            sprites = new int[x*y];
            for(int i = 0; i<(x-1)*(y-1);i++)
            {
                int temp = rand.Next(20);
                if(temp==1|| temp == 2)
                {
                    sprites[i] = temp;
                }
                else
                {
                    sprites[i] = 0;
                }
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        public void draw()
        {
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;
            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with Form1, and with 
            // dimensions the same size as the drawing surface of Form1.
            myBuffer = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            for (int i = 0; i <= x; i++)
            {
                myBuffer.Graphics.DrawLine(myPen, i * 20, 0, i * 20, y * 20);
                //  g.DrawLine(myPen, 0, i * 20, this.Width, i * 20);
            }
            for (int i = 0; i <= y; i++)
            {
                //     g.DrawLine(myPen, i * 20, 0, i * 20, this.Height);
                myBuffer.Graphics.DrawLine(myPen, 0, i * 20, x * 20, i * 20);
            }
            for (int i = 0; i < y; i++)
            {
                for (int j = i * x; j - x * i < x; j++)
                {
                   myBuffer.Graphics.FillRectangle(brushes[sprites[j]].Brush, ((j) - x * i) * 20 + 1, i * 20 + 1, 18, 18);
                }
            }
            for (int i = 0; i < bots.Length; i++)
            {
                Bot bot = bots[i];
                myBuffer.Graphics.FillRectangle(brushes[3].Brush, (bot.location % x) * 20 + 1, ((int)bot.location / x) * 20 + 1, 18, 18);
                myBuffer.Graphics.DrawString(bot.health.ToString(), new Font("Consolas", 15, FontStyle.Bold, GraphicsUnit.Pixel), whitePen.Brush, new PointF((bot.location % x) * 20, ((int)bot.location / x) * 20));
            }
            //myBuffer.Render();
            myBuffer.Render(this.CreateGraphics());
            myBuffer.Dispose();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            while(true)
            {
                foreach(Bot bot in bots)
                {
                    if (sprites[bot.location] == 1) { bot.health -= 10; sprites[bot.location] = 0; sprites[rand.Next(0, x * y - 1)] = 1; }
                    else if (sprites[bot.location] == 2) { bot.health += 10; sprites[bot.location] = 0; sprites[rand.Next(0, x * y - 1)] = 2; }
                    draw();
                }
            }




            //for (int i = 0; i < y; i++)
            //{

            //    for (int j = i* x; j-x*i < x; j++)
            //    {
            //        if (j % x == 0 || (j - (x-1)) % x == 0) { g.FillRectangle(grPen.Brush, (j - x * i) * 20 + 1, i * 20 + 1, 18, 18); g.FillRectangle(grPen.Brush, (x-1) * 20 + 1, i * 20 + 1, 18, 18); }
            //        else if (i == 0 || i == y - 1) { g.FillRectangle(grPen.Brush, (j - x * i) * 20 + 1, i * 20 + 1, 18, 18); }

            //        //g.DrawString(j.ToString(), new Font("Consolas", fnt[(int)Math.Log10(j==0?1:j)], FontStyle.Bold, GraphicsUnit.Pixel), myPen.Brush, new PointF((j - x * i) *20, i * 20));

            //    }

            //}
            //for(int i = 0; i < Bot.Le)
            //{

            //}
        }
    }
}
