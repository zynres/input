using Silk.NET.GLFW;

namespace SweetLib.Devices;

public struct Time
{
    public float Current;
    public float Delta;

    internal float _last;

    internal void Update()
    {
        Current = (float)GraphicContext.Glfw.GetTime();
        Delta = Current - _last;
        _last = Current;
    }
}