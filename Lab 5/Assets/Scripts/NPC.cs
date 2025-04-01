using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public DialogueAsset dialogueAsset1;
    public DialogueAsset dialogueAsset2;
    public DialogueAsset dialogueAsset3;
    public DialogueAsset dialogueAsset4;

    [HideInInspector]
    public int StartPosition {
        get
        {
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return 0;
            }
        }
}
    public DialogueAsset dialogueAsset() {
        if (GameManager.Instance.playerWeight == 0) {
            return dialogueAsset1;
        }
        else if (GameManager.Instance.playerWeight == 1) {
            return dialogueAsset2;
        } 
        else if (GameManager.Instance.playerWeight == 2) {
            return dialogueAsset3;
        }
        else {
            return dialogueAsset4;
        }
    }

    public void halt(){
        GetComponent<NPCFlock>().moving = false;
    }

    public void walk() {
        GetComponent<NPCFlock>().moving = true;
    }
}
