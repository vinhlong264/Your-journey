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
    [SerializeField] private GameObject questSelectPrefabs;
    [SerializeField] private Transform questSelectParent;
    private Quest questSelected;

    private void Start()
    {
        mainQuest = QuestManager.Instance.GetAllQuestMain();
        extraQuest = QuestManager.Instance.GetAllQuestExtra();
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
        GameObject questDump = Instantiate(questSelectPrefabs, questSelectParent.position, Quaternion.identity, questSelectParent);
        questDump.GetComponent<QuestSelect>().setUpQuest(mainQuest[0]);
    }


    private void ExcuteQuestHandler()
    {
        if (questSelected == null) return;
        Debug.Log("Excute Quest: " +questSelected);
        QuestManager.Instance.setQuestExcute(questSelected);
    }
    #endregion
}
