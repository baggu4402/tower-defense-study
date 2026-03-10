using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] waypoints;

    private void Awake()
    {
        waypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }

    public Transform GetWaypoint(int index)
    {
        if (index < 0 || index >= waypoints.Length)
            return null;

        return waypoints[index];
    }

    public int WaypointCount()
    {
        return waypoints.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform point = transform.GetChild(i);
            Gizmos.DrawSphere(point.position, 0.15f);

            if (i < transform.childCount - 1)
            {
                Transform nextPoint = transform.GetChild(i + 1);
                Gizmos.DrawLine(point.position, nextPoint.position);
            }
        }
    }
}