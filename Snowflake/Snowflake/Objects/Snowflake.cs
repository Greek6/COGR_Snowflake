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

        public Vector3 Position { get; private set; }


        private readonly Quad quad;


        private Vector3 velocity;
        public static Vector3 windForce;
        private const float gravitationalForce = -0.05f;

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
                roh = 0.724f;
            }

            // assumption:          snowflake is spherical
            // volume of sphere:    4/3 * r^3 * pi
            return roh * 4f / 3f * (float)Math.Pow(diameter / 2, 3) * (float)Math.PI * 1000f /*mass in gramm*/;
        }

        public Snowflake(Vector3 position, float snowflakeTemperature)
        {
            float diameter = Snowflake.CalculateDiameter(snowflakeTemperature);
            this.mass      = Snowflake.CalculateMass    (snowflakeTemperature, diameter);
            this.quad = new Quad(diameter);

            this.Position = position;
            this.velocity = new Vector3(0f, -0.1f, 0f);
            this.Initialize();
        }

        private void Initialize()
        {
            this.basicEffect = new BasicEffect(this.graphicsDevice);

            this.basicEffect.World = this.camera.World;
            this.basicEffect.View = this.camera.View;
            this.basicEffect.Projection = this.camera.Projection;
            this.basicEffect.TextureEnabled = true;

            //Texture2D texture = this.contentManager.Load<Texture2D>("Mipmaps/Mipster-flake_0");
            Texture2D texture = this.contentManager.Load<Texture2D>("Images/flake_" + (int)(Snowflake.random.NextDouble() * numTextures));
            this.basicEffect.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            this.basicEffect.View = this.camera.View;
            //Position -= new Vector3(0f, this.mass, 0f); // not correct but a bit randomness
            //this.basicEffect.World = Matrix.CreateTranslation(Position);

            float elapsedSeconds = gameTime.ElapsedGameTime.Milliseconds / 1000f;

            this.velocity.X +=  Snowflake.windForce.X                       / this.mass * elapsedSeconds;
            this.velocity.Y += (Snowflake.windForce.Y + gravitationalForce) / this.mass * elapsedSeconds;
            this.velocity.Z +=  Snowflake.windForce.Z                       / this.mass * elapsedSeconds;            

            Vector3 tmp = this.Position;
            tmp.X += this.velocity.X * elapsedSeconds;
            tmp.Y += this.velocity.Y * elapsedSeconds;
            tmp.Z += this.velocity.Z * elapsedSeconds;

            if (tmp.X < -12.5f)
            {
                tmp.X += 25f;
                this.velocity.X = 0f;
            }
            if (tmp.X > 12.5f)
            {
                tmp.X -= 25f;
                this.velocity.X = 0f;
            }

            if (tmp.Y < 0)
            {
                tmp.Y += 25f;
                this.velocity.Y = -0.1f;
            }
            if (tmp.Y > 25)
            {
                tmp.Y -= 25f;
                this.velocity.Y = -0.1f;
            }

            if (tmp.Z < -12.5f)
            {
                tmp.Z += 25f;
                this.velocity.Z = 0f;
            }
            if (tmp.Z > 12.5f)
            {
                tmp.Z -= 25f;
                this.velocity.Z = 0f;
            }

            this.Position = tmp;
            this.basicEffect.World = Matrix.CreateTranslation(this.Position);
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
