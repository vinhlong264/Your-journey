using UnityEngine;

public class NPC : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFacingRigt;
    private Player player;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private GameObject chatBox;
    private int count;
    private int branchID;
    private string dataText;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        chatBox.SetActive(false);
        isFacingRigt = true;
        player = GameManager.Instance.player;
        count = 0;
        branchID = 0;
    }

    private void OnMouseDown()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 2f)
        {
            chatBox.SetActive(true);
            if (player.transform.position.x < transform.position.x && isFacingRigt)
            {
                sr.flipX = true;
                isFacingRigt = false;
            }
            else
            {
                sr.flipX = false;
                isFacingRigt = true;
            }

            int qip = QuestSystem.Instance.GetQipStory(branchID);
            Debug.Log(transform.name + ", Qip: " + qip);
        }
    }
}
