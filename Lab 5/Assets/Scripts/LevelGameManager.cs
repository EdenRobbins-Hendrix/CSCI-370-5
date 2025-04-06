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
        InvokeRepeating("PickSpot", 0.5f, 3.0f);
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

        if (fishList.Count == 0)
        {
            Debug.Log("All fish eaten!");
            returnToLevelSelect();
            //add weight
        }

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

    [SerializeField] List<GameObject> energyCubes;

    public void changeCubeColor(Color color)
    {
        foreach (GameObject cube in energyCubes)
        {
            Color currentColor = cube.GetComponent<SpriteRenderer>().color;
            float alpha = currentColor.a;
            Color newColor = new Color(color.r, color.g, color.b, alpha);
            cube.GetComponent<SpriteRenderer>().color = newColor;
        }
    }
    public void flipCube(int index)
    {
        GameObject cube = energyCubes[index];
        SpriteRenderer renderer = cube.GetComponent<SpriteRenderer>();
        Color currentColor = renderer.color;
        Color newColor;
        if (currentColor.a == 0)
        {
            newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);

        }
        else
        {
            newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.0f);

        }
        renderer.color = newColor;

    }

    public void flipEnergyUnitss()
    {
        //TODO: it might be easeier to just move the x position of each unit to currentPosition.x * -1


        // get count of active units
        List<int> activeUnits = new List<int>();
        int i = 0;
        Debug.Log("Count: " + energyCubes.Count);
        while (i < energyCubes.Count)
        {
            SpriteRenderer SR = energyCubes[i].GetComponent<SpriteRenderer>();
            if (SR.color.a != 0.0f)
            {
                activeUnits.Add(i);
            }
            i += 1;
        }
        Debug.Log("Active units count: " + activeUnits.Count);
        int j = 0;
        while (j < energyCubes.Count)
        {
            if (j < activeUnits.Count)
            {
                flipCube(j);
                Debug.Log("Flipped j: " + j);
                Debug.Log("minus: " + (energyCubes.Count - j));
                flipCube(energyCubes.Count - j);
                Debug.Log("FFlipped: " + j + " and " + (energyCubes.Count - j));
            }
            j += 1;
        }
    }

    // effect of player getting hit by the predator
    public void playerHit()
    {
        //decrease health
        int damage = 100;
        bool dead = player.GetComponent<PlayerHealth>().decreaseHealth(damage);

        //TODO: I would like to display some kind of particle and sound as a feedback here

        //if player died:
        if (dead)
        {
            //display some kind of death message?

            returnToLevelSelect();
        }

        else
        {
            //Some kind of speed boost here would be good

        }

    }







    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

    }
}
