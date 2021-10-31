using UnityEngine;

[CreateAssetMenu(fileName = fileName, menuName = menuName, order = order)]
public sealed class RallyPointSettings : ScriptableObject
{
    #region Create asset menu constants
    private const string fileName = "Rally Point Settings", menuName = "Game settings/Rally Point Settings";
    private const int order = 1;
    #endregion

    #region Parameters
    [SerializeField, Min(1)] private int countInLine = 5;
    [SerializeField, Min(0.0f)] private float horizontalInterval = 1.0f;
    [SerializeField, Min(0.0f)] private float verticalInterval = 1.0f;
    #endregion

    #region Properties
    public float HorizontalInterval => horizontalInterval;
    public float VerticalInterval => verticalInterval;
    public int CountInLine => countInLine;
    #endregion
}