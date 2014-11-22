using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
using ComputerGraphics.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class SkyBox
    {
        //private Quad[] quads;

        private GraphicsDevice graphicsDevice = ApplicationCore.Singleton.GraphicsDevice;
        private ContentManager contentManager = ApplicationCore.Singleton.ContentManager;
        private Camera camera = ApplicationCore.Singleton.Camera;
        private BasicEffect[] basicEffects;

        private VertexBuffer[] VertexBuffer;

        public SkyBox(float length)
        {
            //this.quads = new Quad[6];
            this.basicEffects = new BasicEffect[6];

            for (int i = 0; i < 6; ++i)
            {
                this.basicEffects[i] = new BasicEffect(this.graphicsDevice);

                this.basicEffects[i].World = this.camera.World;
                this.basicEffects[i].View = this.camera.View;
                this.basicEffects[i].Projection = this.camera.Projection;
                this.basicEffects[i].TextureEnabled = true;

                String textureName = "";
                #region TextureName
                switch (i)
                {
                    case 0:
                        textureName = "skyBottom";
                        break;
                    case 1:
                        textureName = "skyTop";
                        break;
                    case 2:
                        textureName = "skyLeft";
                        break;
                    case 3:
                        textureName = "skyRight";
                        break;
                    case 4:
                        textureName = "skyBack";
                        break;
                    case 5:
                        textureName = "skyFront";
                        break;
                    default:
                        break;
                }
                #endregion
                this.basicEffects[i].Texture = this.contentManager.Load<Texture2D>("Images/" + textureName);
            }


            this.VertexBuffer = new VertexBuffer[6];

            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];
            float halfSideLength = length / 2;


            Vector3 frontTopLeft     = new Vector3(-halfSideLength, +length, -halfSideLength);
            Vector3 frontTopRight    = new Vector3(+halfSideLength, +length, -halfSideLength);

            Vector3 frontBottomLeft  = new Vector3(-halfSideLength, +    0f, -halfSideLength);
            Vector3 frontBottomRight = new Vector3(+halfSideLength, +    0f, -halfSideLength);

            Vector3 backTopLeft      = new Vector3(-halfSideLength, +length, +halfSideLength);
            Vector3 backTopRight     = new Vector3(+halfSideLength, +length, +halfSideLength);

            Vector3 backBottomLeft   = new Vector3(-halfSideLength, +    0f, +halfSideLength);
            Vector3 backBottomRight  = new Vector3(+halfSideLength, +    0f, +halfSideLength);


            // front
            vertices[0] = new VertexPositionNormalTexture(frontTopLeft    , Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(frontTopRight   , Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(frontBottomLeft , Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(frontBottomRight, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[5] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[5].SetData<VertexPositionNormalTexture>(vertices);

            // back
            vertices[0] = new VertexPositionNormalTexture(backTopLeft    , Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(backTopRight   , Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(backBottomLeft , Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(backBottomRight, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[4] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[4].SetData<VertexPositionNormalTexture>(vertices);

            // right
            vertices[0] = new VertexPositionNormalTexture(   frontTopRight, Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(    backTopRight, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(frontBottomRight, Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture( backBottomRight, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[3] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[3].SetData<VertexPositionNormalTexture>(vertices);

            // left
            vertices[0] = new VertexPositionNormalTexture(   frontTopLeft, Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(    backTopLeft, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(frontBottomLeft, Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture( backBottomLeft, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[2] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[2].SetData<VertexPositionNormalTexture>(vertices);

            // top
            vertices[0] = new VertexPositionNormalTexture(frontTopLeft , Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(frontTopRight, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture( backTopLeft , Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture( backTopRight, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[1] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[1].SetData<VertexPositionNormalTexture>(vertices);

            // bottom
            vertices[0] = new VertexPositionNormalTexture(frontBottomLeft , Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(frontBottomRight, Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture( backBottomLeft , Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture( backBottomRight, Vector3.Backward, new Vector2(1, 1));

            this.VertexBuffer[0] = new VertexBuffer(ApplicationCore.Singleton.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.None);
            this.VertexBuffer[0].SetData<VertexPositionNormalTexture>(vertices);
        }

        public void Update()
        {
            for (int i = 0; i < 6; ++i)
            {
                this.basicEffects[i].View = this.camera.View;
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.VertexBuffer.Length; ++i)
            {
                this.graphicsDevice.SetVertexBuffer(this.VertexBuffer[i]);
                foreach (EffectPass pass in this.basicEffects[i].CurrentTechnique.Passes)
                {
                    pass.Apply();
                    this.graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
                }
            }
        }
    }
}
