using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace genetic_alghoritm
{
    public partial class Form1 : Form
    {
        int x = 64;
        int y = 32;
        int livetime = 0;
        int generation = 0;
        Bot[] bots;
        int[] sprites;
        Random rand;
        Pen[] brushes = new Pen[4] { new Pen(Color.Black, 1), new Pen(Color.Red, 1), new Pen(Color.Green, 1), new Pen(Color.Orange, 1)};
        Pen myPen = new Pen(Color.Silver, 1);
        Pen grPen = new Pen(Color.Gray, 1);
        Pen whitePen = new Pen(Color.White, 1);
        public Form1(string path)
        {
            InitializeComponent();
            rand = new Random();
            bots = new Bot[64];
            Console.WriteLine("Загрузка поколения...");
            StreamReader stream = new StreamReader(path, Encoding.ASCII);
            string live = stream.ReadLine();
            Console.WriteLine("Загрузка выполнена. " + live);
            int.TryParse(Path.GetFileNameWithoutExtension(path),out generation);
            for (int i = 0; i<64;i++)
            {
                Point loc = new Point(rand.Next(0,x-1),rand.Next(0,y-1));
                bots[i] = new Bot(100,new int[64], loc);
                for (int j = 0; j < 64; j++)
                {
                    bots[i].Genom[j] = rand.Next(0, 63);
                }
                if (!stream.EndOfStream)
                {
                    string gen = stream.ReadLine();
                    string[] genS = gen.Split('|');
                    for(int j = 1; j<9;j++)
                    {
                        bots[i].Genom[j-1] = int.Parse(genS[j]);
                    }
                    Console.WriteLine(bots[i].pointer + " " + bots[i].Genom[bots[i].pointer]);
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
        public int isCellOcupied(Point point)
        {
            foreach(Bot bot in bots)
            {
                if (bot.location==point) { return 3; }
            }
            return sprites[point.Y * x + point.X];
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
            int botscount = 0;
            for (int i = 0; i < bots.Length; i++)
            {
                if (bots[i].health <= 0) { continue; }
                botscount++;
                Bot bot = bots[i];
                myBuffer.Graphics.FillRectangle(brushes[3].Brush, bot.location.X*20+1, bot.location.Y*20 + 1, 18, 18);
            //    Point dir = bot.getDirection(bot.Genom[bot.pointer] % 8, bot.location.X, bot.location.Y);
            //    myBuffer.Graphics.FillRectangle(new Pen(Color.Aqua, 1).Brush, dir.X * 20 + 1, dir.Y * 20 + 1, 18, 18);
           //     myBuffer.Graphics.DrawString(bot.health.ToString(), new Font("Consolas", 15, FontStyle.Bold, GraphicsUnit.Pixel), whitePen.Brush, new Point(bot.location.X*20+1, bot.location.Y * 20 + 1));
                myBuffer.Graphics.DrawString(bot.pointer.ToString(), new Font("Consolas", 15, FontStyle.Bold, GraphicsUnit.Pixel), whitePen.Brush, new Point(dir.X * 20 + 1, dir.Y * 20 + 1));
            }
            if(botscount<=8)
            {
                Console.WriteLine("//////////////////////////////////{0}////////////////////////////////////",generation);
                Console.WriteLine(livetime + " " + botscount);
                using (FileStream stream = new FileStream("Generations\\" + generation + ".txt", FileMode.Append))
                {
                    byte[] file = Encoding.ASCII.GetBytes("Livetime: " + livetime+ "\n");
                    stream.Write(file, 0, file.Length);
                    //  Console.WriteLine(genom);
                    stream.Close();
                }
                livetime = 0;
                Bot[] nbots = new Bot[64];
                int f = 0;
                for (int i = 0; i < bots.Length; i++)
                {
                    if (bots[i].health > 0)
                    {
                        using (FileStream stream = new FileStream("Generations\\"+generation + ".txt", FileMode.Append))
                        {
                            string genom = "BOT " + i + ": ";
                            foreach (int gen in bots[i].Genom)
                            {
                                genom += "|"+ gen;
                            }
                            genom += "|";
                            byte[] file = Encoding.ASCII.GetBytes(genom + "\n");
                            stream.Write(file, 0, file.Length);
                          //  Console.WriteLine(genom);
                            stream.Close();
                        }
                        for (int j = 0; j < 8; j++)
                        {
                            Point loc = new Point(rand.Next(0, x - 1), rand.Next(0, y - 1));
                            nbots[f] = new Bot(100, new int[64], loc);
                            nbots[f].Genom = bots[i].Genom;
                            if (j==7) { nbots[f].Genom[rand.Next(0, 63)] = rand.Next(0, 63); } 
                            f++;
                        }
                    }
                }
                bots = nbots;
                sprites = new int[x * y];
                for (int i = 0; i < (x - 1) * (y - 1); i++)
                {
                    int temp = rand.Next(20);
                    if (temp == 1 || temp == 2)
                    {
                        sprites[i] = temp;
                    }
                    else
                    {
                        sprites[i] = 0;
                    }
                }
                generation++;
            }
            myBuffer.Render();
            myBuffer.Render(this.CreateGraphics());
            myBuffer.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bots.Length; i++)
            {
                bool isEnd = true;
                if (bots[i].health <= 0) { continue; }
                int cmd = bots[i].Genom[bots[i].pointer];
                //  Console.WriteLine(i + "/" + cmd);
                if (cmd < 8)
                {
                    Point point = bots[i].getDirection(cmd, x, y);
                    if (point.X < 0 || point.Y < 0 || point.X >= x || point.Y >= y)
                    {
                        bots[i].addPointer(4);
                        continue;
                    }
                    int result = isCellOcupied(point);
                    if (result == 0)
                    {
                        bots[i].addPointer(6);
                    }
                    else
                    {
                        bots[i].addPointer(result);
                    }
                    if (result == 1) {  sprites[point.Y * x + point.X] = 0; if (bots[i].Damage(20) == 1) { sprites[point.Y * x + point.X] = 1; } if (rand.Next(0, 4) == 2) { sprites[rand.Next(0, x * y - 1)] = 1; } }
                    else if (result == 2) { bots[i].health += 30; sprites[point.Y * x + point.X] = 0; if (rand.Next(0, 4) == 2) { sprites[rand.Next(0, x * y - 1)] = 2; } }
                    else if (result == 3)
                    {
                        continue;
                    }
                    bots[i].location = point;

                }
                else if (cmd < 16)
                {
                    isEnd = false;
                    Point point = bots[i].getDirection(cmd % 8, x, y);
                    if (point.X < 0 || point.Y < 0 || point.X >= x || point.Y >= y)
                    {
                        bots[i].addPointer(3);
                        i--;
                        continue;
                    }
                    int result = isCellOcupied(point);
                    if (result == 0)
                    {
                        bots[i].addPointer(6);
                    }
                    else
                    {
                        bots[i].addPointer(result);
                    }
                }
                else if (cmd < 24)
                {
                    Point point = bots[i].getDirection(cmd % 8, x, y);
                    if (point.X < 0 || point.Y < 0 || point.X >= x || point.Y >= y)
                    {
                        bots[i].addPointer(4);
                        continue;
                    }
                    int result = isCellOcupied(point);
                    if (result == 0)
                    {
                        bots[i].addPointer(6);
                    }
                    else
                    {
                        bots[i].addPointer(result);
                        if (result == 1) { sprites[point.Y * x + point.X] = 2; if (rand.Next(0, 4) == 2) { sprites[rand.Next(0, x * y - 1)] = 1; } }
                        else if (result == 2) { bots[i].health += 30; sprites[point.Y * x + point.X] = 0; if (rand.Next(0, 4) == 2) { sprites[rand.Next(0, x * y - 1)] = 2; } }
                    }
                    
                }
                else if(cmd<32)
                {
                    bots[i].rangle = cmd % 8;
                    bots[i].addPointer(1);
                }
                else
                {
                    bots[i].addPointer(cmd);
                }
                Point point1 = bots[i].location;
                if (bots[i].Damage(10) == 1) { sprites[point1.Y * x + point1.X] = 1; }
                Thread.Sleep(1);
                draw();
                if (!isEnd) { i--; }
            }
            livetime++;
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
