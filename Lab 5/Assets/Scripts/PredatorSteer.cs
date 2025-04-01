
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorSteer : MonoBehaviour
{

    public GameObject target;
    public float speed;
    public float rotationSpeed;

    private Rigidbody2D body;
    SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 desired = (target.transform.position - transform.position).normalized;
        body.AddForce(desired * speed - body.linearVelocity);

        float angle = (Mathf.Atan2(desired.y, desired.x) * Mathf.Rad2Deg);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            q, Time.deltaTime * rotationSpeed);

        if (desired.x < 0)
        {
            // spriteRenderer.flipX = true;
            spriteRenderer.flipY = true;
        }
        else
        {
            // spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
        }

    }

    // public void IncreaseSpeed() {
    // 	speed *= 1.2f;
    // }

    // void OnDrawGizmos() {
    // 	Gizmos.color = Color.yellow;
    // 	Vector3 direction = body.linearVelocity;
    // 	Gizmos.DrawRay(transform.position, direction);
    // }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == target)
        {
            // GetComponent<AudioSource>().Play();
            Debug.Log("Player Hit!!");
        }
    }
}
