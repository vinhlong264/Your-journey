using UnityEngine;

namespace newQuestSystem
{
    [System.Serializable]
    public class Quest
    {
        public int branchStory;
        public string nameQuest; // tên quest
        public string desQuest; // thông tin quest
        public int expReward; // phần thưởng exp
        public int goldReward; // phần thường gold


        public int requireQuest;
        public string enemyType;
        public int qip; // quest ở tiến trình hay ở nhánh truyện nào
        public bool compelete;
        public bool isExcute;
        public int currentQuest = 0;
        
        public void SetQuest()
        {
            currentQuest++;
            if(currentQuest >= requireQuest)
            {
                isExcute = false;
                compelete = true;
            }
        }
    }

    [System.Serializable]
    public class BranchStory
    {
        [SerializeField] private int branchID;
        [SerializeField] private string branchName;
        [SerializeField] private int qip;
        [SerializeField] private int process;
        public int BranchID { get => branchID; }
        public int Qip { get => qip; }
        public int Process { get => process; }

        public BranchStory(int branchID, string branchName, int process , int qip)
        {
            this.branchID = branchID;
            this.branchName = branchName;
            this.process = process;
            this.qip = qip;
        }

        public void SetProcess()
        {
            process++;
        }

        public void SetQuestInProcess()
        {
            qip++;
        }
    }
}

