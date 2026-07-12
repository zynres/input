using SweetLib.Collections.Unsafe.List;

namespace SweetLib.Intents.Actions;

public struct ActionState : IDisposable
{
    public UnsafeList<Clause> Clauses;

    public byte SatisfiedClauses;
    
    public readonly bool IsHeld => SatisfiedClauses == Clauses.Length;
    public bool IsRelease;

    public ActionState(byte groupCapacity)
    {
        Clauses = new UnsafeList<Clause>(groupCapacity);
    }

    public void Dispose()
    {
        Clauses.Dispose();
    }
}