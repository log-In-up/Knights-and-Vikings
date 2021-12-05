public sealed class VikingDeadState : IEntityState
{
    #region Parameters
    private readonly VikingBehaviour vikingBehaviour;
    private readonly BattleCurator curator;
    #endregion

    public VikingDeadState(VikingBehaviour vikingBehaviour, BattleCurator curator)
    {
        this.vikingBehaviour = vikingBehaviour;
        this.curator = curator;
    }

    #region Interface implementation
    public void Act()
    {

    }

    public void Close()
    {
        curator.EntityHandler.AddAliveViking(vikingBehaviour);
    }

    public void Initialize()
    {
        curator.EntityHandler.AddDeadViking(vikingBehaviour);
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
