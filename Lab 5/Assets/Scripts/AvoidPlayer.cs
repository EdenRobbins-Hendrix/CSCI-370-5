using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AvoidPlayer : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public GameObject target;
    // public GameObject dog;
    public float minDistFromPlayer;
    public float minDistToRegroup;

    private bool tagged;
    private Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    [SerializeField] private bool isFleeing;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        isFleeing = false;
    }
    public bool getIsFleeing()
    {
        return isFleeing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (target)
        {
            // Debug.Log(name + "target is true?");
            Vector2 desired = target.transform.position - transform.position;
            Debug.Log(name + ": " + desired);
            Debug.Log(name + "normalized: " + desired.normalized);
            // Debug.Log(name + "Desired: " + desired);
            // Debug.Log(name + "Magnitude: " + desired.magnitude);
            // Debug.Log(name + "Position: " + target.transform.position);



            if (desired.magnitude < minDistFromPlayer)
            {
                LevelGameManager.Instance.s();
                isFleeing = true;
                Debug.Log(name + " is avoiding seal");
                float angle = Mathf.Atan2(desired.y, desired.x) * Mathf.Rad2Deg - 180;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    q, Time.deltaTime * rotationSpeed);


                float actual = desired.magnitude - minDistFromPlayer;
                Vector2 k = desired.normalized *
                    actual * speed - body.linearVelocity;
                body.AddForce(k);
                // if (desired.x < 0)
                // {
                //     // spriteRenderer.flipX = true;
                //     spriteRenderer.flipY = true;
                //     spriteRenderer.flipX = true;
                // }
                // else
                // {
                //     // spriteRenderer.flipX = false;
                //     spriteRenderer.flipY = false;
                //     spriteRenderer.flipX = true;
                // }
            }
            //TODO: put this in a game manager. I think that will help with all the fish trying to go to different places
            //if player is not near, try to regroup with other fish
            else
            {
                isFleeing = false;
                //             Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, minDistToRegroup, LayerMask.GetMask("Prey")); //find nearby fish
                //             Collider2D col = colls[0];
                //             if (col.gameObject != target)
                //             {
                //                 float xValue = (col.gameObject.transform.position.x + transform.position.x) / 2;
                //                 float yValue = (col.gameObject.transform.position.y + transform.position.y) / 2;

                //                 Vector2 meetPoint = new Vector2(xValue, yValue); //get middle between 2 fish

                //                 foreach (Collider2D coll in colls)
                //                 {
                //                     coll.GetComponent<Rigidbody2D>().AddForce(meetPoint.normalized *
                //                          speed - body.linearVelocity); //send each fish towards meet point

                //                 }
                //                 body.AddForce(meetPoint.normalized * //send current fish towards meetpoint
                //  speed - body.linearVelocity);

                //             }


            }
        }

    }

    public void moveToSpot(Vector3 v)
    {
        Vector2 desired = v - transform.position;


        float angle = Mathf.Atan2(desired.y, desired.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,
            q, Time.deltaTime * rotationSpeed);


        // float actual = desired.magnitude - minDistFromPlayer;
        body.AddForce(desired.normalized *
              speed - body.linearVelocity);
        // if (desired.x < 0 && desired.y < 0)
        // {
        //     // spriteRenderer.flipX = true;
        //     spriteRenderer.flipY = true;
        //     spriteRenderer.flipX = true;
        // }
        // else if (desired.x < 0 && desired.y > 0)
        // {
        //     spriteRenderer.flipY = false;
        //     spriteRenderer.flipX = true;
        // }
        // else if (desired.x > 0 && desired.y < 0)
        // {
        //     spriteRenderer.flipY = true;
        //     spriteRenderer.flipX = false;
        // }
        // else
        // {
        //     // spriteRenderer.flipX = false;
        //     spriteRenderer.flipY = false;
        //     spriteRenderer.flipX = false;
        // }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!tagged && coll.gameObject == target)
        {
            print("Seal attack!");
            tagged = true;
            // GetComponent<AudioSource>().Play();
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = GetComponent<Rigidbody2D>().linearVelocity;
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawWireSphere(transform.position, minDistFromPlayer);
    }
}