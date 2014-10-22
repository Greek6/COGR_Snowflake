using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
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

        public Snowflake()
        {
            this.graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
            this.contentManager = ApplicationCore.Singleton.ContentManager;
            this.camera = ApplicationCore.Singleton.Camera;

            this.Initialize();
        }

        private void Initialize()
        {
            this.quad = new Quad(1f);
            this.basicEffect = new BasicEffect(this.graphicsDevice);

            this.basicEffect.World = this.camera.World;
            this.basicEffect.View = this.camera.View;
            this.basicEffect.Projection = this.camera.Projection;
            this.basicEffect.TextureEnabled = true;

            // TODO: Rework here
            Texture2D texture = this.contentManager.Load<Texture2D>("Images/flake_0");
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
    }
}
