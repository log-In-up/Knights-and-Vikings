public sealed class VikingBehaviour : EntityBehaviour
{
    #region Properties
    public override float HealthPoints
    {
        get => base.HealthPoints;
        set
        {
            base.HealthPoints = value;
        }
    }
    #endregion
}
