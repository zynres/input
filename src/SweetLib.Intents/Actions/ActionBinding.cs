namespace SweetLib.Intents.Actions;

public unsafe struct ActionBinding
{
    public ActionState* State;
    public byte ClauseIndex;

    public bool Held;

    public ActionBinding(ActionState* state, byte clauseIndex)
    {
        State = state;
        ClauseIndex = clauseIndex;
    }
}