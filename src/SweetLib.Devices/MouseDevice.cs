using System.Numerics;
using Silk.NET.GLFW;
using SweetLib.Intents;
using SweetLib.Intents.Generated;

namespace SweetLib.Devices;

public unsafe struct MouseDevice
{
    public Vector2 Position;
    public Vector2 Delta;

    internal Vector2 _lastPosition;

    internal void Init(Glfw glfw, WindowHandle* window, in Vector2 Size)
    {
        glfw.SetInputMode(window, CursorStateAttribute.Cursor, CursorModeValue.CursorNormal);
        SetMousePosition(glfw, window, new Vector2(Size.X / 2, Size.Y / 2));
    }

    internal void WrapCursor(Glfw glfw, WindowHandle* window, in Vector2 Size, in Intent intent)
    {
        glfw.GetCursorPos(window, out double x, out double y);

        if (intent.IsHeld(MouseButton.Right) && (x <= 1 || x > Size.X - 5 || y < 0 || y > Size.Y - 1))
        {
            if (x <= 1)
                x = Size.X - 5;
            if (x > Size.X - 5)
                x = 2;

            if (y < 0)
                y = Size.Y - 1;
            if (y > Size.Y - 1)
                y = 0;

            glfw.SetCursorPos(window, x, y);

            _lastPosition = new Vector2((float)x, (float)y);
            Position = _lastPosition;
            Delta = Vector2.Zero;

            return;
        }

        Position = new Vector2((float)x, (float)y);
        Delta = Position - _lastPosition;
        _lastPosition = Position;
    }

    internal void SetMousePosition(Glfw glfw, WindowHandle* window, in Vector2 position)
    {
        glfw.SetCursorPos(window, position.X, position.Y);

        _lastPosition = position;
        Position = position;
        Delta = Vector2.Zero;
    }
}