
using Gravity.Rendering.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Gravity.Core
{
    internal class Game : GameWindow
    {

        private Shader shader;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
        base(gameWindowSettings, nativeWindowSettings)
        { }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            GL.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);

            int _vao = GL.GenVertexArray();
            int _vbo = GL.GenBuffer();
            shader = new Shader("Default.vert", "Default.frag");

            shader.Make();
            shader.Use();

            GL.BindVertexArray(_vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);

            Console.WriteLine("Window Started!");
        }

        protected override void OnUnload()
        {
            shader.Dispose();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }
    }
}
