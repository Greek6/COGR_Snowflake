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
        private float temperature = -2;   // TODO randomize
        private static Random random = new Random();   // do not change to non staic member!

        public Cloud()
        {
            Cloud.random = new Random();
            this.snowflakes = new List<Snowflake>();

            for (int i = 0, numSnowflakes = 10000; i < numSnowflakes; i++)
            {
                Vector3 vec;
                vec.X = (float)Cloud.random.NextDouble() * 25 - 12.5f;
                vec.Y = (float)Cloud.random.NextDouble() * 25        ;
                vec.Z = (float)Cloud.random.NextDouble() * 25 - 12.5f;
                this.snowflakes.Add(new Snowflake(vec, temperature));
            }

            this.snowflakes = this.snowflakes.OrderBy(x => x.Position.Z).ToList();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Snowflake snowflake in this.snowflakes)
            {
                snowflake.Update(gameTime);
            }
            this.snowflakes = this.snowflakes.OrderBy(x => x.Position.Z).ToList();
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
