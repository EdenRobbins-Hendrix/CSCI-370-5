using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector2 meetPoint;
        float sumX = 0;
        float sumY = 0;
        int total = 0;
        foreach (GameObject fish in safeFish)
        {
            sumX += fish.transform.position.x;
            sumY += fish.transform.position.y;
            total += 1;
        }
        meetPoint = new Vector2(sumX / total, sumY / total);
        Debug.Log(meetPoint);
        //send each fish to meetpoint. It may be better to handle this in the fish move area...
        foreach (GameObject fish in safeFish)
        {
            fish.transform.position = Vector2.MoveTowards(fish.transform.position, meetPoint, fish.GetComponent<AvoidPlayer>().speed);
            // fish.GetComponent<Rigidbody2D>().AddForce(meetPoint.normalized *
            //                     fish.GetComponent<AvoidPlayer>().speed - fish.GetComponent<Rigidbody2D>().linearVelocity); //send each fish towards meet point
        }
        yield return null;
    }
    public void s()
    {
        Debug.Log("this starts the manager");
    }






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
