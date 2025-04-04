using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public bool decreaseHealth(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            return true; //if the character dies, let the game manager know
        }
        else
        {
            return false;
        }
    }

    public void increaseHealth(int amount)
    {
        health += amount;
    }

}
