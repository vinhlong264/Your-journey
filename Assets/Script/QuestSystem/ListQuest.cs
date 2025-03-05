using newQuestSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameQuest;
    [SerializeField] private TextMeshProUGUI descriptionQuest;
    [SerializeField] private TextMeshProUGUI currentRequireQ;

    public void ShowDes(Quest q)
    {
        if(q == null) return;
        nameQuest.text = q.nameQuest;
        descriptionQuest.text = q.desQuest;
        currentRequireQ.text = $"{q.currentQuest} / {q.requireQuest}";
    }
}
