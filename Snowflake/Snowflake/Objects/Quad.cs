using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class Quad
    {
        private GraphicsDevice graphicsDevice;

        public float SideLenght { get; private set; }
        public VertexBuffer VertexBuffer { get; private set; }

        public Quad(GraphicsDevice graphicsDevice, float sideLenght)
        {
            this.graphicsDevice = graphicsDevice;
            this.SideLenght = sideLenght;

            this.Initialize();
        }

        private void Initialize()
        {
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];

            vertices[0] = new VertexPositionNormalTexture(new Vector3(0f, 0f, 0), Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(this.SideLenght, 0f, 0), Vector3.Backward, new Vector2(Math.Abs(this.SideLenght), 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(0f, -1f * this.SideLenght, 0), Vector3.Backward, new Vector2(0, Math.Abs(this.SideLenght)));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(this.SideLenght, -1f * this.SideLenght, 0), Vector3.Backward, new Vector2(Math.Abs(this.SideLenght), Math.Abs(this.SideLenght)));

            this.VertexBuffer = new VertexBuffer(this.graphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            this.VertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
        }

    }
}
