// Copyright © 2026 Zynres.

using SweetLib.Intents.Generated;
using SweetLib.Intents.Actions;
using SweetLib.Intents.Axes;
using Silk.NET.GLFW;

namespace SweetLib.Intents;

public unsafe struct Intent
{
    private IntentBuilder builder;
    
    public void Init(WindowHandle* window, Glfw glfw)
    {    
        Input.Init(window, glfw);

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

    public readonly ref float GetAxis(AxisState* state)
    {
        return ref state->Value;
    }

    public readonly bool IsHeld(ActionState* state)
    {
        return state->IsHeld;
    }

    public void Dispose()
    {
        builder.Dispose();
    }
}