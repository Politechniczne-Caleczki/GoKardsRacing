using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoKardsRacing
{
    public class GameStateManager
    {
        #region Fields
        GameComponentCollection collection;
        Game game;
        int state;
        Scene scene;
        #endregion

        #region Methods
        public GameStateManager(Game game)
        {
            state = 0;
            this.game = game;
            collection = new GameComponentCollection();
        }

        public Scene ManageState(Scene scene)
        {
            this.scene = scene;
            switch (state)
            {
               case 0:
                    scene = new Scene(game, "Menu/Background");
                    scene.AddButton("Menu/Start", new Rectangle(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height * 4/3, game.GraphicsDevice.Viewport.Width / 4, game.GraphicsDevice.Viewport.Height / 4), new Vector2(game.GraphicsDevice.Viewport.Width / (float)1.38, game.GraphicsDevice.Viewport.Height / (float)1.40), Play);
                    scene.AddButton("Menu/Volume-up", new Rectangle(game.GraphicsDevice.Viewport.Width * 3 / 4, game.GraphicsDevice.Viewport.Height / 3 / 10, game.GraphicsDevice.Viewport.Width / 15, game.GraphicsDevice.Viewport.Width / 15), SoundState);
                    scene.AddButton("Menu/Mobile", new Rectangle(game.GraphicsDevice.Viewport.Width * 9 / 10, game.GraphicsDevice.Viewport.Height / 3 / 10, game.GraphicsDevice.Viewport.Width / 15, game.GraphicsDevice.Viewport.Width / 15), CameraState);
                break;
                default:
                #if WINDOWS_PHONE_APP
                    Exit();
                #endif
                break;
            }
            return scene;
            
        }
        public void NextScene() { state++; }
        public void PreviousScene() { state--; }

        private void Play(object sender, System.EventArgs e)
        {
            NextScene();
            ManageState(scene);     
        }

        private void SoundState(object sender, System.EventArgs e)
        {
            if (((Button)sender).Texture == "Menu/volume-down")
                ((Button)sender).Texture = "Menu/volume-up";
            else
                ((Button)sender).Texture = "Menu/volume-down";
        }

        private void CameraState(object sender, System.EventArgs e)
        {
            if (((Button)sender).Texture == "Menu/Mobile")
            {
                ((Button)sender).Texture = "Menu/Oculars";
                Camera.cameraMode = CameraMode.Double;
            }
            else
            {
                ((Button)sender).Texture = "Menu/Mobile";
                Camera.cameraMode = CameraMode.Standard;
            }
        }
        #endregion
    }
}
