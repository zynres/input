using System.Numerics;
using Silk.NET.GLFW;

namespace Sweet.Devices;

public unsafe struct MouseDevice
{
    public Vector2 Position;
    public Vector2 Delta;

    internal Vector2 LastPosition;

    internal void Init(Glfw glfw, WindowHandle* window, in Vector2 Size)
    {
        glfw.SetInputMode(window, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
        SetMousePosition(glfw, window, new Vector2(Size.X / 2, Size.Y / 2));
    }

    internal void WrapCursor(Glfw glfw, WindowHandle* window, bool isMouseRight, in Vector2 Size)
    {
        glfw.GetCursorPos(window, out double x, out double y);

        if (isMouseRight && (x < 0 || x > Size.X || y < 0 || y > Size.Y))
        {
            if (x < 0)
                x = Size.X;
            if (x > Size.X)
                x = 0;

            if (y < 0)
                y = Size.Y;
            if (y > Size.Y)
                y = 0;

            glfw.SetCursorPos(window, x, y);

            LastPosition = new Vector2((float)x, (float)y);
            Position = LastPosition;
            Delta = Vector2.Zero;

            return;
        }

        Position = new Vector2((float)x, (float)y);
        Delta = Position - LastPosition;
        LastPosition = Position;
    }

    internal void SetMousePosition(Glfw glfw, WindowHandle* window, in Vector2 position)
    {
        glfw.SetCursorPos(window, position.X, position.Y);

        LastPosition = position;
        Position = position;
        Delta = Vector2.Zero;
    }
}