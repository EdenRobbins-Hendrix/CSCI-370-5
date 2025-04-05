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
        LevelGameManager.Instance.flipEnergyBar();
    }

    private bool flipped = false;
    private bool goingLeft = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (flipped)
        {
            flipped = false;
            LevelGameManager.Instance.flipEnergyBar();
        }
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
        if (push < 0 && !goingLeft)
        {
            flipped = true;
            goingLeft = true;
            spriteRenderer.flipX = true;
        }
        else if (push > 0 && goingLeft)
        {
            flipped = true;
            goingLeft = false;
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
        if (energy < 10)
        {
            LevelGameManager.Instance.flipCube(energy);
            Debug.Log("Cube Flipped: " + energy);
        }
    }
    public void addEnergy(int amount)
    {
        energy += amount;
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
        if (energy > 7)
        {
            LevelGameManager.Instance.changeCubeColor(Color.green);
        }
        else if (energy > 3)
        {
            Debug.Log("Change colors to yellow");
            LevelGameManager.Instance.changeCubeColor(new Color(0.9117833f, 0.9371068f, 0.05009684f));
        }
        else { LevelGameManager.Instance.changeCubeColor(Color.red); }
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
