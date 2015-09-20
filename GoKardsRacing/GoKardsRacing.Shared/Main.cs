using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GoKardsRacing.GameEngine;
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

        private static MainGame mainGame;

        SpriteBatch spriteBatch;


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
                        
            //physic = new Physic(this, new Vector2(0, 0));
            //body = BodyFactory.CreateCircle(physic.World, 5, 1);
            //body.BodyType = BodyType.Dynamic;
            //body.LinearDamping = 0.4f;
            //body.Friction = 0;
            //body.Restitution = 0.8f;      
            
            base.Initialize();
        }


        protected override void LoadContent()
        {          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //  body.Position = new Vector2(480,200);              
            // cokard = Content.Load<Model>("Model/cokart");        

            game = this;
            ApplicationStateMenager.State = ApplicationState.Game;   
            base.LoadContent();
        }
        
        protected override void Update(GameTime gameTime)
        {
        //    body.ApplyLinearImpulse(new Vector2(-speed * (float)Math.Sin(body.Rotation), -speed * (float)Math.Cos(body.Rotation)));
        //    Camera.Position = new Vector3(body.Position.X/10, 2.7f, body.Position.Y/10);


            
            //    body.Rotation = Camera.Rotation.Y;
                 
 
            base.Update(gameTime);
        }        
        protected override void Draw(GameTime gameTime)
        {          
            GraphicsDevice.Clear(Color.CornflowerBlue);
           // Camera.DrawModel(cokard, new Vector3(body.Position.X / 10, 2, body.Position.Y / 10), new Vector3(0,body.Rotation,0), 0.01f);

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


        public static void LoadGame()
        {   
            

            mainGame = new MainGame(game);
            mainGame.Initialize();
            game.Components.Add(mainGame);
            
        }

        public static void LoadMenu()
        {          
            game.Components.Remove(mainGame);
            mainGame = null;
            
        }

    }
}
