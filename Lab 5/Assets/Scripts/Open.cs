using UnityEngine;

public class Open : MonoBehaviour
{
    public bool opened;
    public int requiredWeight;
    GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void SetOpen() {
        if (requiredWeight <= manager.playerWeight){
        opened = true;
        Debug.Log("Crack!");}
    }
}
