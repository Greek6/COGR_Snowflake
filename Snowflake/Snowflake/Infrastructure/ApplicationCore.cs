using System;
using ComputerGraphics.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ComputerGraphics.Infrastructure
{
    public sealed class ApplicationCore
    {
        private static bool isInitialized = false;

        private static GraphicsDeviceManager graphicsDevice;
        public GraphicsDevice GraphicsDevice 
        {
            get { return graphicsDevice.GraphicsDevice; }
        }

        private static ContentManager contentManager;
        public ContentManager ContentManager
        {
            get { return contentManager; }
        }

        private static Camera camera;

        public Camera Camera
        {
            get { return camera; }
        }

        private static readonly ApplicationCore applicationCore = new ApplicationCore();

        private ApplicationCore()
        {
        }

        public static ApplicationCore Singleton
        {
            get
            {
                if (isInitialized)
                {
                    return applicationCore;
                }
                else
                {
                    throw new Exception("Singleton data not set");
                }
                
            }
        }

        public static void Initialization(GraphicsDeviceManager gd, ContentManager cm, Camera c)
        {
            graphicsDevice = gd;
            contentManager = cm;
            camera = c;
            isInitialized = true;
        }

    }
}
