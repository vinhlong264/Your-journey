using newQuestSystem;
using TMPro;
using UnityEngine;

public class QuestInfor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameQuest;
    [SerializeField] private TextMeshProUGUI questDest;
    [SerializeField] private TextMeshProUGUI currentRequire;
    private Quest q;

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.SetUpQuest , setUpQuestInforHandler);

        if(q == null)
        {
            questDest.text = "Không có nhiệm  vụ ở thời điểm hiện tại";
            currentRequire.text = "0/0";
        }
    }

    private void OnDisable()
    {
        Observer.Instance.unsubscribeListener(GameEvent.SetUpQuest, setUpQuestInforHandler);
    }

    private void setUpQuestInforHandler(object value)
    {
        q = (Quest)value;
        if(q != null)
        {
            nameQuest.text = q.nameQuest;
            questDest.text = q.desQuest;
            currentRequire.text = $"{q.currentQuest}/{q.requireQuest}";
        }
    }
}
