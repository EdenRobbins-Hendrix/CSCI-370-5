using UnityEngine;
using UnityEngine.Tilemaps;

public class Open : MonoBehaviour
{
    public bool opened;
    public int requiredWeight;
    TilemapRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void SetOpen() {
        if (requiredWeight <= GameManager.Instance.playerWeight){
        Destroy(sprite);
        opened = true;
        Debug.Log("Crack!");}
    }
}
