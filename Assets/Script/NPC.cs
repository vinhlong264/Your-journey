using UnityEngine;

public class NPC : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFacingRigt;
    private Player player;
    [SerializeField] private DialogueSystem dialogueSystem;

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
        dialogueSystem.setUpDialogue(0, "TextData/Story 1");
    }
}
