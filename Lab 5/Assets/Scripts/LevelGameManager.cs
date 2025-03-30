using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGameManager : MonoBehaviour
{

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InvokeRepeating("PickSpot", 0.5f, 10.0f);
    }

    void FixedUpdate()
    {
        StartCoroutine(haveFishMeetup());
    }

    public static LevelGameManager Instance { get; private set; }

    public List<GameObject> fishList;
    IEnumerator haveFishMeetup()
    {
        Debug.Log("fish meetup begun");
        //get safe fishes
        List<GameObject> safeFish = new List<GameObject>();
        foreach (GameObject fish in fishList)
        {
            if (!fish.GetComponent<AvoidPlayer>().getIsFleeing())
            {
                safeFish.Add(fish);
            }
        }


        //get meetPoint
        float sumX = 0;
        float sumY = 0;
        int total = 0;
        foreach (GameObject fish in safeFish)
        {
            sumX += fish.transform.position.x;
            sumY += fish.transform.position.y;
            total += 1;
        }
        middlePoint = new Vector3(sumX / total, sumY / total);
        //send each fish to meetpoint. It may be better to handle this in the fish move area. TODO handle this with rigid bodies in fish move area
        foreach (GameObject fish in safeFish)
        {

            fish.GetComponent<AvoidPlayer>().moveToSpot(meetPoint);
            // fish.transform.position = Vector2.MoveTowards(fish.transform.position, meetPoint, fish.GetComponent<AvoidPlayer>().speed * Time.deltaTime);
            // fish.GetComponent<Rigidbody2D>().AddForce(meetPoint.normalized *
            //                     fish.GetComponent<AvoidPlayer>().speed - fish.GetComponent<Rigidbody2D>().linearVelocity); I am not sure what is wrong, but basically the fish all just drift in one direction. 
        }
        yield return null;
    }
    public void s()
    {
        Debug.Log("this starts the manager");
    }
    Vector2 meetPoint;
    Vector3 middlePoint;
    void PickSpot()
    {
        int radius = 20;
        Vector2 circle = Random.insideUnitCircle * radius;
        meetPoint = new Vector2(circle.x + middlePoint.x, circle.y + middlePoint.y);
        Debug.Log("meet spot: " + meetPoint);
    }





    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

    }
}
