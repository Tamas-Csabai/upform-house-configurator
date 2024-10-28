using UnityEngine;

namespace Upform
{
    public static class Utils
    {

        public static Vector3 FindClosestPositionOnSection(Vector3 startPosition, Vector3 endPosition, Vector3 position)
        {
            Vector3 startToEnd = endPosition - startPosition;
            Vector3 startToPoint = position - startPosition;
            Vector3 positionOnLine = Vector3.Project(startToPoint, startToEnd);

            float clampedMagnitude = Mathf.Clamp(positionOnLine.magnitude, 0f, startToEnd.magnitude);

            return startPosition + clampedMagnitude * positionOnLine.normalized;
        }

        public static Vector3 FindClosestPositionOnLine(Vector3 startPosition, Vector3 direction, Vector3 position)
        {
            Vector3 startToPoint = position - startPosition;
            Vector3 positionOnLine = Vector3.Project(startToPoint, direction);

            return startPosition + positionOnLine;
        }

    }
}
