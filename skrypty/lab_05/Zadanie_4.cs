using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
  public float upwardForceMultiplier = 3.0f;
  public float upwardForceDuration = 1.0f;

  private bool isPlayerOnPlate = false;

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      isPlayerOnPlate = true;
      StartCoroutine(ApplyUpwardForce(other.transform));
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      isPlayerOnPlate = false;
    }
  }

  IEnumerator ApplyUpwardForce(Transform playerTransform)
  {
    Vector3 upwardForce = Vector3.up * upwardForceMultiplier;
    float elapsedTime = 0f;

    while (elapsedTime < upwardForceDuration && isPlayerOnPlate)
    {
      playerTransform.Translate(upwardForce * Time.deltaTime);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
  }
}
