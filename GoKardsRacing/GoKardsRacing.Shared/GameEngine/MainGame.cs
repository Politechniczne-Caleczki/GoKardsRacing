using GoKardsRacing.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

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
        }

        protected override void LoadContent()
        {
            physic = new Physic(Game, Vector2.Zero);
            physic.Initialize();
            Game.Components.Add(physic);
            
            worldModel = new WorldModel(Game, physic, new Vector3(27, 0, 34));

            player = new Player(Game, new Vector3(480, 2, 200), 50, 0.01f, physic);


            worldModel.Initialize();

            player.Initialize();

            
            Game.Components.Add(worldModel);
  
            Game.Components.Add(player);

            base.LoadContent();
        }

        public override void Initialize()
        {
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


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                ApplicationStateMenager.State = ApplicationState.Menu;
            }
        }
    }
}
