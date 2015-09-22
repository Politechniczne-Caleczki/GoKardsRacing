using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKardsRacing.Menues
{
    public partial class Menu : DrawableGameComponent
    {
        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState(); 
            if( mouse.LeftButton == ButtonState.Pressed)
            {
                if (!tap)
                {
                    tap = true;
                    foreach (Button b in buttonList)
                        b.Tap(mouse.Position);
                }
            }
            else tap = false;

            base.Update(gameTime);
        } 
    }
}
