using SweetLib.Collections.Unsafe.List;
using SweetLib.Intents.Actions;
using SweetLib.Intents.Axes;

namespace SweetLib.Intents;

internal unsafe struct KeyBinding
{
    public UnsafeList<ActionBinding> Actions;
    public UnsafeList<AxisBinding> Axes;

    public void ProcessHeld(in string key)
    {
        for (uint i = 0; i < Actions.Length; i++)
        {
            ref ActionBinding action = ref Actions[i];

            if (!action.Held)
            {
                ref Clause clause = ref action.State->Clauses[action.ClauseIndex];

                action.Held = true;
                action.State->IsRelease = false;

                bool wasActive = clause.IsSatisfied;

                clause.Current++;

                if (!wasActive && clause.IsSatisfied)
                    action.State->SatisfiedClauses++;

                Console.WriteLine();
                Console.WriteLine($"Held Key: {key}");
                Console.WriteLine($"current: {clause.Current}");
                Console.WriteLine($"active: {action.State->SatisfiedClauses}");
            }
        }

        for (uint i = 0; i < Axes.Length; i++)
        {
            ref AxisBinding axis = ref Axes[i];

            if (axis.IsPositive)
                axis.State->Flags |= AxisFlags.PositiveHeld;
            else
                axis.State->Flags |= AxisFlags.NegativeHeld;

            UpdateAxis(axis.State);

            Console.WriteLine();
            Console.WriteLine($"Held Key: {key}");
            Console.WriteLine($"axis: {axis.State->Value}");
        }
    }

    public void ProcessRelease(in string key)
    {
        for (uint i = 0; i < Actions.Length; i++)
        {
            ref ActionBinding action = ref Actions[i];

            if (action.Held)
            {
                ref Clause clause = ref action.State->Clauses[action.ClauseIndex];

                action.Held = false;
                action.State->IsRelease = true;

                bool wasActive = clause.IsSatisfied;

                clause.Current--;

                if (wasActive && !clause.IsSatisfied)
                    action.State->SatisfiedClauses--;

                Console.WriteLine();
                Console.WriteLine($"Release Key: {key}");
                Console.WriteLine($"current: {clause.Current}");
                Console.WriteLine($"active: {action.State->SatisfiedClauses}");
            }
        }

        for (uint i = 0; i < Axes.Length; i++)
        {
            ref AxisBinding axis = ref Axes[i];

            if (axis.IsPositive)
                axis.State->Flags &= ~AxisFlags.PositiveHeld;
            else
                axis.State->Flags &= ~AxisFlags.NegativeHeld;

            UpdateAxis(axis.State);

            Console.WriteLine();
            Console.WriteLine($"Release Key: {key}");
            Console.WriteLine($"axis: {axis.State->Value}");
        }
    }

    private static void UpdateAxis(AxisState* state)
    {
        bool positive = (state->Flags & AxisFlags.PositiveHeld) != 0;
        bool negative = (state->Flags & AxisFlags.NegativeHeld) != 0;

        if (positive == negative)
            state->Value = 0;
        else
            state->Value = positive ? 1 : -1;
    }
}