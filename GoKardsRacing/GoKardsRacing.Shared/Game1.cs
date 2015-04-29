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
        Model cube;
        public static GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
        }



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Camera.cameraMode = CameraMode.Standard;
            Camera.Position = new Vector3(0, 0, 0);
           
            
            Components.Add(new MainMenu(this));
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(ContentManager), this.Content);
            cube = Content.Load<Model>("Model/Cube");  
       
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
                #if WINDOWS_PHONE_APP
                Exit();
                #endif
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Camera.DrawModel(cube, new Vector3(10, 1, 10));
            base.Draw(gameTime);
        }
    }
}
