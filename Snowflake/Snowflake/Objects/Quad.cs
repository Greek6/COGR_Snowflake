using System;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class Quad
    {
        public float SideLenght { get; private set; }
        public VertexBuffer VertexBuffer { get; private set; }

        public Quad(float sideLenght)
        {
            this.SideLenght = sideLenght;

            this.Initialize();
        }

        private void Initialize()
        {
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];

            vertices[0] = new VertexPositionNormalTexture(new Vector3(0f             , 0f              , 0f), Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(this.SideLenght, 0f              , 0f), Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(0f             , -this.SideLenght, 0f), Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(this.SideLenght, -this.SideLenght, 0f), Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            this.VertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
        }

    }
}
