using Gravity.Core;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Gravity
{
    class Program
    {
        static void Main()
        {
            GameWindowSettings gameSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeSettings = new NativeWindowSettings
            {
                Title = "Gravity Test",
                Size = new Vector2i(854, 480)
            };

            Game game = new Game(gameSettings, nativeSettings);
            game.Run();
        }
    }
}