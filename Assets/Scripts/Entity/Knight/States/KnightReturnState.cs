public sealed class KnightReturnState : IEntityState
{
    #region Parameters
    private readonly KnightBehaviour knightBehaviour = null;
    #endregion

    public KnightReturnState(KnightBehaviour knightBehaviour)
    {
        this.knightBehaviour = knightBehaviour;
    }

    #region Interface implementation
    public void Act()
    {

    }

    public void Close()
    {
        knightBehaviour.agent.ResetPath();
    }

    public void Initialize()
    {
        knightBehaviour.agent.SetDestination(knightBehaviour.rallyPointPosition);
    }

    public void Sense()
    {

    }

    public void Think()
    {

    }
    #endregion

    #region Methods

    #endregion
}