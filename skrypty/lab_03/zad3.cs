using UnityEngine;

public class MoveCube2 : MonoBehaviour
{
  public float speed = 2f; // Publiczne pole speed
  public float sideLength = 10f; // Długość boku kwadratu
  public Transform forwardElement; // Referencja do elementu wskazującego kierunek ruchu

  private Vector3 initialPosition;

  void Start()
  {
    initialPosition = transform.position;
  }

  void Update()
  {
    // Ruch względem elementu wskazującego kierunek ruchu
    float moveDistance = speed * Time.deltaTime;
    Vector3 moveDirectionVector = forwardElement.position - transform.position; // Oblicz wektor ruchu
    transform.Translate(moveDirectionVector * moveDistance, Space.World);

    // Jeśli osiągnięto wierzchołek kwadratu, obróć o 90 stopni
    if (Vector3.Distance(initialPosition, transform.position) >= sideLength)
    {
      transform.Rotate(Vector3.up, 90f); // Obrót o 90 stopni w kierunku osi Y
      initialPosition = transform.position; // Zaktualizuj początkową pozycję
    }
  }
}
