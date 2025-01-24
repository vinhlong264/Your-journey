using UnityEngine;

public class NPC : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFacingRigt;
    private Player player;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private int branchID;
    [SerializeField] private string dataText;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        isFacingRigt = true;
        player = GameManager.Instance.player;
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
        dialogueSystem.setUpDialogue(branchID, dataText);
    }
}
