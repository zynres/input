using System.Numerics;
using Silk.NET.GLFW;

namespace SweetLib.Devices;

public unsafe struct WindowDevice
{
    public Vector2 Size;

    internal void Init(Glfw glfw, WindowHandle* window)
    {
        UpdateWindowSize(glfw, window);
    }

    internal void UpdateWindowSize(Glfw glfw, WindowHandle* window)
    {
        glfw.GetWindowSize(window, out int width, out int height);

        Size = new Vector2(width, height);
    }
}