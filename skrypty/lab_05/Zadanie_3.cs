using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public List<Vector3> waypoints = new List<Vector3>();
  public float speed = 2.0f;

  private int currentWaypointIndex = 0;
  private bool movingForward = true;

  void Update()
  {
    MovePlatform();
  }

  void MovePlatform()
  {
    if (waypoints.Count == 0)
    {
      Debug.LogError("No waypoints defined for the platform!");
      return;
    }

    Vector3 targetWaypoint = waypoints[currentWaypointIndex];
    float step = speed * Time.deltaTime;

    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, step);

    if (Vector3.Distance(transform.position, targetWaypoint) < 0.01f)
    {
      if (movingForward)
      {
        currentWaypointIndex++;
        if (currentWaypointIndex == waypoints.Count)
        {
          currentWaypointIndex = waypoints.Count - 1;
          movingForward = false;
        }
      }
      else
      {
        currentWaypointIndex--;
        if (currentWaypointIndex < 0)
        {
          currentWaypointIndex = 0;
          movingForward = true;
        }
      }
    }
  }
}
