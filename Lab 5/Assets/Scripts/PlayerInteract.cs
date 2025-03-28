using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    bool interacting;
    public float radius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interacting = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!interacting && Input.GetKeyDown(KeyCode.Return))
        {
            interacting = true;
            AttemptSpeak();
        }
        if (!interacting && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            AttemptOpen();
        }



    }

    void AttemptSpeak()
    {
        //Add code for dialogue here
        interacting = false;
    }

    void AttemptOpen()
    {
        Debug.Log("attempt open called");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, LayerMask.GetMask("LevelEntry"));
        if (hit)
        {
            Debug.Log("hit!");
            if (hit.collider.gameObject.TryGetComponent(out Open open))
            {
                Debug.Log("got open script");
                if (!open)
                {
                    open.SetOpen();
                }
                else
                {
                    open.gameObject.GetComponent<Enter>().EnterScene();
                }
            }
        }
        interacting = false;
    }

}
