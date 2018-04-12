using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace genetic_alghoritm
{
    class Bot
    {
        public int health;
        public int[] Genom = new int[64];
        public int location;
        public int pointer=0;

        public Point move(int direction,int x, int y)
        {
            int locX = location % x;
            int locY = location / y;
            if (direction<3)
            {
                locX += (direction-1);
                locY--;

            }
            if(direction>4)
            {
                locX += (direction-6);
                locY++;
            }
            if (direction == 3) { locX--; }
            if (direction == 4) { locX++; }
            return new Point(locX,locY);
           
        }
        public Bot(int health, int[] genom,int location)
        {
            this.health = health;
            Genom = genom;
            this.location = location;
        }


    }
}
