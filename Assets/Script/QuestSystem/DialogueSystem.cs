using newQuestSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<Conversation> listConverStation = new List<Conversation>();
    [SerializeField] private QuestPanel questPanel;
    [SerializeField] private int branchStoryID;
    [SerializeField] private string storyDataTxt;

    [Header("UI Dialogue")]
    [SerializeField] private TextMeshProUGUI contentTxt;
    [SerializeField] private GameObject chatBox;
    [SerializeField] private GameObject questBox;
    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private GameObject npcAvatar;
    private int currentIndex = 0;

    void Start()
    {

    }

    public void setUpDialogue(int _branchStory, string _storyDataTXt)
    {
        branchStoryID = _branchStory;
        storyDataTxt = _storyDataTXt;
        LoadText(storyDataTxt);

        Dialogue();
    }

    public void LoadText(string _path)
    {
        TextAsset textData = Resources.Load<TextAsset>(_path);
        string[] lines = textData.text.Split("\n");
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) // Bỏ qua khoảng trắng
                continue;

            string[] cols = lines[i].Split("\t");

            Conversation conversation = new Conversation();
            conversation.id = System.Convert.ToInt32(cols[0]);
            conversation.character = cols[1].Trim();
            conversation.content = cols[2].Trim();

            listConverStation.Add(conversation);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentIndex++;
        Dialogue();
    }

    public void Dialogue()
    {
         chatBox.SetActive(true);
        if (currentIndex < listConverStation.Count)
        {
            if (listConverStation[currentIndex].character == "Player")
            {
                npcAvatar.SetActive(false);
                playerAvatar.SetActive(true);
            }
            else if (listConverStation[currentIndex].character == "NPC")
            {
                playerAvatar.SetActive(false);
                npcAvatar.SetActive(true);
            }
            contentTxt.text = listConverStation[currentIndex].content;
        }
        else
        {
            chatBox.SetActive(false);
            Quest q = QuestManager.Instance.GetQuest(branchStoryID);

            if (q == null) return;
            questPanel.ShowQuest(q);
            questBox.SetActive(true);
        }
    }
}

[System.Serializable]
public class Conversation
{
    public int id;
    public string character;
    public string content;
}
