using newQuestSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuesLog : MonoBehaviour
{
    [Header("List Quest")]
    [SerializeField] private List<Quest> mainQuest;
    [SerializeField] private List<Quest> extraQuest;

    [Header("Button")]
    [SerializeField] private Button mainQuestBtn;
    [SerializeField] private Button extraQuestBtn;
    [SerializeField] private Button excuteQuestBtn;

    [Header("Quest Select")]
    [SerializeField] private Transform questSelectParent;
    private List<QuestSelect> questSelects = new List<QuestSelect>();
    private Quest questSelected;

    private void Start()
    {
        mainQuest = QuestManager.Instance.GetAllQuestMain();
        extraQuest = QuestManager.Instance.GetAllQuestExtra();
        for(int i = 0; i < questSelectParent.childCount; i++)
        {
            questSelects.Add(questSelectParent.GetChild(i).GetComponent<QuestSelect>());
        }
        


        MainQuestHandler();
        excuteQuestBtn.onClick.AddListener(() => ExcuteQuestHandler());
    }

    public void setQuestSelect(Quest newQuestSelect)
    {
        if(newQuestSelect == null)
        {
            Debug.Log("No quest");
            return;
        }

        questSelected = newQuestSelect;
    }


    #region Button handler
    private void MainQuestHandler()
    {
        if (questSelects.Count == 0) return;
        if(mainQuest.Count == 0) return;

        if(questSelects.Count == 1 && mainQuest.Count == 1)
        {
            questSelects[0].setUpQuest(mainQuest[0]);
            return;
        }

        for(int i = 0; i < mainQuest.Count; i++)
        {
            Debug.Log(i+": "+mainQuest[i].desQuest);
            questSelects[i].setUpQuest(mainQuest[i]);
        }
    }


    private void ExcuteQuestHandler()
    {
        if (questSelected == null) return;
        QuestManager.Instance.setQuestExcute(questSelected);
    }
    #endregion
}
