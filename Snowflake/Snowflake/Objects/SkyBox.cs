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

        public SkyBox(float length)
        {
            float half_length = length / 2;
            this.quads = new Quad[6];            
            
            // top & bottom
            this.quads[0] = new Quad(length, new Vector3(0f,       0f    , 0f), Quad.QuadType.STATIC_ORIENTATION_XZ, "images/skyBottom");
            this.quads[1] = new Quad(length, new Vector3(0f,       length, 0f), Quad.QuadType.STATIC_ORIENTATION_XZ, "images/skyTop");

            // left & right
            this.quads[2] = new Quad(length, new Vector3(-half_length, 0f, 0f), Quad.QuadType.STATIC_ORIENTATION_YZ, "images/skyLeft");
            this.quads[3] = new Quad(length, new Vector3(+half_length, 0f, 0f), Quad.QuadType.STATIC_ORIENTATION_YZ, "images/skyRight");

            // front and back
            this.quads[4] = new Quad(length, new Vector3(0f, 0f, +half_length), Quad.QuadType.STATIC_ORIENTATION_XY, "images/skyBack");
            this.quads[5] = new Quad(length, new Vector3(0f, 0f, -half_length), Quad.QuadType.STATIC_ORIENTATION_XY, "images/skyFront");

            for (int i = 0; i < this.quads.Length; ++i)
            {
                this.quads[i].Update();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < quads.Length; ++i)
            {
                this.quads[i].Draw();
            }
        }
    }
}
