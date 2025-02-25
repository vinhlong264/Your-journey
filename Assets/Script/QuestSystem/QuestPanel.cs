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
    [SerializeField] private Button completeQuest;
    private Quest q;


    private void Start()
    {
        receiveQuestBtn.onClick.AddListener(() => ReceiveQuest());
        completeQuest.onClick.AddListener(() => CompeleteQuest());
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
            gameObject.SetActive(true);
        }
    }

    private void ReceiveQuest()
    {
        Debug.Log("Nhận Quest");
        if (q == null) return;
        QuestManager.Instance.ReceiveQuest(q);
        gameObject.SetActive(false);
    }

    private void CompeleteQuest()
    {
        Debug.Log("Hoàn thành Quest");
        if (q == null) return;

        QuestManager.Instance.CompeleteQuest(q);
        gameObject.SetActive(false);

    }
}
