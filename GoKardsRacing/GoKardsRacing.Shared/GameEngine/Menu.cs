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
    class Menu : DrawableGameComponent
    {
        GameComponentCollection collection;
        SpriteBatch spriteBatch;
        Rectangle screenParameters;

        public Menu(Game game)
            : base(game)
        {
            screenParameters = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.Bounds.Width, GraphicsDevice.PresentationParameters.Bounds.Height);
            collection = new GameComponentCollection();
            Button b = new Button(game, "Model/button", new Rectangle(screenParameters.Width , screenParameters.Height * 8 / 10, screenParameters.Width * 1 / 4, screenParameters.Height * 3 / 20));
            b.pressed += new EventHandler(Play);
            collection.Add(b);
            b = new Button(game, "Model/options", new Rectangle(screenParameters.Width , screenParameters.Height * 6 / 10, screenParameters.Width * 1 / 4, screenParameters.Height * 3 / 20));
            b.pressed += new EventHandler(Options);
            collection.Add(b);
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (GameComponent component in collection)
                component.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var touchstate = TouchPanel.GetState();

            foreach (GameComponent component in collection)
            {
                component.Update(gameTime);
                foreach (var touch in touchstate)
                {
                    if (((Button)component).Location.Contains(touch.Position))
                    {
                        if (((Button)component).clickl != true)
                            ((Button)component).OnPressed(EventArgs.Empty);
                        ((Button)component).clickl = true;
                    }
                    else ((Button)component).clickl = false;
                }
            }
            base.Update(gameTime);
        }

        private static void Play(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("lol");
        }

        private static void Options(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("lel");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = Game.Services.GetService(
            typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Begin();

            foreach (DrawableGameComponent component in collection)
                component.Draw(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
