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
    public AudioSource SFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFX.Pause();
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
                SFX.UnPause();
                sprite.flipX = true;
            }
            else if (goal > transform.position.x){
                SFX.UnPause();
                sprite.flipX = false;
            }
            else {
                SFX.Pause();
            }
            animator.SetBool("Moving", true);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (goal, transform.position.y), speed);
            if (timer <= 0) {
                timer = maxTime + waitVariability;
                PickSpot();
            }
            else {
                float decrement = 0.05f * radius/speed*2;
                if (decrement > 0.5f) {
                    decrement = 0.5f;
                } 
                timer -= decrement;
            }
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
        circle += (radius/2);
        goal = circle + player.transform.position.x;
        Debug.Log("The goal is: " + goal + ". The base is " + circle);
    }
}
