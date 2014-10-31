using System;
using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class Quad
    {
        public enum QuadType
        {
            STATIC_ORIENTATION_XY, STATIC_ORIENTATION_XZ, STATIC_ORIENTATION_YZ, BILLBOARD
        }


        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private Camera camera;
        private BasicEffect basicEffect;


        public float SideLenght { get; private set; }
        //public VertexBuffer VertexBuffer { get; private set; }
        public DynamicVertexBuffer VertexBuffer { get; private set; }
        public Vector3 Position { get; set; }
        private QuadType Type;

        public Quad(float sideLenght, Vector3 position, QuadType quadType, String texturePath) 
        {
            this.SideLenght = sideLenght;
            this.Position = position;
            this.Type = quadType;
            this.VertexBuffer = new DynamicVertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);   

            this.graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
            this.contentManager = ApplicationCore.Singleton.ContentManager;
            this.camera = ApplicationCore.Singleton.Camera;

            this.basicEffect = new BasicEffect(this.graphicsDevice)
            {
                World = this.camera.World,
                View = this.camera.View,
                Projection = this.camera.Projection,
                TextureEnabled = true
            };

            Texture2D texture = this.contentManager.Load<Texture2D>(texturePath);
            this.basicEffect.Texture = texture;
        }

        public void Update()
        {
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];

            float half_length = this.SideLenght / 2;

            Vector3 vec0 = Position;
            Vector3 vec1 = Position;
            Vector3 vec2 = Position;
            Vector3 vec3 = Position;

            switch (this.Type)
            {
                case QuadType.STATIC_ORIENTATION_XY:
                    vec0 += new Vector3(-half_length, +half_length, 0f);
                    vec1 += new Vector3(+half_length, +half_length, 0f);
                    vec2 += new Vector3(-half_length, -half_length, 0f);
                    vec3 += new Vector3(+half_length, -half_length, 0f);
                    break;
                case QuadType.STATIC_ORIENTATION_XZ:
                    vec0 += new Vector3(-half_length, 0f, +half_length);
                    vec1 += new Vector3(+half_length, 0f, +half_length);
                    vec2 += new Vector3(-half_length, 0f, -half_length);
                    vec3 += new Vector3(+half_length, 0f, -half_length);
                    break;
                case QuadType.STATIC_ORIENTATION_YZ:
                    vec0 += new Vector3(0f, +half_length, -half_length);
                    vec1 += new Vector3(0f, +half_length, +half_length);
                    vec2 += new Vector3(0f, -half_length, -half_length);
                    vec3 += new Vector3(0f, -half_length, +half_length);
                    break;
                case QuadType.BILLBOARD:    // TODO
                default:
                    return;
            }

            vertices[0] = new VertexPositionNormalTexture(vec0, Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(vec1, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(vec2, Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(vec3, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer.SetData<VertexPositionNormalTexture>(vertices);
        }

        public void Draw()
        {
            this.graphicsDevice.SetVertexBuffer(this.VertexBuffer);
            foreach (EffectPass pass in this.basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }
        }
    }
}
