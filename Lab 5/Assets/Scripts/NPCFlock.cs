using UnityEngine;

public class NPCFlock : MonoBehaviour
{
    public float speed;
    GameObject player;
    public float radius;
    Vector2 goal;
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
        if (!transform.position.x.Equals(goal.x)) {
            if (goal.x < transform.position.x) {
                sprite.flipX = true;
            }
            else if (goal.x > transform.position.x){
                sprite.flipX = false;
            }
            animator.SetBool("Moving", true);
            transform.position = Vector2.MoveTowards(transform.position, goal, speed);
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
        Vector2 circle = Random.insideUnitCircle * radius;
        goal = new Vector2(circle.x + player.transform.position.x, transform.position.y);
        Debug.Log("The goal is: " + goal);
    }
}
