using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
  private Animator anim;
  void Start()
  {
    anim = GetComponent<Animator>();

  }
  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      anim.SetTrigger("touched");

      // Check if the player has a Rigidbody2D
      Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
      if (playerRb != null)
      {
        // Apply a vertical force to make the player jump
        playerRb.velocity = new Vector2(playerRb.velocity.x, 20f);
      }
    }
  }
}
