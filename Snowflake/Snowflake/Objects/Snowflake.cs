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
        private static Random random = new Random();    // do not change to non staic member!
        private const int numTextures = 10;
        
        private Vector3 position;
        private Quad quad;

        private float fallRadius;        
        private float mass;
        private float angle;

        private GraphicsDevice graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
        private ContentManager contentManager = ApplicationCore.Singleton.ContentManager;       
        private Camera camera                 = ApplicationCore.Singleton.Camera;
        private BasicEffect basicEffect;


        private static float CalculateDiameter(float temperature)
        {
            // paper section 2.1
            float toReturn = 0.04f;
            if (temperature <= -0.061f)
            {
                toReturn = 0.015f * (float) Math.Pow(-temperature, -0.35f);
            }

            return toReturn*(float) (Snowflake.random.NextDouble() + 0.5) * 10f /*decimeter meter*/;
        }
        
        private static float CalculateMass(float temperature, float diameter)
        {            
            // paper section 2.1
            float roh = 0.170f;
            if (temperature > -1f)
            {
                roh = 0.740f;
            }

            // assumption:          snowflake is spherical
            // volume of sphere:    4/3 * r^3 * pi
            return 4f / 3f * (float)Math.Pow(diameter / 2, 3) * (float)Math.PI;
        }

        public Snowflake(Vector3 position, int snowflakeTemperature)
        {
            float diameter = Snowflake.CalculateDiameter(snowflakeTemperature);
            this.mass      = Snowflake.CalculateMass    (snowflakeTemperature, diameter);
            this.quad = new Quad(diameter);

            this.position = position;
            this.Initialize();
        }

        private void Initialize()
        {
            this.basicEffect = new BasicEffect(this.graphicsDevice);

            this.basicEffect.World = this.camera.World;
            this.basicEffect.View = this.camera.View;
            this.basicEffect.Projection = this.camera.Projection;
            this.basicEffect.TextureEnabled = true;

            // TODO: Rework here
            Texture2D texture = this.contentManager.Load<Texture2D>("Images/flake_" + (int)(Snowflake.random.NextDouble() * numTextures));
            this.basicEffect.Texture = texture;
        }

        public void Update()
        {
            this.basicEffect.View = this.camera.View;
            position -= new Vector3(0f, this.mass, 0f); // not correct but a bit randomness
            angle += 0.1f;
            this.basicEffect.World = /* Matrix.CreateRotationX(angle) */ Matrix.CreateTranslation(position);
        }

        public void Draw()
        {
            this.graphicsDevice.SetVertexBuffer(this.quad.VertexBuffer);

            foreach (EffectPass pass in this.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }
        }
    }
}
