using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Bobber bobberScript;

    [Header("Fishing Line Attributes")]
    [SerializeField] float maxLineDistance;

    LineRenderer lineRenderer;
    List<Transform> points;

    float lineDistance = 0f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Function that sets up the fishing line points
    public void SetUpLine(Transform firstPoint, Transform secondPoint)
    {
        // Clear the list, then add the two new points
        lineRenderer.positionCount = 2;
        points = new List<Transform>();
        points.Add(firstPoint);
        points.Add(secondPoint);

        // Set the line distance
        lineDistance = Vector3.Distance(firstPoint.position, secondPoint.position);
    }

    private void Update()
    {
        // Draws fishing line constantly between two points
        if (points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }

        // Update the line distance
        if (points != null && points.Count >= 0)
        {
            lineDistance = Vector3.Distance(points[0].position, points[1].position);
        }
        else
        {
            lineDistance = 0f;
        }

        // If the line is too long, stop its velocity
        if (lineDistance >= maxLineDistance)
        {
            bobberScript.SetBobberVelocity(new Vector2(0f, 0f));
        }
    }

    // Function that resets the vertices in the line renderer
    public void ResetLineRenderer()
    {
        lineRenderer.positionCount = 0;
        lineDistance = 0f;
    }
}
