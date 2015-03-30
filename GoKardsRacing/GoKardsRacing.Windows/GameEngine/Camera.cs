using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GoKardsRacing.GameEngine
{
    public static partial class Camera
    {
        #region Fields
        private static Point oldPosition;
        #endregion //-----------------------------------------------------------------//

        #region Methods
        static partial void Initialize()
        {                    
        }

        public static void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();
            Vector2 shift = Vector2.Zero;
            if (oldPosition == Point.Zero) oldPosition = mouseState.Position;

            cameraRotation.Y +=  ((float)(oldPosition.X - mouseState.Position.X)) / 1000f;
            cameraRotation.X -= ((float)(oldPosition.Y - mouseState.Position.Y)) / 1000f;

            if (key.IsKeyDown(Keys.Left))
                cameraRotation.Y += 0.05f;

            if (key.IsKeyDown(Keys.Right))
                cameraRotation.Y -= 0.05f;

            if (key.IsKeyDown(Keys.Up)&& target.Y < 9.9f)
                cameraRotation.X += 0.05f;

            if (key.IsKeyDown(Keys.Down) && target.Y > -9.9f)
                cameraRotation.X -= 0.05f;

            target = Vector3.Transform(cameraRelativeToHead, Matrix.CreateRotationX(cameraRotation.X)* Matrix.CreateRotationY(cameraRotation.Y));
      
            oldPosition = mouseState.Position;
        }
        #endregion //-----------------------------------------------------------------//
    }
}
