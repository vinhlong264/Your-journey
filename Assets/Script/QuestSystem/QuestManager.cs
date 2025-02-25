﻿using System.Collections.Generic;
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
        }

        private void OnEnable()
        {
            excuteQuestEvent += ExcuteQuest;
        }

        private void Start()
        {
            stories = new List<BranchStory>()
            {
                new BranchStory(0, "MainStory" ,0  , 0),
                new BranchStory(1 , "ExtraStory", 0 , 0)
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

        public int GetProcessStory(int _branchID) // Lấy ra Process ở cốt truyện hiện tại
        {
            return stories.FirstOrDefault(x => x.BranchID == _branchID).Process;
        }

        public Quest GetQuest(int _branchID) // Lấy ra quest cần thực hiện ở nhánh truyện hiện tại
        {
            int qip = stories.FirstOrDefault(x => x.BranchID == _branchID).Qip;
            Quest getQ = allQuest.FirstOrDefault(x => x.branchStory == _branchID && x.qip == qip);
            if (getQ != null)
            {
                return getQ;
            }
            return null;
        }

        public void ReceiveQuest(Quest q)
        {
            if (q == null) return;

            if(q.branchStory == 0)
            {
                allQuestMain.Add(q);
            }
            else if(q.branchStory == 1)
            {
                allQuestExtra.Add(q);
            }
            BranchStory getStory = stories.FirstOrDefault(x => x.Qip == q.qip);
            if(getStory != null)
            {
                getStory.SetProcess();
            }

        }

        public void ExcuteQuest(EnemyType _type)
        {
            if (currentQuest == null) return;

            if (currentQuest.enemyType == _type.ToString())
            {
                currentQuest.SetQuest();
            }
        }

        public void CompeleteQuest(Quest q)
        {
            if(q == null) return;

            if (q.compelete)
            {
                BranchStory getStory = stories.FirstOrDefault(x => x.Qip == q.qip);
                if (getStory != null)
                {
                    getStory.SetQuestInProcess();
                }
            }
        }
    }
}


