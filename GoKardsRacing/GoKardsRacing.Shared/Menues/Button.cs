using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GoKardsRacing.Menues
{
    public delegate void ButtonAction();

    class Button : DrawableGameComponent
    {

        private Rectangle rect;
        private Texture2D[] textures;
        private int index;
        public event ButtonAction Action;

        public Button(Game game, Rectangle rect, Texture2D[] textures ): base(game)
        {
            this.rect = rect;
            this.textures = textures;
            if (textures != null)
                index = 0;
            else
                index = -1;
            DrawOrder = 1;
        }

        public void Tap(Vector2 position)
        {
            if (rect.Intersects(new Rectangle((int)position.X, (int)position.Y, 1, 1)))
                Action();
        }

        public void Tap(Point position)
        {
            if (rect.Intersects(new Rectangle(position.X, position.Y, 1, 1)))
                Action();
        }

        public override void Draw(GameTime gameTime)
        {
            if(index>=-1)
            {
                Main.SpriteBatch.Begin();
                Main.SpriteBatch.Draw(textures[index], rect, Color.White);
                Main.SpriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
