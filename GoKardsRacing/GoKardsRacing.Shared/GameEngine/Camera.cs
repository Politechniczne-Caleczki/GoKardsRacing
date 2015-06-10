using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GoKardsRacing.GameEngine
{
    public static partial class Camera
    {
        #region Delegates
        private delegate void del_DrawModel_01(Model model, Vector3 Position);
        private delegate void del_DrawModel_02(Model model, Vector3 position, Vector3 rotation);
        private delegate void del_DrawModel_03(Model model, Vector3 position, Vector3 rotation, float scale);
        #endregion //-----------------------------------------------------------------//

        #region Fields
        private static Vector3 position, target;

        private static CameraMode _cameraMode;

        private static del_DrawModel_01 drawModel_01;
        private static del_DrawModel_02 drawModel_02;
        private static del_DrawModel_03 drawModel_03;

        private static Viewport leftViewprot, rightViewport, defaultViewport;

        private static Matrix projectionMatrix, halfProjectionMaxtrix;

        private static Vector3 cameraRelativeToHead;
        private static Vector3 cameraRotation;

        private static Vector3 cameraDirection;

        private static int sight;
        #endregion //-----------------------------------------------------------------//

        #region Properties
        public static CameraMode cameraMode
        {
            set
            {
                _cameraMode = value;
                switch(_cameraMode)
                {
                    case CameraMode.Standard:
                        {
                           drawModel_01 = S_DrawModel;
                           drawModel_02 = S_DrawModel;
                           drawModel_03 = S_DrawModel;
                           MainGame.Graphics.GraphicsDevice.Viewport = defaultViewport;
                        }break;
                    case CameraMode.Double:
                        {
                            drawModel_01 = D_DrawModel;
                            drawModel_02 = D_DrawModel;
                            drawModel_03 = D_DrawModel;      
                        }break;
                }
            }
            get
            {                
                return _cameraMode;
            }
        }                      
        
        public static int Sight
        {
            get
            {
                return sight;
            }
            set
            {
                if(value>0 && value < 100000)
                {   
                    sight = value;
                    projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, defaultViewport.AspectRatio, 0.01f, sight);
                    halfProjectionMaxtrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, leftViewprot.AspectRatio, 0.01f, sight);            
                }
            }
        }

        public static Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public static Vector3 Rotation
        {
            get
            {
                return cameraRotation;
            }
        }

        public static Vector3 Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        public static Vector3 PositionRelativeToHead
        {
            get
            {
                return cameraRelativeToHead;
            }
            set
            {
                cameraRelativeToHead = value;
            }
        }

        #endregion//-----------------------------------------------------------------//

        #region Methods      

        public static void DrawModel(Model model, Vector3 position)
        {
            drawModel_01(model, position);
        }

        public static void DrawModel(Model model, Vector3 position, Vector3 rotation)
        {
            drawModel_02(model, position, rotation);
        }

        public static void DrawModel(Model model, Vector3 position, Vector3 rotation, float scale)
        {
            drawModel_03(model, position, rotation, scale);
        }

        static Camera()
        {
            leftViewprot = rightViewport = defaultViewport = MainGame.Graphics.GraphicsDevice.Viewport;
            leftViewprot.Width = rightViewport.Width = defaultViewport.Width / 2;
            rightViewport.X = leftViewprot.Width;
            
            Sight = 100;       
            cameraMode = CameraMode.Standard;
            cameraRelativeToHead = new Vector3(0, 0, 10);
            cameraDirection = Vector3.Up;
            Initialize();
        }

        static partial void Initialize(); 

        private static void S_DrawModel(Model model, Vector3 position)
        {
            Matrix world = Matrix.CreateTranslation(position);
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), projectionMatrix);
        }

        private static void S_DrawModel(Model model, Vector3 position, Vector3 rotation)
        {
            Matrix world = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y)
                * Matrix.CreateRotationZ(rotation.Z) * Matrix.CreateTranslation(position);
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), projectionMatrix);
        }

        private static void S_DrawModel(Model model, Vector3 position, Vector3 rotation, float scale)
        {
            Matrix world = Matrix.CreateRotationX(rotation.X) *
                 Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), projectionMatrix);
        }

        private static void D_DrawModel(Model model, Vector3 position)
        {
            Matrix world = Matrix.CreateTranslation(position);
            MainGame.Graphics.GraphicsDevice.Viewport = leftViewprot;
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
            MainGame.Graphics.GraphicsDevice.Viewport = rightViewport;
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
        }

        private static void D_DrawModel(Model model, Vector3 position, Vector3 rotation)
        {
            Matrix world = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y)
                * Matrix.CreateRotationZ(rotation.Z) * Matrix.CreateTranslation(position);
            MainGame.Graphics.GraphicsDevice.Viewport = leftViewprot;            
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
            MainGame.Graphics.GraphicsDevice.Viewport = rightViewport;
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
        }

        private static void D_DrawModel(Model model, Vector3 position, Vector3 rotation, float scale)
        {
            Matrix world = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y)
                * Matrix.CreateRotationZ(rotation.Z) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);
            MainGame.Graphics.GraphicsDevice.Viewport = leftViewprot;
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
            MainGame.Graphics.GraphicsDevice.Viewport = rightViewport;
            drawModel(model, world, Matrix.CreateLookAt(Position, Target + Position, cameraDirection), halfProjectionMaxtrix);
        }

        private static void drawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

        #endregion//-----------------------------------------------------------------//
    }
}
