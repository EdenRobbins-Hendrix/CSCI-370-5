using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    Rigidbody2D self;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Replace this with animator based movement later on
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0) {
            spriteRenderer.flipX = true;
        } else if (horizontal > 0) {
            spriteRenderer.flipX = false;
        }
        if (vertical != 0) {
            animator.SetBool("VerticalMove", true);
        }
        else {
            animator.SetBool("VerticalMove", false);
        }
        animator.SetFloat("Horizontal", horizontal);

        self.linearVelocity = new Vector2(horizontal, vertical);
    }
}
