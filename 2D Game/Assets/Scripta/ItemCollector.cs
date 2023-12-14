using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
  private static int collectables = 0;
  private GameObject[] objects;
  int count;
  [SerializeField] private Text collectablesText;
  [SerializeField] private AudioSource collectSoundEffect;

  void Start()
  {
    collectables = 0;
    objects = GameObject.FindGameObjectsWithTag("Collectable");
    count = objects.Length;
    collectablesText.text = " " + collectables + " / " + count;

  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Collectable"))
    {
      collectSoundEffect.Play();
      Destroy(collision.gameObject);
      collectables++;
      collectablesText.text = " " + collectables + " / " + count;
    }
  }
}
