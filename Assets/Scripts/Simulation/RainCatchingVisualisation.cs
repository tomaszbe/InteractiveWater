using UnityEngine;

// Extends MeshVisualisation adding Collision handling.
public class RainCatchingVisualisation : MeshVisualization {
    
    // Get a reference to Sim object so we can access its "AddValueAtPoint" method.
    public HeightFieldSim heightFieldSim;

    protected void OnCollisionEnter(Collision collision)
    {
        // Works only with HeightFieldSim attached.
        if (heightFieldSim == null)
            return;

        // Destroy colliding raindrop.
        Destroy(collision.gameObject);

        // Use method from parent class to get the cell we need 
        // to modify using colliding raindrop's position.
        Point point = GetCellFromPosition(collision.transform.position);

        // Make sure we don't try to modify cells which don't exist.
        if (point.x < 0 || point.y < 0)
            return;

        if (point.x >= heightFieldSim.countX || point.y >= heightFieldSim.countY)
            return;

        // Use method exposed by HeightFieldSim to modify the cell we hit.
        heightFieldSim.AddValueAtPoint(point);
    }
}
