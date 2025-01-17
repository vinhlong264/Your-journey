﻿using System.Collections.Generic;
using System.Linq;
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
        LoadTextAsset("TextData/subQuest");
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


    public Quest GetQuest(int _id)
    {
        var qip = GetQipStory(_id);
        var getQuest = allQuest.FirstOrDefault(x=> x.branchStoryID == _id && x.qip == qip);

        if(getQuest != null)
        {
            return getQuest;
        }
        else
        {
            return null;
        }
    }

    public int GetQipStory(int _branchID)
    {
        var getBranch = branchStory.FirstOrDefault(x => x.branchID ==  _branchID);
        if(getBranch != null)
        {
            return getBranch.questProgress;
        }
        else
        {
            return 0;
        }

    }

    public void setQipStory(int id)
    {
        var getQip = branchStory.FirstOrDefault(x=>x.branchID == id);
        if(getQip != null)
        {
            getQip.questProgress++;
        }
    }

    public void backQipStory(int id)
    {
        var getBranch = branchStory.FirstOrDefault(y => y.branchID == id);
        if (getBranch != null)
        {
            getBranch.questProgress--;
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
