using GameLogic.Settings;
using UnityEngine;
using Zenject;

namespace GameLogic.Mechanics
{
    public sealed class KnightRallyPoint : MonoBehaviour
    {
        #region Editor parameters
        [SerializeField] private Transform rallyPoint = null;
        #endregion

        #region Parameters
        private const float startOffsetValue = 0.0f, invertor = -1.0f;
        private const int startCount = 0;

        private PlayerSpawnSettings pointSettings = null;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(PlayerSpawnSettings playerSpawnSettings)
        {
            pointSettings = playerSpawnSettings;
        }
        #endregion

        #region Custom methods
        public Vector3[] GetPointsForPlacement(int count)
        {
            Vector3[] points = new Vector3[count];

            return SetPoints(points);
        }

        private Vector3[] SetPoints(Vector3[] points)
        {
            bool isOnRight = true;
            float verticalOffset = startOffsetValue, horizontalOffset = startOffsetValue;
            int countInLine = startCount;

            for (int index = 0; index < points.Length; index++)
            {
                points[index] = new Vector3(rallyPoint.position.x + horizontalOffset, rallyPoint.position.y, rallyPoint.position.z + verticalOffset);
                countInLine++;

                if (isOnRight)
                {
                    if (startOffsetValue > horizontalOffset)
                    {
                        horizontalOffset *= invertor;
                    }
                    horizontalOffset += pointSettings.HorizontalInterval;
                }
                else
                {
                    horizontalOffset *= invertor;
                }

                isOnRight = !isOnRight;

                if (countInLine >= pointSettings.CountInLine)
                {
                    verticalOffset += pointSettings.VerticalInterval;

                    horizontalOffset = startOffsetValue;
                    countInLine = startCount;

                    isOnRight = true;
                }
            }

            return points;
        }
        #endregion
    }
}