using newQuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private Button interactBtn;
    private int branchID;
    private string storyDataTxt;

    private void Start()
    {
        branchID = 0;
        interactBtn.onClick.AddListener(() => InteractHandler());
    }

    private void InteractHandler()
    {
        int process = QuestManager.Instance.GetProcessStory(branchID);
       storyDataTxt = GetStoryTxt(process);


        _dialogueSystem.setUpDialogue(branchID, storyDataTxt);
    }


    private string GetStoryTxt(int Process)
    {
        string result = "";
        int process = QuestManager.Instance.GetProcessStory(branchID);
        Quest questCurrent = QuestManager.Instance.GetQuest(branchID);

        if(questCurrent != null && !questCurrent.compelete)
        {
            result =  "TextData/Story 3";
        }

        switch (process)
        {
            case 0:
                result = "TextData/Story 1";
                break;
            case 2:
                result = "TextData/Story 2";
                break;
        }

       return result;

    }
}
