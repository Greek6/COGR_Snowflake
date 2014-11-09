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
        private float angle;
        private Vector3 position;

        private BasicEffect basicEffect;

        public Snowflake(Vector3 position)
        {
            this.graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
            this.contentManager = ApplicationCore.Singleton.ContentManager;
            this.camera = ApplicationCore.Singleton.Camera;
            this.position = position;

            this.Initialize();
        }

        private void Initialize()
        {
            this.quad = new Quad(0.5f);
            this.basicEffect = new BasicEffect(this.graphicsDevice);

            this.basicEffect.World = this.camera.World;
            this.basicEffect.View = this.camera.View;
            this.basicEffect.Projection = this.camera.Projection;
            this.basicEffect.TextureEnabled = true;

            // TODO: Rework here
            Texture2D texture = this.contentManager.Load<Texture2D>("Images/flake_0");
            this.basicEffect.Texture = texture;
        }

        public void Update()
        {
            this.basicEffect.View = this.camera.View;
            position -= new Vector3(0, 0.01f, 0);
            angle += 0.1f;
            this.basicEffect.World = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
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
