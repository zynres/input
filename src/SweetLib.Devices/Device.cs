// Copyright © 2026 Zynres.

using System.Runtime.InteropServices;
using Silk.NET.GLFW;
using SweetLib.Intents;

namespace SweetLib.Devices;

public unsafe struct Device
{
    public Window Window;
    public Mouse Mouse;
    public Time Time;

    private WindowHandle* window;

    public GraphicContext Init()
    {
        Window = new Window();
        GraphicContext context = Window.Init(1060, 640);

        Mouse = new Mouse();
        Mouse.Init(context.Glfw, context.Window, in Window.Size);

        Time = new Time();
     
        this.window = context.Window;

        return context;
    }

    public void Update(Glfw glfw)
    {
        Time.Update(glfw);
        
        Window.UpdateWindowSize(glfw, window);

        Mouse.WrapCursor(glfw, window, in Window.Size);
    }
}