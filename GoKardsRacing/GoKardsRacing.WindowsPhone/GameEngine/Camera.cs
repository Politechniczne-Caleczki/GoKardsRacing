using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System.Diagnostics;

namespace GoKardsRacing.GameEngine
{
    public static partial class Camera
    {
        #region Fields             
        #endregion //-----------------------------------------------------------------//

        #region Methods
        static partial void Initialize()
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Game);
            CrossDeviceMotion.Current.Start(MotionSensorType.Compass, MotionSensorDelay.Game);

            App.Current.Suspending += Current_Suspending;
            App.Current.Resuming += Current_Resuming;
            App.Current.UnhandledException += Current_UnhandledException;

            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;           
        }

        static void Current_SensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            switch (e.SensorType)
            {
                case MotionSensorType.Accelerometer:
                    {                        
                        cameraRotation.X = MathHelper.SmoothStep(cameraRotation.X, (float)((MotionVector)e.Value).Y * MathHelper.PiOver2, 0.08f);
                        cameraRotation.Z = MathHelper.SmoothStep(cameraRotation.Z, (float)((MotionVector)e.Value).Z * MathHelper.PiOver2, 0.08f);
                        Matrix rotation = Matrix.CreateFromYawPitchRoll(cameraRotation.Y, cameraRotation.Z, MainGame.MainWindow.CurrentOrientation == DisplayOrientation.LandscapeLeft ? cameraRotation.X : -cameraRotation.X);

                        cameraDirection = Vector3.Transform(Vector3.Up, rotation);
                        target = Vector3.Transform(Vector3.Forward, rotation);

                    } break;
                case MotionSensorType.Compass:
                    {
                        cameraRotation.Y =  MathHelper.WrapAngle(MathHelper.ToRadians(-(float)((MotionValue)e.Value).Value));
                    }break;
            }
        }

        static void Current_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            CrossDeviceMotion.Current.Stop(MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.Stop(MotionSensorType.Compass);
        }

        static void Current_Resuming(object sender, object e)
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Game);
            CrossDeviceMotion.Current.Start(MotionSensorType.Compass, MotionSensorDelay.Game);
        }

        static void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            CrossDeviceMotion.Current.Stop(MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.Stop(MotionSensorType.Compass);
        }

        #endregion //-----------------------------------------------------------------//
    }
}
