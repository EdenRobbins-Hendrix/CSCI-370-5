using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayer : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public GameObject target;
    // public GameObject dog;
    public float minDist;

    private bool tagged;
    private Rigidbody2D body;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        minDist = 2.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (target)
        {
            // Debug.Log(name + "target is true?");
            Vector2 desired = target.transform.position - transform.position;
            // Debug.Log(name + "Desired: " + desired);
            // Debug.Log(name + "Magnitude: " + desired.magnitude);
            // Debug.Log(name + "Position: " + target.transform.position);



            if (desired.magnitude < minDist)
            {
                Debug.Log(name + " is avoiding seal");
                float angle = (Mathf.Atan2(desired.y, desired.x) * Mathf.Rad2Deg);
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    q, Time.deltaTime * rotationSpeed);


                float actual = desired.magnitude - minDist;
                body.AddForce(desired.normalized *
                    actual * speed - body.linearVelocity);
                if (desired.x < 0)
                {
                    // spriteRenderer.flipX = true;
                    spriteRenderer.flipY = true;
                    spriteRenderer.flipX = true;
                }
                else
                {
                    // spriteRenderer.flipX = false;
                    spriteRenderer.flipY = false;
                    spriteRenderer.flipX = true;
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!tagged && coll.gameObject == target)
        {
            print("Seal!");
            minDist += 2;
            tagged = true;
            // GetComponent<AudioSource>().Play();
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = GetComponent<Rigidbody2D>().linearVelocity;
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
}