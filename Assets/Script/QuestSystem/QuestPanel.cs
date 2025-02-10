using newQuestSystem;
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
    private Quest q;


    private void Start()
    {
        receiveQuestBtn.onClick.AddListener(() => ReceiveQuest());
    }
    public void ShowQuest(Quest q)
    {
        this.q = q;
        if(q != null)
        {
            nameQuestTxt.text = q.nameQuest;
            questDescriptionTxt.text = q.desQuest;
            expRewardTxt.text = q.expReward.ToString();
            currentQuestTxt.text = $"{q.currentQuest}/{q.requireQuest}";
        }
    }

    private void ReceiveQuest()
    {
        Debug.Log("Nhận Quest");
        if (q == null) return;
        QuestManager.Instance.ReceiveQuest(q);
        gameObject.SetActive(false);
    }
}
