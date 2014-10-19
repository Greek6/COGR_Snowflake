using Microsoft.Xna.Framework;

namespace ComputerGraphics.Components
{
    public class Camera
    {
        public Matrix World { get; private set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public Camera()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.World = Matrix.CreateTranslation(0, 0, 0);
            // Matrx.CreateLookAt
            // P1: The position of the camera.
            // P2: The target towards which the camera is pointing.
            // P3: The direction that is "up" from the camera's point of view.
            this.View = Matrix.CreateLookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.Up);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f); // calculate aspect ratio
        }

    }
}
