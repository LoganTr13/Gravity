using OpenTK.Graphics.OpenGL4;

namespace Gravity.Rendering.Shaders
{
    public class Shader : IDisposable
    {
        private int _handle;
        private bool disposedValue = false;
        private const string shaderPath = "../../../Rendering/Shaders";
        private readonly string _vertexPath;
        private readonly string _fragmentPath;

        private readonly string _vertexShaderSource;
        private readonly string _fragmentShaderSource;

        public Shader(string vertexFile, string fragmentFile)
        {
            _vertexPath = Path.Combine(shaderPath, vertexFile);
            _fragmentPath = Path.Combine(shaderPath, fragmentFile);

            _vertexShaderSource = File.ReadAllText(_vertexPath);
            _fragmentShaderSource = File.ReadAllText(_fragmentPath);
        }

        private (int vertexShader, int fragmentShader) Create() 
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, _vertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, _fragmentShaderSource);

            return (vertexShader, fragmentShader);

        }

        private bool Compile(int shaderPart)
        {
            GL.CompileShader(shaderPart);

            GL.GetShader(shaderPart, ShaderParameter.CompileStatus, out int success);
            if(success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shaderPart);
                Console.WriteLine(infoLog);
                return false;
            }
            return true;

        }
        public bool Make() 
        {
            (int vertexShader, int fragmentShader) shader = Create();

            if (!Compile(shader.vertexShader) ||
                !Compile(shader.fragmentShader))
            {
                return false;
            }

            _handle = GL.CreateProgram();

            GL.AttachShader(_handle, shader.vertexShader);
            GL.AttachShader(_handle, shader.fragmentShader);

            GL.LinkProgram(_handle);

            GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out int success);

            GL.DetachShader(_handle, shader.vertexShader);
            GL.DetachShader(_handle, shader.fragmentShader);

            GL.DeleteShader(shader.fragmentShader);
            GL.DeleteShader(shader.vertexShader);

            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(_handle);
                GL.DeleteProgram(_handle);
                _handle = 0;
                Console.WriteLine(infoLog);
                return false;
            }
            return true;
        }
        public void Use()
        {
            GL.UseProgram(_handle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(_handle);
                _handle = 0;

                disposedValue = true;
            }
        }

        ~Shader()
        {
            if (disposedValue == false)
            {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
