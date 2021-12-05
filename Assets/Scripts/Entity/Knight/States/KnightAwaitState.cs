using UnityEngine;

public sealed class KnightAwaitState : IEntityState
{
    #region Parameters
    private readonly KnightBehaviour knightBehaviour = null;
    private readonly float awaitingTime;

    private bool canReturn;
    private float initializeTime;
    #endregion

    public KnightAwaitState(KnightBehaviour knightBehaviour, float awaitingTime)
    {
        this.knightBehaviour = knightBehaviour;
        this.awaitingTime = awaitingTime;
    }

    #region Interface implementation
    public void Act()
    {
        Expect();
    }

    public void Close()
    {

    }

    public void Initialize()
    {
        initializeTime = Time.time;
        canReturn = false;
    }

    public void Sense()
    {

    }

    public void Think()
    {
        canReturn = Time.time >= initializeTime + awaitingTime;
    }
    #endregion

    #region Methods
    private void Expect()
    {
        if (canReturn)
        {
            knightBehaviour.State = KnightState.Return;
        }
    }
    #endregion
}