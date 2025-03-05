using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Active window")]
    [SerializeField] private GameObject ui_Character;
    [SerializeField] private GameObject ui_Ingame;
    [SerializeField] private GameObject ui_QuestLog;

    private void Start()
    {
        ui_Ingame.SetActive(true);
    }

    public void SwitchCharacter()
    {
        if (ui_Ingame.activeSelf)
        {
            ui_Ingame.SetActive(false);
            ui_Character.SetActive(true);
        }
        else
        {
            ui_Character.SetActive(false);
            ui_Ingame.SetActive(true);
        }
    }

    public void SwitchQuestLog()
    {
        if (ui_Ingame.activeSelf)
        {
            ui_Ingame.SetActive(false);
            ui_QuestLog.SetActive(true);
        }
        else
        {
            ui_QuestLog.SetActive(false);
            ui_Ingame.SetActive(true);
        }
    }
}
