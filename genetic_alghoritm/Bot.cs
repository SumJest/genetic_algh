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
        
        public Bot(int health, int[] genom,int location)
        {
            this.health = health;
            Genom = genom;
            this.location = location;
        }


    }
}
