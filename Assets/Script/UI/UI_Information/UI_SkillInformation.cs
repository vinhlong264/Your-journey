using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SkillInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void showInformatioSkill(string _skillName , string _skillDescription)
    {
        skillName.text = _skillName;
        skillDescription.text = _skillDescription;


        gameObject.SetActive(true);
    }


    public void hideInformationWindow()
    {
        gameObject.SetActive(false);
    }

}
