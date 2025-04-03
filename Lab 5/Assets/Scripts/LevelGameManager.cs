using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class LevelGameManager : MonoBehaviour
{
    public TextMeshProUGUI totalFishCaughtText;
    public GameObject player;

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
    public float timePassed = 0.0f;
    void Start()
    {
        InvokeRepeating("PickSpot", 0.5f, 10.0f);
        InvokeRepeating("reduceEnergy", 5.0f, 5.0f);
    }
    public TextMeshProUGUI timerText;
    void FixedUpdate()
    {
        haveFishMeetup();
        timePassed += Time.deltaTime;
        Debug.Log("Time Passed: " + timePassed);
        timerText.text = "Time: " + Math.Round(timePassed, 1);
    }


    public static LevelGameManager Instance { get; private set; }

    [SerializeField] List<GameObject> fishList;
    public AudioSource crunch;
    public void haveFishMeetup()
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
        safeFish.Clear();
        // yield return null;
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
    private int totalFishCaught = 0;
    public void eatPrey(GameObject prey)
    {
        Debug.Log("Prey Eaten!");
        crunch.Play();
        totalFishCaught += 1;
        totalFishCaughtText.text = "Fish Caught: " + totalFishCaught;

        fishList.Remove(prey);
        Destroy(prey);

    }

    public void returnToLevelSelect()
    {
        SceneManager.LoadScene("Game");
    }

    public void reduceEnergy()
    {
        player.GetComponent<PlayerLevelSteer>().decrementEnergy();
        Debug.Log("Energy Decremented");

    }







    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

    }
}
