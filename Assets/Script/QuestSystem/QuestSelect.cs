using newQuestSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelect : MonoBehaviour
{
    [Header("Quest infor")]
    [SerializeField] private TextMeshProUGUI nameQuest; // name quest
    [SerializeField] private TextMeshProUGUI descriptionQuest; // thông tin của quest

    [Header("Button")]
    [SerializeField] private Button selectQuestBtn;
    [SerializeField] private Button excuteQuestBtn;

    private Quest q;

    private void OnEnable()
    {
        selectQuestBtn.onClick.AddListener(() => SelectQuestHandler()); // Button để chọn Quest
        excuteQuestBtn.onClick.AddListener(() => 
        {
            Debug.Log("Call back event");
            ExcuteQuestHandler();
        });
    }

    private void OnDisable()
    {
        selectQuestBtn.onClick.RemoveAllListeners();
        excuteQuestBtn.onClick.RemoveAllListeners();
    }

    public void setUpQuest(Quest newQuest) // set up dữ liệu cho quest ở quest log
    {
        if (newQuest == null) return;
        this.q = newQuest;

        Debug.Log(q.nameQuest);
        nameQuest.text = q.nameQuest;
        descriptionQuest.text = q.desQuest;
    }

    private void SelectQuestHandler() // chọn quest ở quest log
    {
        if(q == null) return;
        
        descriptionQuest.text = q.desQuest;
    }

    private void ExcuteQuestHandler() // thực thi quest gì đó
    {
        if(q == null) return;
        QuestManager.Instance.setQuestExcute(q);
    }


}
