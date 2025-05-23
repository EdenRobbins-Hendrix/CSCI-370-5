using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementSelect : MonoBehaviour
{
    public Animator animator;

    public ParticleSystem dust;

    Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    float horizontal;

    public float runSpeed = 3f;
    private bool m_Grounded;

    public int dir = 1;
    public int newDir = 1;
    public AudioSource SFX;
    public UnityEvent OnLandEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFX.Pause();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // if (OnLandEvent == null) {
		//     OnLandEvent = new UnityEvent();
        // }
        // OnLandEvent.AddListener(Landed);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (horizontal < 0) {
            SFX.UnPause();
            spriteRenderer.flipX = true;
            newDir = 1;
        } 
        else if (horizontal > 0) {
            SFX.UnPause();
            spriteRenderer.flipX = false;
            newDir = 0;
        }
        else {
            SFX.Pause();
        }

        if (dir != newDir) { // Determines if direction changes, plays dust
            dir = newDir;
            createDust();
        }

        if (Input.GetKeyDown("space") && m_Grounded)
        {
            createDust();
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
        }

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(horizontal * runSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
    
		bool wasGrounded = m_Grounded;
;
        m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

    void createDust() {
        dust.Play();
    }
}