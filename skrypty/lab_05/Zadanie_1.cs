using System.Collections;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
  public Transform startPoint;
  public Transform endPoint;
  public float speed = 2.0f;
  public float pauseTime = 1.0f;

  private bool movingToEnd = true;
  private bool playerOnPlatform = true;

  void Start()
  {
    StartCoroutine(MovePlatform());
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerOnPlatform = true;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerOnPlatform = false;
    }
  }

  IEnumerator MovePlatform()
  {
    while (true)
    {
      if (playerOnPlatform)
      {
        Vector3 targetPosition = movingToEnd ? endPoint.position : startPoint.position;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
          transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
          yield return null;
        }

        yield return new WaitForSeconds(pauseTime);

        movingToEnd = !movingToEnd;
      }
      else
      {
        yield return null;
      }
    }
  }
}
