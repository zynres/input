using Sweet.Collections.Unsafe.List;
using Sweet.Intents.Actions;
using Sweet.Intents.Axes;
using Silk.NET.GLFW;

namespace Sweet.Intents;

public unsafe struct IntentBuilder : IDisposable
{
    internal UnsafeList<UnsafeList<Actions.Action>> Keys;

    public System.Action KickBack;

    public IntentBuilder()
    {
        Keys = new UnsafeList<UnsafeList<Actions.Action>>(348);

        for (uint i = 0; i < Keys.Capacity; i++)
        {
            Keys.Add(new UnsafeList<Actions.Action>(2));
        }

        for (uint i = 0; i < (uint)MouseButton.Button8; i++)
        {
            Keys.Add(new UnsafeList<Actions.Action>(2));
        }
    }

    internal void Bind(Keys key, ActionState* actionState, byte index)
    {
        Keys[(uint)key].Add(new Actions.Action(actionState, index));
    }

    internal void Bind(MouseButton key, ActionState* actionState, byte index)
    {
        Keys[(uint)key].Add(new Actions.Action(actionState, index));
    }

    internal void UnBind(Keys key, ActionState* actionState)
    {

    }

    internal void UnBind(MouseButton key, ActionState* actionState)
    {

    }

    public void Dispose()
    {
        for (uint i = 0; i < Keys.Length; i++)
        {
            Keys[i].Dispose();
        }

        Keys.Dispose();
    }
}