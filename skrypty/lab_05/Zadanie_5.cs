using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Obstacle"))
    {
      Debug.Log("Rozpoczęto kontakt z przeszkodą!");
    }
  }

  private void OnCollisionExit(Collision collision)
  {
    if (collision.gameObject.CompareTag("Obstacle"))
    {
      Debug.Log("Zakończono kontakt z przeszkodą!");
    }
  }
}
