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
           
            
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            game = this;
            ApplicationStateMenager.State = ApplicationState.Menu;  
 
            base.LoadContent();
        }
        
        protected override void Update(GameTime gameTime)
        { 
            base.Update(gameTime);
        }        
        protected override void Draw(GameTime gameTime)
        {          
            GraphicsDevice.Clear(Color.CornflowerBlue);          
            base.Draw(gameTime);
        }


        public static void LoadGame()
        {   
            if(menu!=null)
            {
                game.Components.Remove(menu);
                menu = null;
            }

            mainGame = new MainGame(game);
            mainGame.Initialize();
            game.Components.Add(mainGame);
        }

        public static void LoadMenu()
        {
            if (mainGame != null)
            {
                game.Components.Remove(mainGame);
                mainGame = null;
            }
            menu = new Menu(game);
            menu.Initialize();
            game.Components.Add(menu);
        }

    }
}
