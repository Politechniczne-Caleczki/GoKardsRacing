using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.TextureTools;
using FarseerPhysics.Common.Decomposition;
using System.Collections.Generic;

namespace GoKardsRacing
{
    public class MainGame : Game
    {
        private static GameWindow mainWindow;
        private static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model cube;

        Texture2D text;
  

        Texture2D collisionCenter, collisionBorder;

        Body body, bodyCollisionBorde, bodyCollisionCenter;

        Physic physic;

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
                
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            mainWindow = Window;            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            physic = new Physic(this, new Vector2(0, 0));
            body = BodyFactory.CreateCircle(physic.World, 5, 1);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 0.4f;
            body.Friction = 0;
            body.Restitution = 0.8f;
          

            Components.Add(physic);

            Camera.cameraMode = CameraMode.Standard;
            Camera.Position = new Vector3(0, 1.3f, 0);          
            
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);

            collisionBorder = Content.Load<Texture2D>("Collision/tor_border");
            collisionCenter = Content.Load<Texture2D>("Collision/tor_center");

            text = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[] { Color.White };
            text.SetData(colors);

            bodyCollisionBorde = BodyFactory.CreateCompoundPolygon(physic.World, getVerticesList(collisionBorder), 1);
            bodyCollisionBorde.BodyType = BodyType.Static;
            bodyCollisionCenter = BodyFactory.CreateCompoundPolygon(physic.World, getVerticesList(collisionCenter), 1);
            bodyCollisionCenter.BodyType = BodyType.Static;
            bodyCollisionCenter.Position = new Vector2(72, 74);

             body.Position = new Vector2(480,200);  

            collisionCenter = collisionBorder = null;

            cube = Content.Load<Model>("Model/tor");

            base.LoadContent();
        }


        protected override void UnloadContent()
        { 
        }


        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
           // if (mouse.LeftButton == ButtonState.Pressed)
            body.ApplyLinearImpulse(new Vector2(Camera.Target.X, Camera.Target.Z)*10);

  
            Camera.Update(gameTime);
            Camera.Position = new Vector3(body.Position.X/10, 2.6f, body.Position.Y/10);

  

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {          
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Camera.DrawModel(cube, new Vector3(27, 0, 34));

            //spriteBatch.Begin();
            //spriteBatch.Draw(collisionBorder, new Rectangle((int)bodyCollisionBorde.Position.X, (int)bodyCollisionBorde.Position.Y, collisionBorder.Width, collisionBorder.Height)
            //     , null, Color.Red, bodyCollisionBorde.Rotation, Vector2.Zero, SpriteEffects.None, 0f);

            //spriteBatch.Draw(collisionCenter, new Rectangle((int)bodyCollisionCenter.Position.X, (int)bodyCollisionCenter.Position.Y, collisionCenter.Width, collisionCenter.Height)
            //     , null, Color.Red, bodyCollisionCenter.Rotation, Vector2.Zero, SpriteEffects.None, 0f);

            //spriteBatch.Draw(text, new Rectangle((int)body.Position.X, (int)body.Position.Y, 10, 10)
            //    , null, Color.White, body.Rotation, new Vector2(.5f), SpriteEffects.None, 0f);

            //spriteBatch.End();            

            base.Draw(gameTime);
        }

        private List<Vertices> getVerticesList(Texture2D texture)
        {
            uint[] textData = new uint[texture.Width * texture.Height];
            texture.GetData<uint>(textData);

            Vertices verts = TextureConverter.DetectVertices(textData, texture.Width);

            return BayazitDecomposer.ConvexPartition(verts);
        }
    }
}
