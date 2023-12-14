using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Transform player1;
  [SerializeField] private Transform player2;
  [SerializeField] private Vector2 minBounds;
  [SerializeField] private Vector2 maxBounds;

  private void Update()
  {
    // Calculate the midpoint between the two players
    Vector3 midpoint = (player1.position + player2.position) / 2f;

    // Clamp the midpoint to stay within the specified bounds
    float clampedX = Mathf.Clamp(midpoint.x, minBounds.x, maxBounds.x);
    float clampedY = Mathf.Clamp(midpoint.y, minBounds.y, maxBounds.y);

    // Set the camera position
    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
  }
}
