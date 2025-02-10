using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace newQuestSystem
{
    public class QuestManager : Singleton<QuestManager>, IQuest
    {
        [Header("List Quest")]
        [SerializeField] private List<BranchStory> stories;
        [SerializeField] private List<Quest> allQuest = new List<Quest>();
        [SerializeField] private List<Quest> allQuestMain = new List<Quest>();
        [SerializeField] private List<Quest> allQuestExtra = new List<Quest>();

        [SerializeField] private Quest currentQuest; // Quest hiện tại đang thực thi
        [SerializeField] private Quest prevCurrentQuest;
        public System.Action<EnemyType> excuteQuestEvent;

        protected override void Awake()
        {
            MakeSingleton(true);

            excuteQuestEvent += ExcuteRequireQuest;
        }

        private void Start()
        {
            stories = new List<BranchStory>()
            {
                new BranchStory(0, "MainStory"  , 0),
                new BranchStory(1 , "ExtraStory", 0)
            };
            LoadData();
        }


        private void LoadData()
        {
            TextAsset datatTextAsset = Resources.Load<TextAsset>("TextData/subQuest");
            string[] data = datatTextAsset.text.Split("\n");
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == " ")
                {
                    continue;
                }

                string[] cols = data[i].Split("\t");
                Quest quest = new Quest();
                quest.branchStory = int.Parse(cols[0]);
                quest.nameQuest = cols[1];
                quest.desQuest = cols[2];
                quest.expReward = int.Parse(cols[3]);
                quest.goldReward = int.Parse(cols[4]);
                quest.requireQuest = int.Parse(cols[5]);
                quest.enemyType = cols[6];
                quest.qip = int.Parse(cols[7]);
                allQuest.Add(quest);
            }
        }

        public Quest GetQuest(int branchID)
        {
            int qip = GetQipStory(branchID);
            Quest getQuest = allQuest.FirstOrDefault(x => x.qip == qip);
            if (getQuest != null)
            {
                Debug.Log("Lấy ra quest: " + getQuest.nameQuest);
                return getQuest;
            }

            return null;
        }


        public BranchStory GetBranchStory(int branchID)
        {
            BranchStory getStory = stories.FirstOrDefault(x => x.BranchID == branchID);
            if (getStory != null)
            {
                return getStory;
            }

            return null;
        }

        private int GetQipStory(int branchID)
        {
            BranchStory getStory = stories.FirstOrDefault(x => x.BranchID == branchID);
            if (getStory != null)
            {
                return getStory.Qip;
            }

            return 0;
        }


        public void setQuest(Quest q)
        {
            if (q != null) currentQuest = q;
        }


        public void changeQuest(Quest newQuest)
        {
            prevCurrentQuest = currentQuest;

            currentQuest = newQuest;
            if (currentQuest != null && prevCurrentQuest != null)
            {
                currentQuest.isExcute = true;
                prevCurrentQuest.isExcute = false;
            }
        }



        public void ReceiveQuest(Quest q)
        {
            if (q == null) return;

            if (q.qip == 0)
            {
                allQuestMain.Add(q);
                currentQuest = q;
                ExcuteQuest();
            }
            else if (q.qip == 1)
            {
                allQuestExtra.Add(q);
            }
        }

        public void ExcuteQuest()
        {
            if (currentQuest != null) currentQuest.isExcute = true;
        }

        private void ExcuteRequireQuest(EnemyType typeID)
        {
            if (currentQuest != null)
            {
                if (currentQuest.enemyType == typeID.ToString() && currentQuest.isExcute)
                {
                    if (currentQuest.compelete) return;

                    currentQuest.SetQuest();
                    Debug.Log("CallBack: " + currentQuest.currentQuest);
                }
            }
        }


        public void CompeleteQuest(Quest q)
        {
            if (q.compelete)
            {
                Observer.Instance.NotifyEvent(GameEvent.RewardExp, q.expReward);
                BranchStory getStory = stories.FirstOrDefault(x => x.Qip == q.qip);

                if (getStory != null)
                {
                    getStory.SetQuestInProcess();
                    allQuestMain.Remove(q);
                }

            }
        }
    }
}


