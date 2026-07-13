using System.Runtime.InteropServices;
using System.Numerics;
using Silk.NET.OpenGL;
using Silk.NET.GLFW;

namespace SweetLib.Devices;

public unsafe struct Window
{
    public Vector2 Size;

    internal void Init(short width, short height)
    {
        Size = new(width, height);
        
        CreateWindow();
    }

    internal void UpdateWindowSize()
    {
        GraphicContext.Glfw.GetWindowSize(GraphicContext.Window, out int width, out int height);

        Size = new Vector2(width, height);
    }

    private void CreateWindow()
    {
        SetupDisplayBackend();

        var glfw = Glfw.GetApi();

        if (!glfw.Init())
            throw new Exception("Failed to init GLFW");

        glfw.WindowHint(WindowHintInt.ContextVersionMajor, 3);
        glfw.WindowHint(WindowHintInt.ContextVersionMinor, 3);
        glfw.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGL);
        glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);

        WindowHandle* window = glfw.CreateWindow((int)Size.X, (int)Size.Y, "Sweet Engine", null, null);

        if (window == null)
            throw new Exception("Failed to create window");

        glfw.MakeContextCurrent(window);
        glfw.SetWindowOpacity(window, 1f);

        GraphicContext.Init(window, glfw, GL.GetApi(glfw.GetProcAddress));
    }

    private readonly void SetupDisplayBackend()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Running on Windows - no display setup needed");
            return;
        }

        string waylandDisplay = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");
        string x11Display = Environment.GetEnvironmentVariable("DISPLAY");

        if (!string.IsNullOrEmpty(waylandDisplay))
        {
            Console.WriteLine($"Using Wayland display: {waylandDisplay}");
            Environment.SetEnvironmentVariable("XDG_SESSION_TYPE", "wayland");
            Environment.SetEnvironmentVariable("GDK_BACKEND", "wayland");
        }
        else if (!string.IsNullOrEmpty(x11Display))
        {
            Console.WriteLine($"Using X11 display: {x11Display}");
            Environment.SetEnvironmentVariable("XDG_SESSION_TYPE", "x11");
            Environment.SetEnvironmentVariable("GDK_BACKEND", "x11");
        }
        else
        {
            Console.WriteLine("No display found, defaulting to X11");
            Environment.SetEnvironmentVariable("DISPLAY", ":1");
            Environment.SetEnvironmentVariable("XDG_SESSION_TYPE", "x11");
            Environment.SetEnvironmentVariable("GDK_BACKEND", "x11");
        }
    }
}