using UnityEngine;

namespace Entity.Enums
{
    public enum EntitySubtype
    {
        [InspectorName("Shooter")]
        Shooter,
        [InspectorName("Swordsman")]
        Swordsman,
        [InspectorName("Two-Handed Swordsman")]
        TwoHandedSwordsman
    }
}