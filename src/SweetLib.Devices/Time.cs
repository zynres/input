using Silk.NET.GLFW;

namespace SweetLib.Devices;

public struct Time
{
    public float Current;
    public float Delta;

    internal float _last;

    internal void Update(Glfw glfw)
    {
        Current = (float)glfw.GetTime();
        Delta = Current - _last;
        _last = Current;
    }
}