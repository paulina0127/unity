using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
  private SpriteRenderer sprite;
  private Animator anim;
  [SerializeField] private GameObject[] waypoints;
  private int currentWaypointIndex = 0;

  [SerializeField] private float patrolSpeed = 1f;
  [SerializeField] private float chaseSpeed = 3f;
  [SerializeField] private float chaseDistance = Mathf.Infinity;

  private Transform player1;
  private Transform player2;

  private void Start()
  {
    sprite = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
    player1 = GameObject.Find("Player1").transform;
    player2 = GameObject.Find("Player2").transform;
  }

  private void Update()
  {
    Transform targetPlayer = GetClosestPlayer();

    if (targetPlayer != null && Vector2.Distance(targetPlayer.position, transform.position) < chaseDistance)
    {
      // Chase the closest player
      ChasePlayer(targetPlayer);
    }
    else
    {
      // Patrol between waypoints
      Patrol();
    }
  }

  private Transform GetClosestPlayer()
  {
    // Return transform of closest player based on the distance
    float distanceToPlayer1 = Vector2.Distance(player1.position, transform.position);
    float distanceToPlayer2 = Vector2.Distance(player2.position, transform.position);

    if (distanceToPlayer1 < distanceToPlayer2)
    {
      return player1;
    }
    else
    {
      return player2;
    }
  }

  private void ChasePlayer(Transform targetPlayer)
  {
    // Flip the sprite to face the player
    FlipTowardsObject(targetPlayer.position.x);

    // Find the first and last waypoints in the array
    Vector2 firstWaypoint = waypoints[0].transform.position;
    Vector2 lastWaypoint = waypoints[waypoints.Length - 1].transform.position;

    // Create a clamped target position
    Vector2 clampedTargetPosition = new Vector2(
        Mathf.Clamp(targetPlayer.position.x, firstWaypoint.x, lastWaypoint.x),
        Mathf.Clamp(transform.position.y, firstWaypoint.y, lastWaypoint.y)
    );

    // Move towards the target
    transform.position = Vector2.MoveTowards(transform.position, clampedTargetPosition, Time.deltaTime * chaseSpeed);
  }



  private void Patrol()
  {
    // Get position of current waypoint
    Vector2 targetPosition = new Vector2(waypoints[currentWaypointIndex].transform.position.x, transform.position.y);

    // Check if the enemy should turn based on the relative position of the waypoint
    FlipTowardsObject(waypoints[currentWaypointIndex].transform.position.x);

    if (Vector2.Distance(targetPosition, transform.position) < .1f)
    {
      currentWaypointIndex++;
      if (currentWaypointIndex >= waypoints.Length)
      {
        // Reached the last waypoint, set the index to 0
        currentWaypointIndex = 0;
      }
    }

    // Move enemy towards waypoint
    transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * patrolSpeed);
  }

  private void FlipTowardsObject(float objectX)
  {
    // Flip the sprite based on the relative position of the object
    if (transform.position.x < objectX)
    {
      sprite.flipX = true; // Face right
    }
    else
    {
      sprite.flipX = false; // Face left
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // Trigger the hit animation on collision with Player
    if (collision.gameObject.CompareTag("Player"))
    {
      anim.SetTrigger("hit");
    }
  }
}
