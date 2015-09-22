using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GoKardsRacing.GameEngine;
using GoKardsRacing.Menues;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Diagnostics;

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
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            mainGame = new MainGame(game);
            menu = new Menu(game);

            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            game = this;
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
            game.Components.Clear();
  
            mainGame.Initialize();
            game.Components.Add(mainGame);
        }

        public static void LoadMenu()
        {
            game.Components.Clear();
  
            menu.Initialize();
            game.Components.Add(menu);
        }

    }
}
