using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing.GameEngine
{
    public delegate void EventHandler(object sender, EventArgs e);

    public class Button: DrawableGameComponent
    {
        #region Fields

        private Rectangle location;
        private Texture2D pressTexture;
        private string textureName;
        public event EventHandler pressed;
        SpriteBatch spriteBatch;
        public bool clickl;
        #endregion

        #region Methods

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }

        public string TextureName
        { get { return textureName; } }

        public Button(Game game,string textureName, Rectangle rect):base(game)
        {
            this.textureName = textureName;
            location = rect;
            clickl = false;
        }

        protected override void LoadContent()
        {
            pressTexture = Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = Game.Services.GetService(
            typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Draw(pressTexture, location,Color.White);
 	        base.Draw(gameTime);
        }
        public virtual void OnPressed(EventArgs e)
        {
            if (pressed != null)
                pressed(this, e);
        }

        public override void Update(GameTime gameTime)
        {
            if ((float)location.X > GraphicsDevice.PresentationParameters.Bounds.Width * 3 / 4)
            location.X -= 5;
            base.Update(gameTime);
        }

        #endregion
    }
}
