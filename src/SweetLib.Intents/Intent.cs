// Copyright © 2026 Zynres.

using SweetLib.Intents.Generated;
using SweetLib.Intents.Actions;
using SweetLib.Intents.Axes;
using Silk.NET.GLFW;

namespace SweetLib.Intents;

public unsafe struct Intent
{
    private IntentBuilder builder;
    private WindowHandle* window;
    private Glfw glfw;

    public void Init(WindowHandle* _window, Glfw _glfw)
    {
        window = _window;
        glfw = _glfw;

        builder = new();

        EditorCameraIntents.Build(ref builder);

        glfw.SetKeyCallback(window, KeyCallback);
        glfw.SetMouseButtonCallback(window, MouseCallback);
    }

    private void KeyCallback(WindowHandle* window, Keys key, int scanCode, InputAction inputAction, KeyModifiers mods)
    {
        ref readonly KeyBinding binding = ref builder.Keys[(uint)key];
        ProcessBinding(in binding, inputAction, key.ToString());
    }

    private void MouseCallback(WindowHandle* window, MouseButton key, InputAction inputAction, KeyModifiers mods)
    {
        ref readonly KeyBinding binding = ref builder.Keys[(uint)key];
        ProcessBinding(in binding, inputAction, key.ToString());
    }

    private void ProcessBinding(in KeyBinding binding, InputAction inputAction, string key)
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

    public void KickBackInvoke() => builder.KickBack?.Invoke();

    public static ref float GetAxis(AxisState* state)
    {
        return ref state->Value;
    }

    public bool IsHeld(ActionState* state)
    {
        return state->IsHeld;
    }

    public bool IsDown(Keys key)
    {
        return InputAction.Press == (InputAction)glfw.GetKey(window, key);
    }

    public bool IsHeld(Keys key)
    {
        var state = (InputAction)glfw.GetKey(window, key);

        return InputAction.Press == state || InputAction.Repeat == state;
    }

    public readonly bool IsRelease(Keys key)
    {
        return InputAction.Release == (InputAction)glfw.GetKey(window, key);
    }

    public readonly bool IsDown(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Press == state;
    }

    public readonly bool IsHeld(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Press == state || InputAction.Repeat == state;
    }

    public readonly bool IsRelease(MouseButton button)
    {
        var state = (InputAction)glfw.GetMouseButton(window, (int)button);

        return InputAction.Release == state;
    }

    public void Dispose()
    {
        builder.Dispose();
    }
}