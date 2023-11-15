using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
  public int numberOfObjects = 10;
  public float delay = 3.0f;
  public GameObject block;
  public Material[] materials;

  void Start()
  {
    List<Vector3> positions = GenerateRandomPositions(transform.position, numberOfObjects);

    foreach (Vector3 elem in positions)
    {
      Debug.Log(elem);
    }

    StartCoroutine(GenerujObiekt(positions));
  }

  void Update()
  {

  }

  List<Vector3> GenerateRandomPositions(Vector3 center, int count)
  {
    List<Vector3> randomPositions = new List<Vector3>();

    for (int i = 0; i < count; i++)
    {
      float x = center.x + UnityEngine.Random.Range(-5f, 5f);
      float z = center.z + UnityEngine.Random.Range(-5f, 5f);
      float y = 5;

      randomPositions.Add(new Vector3(x, y, z));
    }

    return randomPositions;
  }

  IEnumerator GenerujObiekt(List<Vector3> positions)
  {
    Debug.Log("wywołano coroutine");

    for (int i = 0; i < numberOfObjects; i++)
    {
      GameObject newObject = Instantiate(block, positions[i], Quaternion.identity);

      Renderer renderer = newObject.GetComponent<Renderer>();
      if (renderer == null)
      {
        renderer = newObject.GetComponentInChildren<Renderer>();
      }

      if (renderer != null && materials.Length > 0)
      {
        Material randomMaterial = materials[UnityEngine.Random.Range(0, materials.Length)];
        renderer.material = randomMaterial;
      }

      yield return new WaitForSeconds(delay);
    }
  }
}
