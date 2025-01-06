using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private DialogueSystem conversation;
    private void OnMouseDown()
    {
        if(Vector2.Distance(transform.position , PlayerManager.Instance.player.transform.position) < 2)
        {
            conversation.LoadText("TextData/Story 1");
            conversation.Dialogue();
        }
    }
}
