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
        public Point location;
        public int pointer=0;
        public int rangle = 0;

        public Point getDirection(int direction,int x, int y)
        {
            Point point = default(Point);
            if(direction<8)
            {
                direction = (direction + rangle)%8;
                switch (direction)
                {
                    case 0:
                        point = new Point(location.X - 1, location.Y - 1);
                        break;
                    case 1:
                        point = new Point(location.X, location.Y - 1);
                        break;
                    case 2:
                        point = new Point(location.X + 1, location.Y - 1);
                        break;
                    case 3:
                        point = new Point(location.X + 1, location.Y);
                        break;
                    case 4:
                        point = new Point(location.X + 1, location.Y+1);
                        break;
                    case 5:
                        point = new Point(location.X, location.Y + 1);
                        break;
                    case 6:
                        point = new Point(location.X-1, location.Y + 1);
                        break;
                    case 7:
                        point = new Point(location.X - 1, location.Y);
                        break;
                }
            }
            return point;
        }
        public int Damage(int damage)
        {
            health -= damage;
            if (health<=0) { return 1; }
            return 0;
        }
        public void addPointer(int add)
        {
            pointer += add;
            pointer %= 64;
        }
        public Bot(int health, int[] genom,Point location)
        {
            this.health = health;
            Genom = genom;
            this.location = location;
        }


    }
}
