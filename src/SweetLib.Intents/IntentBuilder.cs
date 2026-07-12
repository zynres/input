using SweetLib.Collections.Unsafe.List;
using SweetLib.Intents.Actions;
using SweetLib.Intents.Axes;
using Silk.NET.GLFW;

namespace SweetLib.Intents;

public unsafe struct IntentBuilder
{
    internal UnsafeList<KeyBinding> Keys;

    internal Action KickBack;

    public IntentBuilder()
    {
        Keys = new UnsafeList<KeyBinding>(348);

        for (uint i = 0; i < Keys.Capacity; i++)
        {
            Keys.Add(new()
            {
                Actions = new(),
                Axes = new()
            });
        }

        for (uint i = 0; i < (uint)MouseButton.Button8; i++)
        {
            Keys[i] = new()
            {
                Actions = new(),
                Axes = new()
            };
        }
    }

    internal void Bind(Keys key, ActionState* state, byte index)
    {
        Keys[(uint)key].Actions.Add(new(state, index));
    }

    internal void Bind(MouseButton key, ActionState* state, byte index)
    {
        Keys[(uint)key].Actions.Add(new(state, index));
    }

    internal void Bind(Keys key, AxisState* state, bool isPositive)
    {
        Keys[(uint)key].Axes.Add(new(state, isPositive));
    }

    internal void Bind(MouseButton key, AxisState* state, bool isPositive)
    {
        Keys[(uint)key].Axes.Add(new(state, isPositive));
    }


    internal void UnBind(Keys key, ActionState* actionState)
    {

    }

    internal void UnBind(MouseButton key, ActionState* actionState)
    {

    }

    internal void UnBind(Keys key, AxisState* state, bool isPositive)
    {
        Keys[(uint)key].Axes.Add(new(state, isPositive));
    }

    internal void UnBind(MouseButton key, AxisState* state, bool isPositive)
    {
        Keys[(uint)key].Axes.Add(new(state, isPositive));
    }

    public void Dispose()
    {
        for (uint i = 0; i < Keys.Length; i++)
        {
            Keys[i].Actions.Dispose();
            Keys[i].Axes.Dispose();
        }

        Keys.Dispose();
    }
}