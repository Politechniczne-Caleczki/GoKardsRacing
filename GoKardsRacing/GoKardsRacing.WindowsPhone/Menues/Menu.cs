using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKardsRacing.Menues
{
    public partial class Menu : DrawableGameComponent
    {
        public override void Update(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count > 0)
            {
                if (!tap)
                {
                    tap = true;
                    foreach (Button b in buttonList)
                        b.Tap(touches[0].Position);
                }
            }
            else tap = false;

            base.Update(gameTime);
        }
    }
}
