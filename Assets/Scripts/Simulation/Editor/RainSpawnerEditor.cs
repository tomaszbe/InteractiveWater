using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(RainSpawner))]
public class RainSpawnerEditor : Editor
{

    private void OnSceneGUI()
    {
        DrawCloud();
    }

    // Draws a bounding rectangle in scene view 
    // representing the spawning area ("cloud").
    private void DrawCloud()
    {
        // Get RainSpawner's center and size.
        RainSpawner rainSpawner = target as RainSpawner;
        Vector3 center = rainSpawner.transform.position;
        float size = rainSpawner.cloudSize;

        // Define rectangle's vertices.
        Vector3[] vertices = {
            center + new Vector3(-size, 0f, -size),
            center + new Vector3(-size, 0f, size),
            center + new Vector3(size, 0f, size),
            center + new Vector3(size, 0f, -size)
        };

        // Draw the rectangle.
        Handles.DrawSolidRectangleWithOutline(vertices, new Color(0, 0, 0, 0), Color.cyan);

        // Draw the scale handle and allow for 
        // cloud resizing.
        Handles.color = Color.blue;
        rainSpawner.cloudSize = Handles.ScaleValueHandle(
            rainSpawner.cloudSize,
            center + new Vector3(size, 0f, 0f),
            Quaternion.identity,
            2f,
            Handles.CubeHandleCap,
            1f
        );
    }
}
