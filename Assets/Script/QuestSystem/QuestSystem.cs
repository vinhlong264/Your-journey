using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : Singleton<QuestSystem>
{
    public List<BranchStory> branchStory = new List<BranchStory>(); // danh sách các nhánh truyện
    public List<Quest> allQuest = new List<Quest>(); // danh sách tất cả các quest
    public List<Quest> questReceive = new List<Quest>(); // danh sách các Quest đã nhận

    protected override void Awake()
    {
        MakeSingleton(true);
    }

    private void Start()
    {
        
    }

    public void LoadTextAsset(string path)
    {
        TextAsset textData = Resources.Load<TextAsset>(path);
        if (textData == null) return;

        string[] lines = textData.text.Split("\n");
        for(int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] cols = lines[i].Split("\t");
            Quest quest = new Quest();
            quest.branchStoryID = int.Parse(cols[0]);
            quest.qip = int.Parse(cols[1]);
            quest.name = cols[2];
            quest.description = cols[3];
            quest.expReward = int.Parse(cols[4]);
            quest.goldReward = int.Parse(cols[5]);
            quest.reqireQuest = int.Parse(cols[6]);

            allQuest.Add(quest);
        }
    }


}

[System.Serializable]
public class BranchStory
{
    public int branchID; // nhánh story
    public string name; // tên nhánh
    public int questProgress; // Quest đang thực thi
}

[System.Serializable]
public class Quest
{
    public int branchStoryID;
    public int qip; // Quest đang thực thi
    public string name;
    public string description;
    public int expReward;
    public int goldReward;
    public int reqireQuest; // yêu cầu của Quest
    public int currentQuest; // số lượng yêu cầu của Quest hiện tại
    public bool compeleteQuest; // Kiểm tra đã hoàn thành Quest chưa

    public void setCurrentQuest()
    {
        currentQuest++;
        if (currentQuest >= reqireQuest)
        {
            compeleteQuest = true;
        }
    }

}
