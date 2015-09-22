using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKardsRacing.Menues
{
    class Menu : DrawableGameComponent
    {
        private Texture2D background;
        List<Button> buttonList;

        public Menu(Game game): base(game)
        {
            buttonList = new List<Button>();
            DrawOrder = 0;
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("Menu/Background");
            Button start = new Button(Game, new Rectangle(Game.GraphicsDevice.Viewport.Width * 3/4, Game.GraphicsDevice.Viewport.Height * 4/3, Game.GraphicsDevice.Viewport.Width / 4, Game.GraphicsDevice.Viewport.Height / 4), new Texture2D[] { Game.Content.Load<Texture2D>("Menu/Start") });
            buttonList.Add(start);
            Game.Components.Add(start);


            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            foreach (Button b in buttonList)
                Game.Components.Remove(b);

            base.UnloadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            Main.SpriteBatch.Begin();
            Main.SpriteBatch.Draw(background, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            Main.SpriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
