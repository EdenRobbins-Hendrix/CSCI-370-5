using UnityEngine;

public class NPCFlock : MonoBehaviour
{
    public float speed;
    GameObject player;
    public float radius;
    float goal;
    int timer;
    public int maxTime;
    Animator animator;
    SpriteRenderer sprite;
    public bool moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moving = true;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        timer = maxTime;
        player = GameObject.FindWithTag("Player");
        PickSpot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                timer = maxTime;
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
