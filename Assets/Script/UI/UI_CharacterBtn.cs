using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CharacterBtn : MonoBehaviour
{
    [SerializeField] private GameObject[] uiBtn;

    private void Start()
    {
        for(int i = 0; i < uiBtn.Length; i++)
        {
            uiBtn[i].gameObject.SetActive(false);
        }

        uiBtn[0].SetActive(true);
    }

    public void switchUI(GameObject active)
    {
        for(int i = 0; i < uiBtn.Length; i++)
        {
            uiBtn[i].SetActive(false);
        }

        if(active != null)
        {
            active.SetActive(true);
        }
    }
}
