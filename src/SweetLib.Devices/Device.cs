// Copyright © 2026 Zynres.

using System.Runtime.InteropServices;
using Silk.NET.GLFW;
using SweetLib.Intents;

namespace SweetLib.Devices;

public unsafe struct Device
{
    public WindowDevice Window;
    public MouseDevice Mouse;
    public Time Time;

    private WindowHandle* window;

    public void Init(WindowHandle* window, Glfw glfw)
    {
        this.window = window;

        Window = new WindowDevice();
        Window.Init(glfw, window);

        Mouse = new MouseDevice();
        Mouse.Init(glfw, window, in Window.Size);

        Time = new Time();
    }

    public void Update(Glfw glfw, in Intent intent)
    {
        Time.Update(glfw);
        
        Window.UpdateWindowSize(glfw, window);

        Mouse.WrapCursor(glfw, window, in Window.Size, in intent);
    }
}