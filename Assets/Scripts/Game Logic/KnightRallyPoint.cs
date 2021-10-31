using UnityEngine;

public sealed class KnightRallyPoint
{
    private readonly Transform rallyPoint = null;
    private readonly RallyPointSettings pointSettings = null;

    private const float startOffsetValue = 0.0f, inventor = -1.0f;
    private const int startCount = 0;

    public KnightRallyPoint(Transform rallyPoint, RallyPointSettings pointSettings)
    {
        this.rallyPoint = rallyPoint;
        this.pointSettings = pointSettings;
    }

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
                    horizontalOffset *= inventor;
                }

                horizontalOffset += pointSettings.HorizontalInterval;

                isOnRight = false;
            }
            else
            {
                horizontalOffset *= inventor;
                isOnRight = true;
            }

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
}