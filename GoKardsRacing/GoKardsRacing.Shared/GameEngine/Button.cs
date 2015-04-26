using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing.GameEngine
{
    public class Button
    {
        #region Fields

        private Rectangle location;
        private readonly Texture2D pressTexture;
        private int touchID;
        public event EventHandler pressed;
        #endregion

        #region Methods

        public Button(Texture2D pressTexture, int x, int y)
        {
            this.pressTexture = pressTexture;
            location = new Rectangle(x, y, pressTexture.Width, pressTexture.Height);
        }
        
        public void WasPressed(ref TouchCollection touches)
        { 
            foreach(var touch in touches)
            {
                if (touch.Id == touchID) continue;
                if (touch.State != TouchLocationState.Pressed) continue;
                if(location.Contains(touch.Position))
                {
                    touchID = touch.Id;
               }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pressTexture, location,Color.White);
        }

        #endregion
    }
}
