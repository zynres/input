// auto-generated

using System.Runtime.InteropServices;
using SweetLib.Intents.Actions;
using SweetLib.Intents.Axes;
using Silk.NET.GLFW;

namespace SweetLib.Intents.Generated;

/// <summary>
///     this code was automatically generated from the .intents sourse file
/// </summary>
public unsafe class EditorCameraIntents : IDisposable
{
    public static ActionState* MoveState { get; private set; }
    public static ActionState* Sprint { get; private set; }

    public static AxisState* MoveForward { get; private set; }
    public static AxisState* MoveRight { get; private set; }
    public static AxisState* MoveUp { get; private set; }

    internal static void Build(ref IntentBuilder builder)
    {
        // Acion states
        MoveState = (ActionState*)NativeMemory.Alloc((nuint)sizeof(ActionState));
        *MoveState = new ActionState(1); // value changing from .intents file

        MoveState->Clauses.Add(new Clause(1)); // 0

        builder.Bind(MouseButton.Right, MoveState, 0);

        Sprint = (ActionState*)NativeMemory.Alloc((nuint)sizeof(ActionState));
        *Sprint = new ActionState(1); // value changing from .intents file

        Sprint->Clauses.Add(new Clause(1)); // 0

        builder.Bind(Keys.ShiftLeft, Sprint, 0);

        // Axis states
        MoveForward = (AxisState*)NativeMemory.Alloc((nuint)sizeof(AxisState));
        *MoveForward = new AxisState();

        builder.Bind(Keys.W, MoveForward, true);
        builder.Bind(Keys.S, MoveForward, false);

        MoveRight = (AxisState*)NativeMemory.Alloc((nuint)sizeof(AxisState));
        *MoveRight = new AxisState();

        builder.Bind(Keys.D, MoveRight, true);
        builder.Bind(Keys.A, MoveRight, false);

        MoveUp = (AxisState*)NativeMemory.Alloc((nuint)sizeof(AxisState));
        *MoveUp = new AxisState();

        builder.Bind(Keys.E, MoveUp, true);
        builder.Bind(Keys.Q, MoveUp, false);

        builder.KickBack += KickBack;
    }

    private static void KickBack()
    {
        MoveState->IsRelease = false;
        Sprint->IsRelease = false;
    }

    public void Dispose()
    {
        if (MoveState != null)
        {
            MoveState->Dispose();
            NativeMemory.Free(MoveState);

            MoveState = null;
        }
        
        if (Sprint != null)
        {
            Sprint->Dispose();
            NativeMemory.Free(Sprint);

            Sprint = null;
        }

        if (MoveForward != null)
        {
            NativeMemory.Free(MoveForward);

            MoveForward = null;
        }

        if (MoveRight != null)
        {
            NativeMemory.Free(MoveRight);

            MoveRight = null;
        }

        if (MoveUp != null)
        {
            NativeMemory.Free(MoveUp);

            MoveUp = null;
        }
    }
}