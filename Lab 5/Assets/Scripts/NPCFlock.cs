using UnityEngine;

public class NPCFlock : MonoBehaviour
{
    public float speed;
    GameObject player;
    public float radius;
    Vector2 goal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PickSpot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!transform.position.Equals(goal)) {
            transform.position = Vector2.MoveTowards(transform.position, goal, speed);
        }
        else {
            PickSpot();
        }
    }

    void PickSpot(){
        Vector2 circle = Random.insideUnitCircle * radius;
        goal = new Vector2(circle.x + player.transform.position.x, circle.y + player.transform.position.y);
    }
}
