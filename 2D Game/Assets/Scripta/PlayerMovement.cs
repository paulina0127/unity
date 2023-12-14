using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D rb;
  private BoxCollider2D coll;
  private SpriteRenderer sprite;
  private Animator anim;

  [SerializeField] private LayerMask jumpableGround;

  private float dirX = 0f;
  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private float jumpForce = 14f;

  private enum MovementState { idle, running, jumping, falling }

  [SerializeField] private AudioSource jumpSoundEffect;

  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
    sprite = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  private void Update()
  {
    // Move left and right based on player controls 
    dirX = Input.GetAxisRaw("Horizontal_" + gameObject.name);
    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

    // Jump
    if (Input.GetButtonDown("Jump_" + gameObject.name) && (IsGrounded() || CanJumpOnPlayer()))
    {
      jumpSoundEffect.Play();
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    UpdateAnimationState();
  }

  private void UpdateAnimationState()
  {
    MovementState state;

    if (dirX > 0f)
    {
      state = MovementState.running;
      sprite.flipX = false;
    }
    else if (dirX < 0f)
    {
      state = MovementState.running;
      sprite.flipX = true;
    }
    else
    {
      state = MovementState.idle;
    }

    if (rb.velocity.y > .1f)
    {
      state = MovementState.jumping;
    }
    else if (rb.velocity.y < -.1f)
    {
      state = MovementState.falling;
    }

    anim.SetInteger("state", (int)state);
  }

  private bool CanJumpOnPlayer()
  {
    // Width and height of the rectangular area that will be used to check for overlapping colliders
    float overlapWidth = coll.bounds.size.x * 0.8f;
    float overlapHeight = coll.bounds.extents.y + 0.1f;

    Vector2 overlapCenter = new Vector2(coll.bounds.center.x, coll.bounds.center.y - coll.bounds.extents.y);

    // Check for overlapping colliders in layer Player in the specified rectangular area
    // Area is defined by the bottom left and top right corners calculated based on the previously determined width and height
    Collider2D[] colliders = Physics2D.OverlapAreaAll(overlapCenter - new Vector2(overlapWidth / 2, overlapHeight),
                                                      overlapCenter + new Vector2(overlapWidth / 2, 0),
                                                      LayerMask.GetMask("Player"));

    // Check if there is at least one other player below the current player
    return colliders.Length > 1;
  }

  private bool IsGrounded()
  {
    // Cast a box-shaped ray downward from the center of the player's collider
    // (Center of box collider, size of box collider, angle, direction, distance, ground)
    // Function returns true if the box cast hits something on the specified layers within the defined distance
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
  }
}