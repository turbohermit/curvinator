using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineRendererCurvinator))]
public class CurvinatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LineRendererCurvinator targetRenderer = (LineRendererCurvinator)target;

        if (GUILayout.Button("Get Points from Container"))
        {
            targetRenderer.GetControlPointsFromContainer();
        }

        if (targetRenderer.controlPointsContainer == null)
        {
            return;
        }

        if (GUILayout.Button("Update Curve"))
        {
            targetRenderer.CreateSplineFromControlPoints();
        }
    }
}