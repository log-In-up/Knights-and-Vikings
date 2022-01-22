using GameLogic;
using GameLogic.Mechanics;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public sealed class MainGameSceneMonoInstaller : MonoInstaller
{
    #region Editor parameters
    [Header("Instances from the scene")]
    [SerializeField] private EntityHandler entityHandler = null;
    [SerializeField] private BattleCurator battleCurator = null;
    [SerializeField] private PlayerBase playerBase = null;
    [SerializeField] private KnightRallyPoint knightRallyPoint = null;
    #endregion

    #region Zenject
    public sealed override void InstallBindings()
    {
        BindEntityHandler();
        BindBattleCurator();
        BindPlayerBase();
        BindKnightRallyPoint();
    }
    #endregion

    #region Injection methods
    private void BindKnightRallyPoint()
    {
        Container
            .Bind<KnightRallyPoint>()
            .FromInstance(knightRallyPoint)
            .AsSingle();
    }

    private void BindEntityHandler()
    {
        Container
            .Bind<EntityHandler>()
            .FromInstance(entityHandler)
            .AsSingle();
    }

    private void BindPlayerBase()
    {
        Container
            .Bind<PlayerBase>()
            .FromInstance(playerBase)
            .AsSingle();
    }

    private void BindBattleCurator()
    {        
        Container
            .Bind<BattleCurator>()
            .FromInstance(battleCurator)
            .AsSingle();
    }
    #endregion
}