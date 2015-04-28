using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework.Input.Touch;
using System;


namespace GoKardsRacing.GameEngine
{
    class MainMenu : DrawableGameComponent
    {
        #region Fields
        GameComponentCollection collection;
        SpriteBatch spriteBatch;
        Rectangle screenParameters;
        Texture2D background;
        static Button b;
        GestureSample gesture;
        bool tap;
        #endregion

        #region Methods
        public MainMenu(Game game)
            : base(game)
        {
            screenParameters = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.Bounds.Width, GraphicsDevice.PresentationParameters.Bounds.Height);
            TouchPanel.EnabledGestures = GestureType.Tap;

            collection = new GameComponentCollection();
            b = new Button(game, "Menu/Start", PercentageSize(100, 80, 25, 20), PercentagePosition(75, 80));
            b.pressed += new EventHandler(Play);
            collection.Add(b);

            b = new Button(game, "Menu/Volume-up", PercentageSize(92, 8, 7, 10));
            b.pressed += new EventHandler(SoundState);
            collection.Add(b);

            b = new Button(game, "Menu/Mobile", PercentageSize(75, 8, 7, 10));
            b.pressed += new EventHandler(CameraState);
            collection.Add(b);
            tap = false;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (GameComponent component in collection)
                component.Initialize();
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("Menu/Background");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var touchstate = TouchPanel.GetState();
            if (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.Tap)
                    tap = true;
            }
            foreach (GameComponent component in collection)
            {
                component.Update(gameTime);
                foreach (var touch in touchstate)
                {
                    if (((Button)component).Position.Contains(touch.Position) && tap == true)
                    {
                        tap = false;
                        ((Button)component).OnPressed(EventArgs.Empty);
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = Game.Services.GetService(
            typeof(SpriteBatch)) as SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(background, screenParameters, Color.White);
            foreach (DrawableGameComponent component in collection)
                component.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle PercentageSize(int x, int y, int width, int height)
        {
            return new Rectangle(screenParameters.Width * x / 100, screenParameters.Height * y / 100, screenParameters.Width * width / 100, screenParameters.Height * height / 100);
        }

        public Vector2 PercentagePosition(int x, int y)
        {
            return new Vector2(screenParameters.Width * x / 100, screenParameters.Height * y / 100);
        }

        #endregion

        #region ButtonsMethods
        private static void Play(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("lol");
        }

        private static void SoundState(object sender, System.EventArgs e)
        {
            if (((Button)sender).Texture == "Menu/volume-down")
                ((Button)sender).Texture = "Menu/volume-up";
            else
                ((Button)sender).Texture = "Menu/volume-down";
        }

        private static void CameraState(object sender, System.EventArgs e)
        {
            if (((Button)sender).Texture == "Menu/Mobile")
                ((Button)sender).Texture = "Menu/Oculars";
            else
                ((Button)sender).Texture = "Menu/Mobile";
        }
        #endregion
    }
}
