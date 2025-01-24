using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameQuestTxt;
    [SerializeField] private TextMeshProUGUI questDescriptionTxt;
    [SerializeField] private TextMeshProUGUI expRewardTxt;
    [SerializeField] private TextMeshProUGUI currentQuestTxt;

    [SerializeField] private Button receiveQuestBtn;
    [SerializeField] private Button refuseQuestBtn;

    private Quest quest;

    private void Start()
    {
        receiveQuestBtn.onClick.AddListener(() => ReceiveQuest());
        refuseQuestBtn.onClick.AddListener(() => RefuseQuest());
    }
    public void ShowQuest(Quest q)
    {
        if(q == null) return;

        gameObject.SetActive(true);
        quest = q;

        nameQuestTxt.text = q.name;
        questDescriptionTxt.text = q.description;
        expRewardTxt.text = "ExpReward: "+q.expReward;
        currentQuestTxt.text = $"{q.currentQuest}/{q.reqireQuest}";
    }

    private void ReceiveQuest()
    {
        Debug.Log("Nhận Quest");
        QuestSystem.Instance.ReceiveQuest(quest);
        gameObject.SetActive(false);
    }

    private void RefuseQuest()
    {
        Debug.Log("Không nhận Quest");
    }

}
