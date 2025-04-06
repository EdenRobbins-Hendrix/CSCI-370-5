using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    bool interacting;
    public float radius;
    int talking;
    Animator animator;
    public AudioSource enter;
    float timer;
    public float maxTime;
    NPC mostRecent;
    bool ticking;
    public GameObject dialoguePanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ticking = false;
        timer = 0;
        talking = 0;
        interacting = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!interacting && Input.GetKeyDown(KeyCode.Return))
        {
            interacting = true;
            AttemptSpeak();
        }
        if (!interacting && talking == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            interacting = true;
            Debug.Log("Space pressed");
            AttemptOpen();
        }

        interacting = false;



    }

    void FixedUpdate()
    {
        if (ticking) {
            if (timer <= 0) {
            Debug.Log("Ding!");
            ticking = false;
        }
    else {
        Debug.Log("Tick! " + timer);
        timer -= Time.deltaTime;
        }   
    }
    }

    void AttemptSpeak()
    {   if (talking > 0)
        {
            Debug.Log("Skipping Line");
            GameManager.Instance.SkipLine();
            talking++;
            if (talking > 4) {
                ticking = true;
                talking = 0;
                dialoguePanel.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Looking for NPC");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);

                if (hit.collider.gameObject.TryGetComponent(out NPC npc) && !ticking)
                {
                    mostRecent = npc;
                    talking = 1;
                    timer = maxTime;
                    GameManager.Instance.StartDialogue(npc);
                }
            }
        }

        //Add code for dialogue here
    }

    void AttemptOpen()
    {
        Debug.Log("attempt open called");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 0, LayerMask.GetMask("LevelEntry"));
        if (hit)
        {
            Debug.Log("hit! " + hit.collider.gameObject);
            if (hit.collider.gameObject.TryGetComponent(out Open open))
            {
                Debug.Log("got open script");
                if (!open.opened)
                {
                    Debug.Log("opening!");
                    open.SetOpen();
                }
                else
                {
                    Debug.Log("diving in!");
                    enter.Play();
                    open.gameObject.GetComponent<Enter>().EnterScene();
                }
            }
        }
    }

    void JoinConversation()
    {
    }

    void LeaveConversation()
    {
    }



}
