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
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private Camera camera;
        private Quad quad;

        private BasicEffect basicEffect;

        private static Random random = new Random(42 << 1); // nice random seed? :)

        public Snowflake()
        {
            this.graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
            this.contentManager = ApplicationCore.Singleton.ContentManager;
            this.camera = ApplicationCore.Singleton.Camera;

            this.Initialize();
        }

        private void Initialize()
        {
            Vector3 pos = new Vector3((float)(Snowflake.random.NextDouble() * 40 - 20), (float)(Snowflake.random.NextDouble() * 20 + 10), -15f);
            this.quad = new Quad(0.3f, pos, Quad.Orientation.ORIENTATION_XY);
            this.basicEffect = new BasicEffect(this.graphicsDevice);

            this.basicEffect.World = this.camera.World;
            this.basicEffect.View = this.camera.View;
            this.basicEffect.Projection = this.camera.Projection;
            this.basicEffect.TextureEnabled = true;

            // TODO: Rework here
            Texture2D texture = this.contentManager.Load<Texture2D>("Images/flake_1");
            this.basicEffect.Texture = texture;
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

        public void Update(GameTime gameTime)
        {
            double time = gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 newPos = this.quad.getPosition() - (new Vector3(0f, (float)time, 0f));

            if (newPos.Y < -7.5)
            {
                newPos.Y = 10;
            }

            this.quad.setPositon(newPos);
            this.quad.Update(gameTime);
        }
    }
}
