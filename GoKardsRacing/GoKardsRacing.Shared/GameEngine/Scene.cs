using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework.Input.Touch;
using System;


namespace GoKardsRacing.GameEngine
{
   public class Scene : DrawableGameComponent
    {
        #region Fields
        public delegate void Del(object sender, System.EventArgs e);
        GameComponentCollection collection;
        SpriteBatch spriteBatch;
        Texture2D background;
        public Rectangle screenParameters;
        static Button b;
        Game game;
        string backgroundAddress;
        GestureSample gesture;
        bool tap;
        #endregion  

        #region Methods
        public Scene(Game game, string texture):
            base(game)
        {
            this.game = game;
            collection = new GameComponentCollection();
            backgroundAddress = texture;
            tap = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (GameComponent component in collection)
                component.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var touchstate = TouchPanel.GetState();
            if (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap)
                    tap = true;
            }

            foreach (GameComponent component in collection)
            {
                component.Update(gameTime);
                foreach (var touch in touchstate)
                {
                    if (((Button)component).Position.Contains(touch.Position) && tap == true)
                    {
                        tap = false;
                        ((Button)component).OnPressed(EventArgs.Empty);
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>(backgroundAddress);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = Game.Services.GetService(
                typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(background,new Rectangle(0,0,game.GraphicsDevice.Viewport.Width ,game.GraphicsDevice.Viewport.Height), Color.White);
            foreach (DrawableGameComponent component in collection)
                component.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddButton(string texture, Rectangle position, Del function)
        {
            b = new Button(game, texture, position);
            b.pressed += new GoKardsRacing.GameEngine.EventHandler(function);
            collection.Add(b);
        }

        public void AddButton(string texture, Rectangle position, Vector2 destination, Del function)
        {
            b = new Button(game, texture, position, destination);
            b.pressed += new GoKardsRacing.GameEngine.EventHandler(function);
            collection.Add(b);
        }
        #endregion
    }
}
