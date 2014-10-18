#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Snowflake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private double angle = 0;   // For rotating, will be deleted later
        private VertexBuffer vertexBuffer;

        private Texture2D texture;

        private BasicEffect basicEffect;
        private Matrix world = Matrix.CreateTranslation(0, 0, 0);
        // Matrx.CreateLookAt
        // P1: The position of the camera.
        // P2: The target towards which the camera is pointing.
        // P3: The direction that is "up" from the camera's point of view.
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.Up);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f); // calculate aspect ratio
 
        public Game1()
            : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Add initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.basicEffect = new BasicEffect(GraphicsDevice);

            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];

            vertices[0] = new VertexPositionNormalTexture(new Vector3(0f, 0f, 0), Vector3.Backward, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(1f, 0f, 0), Vector3.Backward, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(0f,-1f, 0), Vector3.Backward, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(1f,-1f, 0), Vector3.Backward, new Vector2(1, 1));

            this.vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            this.vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);

            this.texture = this.Content.Load<Texture2D>("Images/flake_0");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.angle += 0.01;
            this.view = Matrix.CreateLookAt(new Vector3(5*(float) Math.Sin(this.angle), -2, 5*(float) Math.Cos(angle)), Vector3.Zero, Vector3.Up);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            this.basicEffect.World = world;
            this.basicEffect.View = view;
            this.basicEffect.Projection = projection;
            this.basicEffect.TextureEnabled = true;
            this.basicEffect.Texture = this.texture;

            GraphicsDevice.SetVertexBuffer(this.vertexBuffer);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }

            base.Draw(gameTime);
        }
    }
}
