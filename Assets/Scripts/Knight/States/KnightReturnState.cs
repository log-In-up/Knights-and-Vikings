using UnityEngine;

public class KnightReturnState : IKnightState
{
    private readonly KnightBehaviour knightBehaviour = null;

    public KnightReturnState(KnightBehaviour knightBehaviour)
    {
        this.knightBehaviour = knightBehaviour;
    }

    public void Close()
    {

    }

    public void Initialize()
    {

    }

    public void Update()
    {
        Debug.Log(knightBehaviour.State);
    }
}
