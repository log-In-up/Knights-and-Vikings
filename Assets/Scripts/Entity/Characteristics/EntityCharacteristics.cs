using UnityEngine;

public class EntityCharacteristics : ScriptableObject
{
    #region Parameters
#pragma warning disable IDE0044
    [SerializeField, Min(0.0f)] private float healthPoints = 50.0f;
#pragma warning restore IDE0044
    #endregion

    #region Properties
    public float HealthPoints => healthPoints;
    #endregion
}
