using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing.Menues
{
    public delegate void ButtonAction();

    class Button
    {

        private Rectangle rect;
        private Texture2D[] textures;
        private int index;
        public event ButtonAction Action;

        public int Index
        {
            get { return index; }
            set
            {
                if (textures != null)
                    if (value >= 0 && value < textures.Length)
                        index = value;
            }
        }


        public Button(Game game, Rectangle rect, Texture2D[] textures )
        {
            this.rect = rect;
            this.textures = textures;
            if (textures != null)
                index = 0;
            else
                index = -1;
        }

        public void Tap(Vector2 position)
        {
            Tap(new Point((int)position.X, (int)position.Y));
        }

        public void Tap(Point position)
        {
            if (rect.Intersects(new Rectangle(position.X, position.Y, 2, 2)))
            {
                if (textures != null)
                    index = (index + 1) % textures.Length; ;
                Action();
            }
        }

        public void Draw()
        {
            if(index>=-1)
            {
                Main.SpriteBatch.Draw(textures[index], rect, Color.White);
            }
        }
    }
}
