namespace SweetLib.Intents.Axes;

public unsafe struct AxisBinding
{
    public AxisState* State;

    public bool IsPositive;

    public AxisBinding(AxisState* state, bool isPositive)
    {
        State = state;
        IsPositive = isPositive;
    }
}
