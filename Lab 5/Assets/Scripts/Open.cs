using UnityEngine;

public class Open : MonoBehaviour
{
    public bool opened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void SetOpen() {
        opened = true;
        Debug.Log("Crack!");
    }
}
