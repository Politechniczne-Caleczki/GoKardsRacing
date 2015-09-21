using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework.Input.Touch;


namespace GoKardsRacing
{

    public class Game1 : Game
    {
        private static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        GameStateManager gameStateManager;
        public static GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
        }



        public Game1()
        {
            gameStateManager = new GameStateManager(this);
            graphics = new GraphicsDeviceManager(this);
            scene = new Scene(this, "Menu/Background");
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Camera.cameraMode = CameraMode.Standard;
            Components.Add(gameStateManager.ManageState(scene));
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(ContentManager), this.Content);
            base.LoadContent();
           
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            
            Camera.Update(gameTime);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                gameStateManager.PreviousScene();
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
        }
    }
}
