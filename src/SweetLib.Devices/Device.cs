// Copyright © 2026 Zynres.

namespace SweetLib.Devices;

public unsafe struct Device
{
    public Window Window;
    public Mouse Mouse;
    public Time Time;

    public void Init()
    {
        Window = new Window();
        Window.Init(1060, 640);

        Mouse = new Mouse();
        Mouse.Init(in Window.Size);

        Time = new Time();
    }

    public void Update()
    {
        Time.Update();
        Window.UpdateWindowSize();
        Mouse.WrapCursor(in Window.Size);
    }
}