using System.Collections;
using TMPro;
using UnityEngine;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public class GameManager : MonoBehaviour
{
    public AudioSource enter;
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

    public static GameManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;

    public int playerWeight;
    public void StartDialogue(NPC npc)
    {
        npc.halt();
        nameText.text = npc.name + "...";
        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(npc.dialogueAsset().dialogue, npc.StartPosition, npc));
    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition, NPC npc)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
        npc.walk();
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
    }

    float charactersPerSecond = 90;

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }

    public void incrementWeight() {
        playerWeight++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialoguePanel.GetComponent<Keep>().KeepAlive();
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}