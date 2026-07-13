using Silk.NET.GLFW;

namespace SweetLib.Intents;

public unsafe static class Input
{
    private static WindowHandle* window;
    private static Glfw glfw;
    
    public static void Init(WindowHandle* window, Glfw glfw)
    {
        Input.window = window;
        Input.glfw = glfw;
    }

    public static bool IsDown(Keys key)
    {
        return InputAction.Press == (InputAction)glfw.GetKey(window, key);
    }

    public static bool IsHeld(Keys key)
    {
        var state = (InputAction)glfw.GetKey(window, key);

        return InputAction.Press == state || InputAction.Repeat == state;
    }

    public static bool IsRelease(Keys key)
    {
        return InputAction.Release == (InputAction)glfw.GetKey(window, key);
    }

    public static bool IsDown(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Press == state;
    }

    public static bool IsHeld(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Press == state || InputAction.Repeat == state;
    }

    public static bool IsRelease(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Release == state;
    }
}