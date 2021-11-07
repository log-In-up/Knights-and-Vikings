using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class VikingDeadState : IEntityState
{
    private readonly VikingBehaviour vikingBehaviour;
    private readonly BattleCurator curator;

    public VikingDeadState(VikingBehaviour vikingBehaviour, BattleCurator curator)
    {
        this.vikingBehaviour = vikingBehaviour;
        this.curator = curator;
    }

    public void Close()
    {
        curator.EntityHandler.AddAliveViking(vikingBehaviour);
    }

    public void Initialize()
    {
        curator.EntityHandler.AddDeadViking(vikingBehaviour);
    }

    public void Update()
    {

    }
}
