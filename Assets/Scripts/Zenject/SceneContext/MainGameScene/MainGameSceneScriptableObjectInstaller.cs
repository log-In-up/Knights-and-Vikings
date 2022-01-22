using GameLogic.Settings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = fileName, menuName = menuName)]
public sealed class MainGameSceneScriptableObjectInstaller : ScriptableObjectInstaller<MainGameSceneScriptableObjectInstaller>
{
    #region Editor parameters
    [Header("Instances of Scriptable Objects")]
    [SerializeField] private PlayerSpawnSettings playerSpawnSettings = null;
    [SerializeField] private EnemySpawnSettings enemySpawnSettings = null;
    #endregion

    #region Parameters 
    private const string fileName = "MainGameSceneScriptableObjectInstaller", menuName = "Installers/MainGameSceneScriptableObjectInstaller";
    #endregion

    #region Zenject
    public sealed override void InstallBindings()
    {
        BindPlayerSpawnSettings();
        BindEnemySpawnSettings();
    }

    #endregion

    #region Injection methods
    private void BindPlayerSpawnSettings()
    {
        Container
            .BindInterfacesAndSelfTo<PlayerSpawnSettings>()
            .FromInstance(playerSpawnSettings)
            .AsSingle();
    }

    private void BindEnemySpawnSettings()
    {
        Container
            .BindInterfacesAndSelfTo<EnemySpawnSettings>()
            .FromInstance(enemySpawnSettings)
            .AsSingle();
    }
    #endregion
}