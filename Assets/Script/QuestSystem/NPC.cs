using newQuestSystem;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject interactBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactBox.SetActive(false);
        }
    }
}
