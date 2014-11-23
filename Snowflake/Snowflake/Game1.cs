#region Using Statements
using System;
using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ComputerGraphics.Objects;

#endregion

namespace ComputerGraphics
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;

        private Cloud cloud;
        private SkyBox skyBox;
        private Vector3 windForce;
        private const float keyAdjustment = 0.05f;
 
        public Game1()
            : base()
        {
            this.Content.RootDirectory = "Content";
            GraphicsDeviceManager graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth  = 1600;
            graphicsDeviceManager.PreferredBackBufferHeight =  900;
            graphicsDeviceManager.PreferMultiSampling = true;
            ApplicationCore.Initialization(graphicsDeviceManager, this.Content, new Camera());
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(ApplicationCore.Singleton.GraphicsDevice);
            this.windForce = new Vector3(0f, 0f, 0f);
            Snowflake.windForce = this.windForce;
            this.cloud = new Cloud();
            this.skyBox = new SkyBox(25f);
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
                this.Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    this.windForce.X += Game1.keyAdjustment;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    this.windForce.X -= Game1.keyAdjustment;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    this.windForce.Y += Game1.keyAdjustment;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    this.windForce.Y -= Game1.keyAdjustment;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    this.windForce.Z += Game1.keyAdjustment;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    this.windForce.Z -= Game1.keyAdjustment;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                this.windForce = new Vector3(0f, 0f, 0f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                Snowflake.tornadoMode = !Snowflake.tornadoMode;
                Snowflake.tornadoPosition = new Vector3(0f, 25f, 0f);
            }
            else ;

            Snowflake.windForce = this.windForce;

            ApplicationCore.Singleton.Camera.Update();
            this.cloud.Update(gameTime);
            this.skyBox.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.skyBox.Draw();
            this.cloud.Draw();
            base.Draw(gameTime);            
        }
    }
}
