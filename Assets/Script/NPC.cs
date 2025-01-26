using UnityEngine;

public class NPC : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFacingRigt;
    private Player player;
    [SerializeField] private DialogueSystem dialogueSystem;
    private int count;
    private int branchID;
    private string dataText;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        isFacingRigt = true;
        player = GameManager.Instance.player;
        count = 0;
        branchID = 0;
    }

    private void OnMouseDown()
    {
        if(player.transform.position.x < transform.position.x && isFacingRigt)
        {
            sr.flipX = true;
            isFacingRigt = false;
        }
        else
        {
            sr.flipX = false;
            isFacingRigt = true;
        }
        count++;
        switch (count)
        {
            case 1:
                dataText = "TextData/Story 1";
                break;
            case 2:
                dataText = "TextData/Story 2";
                break;
        }
        dialogueSystem.setUpDialogue(branchID, dataText);
    }
}
