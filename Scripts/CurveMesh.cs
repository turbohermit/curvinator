namespace Curvinator
{
    using UnityEngine;

    public static class CurveMesh
    {
        private static Vector2[] defaultUV = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

        public static Mesh CreateSplineMesh(Vector3[] splinePoints, float width)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[splinePoints.Length * 2];

            //Create two vertices perpendicular to the spline point
            for (int i = 0; i < splinePoints.Length; i++)
            {
                if (i * 2 < vertices.Length - 1)
                {
                    Vector3 heading = Vector3.zero;

                    if (i < splinePoints.Length - 1)
                    {
                        heading = splinePoints[i + 1] - splinePoints[i];
                    }
                    else
                    {
                        heading = splinePoints[i] - splinePoints[i - 1];
                    }

                    heading /= heading.magnitude;
                    vertices[i * 2] = splinePoints[i] + Quaternion.Euler(0, -90, 0) * heading * width;
                    vertices[i * 2 + 1] = splinePoints[i] + Quaternion.Euler(0, 90, 0) * heading * width;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = Triangulate(vertices.Length);
            mesh.uv = GenerateUVMap(vertices.Length);
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();

            return mesh;
        }

        public static int[] Triangulate(int vertexCount)
        {
            //Calculate triangles based on created vertices
            int[] triangles = new int[vertexCount * 3];

            for (int i = 0; i < vertexCount - 2; i++)
            {
                //Odd numbered triangles need to be reversed
                if (i % 2 == 0)
                {
                    triangles[i * 3] = i + 1;
                    triangles[i * 3 + 1] = i;
                    triangles[i * 3 + 2] = i + 2;
                }
                else
                {
                    triangles[i * 3] = i;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            triangles[triangles.Length - 1] = vertexCount - 1;
            return triangles;
        }

        public static Vector2[] GenerateUVMap(int vertexCount)
        {
            //Default UV's strectch entire texture over every quad
            Vector2[] newUVs = new Vector2[vertexCount];
            int j = 0;

            for (int i = 0; i < newUVs.Length; i++)
            {
                newUVs[i] = defaultUV[j];
                if (j < 3)
                {
                    j++;
                }
                else
                {
                    j = 0;
                }
            }

            return newUVs;
        }
    }
}