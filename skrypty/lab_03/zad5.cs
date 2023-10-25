using UnityEngine;
using System.Collections.Generic;

public class ObjectGenerator : MonoBehaviour
{
  public GameObject cubePrefab; // Prefab obiektu "Cube"
  public int numberOfCubes = 10; // Liczba obiektów do wygenerowania
  public float planeSize = 10f; // Rozmiar płaszczyzny
  public float minDistance = 1.0f; // Minimalny odstęp między obiektami
  private List<Vector3> generatedPositions = new List<Vector3>();

  void Start()
  {
    GenerateCubes();
  }

  void GenerateCubes()
  {
    for (int i = 0; i < numberOfCubes; i++)
    {
      Vector3 randomPosition = GenerateUniqueRandomPosition();
      Instantiate(cubePrefab, randomPosition, Quaternion.identity);
    }
  }

  Vector3 GenerateUniqueRandomPosition()
  {
    Vector3 randomPosition;
    float x, z;

    do
    {
      x = Random.Range(-planeSize / 2f, planeSize / 2f);
      z = Random.Range(-planeSize / 2f, planeSize / 2f);
      randomPosition = new Vector3(x, 0.5f, z);
    } while (IsPositionGenerated(randomPosition));

    generatedPositions.Add(randomPosition);
    return randomPosition;
  }

  bool IsPositionGenerated(Vector3 position)
  {
    foreach (Vector3 existingPosition in generatedPositions)
    {
      if (Vector3.Distance(position, existingPosition) < minDistance)
      {
        return true; // Pozycja jest zbyt bliska istniejącej pozycji
      }
    }
    return false;
  }
}
