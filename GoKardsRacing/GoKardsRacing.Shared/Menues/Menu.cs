using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKardsRacing.Menues
{
    public partial class Menu : DrawableGameComponent
    {
        private Texture2D background;
        private List<Button> buttonList;
        private bool tap;

        public Menu(Game game): base(game)
        {
            buttonList = new List<Button>();
            DrawOrder = 0;
            tap = false;
        }


        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("Menu/Background");
            Button b = new Button(Game, new Rectangle(Game.GraphicsDevice.Viewport.Width * 3/4, Game.GraphicsDevice.Viewport.Height * 3/4, Game.GraphicsDevice.Viewport.Width/5, Game.GraphicsDevice.Viewport.Height /5), new Texture2D[] { Game.Content.Load<Texture2D>("Menu/Start") });
            b.Action+=Start;
            buttonList.Add(b);

            b = new Button(Game, new Rectangle(Game.GraphicsDevice.Viewport.Width * 3 / 4, Game.GraphicsDevice.Viewport.Height * 1 / 10, Game.GraphicsDevice.Viewport.Width / 10, Game.GraphicsDevice.Viewport.Height / 10), new Texture2D[] { Game.Content.Load<Texture2D>("Menu/Mobile"), Game.Content.Load<Texture2D>("Menu/Oculars") });
            b.Action += ChangeCameraMode;
            b.Index = (int)Settings.CameraMode;
            buttonList.Add(b);


            //b = new Button(Game, new Rectangle(Game.GraphicsDevice.Viewport.Width * 3 / 4, Game.GraphicsDevice.Viewport.Height * 3 / 4, Game.GraphicsDevice.Viewport.Width / 5, Game.GraphicsDevice.Viewport.Height / 5), new Texture2D[] { Game.Content.Load<Texture2D>("Menu/Start") });
            //b.Action += Start;
            //buttonList.Add(b);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {

            Main.SpriteBatch.Begin();
            Main.SpriteBatch.Draw(background, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            foreach (Button b in buttonList)
                b.Draw();

            Main.SpriteBatch.End();


            base.Draw(gameTime);
        }

        private void Start()
        {
            ApplicationStateMenager.State = ApplicationState.Game;
        }

        private void ChangeCameraMode()
        {
            Settings.CameraMode = Settings.CameraMode == CameraMode.Double ? CameraMode.Standard : CameraMode.Double;
        }

        
    }
}
