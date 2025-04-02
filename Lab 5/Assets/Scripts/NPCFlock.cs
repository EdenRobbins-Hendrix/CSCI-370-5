using UnityEngine;

public class NPCFlock : MonoBehaviour
{
    public float speed;
    GameObject player;
    public float radius;
    float waitVariability;
    float goal;
    float timer;
    public int maxTime;
    Animator animator;
    SpriteRenderer sprite;
    public bool moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeWait();
        moving = true;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        timer = maxTime + waitVariability;
        player = GameObject.FindWithTag("Player");
        PickSpot();
    }

    void changeWait() {
        waitVariability = Random.Range(-maxTime/2, maxTime/2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   changeWait();
        if (transform.position.x != goal) {
            if (goal < transform.position.x) {
                sprite.flipX = true;
            }
            else if (goal > transform.position.x){
                sprite.flipX = false;
            }
            animator.SetBool("Moving", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (goal, transform.position.y), speed);
        }
        else {
            animator.SetBool("Moving", false);
            if (timer <= 0) {
                timer = maxTime + waitVariability;
                PickSpot();}
            else {
                timer--;
            }
        }
    }

    void PickSpot(){
        float circle = Random.Range(-1, 1) * radius;
        goal = circle + player.transform.position.x;
        Debug.Log("The goal is: " + goal);
    }
}
