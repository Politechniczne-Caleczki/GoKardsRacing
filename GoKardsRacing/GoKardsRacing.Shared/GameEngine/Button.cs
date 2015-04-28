using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing.GameEngine
{
    public delegate void EventHandler(object sender, EventArgs e);

    public class Button : DrawableGameComponent
    {
        #region Fields

        private Rectangle position;
        private Vector2 destination;
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private string textureName;
        public event EventHandler pressed;

        #endregion

        #region Properties
        public Rectangle Position
        {
            get { return position; }
        }

        public string Texture
        {
            get { return textureName; }
            set
            {
                textureName = value;
                texture = Game.Content.Load<Texture2D>(textureName);
            }
        }
        #endregion

        #region Methods

        public Button(Game game, string textureName, Rectangle position)
            : base(game)
        {

            this.textureName = textureName;
            this.position = position;
            destination.X = position.X;
            destination.Y = position.Y;
        }
        public Button(Game game, string textureName, Rectangle position, Vector2 destination)
            : base(game)
        {

            this.textureName = textureName;
            this.position = position;
            this.destination.X = destination.X;
            this.destination.Y = destination.Y;
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = Game.Services.GetService(
            typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Draw(texture, position, Color.White);
            base.Draw(gameTime);
        }
        public virtual void OnPressed(EventArgs e)
        {
            if (pressed != null)
                pressed(this, e);
        }

        //
        public void MoveToDestination(float percentageSpeed)
        {
            if (position.X != destination.X)
                position.X = (int)MathHelper.SmoothStep(position.X, destination.X, percentageSpeed);

            if (position.Y != destination.Y)
                position.Y = (int)MathHelper.SmoothStep(position.Y, destination.Y, percentageSpeed);
        }

        public override void Update(GameTime gameTime)
        {
            MoveToDestination(0.15f);
            base.Update(gameTime);
        }

        #endregion
    }
}
