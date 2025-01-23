using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private List<Conversation> listConverStation = new List<Conversation>();
    [SerializeField] private TextMeshProUGUI contentTxt;
    [SerializeField] private GameObject chatBoxObj;
    private int currentIndex = 0;


    void Start()
    {

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
        chatBoxObj.SetActive(true);
        if(currentIndex < listConverStation.Count)
        {
            contentTxt.text = listConverStation[currentIndex].content;
        }
        else
        {
            QuestSystem.Instance.setQipStory(1);
            var getQuest = QuestSystem.Instance.GetQuest(2);
            if(getQuest != null)
            {
                Debug.Log("Quest tồn tại");
            }
            Debug.Log("Quest không tồn tại");

            gameObject.SetActive(false);
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
