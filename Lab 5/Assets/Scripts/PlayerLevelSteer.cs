using UnityEngine;

public class PlayerLevelSteer : MonoBehaviour
{
    public float rotationSpeed;
    float rotation;
    public float directionalSpeed;
    float push;
    float accelerationBuffer;
    public float accelerationFactor;
    Rigidbody2D self;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        accelerationBuffer = 0;
        rotation = 0;
        self = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotation += Input.GetAxisRaw("Vertical") * rotationSpeed;
        if (rotation > 90)
        {
            rotation = 90;
        }
        if (rotation < -90)
        {
            rotation = -90;
        }
        self.transform.eulerAngles = Vector3.forward * rotation;
        push = Input.GetAxisRaw("Horizontal") * directionalSpeed * Time.deltaTime;
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
            Debug.Log("Hit prey");
        }
        else if (collision.gameObject.name.Contains("Predator"))
        {
            Debug.Log("Hit Predator");
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Prey"))
        {
            // GetComponent<AudioSource>().Play();
            Debug.Log("Hit prey");
        }
        else if (collision.gameObject.name.Contains("Predator"))
        {
            Debug.Log("Hit Predator");
        }
    }

}
