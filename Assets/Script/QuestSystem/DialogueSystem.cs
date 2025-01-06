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
        //TextAsset textData = Resources.Load<TextAsset>("TextData/Story 1");
        //string[] lines = textData.text.Split("\n");
        //for(int i = 1; i < lines.Length; i++)
        //{
        //    if (string.IsNullOrWhiteSpace(lines[i]))
        //        continue; // Bỏ qua dòng trống

        //    string[] cols = lines[i].Split("\t");

        //    Conversation conversation = new Conversation();       
        //    conversation.id = System.Convert.ToInt32(cols[0]);
        //    conversation.character = cols[1].Trim();
        //    conversation.content = cols[2].Trim();

        //    listConverStation.Add(conversation);
        //}
    }

    public void LoadText(string _path)
    {
        TextAsset textData = Resources.Load<TextAsset>(_path);
        string[] lines = textData.text.Split("\n");
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue; // Bỏ qua dòng trống

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
            chatBoxObj.SetActive(false);
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
