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

    [Header("Quest Select")]
    [SerializeField] private Transform questSelectParent;
    private List<QuestSelect> questSelects = new List<QuestSelect>();

    private void Start()
    {
        mainQuest = QuestManager.Instance.GetAllQuestMain();
        extraQuest = QuestManager.Instance.GetAllQuestExtra();
        for(int i = 0; i < questSelectParent.childCount; i++)
        {
            questSelects.Add(questSelectParent.GetChild(i).GetComponent<QuestSelect>());
        }
        


        MainQuestHandler();
    }

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




}
