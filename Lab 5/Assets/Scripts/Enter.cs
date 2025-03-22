
using UnityEngine;

public class Enter : MonoBehaviour
{
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterScene() {
        if (GetComponent<Open>().opened) {
            //Initiate.Fade(sceneName, Color.black, 0.5f);
            Debug.Log("Come on in!");
        }
    }
}
