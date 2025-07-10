using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints2.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.15f)
        {
            getnextWaypoint();
        }
    }

    void getnextWaypoint()
    {
        waypointIndex++;

        // Uncomment the following lines if you want to destroy the enemy when it reaches the last waypoint
        //if (waypointIndex >= Waypoints2.points.Length - 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        if (waypointIndex >= Waypoints2.points.Length)
        {
            waypointIndex = 0;
        }
        target = Waypoints2.points[waypointIndex];
    }
}
