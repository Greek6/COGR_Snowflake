#region Using Statements
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using ComputerGraphics.Components;
using ComputerGraphics.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ComputerGraphics.Objects;
using Microsoft.Xna.Framework.Media;
using OpenTK.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using GamePad = Microsoft.Xna.Framework.Input.GamePad;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

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
        private const int applicationWidth = 1600;
        private const int applicationHeight = 900;

        private Microsoft.Xna.Framework.Input.Keys recentlyPressedKey;
        private const float maximumWindforceVolume = 20;

        public Game1()
            : base()
        {
            this.Content.RootDirectory = "Content";
            GraphicsDeviceManager graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = applicationWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = applicationHeight;
            graphicsDeviceManager.PreferMultiSampling = true;
            ApplicationCore.Initialization(graphicsDeviceManager, this.Content, new Camera(applicationWidth, applicationHeight));
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
            Song song = Content.Load<Song>("wind.wav");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0f;

            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
        }

        private void ConsoleInput()
        {
            const String dataFile = "data.file";
            const int sleeptime = 200; // ms
            String str;
            StreamReader sr;

            while (true)
            {
                while (File.Exists(dataFile) == false)
                {
                    Thread.Sleep(sleeptime);
                }

                sr = new StreamReader(dataFile);
                while ((str = sr.ReadLine()) != null)
                {
                    if (str == "wind")
                    {
                        Snowflake.tornadoMode = false;
                    }
                    else if (str == "tornado")
                    {
                        Snowflake.tornadoPosition = new Vector3(0f, 25f, 0f);
                        Snowflake.tornadoMode = true;                    
                    }
                    else if (str.StartsWith("force x: "))
                    {
                        float tmp = float.Parse(str.Substring(str.LastIndexOf(' ') + 1), CultureInfo.InvariantCulture);
                        this.windForce.X = tmp;
                    }
                    else if (str.StartsWith("force y: "))
                    {
                        float tmp = float.Parse(str.Substring(str.LastIndexOf(' ') + 1), CultureInfo.InvariantCulture);
                        this.windForce.Y = tmp;
                    }
                    else if (str.StartsWith("force z: "))
                    {
                        float tmp = float.Parse(str.Substring(str.LastIndexOf(' ') + 1), CultureInfo.InvariantCulture);
                        this.windForce.Z = tmp;
                    }
                    else ;
                }

                sr.Close();

                File.Delete(dataFile);
                Thread.Sleep(sleeptime);
            }
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

            Thread t = new Thread(this.ConsoleInput);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        private void handleKeyboardEvents()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Process.GetCurrentProcess().Kill();

            else if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    this.recentlyPressedKey = Keys.Up;
                    this.windForce.X += Game1.keyAdjustment;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    this.recentlyPressedKey = Keys.Down;
                    this.windForce.X -= Game1.keyAdjustment;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    this.recentlyPressedKey = Keys.Up;
                    this.windForce.Y += Game1.keyAdjustment;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    this.recentlyPressedKey = Keys.Down;
                    this.windForce.Y -= Game1.keyAdjustment;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    this.recentlyPressedKey = Keys.Up;
                    this.windForce.Z += Game1.keyAdjustment;                    
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    this.recentlyPressedKey = Keys.Down;
                    this.windForce.Z -= Game1.keyAdjustment;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                this.windForce = new Vector3(0f, 0f, 0f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                if (this.recentlyPressedKey != Keys.F3)
                {
                    this.recentlyPressedKey = Keys.F3;
                    Snowflake.tornadoPosition = new Vector3(0f, 25f, 0f);
                    Snowflake.tornadoMode = !Snowflake.tornadoMode;                    
                }
            }
            else ;

            if (this.recentlyPressedKey == Keys.F3 && Keyboard.GetState()[Keys.F3] == KeyState.Up)
            {
                this.recentlyPressedKey = Keys.None;
            }
            else ;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.handleKeyboardEvents();
            Snowflake.windForce = this.windForce;

            // adjust wind volume
            if (this.windForce.Length() >= Game1.maximumWindforceVolume)
                MediaPlayer.Volume = 1f;
            else
                MediaPlayer.Volume = windForce.Length() / Game1.maximumWindforceVolume;

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
