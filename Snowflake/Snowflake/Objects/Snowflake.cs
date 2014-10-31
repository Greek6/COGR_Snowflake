using System;
using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class Snowflake
    {
        private Quad quad;

        private static Random random = new Random(42 << 1); // nice random seed? :)
        private const int num_textures = 10;

        public Snowflake()
        {
            Vector3 pos = new Vector3((float)(Snowflake.random.NextDouble() * 40 - 20),
               (float)(Snowflake.random.NextDouble() * 20 + 10), -15f);

            // TODO: Rework here
            String texture;
            #region select random texture
            switch ((int)(Snowflake.random.NextDouble() * Snowflake.num_textures))
            {
                case 0:
                    texture = "Images/flake_0";
                    break;
                case 1:
                    texture = "Images/flake_1";
                    break;
                case 2:
                    texture = "Images/flake_2";
                    break;
                case 3:
                //texture = "Images/flake_3";  // windows crash using this texture
                //break;
                case 4:
                    texture = "Images/flake_4";
                    break;
                case 5:
                    texture = "Images/flake_5";
                    break;
                case 6:
                //texture = "Images/flake_6";  // windows crash using this texture
                //break;
                case 7:
                    texture = "Images/flake_7";
                    break;
                case 8:
                //texture = "Images/flake_8";  // windows crash using this texture
                //break;
                case 9:
                    texture = "Images/flake_9";
                    break;
                default:
                    throw new Exception("Error in function Snowflake.Initialize()");
            }
            #endregion
            this.quad = new Quad(1f, pos, Quad.QuadType.STATIC_ORIENTATION_XY, texture);
        }

        public void Draw()
        {
            this.quad.Draw();
        }

        public void Update(GameTime gameTime)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 newPos = this.quad.Position - (new Vector3(0f, (float)time, 0f));

            if (newPos.Y < -7.5)
            {
                newPos.Y = 10;
            }

            this.quad.Position = newPos;
            this.quad.Update();
        }
    }
}
