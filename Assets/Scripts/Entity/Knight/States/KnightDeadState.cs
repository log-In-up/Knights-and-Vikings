public class KnightDeadState : IEntityState
{
    private readonly KnightBehaviour knightBehaviour = null;
    private readonly BattleCurator curator;

    public KnightDeadState(KnightBehaviour knightBehaviour, BattleCurator curator)
    {
        this.knightBehaviour = knightBehaviour;
        this.curator = curator;
    }

    public void Close()
    {
        curator.EntityHandler.AddAliveKnight(knightBehaviour);
    }

    public void Initialize()
    {
        curator.EntityHandler.AddDeadKnight(knightBehaviour);
    }

    public void Update()
    {

    }
}
