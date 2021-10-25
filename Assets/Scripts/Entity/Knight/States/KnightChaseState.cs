using UnityEngine;

public class KnightChaseState : IEntityState
{
    private readonly KnightBehaviour knightBehaviour = null;

    public KnightChaseState(KnightBehaviour knightBehaviour)
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
