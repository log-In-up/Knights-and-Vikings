using System.Collections.Generic;
using UnityEngine;

public sealed class EntityHandler
{
    #region Parameters
    private readonly List<KnightBehaviour> aliveKnights = null, deadKnights = null;
    private readonly List<VikingBehaviour> aliveVikings = null, deadVikings = null;

    private readonly KnightRallyPoint rallyPoint = null;
    #endregion

    public EntityHandler(KnightRallyPoint rallyPoint)
    {
        this.rallyPoint = rallyPoint;

        aliveKnights = new List<KnightBehaviour>();
        aliveVikings = new List<VikingBehaviour>();
        deadKnights = new List<KnightBehaviour>();
        deadVikings = new List<VikingBehaviour>();
    }

    #region Custom methods
    internal void AddAliveKnight(KnightBehaviour knight)
    {
        if (deadKnights.Contains(knight))
        {
            deadKnights.Remove(knight);
        }
        aliveKnights.Add(knight);

        SetRallyPoints();
    }

    internal void AddAliveViking(VikingBehaviour viking)
    {
        if (deadVikings.Contains(viking))
        {
            deadVikings.Remove(viking);
        }
        aliveVikings.Add(viking);
    }

    internal void AddDeadKnight(KnightBehaviour knight)
    {
        aliveKnights.Remove(knight);
        deadKnights.Add(knight);

        SetRallyPoints();
    }

    internal void AddDeadViking(VikingBehaviour viking)
    {
        aliveVikings.Remove(viking);
        deadVikings.Add(viking);
    }

    internal int GetAliveVikingsCount() => aliveVikings.Count;

    private void SetRallyPoints()
    {
        int count = aliveKnights.Count;

        Vector3[] positions = rallyPoint.GetPointsForPlacement(count);

        for (int index = 0; index < count; index++)
        {
            aliveKnights[index].rallyPointPosition = positions[index];
        }
    }
    #endregion
}
