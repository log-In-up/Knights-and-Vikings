using UnityEngine;

public class KnightAwaitState : IKnightState
{
    private readonly KnightBehaviour knightBehaviour = null;
    private float initializeTime = 0;

    public KnightAwaitState(KnightBehaviour knightBehaviour)
    {
        this.knightBehaviour = knightBehaviour;
    }

    public void Close()
    {

    }

    public void Initialize()
    {
        initializeTime = Time.time;
    }

    public void Update()
    {
        if(Time.time >= initializeTime + 5.0f)
        {
            knightBehaviour.State = KnightState.Return;
        }
    }
}
