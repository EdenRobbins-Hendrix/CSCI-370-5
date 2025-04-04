using UnityEngine;

public class PlayerLevelSteer : MonoBehaviour
{
    public float rotationSpeed;
    float rotation;
    [SerializeField]
    private float directionalSpeed;
    float push;
    float accelerationBuffer;
    public float accelerationFactor;
    Rigidbody2D self;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private int energy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        accelerationBuffer = 0;
        rotation = 0;
        self = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        energy = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotation += Input.GetAxisRaw("Vertical") * rotationSpeed;
        // if (rotation > 90)
        // {
        //     rotation = 90;
        // }
        // if (rotation < -90)
        // {
        //     rotation = -90;
        // }
        push = Input.GetAxisRaw("Horizontal") * directionalSpeed * Time.deltaTime;
        if (push < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (push > 0)
        {
            spriteRenderer.flipX = false;

        }
        if (spriteRenderer.flipX == true)
        {
            self.transform.eulerAngles = Vector3.back * rotation;
        }
        else
        {
            self.transform.eulerAngles = Vector3.forward * rotation;
        }

        if (push != 0)
        {
            if (accelerationBuffer < 1)
            {
                accelerationBuffer += accelerationFactor;
            }
            else
            {
                accelerationBuffer = 1;
            }
            push *= accelerationBuffer;
            self.transform.position += (self.transform.right * push);
            // Debug.Log(push + " " + accelerationBuffer + " " + accelerationFactor);

        }
        else
        {
            accelerationBuffer = 0;
        }


    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Prey"))
        {
            // GetComponent<AudioSource>().Play();
            Debug.Log("Prey Eaten!!");
            LevelGameManager.Instance.eatPrey(collision.gameObject);
            energy += 5;
            reCalculateEnergy();
        }
        else if (collision.gameObject.name.Contains("Predator"))
        {
            Debug.Log("Hit Predator");
        }

    }

    public void decrementEnergy()
    {
        energy -= 1;
        reCalculateEnergy();
    }

    public void reCalculateEnergy()
    {
        if (energy > 10)
        {
            directionalSpeed = 5.0f;
        }
        else if (energy > 9)
        {
            directionalSpeed = 4.0f;
        }
        else if (energy > 8)
        {
            directionalSpeed = 3.75f;
        }
        else if (energy > 7)
        {
            directionalSpeed = 3.5f;
        }
        else if (energy > 6)
        {
            directionalSpeed = 3.25f;
        }
        else if (energy > 5)
        {
            directionalSpeed = 3.0f;
        }
        else if (energy > 4)
        {
            directionalSpeed = 2.75f;
        }
        else if (energy > 3)
        {
            directionalSpeed = 2.5f;
        }
        else if (energy > 2)
        {
            directionalSpeed = 2.0f;
        }
        else if (energy > 1)
        {
            directionalSpeed = 1.5f;
        }
        else
        {
            directionalSpeed = 1.0f;
        }
        int j = 0;
        while (j < 10)
        {
            LevelGameManager.Instance.removeCube(j);
            j += 1;
        }
        int i = 0;
        while (i < energy && i < 10)
        {
            LevelGameManager.Instance.addCube(i);
            i += 1;

        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.name.Contains("Prey"))
    //     {
    //         // GetComponent<AudioSource>().Play();
    //         Debug.Log("Hit prey");
    //     }
    //     else if (collision.gameObject.name.Contains("Predator"))
    //     {
    //         Debug.Log("Hit Predator");
    //     }
    // }

}
