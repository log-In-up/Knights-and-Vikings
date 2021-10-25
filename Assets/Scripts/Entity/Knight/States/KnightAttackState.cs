using UnityEngine;

public class KnightAttackState : IEntityState
{
    private readonly KnightBehaviour knightBehaviour = null;

    public KnightAttackState(KnightBehaviour knightBehaviour)
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