using UnityEngine;

public class MovePlayer : MonoBehaviour
{
  public float speed = 50f;  // Publiczne pole speed
  Rigidbody rb;
  public GameObject ground; // Referencja do obiektu płaszczyzny

  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    // Pobranie wartości zmiany pozycji w danej osi
    // Wartości są z zakresu [-1, 1]
    float mH = Input.GetAxis("Horizontal");
    float mV = Input.GetAxis("Vertical");

    // Tworzymy wektor prędkości
    Vector3 velocity = new Vector3(mH, 0, mV);
    velocity = velocity.normalized * speed * Time.deltaTime;

    // Oblicz nową pozycję gracza
    Vector3 newPosition = transform.position + velocity;

    // Pobierz pozycję i rozmiary płaszczyzny
    Vector3 groundPosition = ground.transform.position;
    Vector3 groundSize = ground.transform.localScale;

    // Ogranicz ruch gracza do obszaru płaszczyzny
    float minX = groundPosition.x - (groundSize.x / 2f) * 10;
    float maxX = groundPosition.x + (groundSize.x / 2f) * 10;
    float minZ = groundPosition.z - (groundSize.z / 2f) * 10;
    float maxZ = groundPosition.z + (groundSize.z / 2f) * 10;

    newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
    newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

    // Wykonujemy przesunięcie Rigidbody z uwzględnieniem sił fizycznych
    rb.MovePosition(newPosition);
  }
}
