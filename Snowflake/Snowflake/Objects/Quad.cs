using System;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class Quad
    {
        public enum Orientation
        {
            ORIENTATION_XY, ORIENTATION_XZ, ORIENTATION_YZ
        }


        public float SideLenght { get; private set; }
        //public VertexBuffer VertexBuffer { get; private set; }
        public DynamicVertexBuffer VertexBuffer { get; private set; }
        
        private Vector3 Position;
        private Orientation Ori;

        public void setPositon(Vector3 pos)
        {
            this.Position = pos;            
        }

        public Vector3 getPosition()
        {
            return this.Position;
        }


        public Quad(float sideLenght, Vector3 position, Orientation ori)
        {
            this.SideLenght = sideLenght;
            this.Position = position;
            this.Ori = ori;
            this.Initialize();
        }

        private void Initialize()
        {
            this.VertexBuffer = new DynamicVertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);            
        }

        public void Update(GameTime gameTime)
        {
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];

            float half_length = this.SideLenght / 2;

            Vector3 vec0 = Position;
            Vector3 vec1 = Position;
            Vector3 vec2 = Position;
            Vector3 vec3 = Position;

            switch (this.Ori)
            {
                case Orientation.ORIENTATION_XY:
                    vec0 += new Vector3(-half_length, +half_length, 0f);
                    vec1 += new Vector3(+half_length, +half_length, 0f);
                    vec2 += new Vector3(-half_length, -half_length, 0f);
                    vec3 += new Vector3(+half_length, -half_length, 0f);
                    break;
                case Orientation.ORIENTATION_XZ:
                    vec0 += new Vector3(-half_length, 0f, +half_length);
                    vec1 += new Vector3(+half_length, 0f, +half_length);
                    vec2 += new Vector3(-half_length, 0f, -half_length);
                    vec3 += new Vector3(+half_length, 0f, -half_length);
                    break;
                case Orientation.ORIENTATION_YZ:
                    vec0 += new Vector3(0f, +half_length, -half_length);
                    vec1 += new Vector3(0f, +half_length, +half_length);
                    vec2 += new Vector3(0f, -half_length, -half_length);
                    vec3 += new Vector3(0f, -half_length, +half_length);
                    break;
                default:
                    return;
            }

            vertices[0] = new VertexPositionNormalTexture(vec0, Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(vec1, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(vec2, Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(vec3, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
        }
    }
}
