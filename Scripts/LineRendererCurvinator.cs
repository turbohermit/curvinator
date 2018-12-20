using UnityEngine;
using Curvinator;

public class LineRendererCurvinator : MonoBehaviour
{
    //Inspector references
    public LineRenderer lineRenderer;
    public Transform controlPointsContainer;
    public Transform[] controlPoints;

    //Curve configuration
    public int smoothSteps;
    public bool updateEveryFrame;

    public void Update()
    {
        if (updateEveryFrame)
        {
            CreateSplineFromControlPoints();
        }
    }

    public void CreateSplineFromControlPoints()
    {
        if(lineRenderer == null)
        {
            Debug.Log("Assign line renderer to update");
            return;
        }

        if (controlPoints.Length < 1)
        {
            Debug.Log("At least 2 control points needed");
            return;
        }

        Vector3[] controlPointPositions = new Vector3[controlPoints.Length];
        for (int i = 0; i < controlPoints.Length; i++)
        {
            controlPointPositions[i] = controlPoints[i].position;
        }

        Vector3[] curvePointPositions = Chaikin.Curve(controlPointPositions, smoothSteps);
        lineRenderer.positionCount = curvePointPositions.Length;
        lineRenderer.SetPositions(curvePointPositions);
    }

    public void GetControlPointsFromContainer()
    {
        if (controlPointsContainer == null || controlPointsContainer.childCount == 0)
        {
            Debug.Log("Control point container doesn't have any children");
            return;
        }

        Transform[] children = new Transform[controlPointsContainer.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = controlPointsContainer.GetChild(i);
        }
        controlPoints = children;
    }
}