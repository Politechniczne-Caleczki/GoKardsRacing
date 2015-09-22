using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace GoKardsRacing.GameEngine
{
    public class MainGame : DrawableGameComponent
    {
        private Physic physic;
        private WorldModel worldModel;
        private Player player;

        private bool tap = false;

        public MainGame(Game game) : base(game)
        {
            physic = new Physic(game, Vector2.Zero);
            worldModel = new WorldModel(game, physic, new Vector3(27, 0, 34));
            player = new Player(game, new Vector3(480,2, 200), 50, 0.01f, physic);
        }

        protected override void LoadContent()
        {           
            Game.Components.Add(physic);
            Game.Components.Add(worldModel);
            Game.Components.Add(player);
            base.LoadContent();
        }

        public override void Initialize()
        {
            physic.Initialize();
            worldModel.Initialize();
            player.Initialize();

            Camera.cameraMode = Settings.CameraMode;

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(physic);
            Game.Components.Remove(worldModel);
            Game.Components.Remove(player);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            TouchInput();
            base.Update(gameTime);
        }

        private void TouchInput()
        {
            TouchCollection touches = TouchPanel.GetState();
            if (touches.Count > 0)
            {
                if (!tap)
                {
                    tap = true;
                    Camera.cameraMode = Camera.cameraMode == CameraMode.Double ? CameraMode.Standard : CameraMode.Double;
                }
            }else tap = false;


            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape) || keyState.IsKeyDown(Keys.Back))
            {
                ApplicationStateMenager.State = ApplicationState.Menu;
            }            
        }
    }
}
