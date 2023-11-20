using UnityEngine;

public class DoorController : MonoBehaviour
{
  public float openDistance = 2.0f;
  public float openingSpeed = 2.0f;

  private bool isOpen = false;
  private Transform player;
  private Quaternion closedRotation;
  private Vector3 pivotPoint;

  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    closedRotation = transform.rotation;

    pivotPoint = transform.position - transform.right * 0.5f;
  }

  void Update()
  {
    float distanceToPlayer = Vector3.Distance(pivotPoint, player.position);

    if (distanceToPlayer < openDistance)
    {
      OpenDoor();
    }
    else if (isOpen)
    {
      CloseDoor();
    }
  }

  void OpenDoor()
  {
    if (!isOpen)
    {
      Quaternion targetRotation = Quaternion.Euler(0, 90, 0) * closedRotation;

      transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openingSpeed);

      if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
      {
        isOpen = true;
      }
    }
  }

  void CloseDoor()
  {
    transform.rotation = Quaternion.Lerp(transform.rotation, closedRotation, Time.deltaTime * openingSpeed);

    if (Quaternion.Angle(transform.rotation, closedRotation) < 1.0f)
    {
      isOpen = false;
    }
  }
}