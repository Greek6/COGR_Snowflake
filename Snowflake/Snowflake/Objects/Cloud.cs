using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ComputerGraphics.Objects
{
    public class Cloud
    {
        private List<Snowflake> snowflakes;
        private Random random;

        public Cloud()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            this.random = new Random();
            this.snowflakes = new List<Snowflake>();

            for (int i = 0; i < this.random.Next(0, 100); i++)
            {
                this.snowflakes.Add(new Snowflake(new Vector3(this.random.Next(0, 10), this.random.Next(0, 3), this.random.Next(0, 4))));
            }
        }

        public void Update()
        {
            foreach (Snowflake snowflake in this.snowflakes)
            {
                snowflake.Update();
            }
        }

        public void Draw()
        {
            foreach (Snowflake snowflake in this.snowflakes)
            {
                snowflake.Draw();
            }
        }

    }
}
