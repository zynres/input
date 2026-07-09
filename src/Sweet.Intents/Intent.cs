// Copyright © 2026 Zynres.

using Sweet.Intents.Generated;
using Sweet.Intents.Actions;
using Sweet.Intents.Axes;
using Silk.NET.GLFW;

namespace Sweet.Intents;

public unsafe static class Intent
{
    private static IntentBuilder builder;
    private static WindowHandle* window;
    private static Glfw glfw;

    public static void Init(WindowHandle* _window, Glfw _glfw)
    {
        window = _window;
        glfw = _glfw;

        builder = new();

        EditorCameraIntents.Build(ref builder);

        glfw.SetKeyCallback(window, KeyCallback);
        glfw.SetMouseButtonCallback(window, MouseCallback);
    }

    private static void KeyCallback(WindowHandle* window, Keys key, int scanCode, InputAction inputAction, KeyModifiers mods)
    {
        ref readonly KeyBinding binding = ref builder.Keys[(uint)key];
        ProcessBinding(in binding, inputAction, key.ToString());
    }

    private static void MouseCallback(WindowHandle* window, MouseButton key, InputAction inputAction, KeyModifiers mods)
    {
        ref readonly KeyBinding binding = ref builder.Keys[(uint)key];
        ProcessBinding(in binding, inputAction, key.ToString());
    }

    private static void ProcessBinding(in KeyBinding binding, InputAction inputAction, string key)
    {
        switch (inputAction)
        {
            case InputAction.Press:
                binding.ProcessHeld(in key);
                break;
            case InputAction.Release:
                binding.ProcessRelease(in key);
                break;
            default:

                break;
        }
    }

    public static void KickBackInvoke() => builder.KickBack?.Invoke();

    public static ref float GetAxis(AxisState* state)
    {
        return ref state->Value;
    }

    public static bool IsHeld(ActionState* state)
    {
        return state->IsHeld;
    }

    public static bool IsRelease(ActionState* state)
    {
        return true; //actionState->IsRelease;
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

    public static void Dispose()
    {
        builder.Dispose();
    }
}