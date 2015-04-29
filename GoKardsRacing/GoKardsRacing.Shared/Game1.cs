using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;


namespace GoKardsRacing
{
    public class Game1 : Game
    {
        static GraphicsDeviceManager graphics;
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
            Camera.cameraMode = CameraMode.Double;
            Camera.Position = new Vector3(0, 0, 0);
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cube = Content.Load<Model>("Model/Terrain2");         
            base.LoadContent();
        }


        protected override void UnloadContent()
        { 
        }


        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Camera.DrawModel(cube, new Vector3(0, -5, 0));

            base.Draw(gameTime);
        }
    }
}
