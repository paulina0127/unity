using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
  [SerializeField] private GameObject[] waypoints;
  private int currentWaypointIndex = 0;

  [SerializeField] private float speed = 2f;

  private void Update()
  {
    // Switch to next waypoint if distance to current waypoint is small
    if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
    {
      currentWaypointIndex++;
      if (currentWaypointIndex >= waypoints.Length)
      {
        currentWaypointIndex = 0;
      }
    }

    // Move platform
    transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
  }
}