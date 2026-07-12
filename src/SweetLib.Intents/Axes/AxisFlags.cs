namespace SweetLib.Intents.Axes;

[Flags]
public enum AxisFlags : byte
{
    PositiveHeld = 1 << 0,
    NegativeHeld = 1 << 1
}