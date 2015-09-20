using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace GoKardsRacing
{
    class MainGame : DrawableGameComponent
    {
        private Physic physic;
        private WorldModel worldModel;


        private bool tap = false;

        public MainGame(Game game) : base(game)
        {
            physic = new Physic(game, Vector2.Zero);
            worldModel = new WorldModel(game, physic, new Vector3(27, 0, 34));
        }

        protected override void LoadContent()
        {           
            Game.Components.Add(physic);
            Game.Components.Add(worldModel);        
            base.LoadContent();
        }

        public override void Initialize()
        {
            physic.Initialize();
            worldModel.Initialize();

            Camera.cameraMode = Settings.CameraMode;

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            Game.Components.Remove(physic);
            Game.Components.Remove(worldModel);

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
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
