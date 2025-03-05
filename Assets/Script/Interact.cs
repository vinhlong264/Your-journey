using newQuestSystem;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] private DialogueSystem _dialogueSystem;
    [SerializeField] private Button interactBtn;
    private int branchID;
    private string storyDataTxt;
    [SerializeField] private int process;

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
        process = QuestManager.Instance.GetProcessStory(branchID);
        Debug.Log(process);
        Quest questCurrent = QuestManager.Instance.GetQuest(branchID);

        switch (process)
        {
            case 0:
                result = "TextData/Story 1";
                break;
            case 1:
                result = "TextData/Story 2";
                break;
        }

        return result;

    }
}
