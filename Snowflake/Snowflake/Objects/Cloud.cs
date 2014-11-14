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
        private int temperature = -1;   // TODO randomize
        private static Random random = new Random();   // do not change to non staic member!

        public Cloud()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            Cloud.random = new Random();
            this.snowflakes = new List<Snowflake>();
           
            for (int i = 0, numSnowflakes = Cloud.random.Next(0, 10000); i < numSnowflakes; i++)
            {
                Vector3 vec;
                vec.X = (float) Cloud.random.NextDouble() * 20f;
                vec.Y = (float) Cloud.random.NextDouble() * 15f;
                vec.Z = (float) Cloud.random.NextDouble() * 15f;
                this.snowflakes.Add(new Snowflake(vec, temperature));
            }

            this.snowflakes = this.snowflakes.OrderBy(x => x.Position.Z).ToList();
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
