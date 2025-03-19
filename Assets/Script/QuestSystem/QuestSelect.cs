using newQuestSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelect : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button selectQuestBtn;

    private Quest q;

    private void Start()
    {
        selectQuestBtn = GetComponent<Button>();
        selectQuestBtn.onClick.AddListener(() => SelectQuestHandler()); // Button để chọn Quest
    }

    public void setUpQuest(Quest newQuest) // set up dữ liệu cho quest ở quest log
    {
        if (newQuest == null) return;
        this.q = newQuest;

        SelectQuestHandler();
        Observer.Instance.NotifyEvent(GameEvent.SetUpQuest, newQuest);
    }

    private void SelectQuestHandler() // chọn quest ở quest log
    {
        if (q == null) return;
    }
}
