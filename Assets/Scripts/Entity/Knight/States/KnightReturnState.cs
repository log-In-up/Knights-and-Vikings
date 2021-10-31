using UnityEngine;

public class KnightReturnState : IEntityState
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
        knightBehaviour.agent.SetDestination(knightBehaviour.rallyPointPosition);
    }

    public void Update()
    {

    }
}
