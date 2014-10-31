using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputerGraphics.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Objects
{
    public class SkyBox
    {
        private Quad[] quads;
        private Texture2D[] textures;

        public SkyBox(float length)
        {
            this.quads = new Quad[6];
            this.textures = new Texture2D[6];

            float half_length = length/2;
            /*
            this.quads[0] = new Quad(length, new Vector3(0f, 0f    , 0f), Quad.QuadType.STATIC_ORIENTATION_XZ);
            this.quads[1] = new Quad(length, new Vector3(0f, length, 0f), Quad.QuadType.STATIC_ORIENTATION_XZ);

            this.quads[2] = new Quad(length, new Vector3(-half_length, 0f, 0f), Quad.QuadType.STATIC_ORIENTATION_YZ);
            this.quads[3] = new Quad(length, new Vector3(+half_length, 0f, 0f), Quad.QuadType.STATIC_ORIENTATION_YZ);

            this.quads[4] = new Quad(length, new Vector3(0f, 0f, -half_length), Quad.QuadType.STATIC_ORIENTATION_XY);
            this.quads[5] = new Quad(length, new Vector3(0f, 0f, +half_length), Quad.QuadType.STATIC_ORIENTATION_XY);
             */
        }

        public void Draw()
        {
            for (int i = 0; i < quads.Length; ++i)
            {
                
            }
        }
    }
}
