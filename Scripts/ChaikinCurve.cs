namespace Curvinator
{
    using UnityEngine;

    public static class Chaikin
    {

        public static Vector3[] Curve(Vector3[] controlPoints, int passes)
        {
            Vector3[] newPts = new Vector3[(controlPoints.Length - 2) * 2 + 2];
            newPts[0] = controlPoints[0];
            newPts[newPts.Length - 1] = controlPoints[controlPoints.Length - 1];

            int j = 1;
            for (int i = 0; i < controlPoints.Length - 2; i++)
            {
                newPts[j] = controlPoints[i] + (controlPoints[i + 1] - controlPoints[i]) * 0.75f;
                newPts[j + 1] = controlPoints[i + 1] + (controlPoints[i + 2] - controlPoints[i + 1]) * 0.25f;
                j += 2;
            }

            passes--;

            if (passes > 0)
            {
                newPts = Curve(newPts, passes);
            }
            return newPts;
        }

    }

}
