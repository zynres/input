namespace SweetLib.Intents;

public struct Clause
{
    public byte Required; //(downed or upped...) keys
    public byte Current;

    public readonly bool IsSatisfied => Current >= Required;
    
    public Clause(byte required)
    {
        Required = required;
    }
}