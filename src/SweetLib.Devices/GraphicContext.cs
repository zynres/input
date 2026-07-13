using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace SweetLib.Devices;

public unsafe static class GraphicContext
{
    public static WindowHandle* Window;
    public static Glfw Glfw; 
    public static GL GL;

    public static void Init(WindowHandle* window, Glfw glfw, GL gl)
    {
        Window = window;
        Glfw = glfw;
        GL = gl;
    }
}