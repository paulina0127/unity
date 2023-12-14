using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
  private Animator anim;
  private AudioSource finishSound;

  private bool levelCompleted = false;
  private bool player1TouchedFlag = false;
  private bool player2TouchedFlag = false;
  private int playersTouchedFlag = 0;

  private void Start()
  {
    anim = GetComponent<Animator>();
    finishSound = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.name == "Player1" && !levelCompleted)
    {
      player1TouchedFlag = true;
      playersTouchedFlag++;
    }
    else if (collision.gameObject.name == "Player2" && !levelCompleted)
    {
      player2TouchedFlag = true;
      playersTouchedFlag++;
    }

    if (playersTouchedFlag == 1)
    {
      anim.SetTrigger("touched");
    }

    if (playersTouchedFlag == 2 && player1TouchedFlag && player2TouchedFlag)
    {
      finishSound.Play();
      levelCompleted = true;
      Invoke("CompleteLevel", 2f);
    }
  }

  private void CompleteLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}