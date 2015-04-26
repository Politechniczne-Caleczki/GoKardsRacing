using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework.Input.Touch;


namespace GoKardsRacing.GameEngine
{
    class Menu: DrawableGameComponent
    {
        Button start;
        Button options;
        public Menu(Game game) : base(game) 
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D startButtonTexture = Game.Content.Load<Texture2D>("Model/button");
            start = new Button(startButtonTexture, Game1.graphics.GraphicsDevice.Viewport.Bounds.Center.X - (startButtonTexture.Width / 2), Game1.graphics.GraphicsDevice.Viewport.Bounds.Center.Y - (startButtonTexture.Height / 2));
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(
            typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Begin();

            start.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
