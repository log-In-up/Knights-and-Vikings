public sealed class KnightDeadState : IEntityState
{
    #region Parameters
    private readonly KnightBehaviour knightBehaviour = null;
    private readonly BattleCurator curator;
    #endregion

    public KnightDeadState(KnightBehaviour knightBehaviour, BattleCurator curator)
    {
        this.knightBehaviour = knightBehaviour;
        this.curator = curator;
    }

    #region Interface implementation
    public void Act()
    {

    }

    public void Close()
    {
        curator.EntityHandler.AddAliveKnight(knightBehaviour);
    }

    public void Initialize()
    {
        curator.EntityHandler.AddDeadKnight(knightBehaviour);
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