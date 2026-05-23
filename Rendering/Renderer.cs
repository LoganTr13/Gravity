using OpenTK.Graphics.OpenGL4;

namespace Gravity.Rendering
{
    internal class Renderer
    {
        private readonly int _shaderProgram;
        private readonly int _vao;
        private readonly int _vertexCount;

        public Renderer (int shaderProgram, int vao, int vertexCount)
        {
            _shaderProgram = shaderProgram;
            _vao = vao;
            _vertexCount = vertexCount;
        }

        public void Draw() 
        {
            GL.UseProgram(_shaderProgram);
            GL.BindVertexArray(_vao);

            GL.DrawArrays(
                PrimitiveType.TriangleFan,
                0,
                _vertexCount);
        }
    }
}
