using OpenTK.Graphics.OpenGL4;
using Gravity.Rendering.Shaders;

namespace Gravity.Rendering
{
    internal class Renderer
    {
        private readonly int _shaderProgram;
        private readonly int _vao;
        private readonly int _vertexCount;

        public Renderer (int shaderProgram, int vao, int vertexCount)
        {
            _vao = vao;
            _vertexCount = vertexCount;
        }

        public void Draw() 
        {

            GL.BindVertexArray(_vao);

            GL.DrawArrays(
                PrimitiveType.TriangleFan,
                0,
                _vertexCount);
        }
    }
}
