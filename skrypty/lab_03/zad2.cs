using UnityEngine;

public class MoveCube : MonoBehaviour
{
  public float speed = 2f; // Publiczne pole speed

  private bool movingRight = true; // Zmienna do śledzenia kierunku ruchu

  void Update()
  {
    // Przesuń obiekt wzdłuż osi x w zależności, w którą stronę się teraz porusza
    if (movingRight)
    {
      transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    else
    {
      transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    // Sprawdź, czy obiekt osiągnął punkt, w którym ma zmienić kierunek
    if (transform.position.x >= 10f)
    {
      movingRight = false;
    }
    else if (transform.position.x <= -10f)
    {
      movingRight = true;
    }
  }
}
