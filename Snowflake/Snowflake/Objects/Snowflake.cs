using ComputerGraphics.Components;
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

        public Snowflake(GraphicsDevice graphicsDevice, ContentManager contentManager, Camera camera)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.camera = camera;

            this.Initialize();
        }

        private void Initialize()
        {
            this.quad = new Quad(this.graphicsDevice, 1f);
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
