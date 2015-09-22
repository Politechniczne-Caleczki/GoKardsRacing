using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GoKardsRacing.GameEngine;
using GoKardsRacing.Menues;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace GoKardsRacing
{
    public class Main : Game
    {
        private static GameWindow mainWindow;
        private static GraphicsDeviceManager graphics;
        private static Game game;
        private static Menu menu;

        private static MainGame mainGame;

        private static SpriteBatch spriteBatch;


        public static SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public static GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
        }

        public static GameWindow MainWindow
        {
            get
            {
                return mainWindow;
            }
        }
                
        public Main()
        {                     
            graphics = new GraphicsDeviceManager(this);
            mainWindow = Window;            
            Content.RootDirectory = "Content";
            game = this;
            
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainGame = new MainGame(game);
            mainGame.Initialize();
            mainGame.Enabled = false;
            game.Components.Add(mainGame);

            ApplicationStateMenager.State = ApplicationState.Menu;
            base.LoadContent();
        }
          
        protected override void Draw(GameTime gameTime)
        {          
            GraphicsDevice.Clear(Color.CornflowerBlue);          
            base.Draw(gameTime);
        }


        public static void LoadGame()
        {
            Graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Graphics.GraphicsDevice.BlendState = BlendState.Opaque;
            Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            game.Components.Remove(menu);
            Camera.cameraMode = Settings.CameraMode;
            mainGame.Enabled = true;
        }

        public static void LoadMenu()
        {
            Camera.cameraMode = CameraMode.Standard;
            mainGame.Enabled = false;
            menu = new Menu(game);
            menu.Initialize(); 
            game.Components.Add(menu);

        }

    }
}
