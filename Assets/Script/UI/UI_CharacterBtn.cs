using TMPro;
using UnityEngine;

public class UI_CharacterBtn : MonoBehaviour
{
    [SerializeField] private GameObject[] uiBtn;

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI pointExpText;
    [SerializeField] private TextMeshProUGUI pointSkillText;

    private LevelSystem level;

    private void Start()
    {
        level = GameManager.Instance.PlayerLevel.Level;

        UpdateText();

        for (int i = 0; i < uiBtn.Length; i++)
        {
            uiBtn[i].gameObject.SetActive(false);
        }

        uiBtn[0].SetActive(true);
    }

    private void UpdateText()
    {
        pointExpText.text = "Điểm kinh nghiệm: " + level.pointAtributte;
        pointSkillText.text = "Điềm kĩ năng: " + level.pointSkill;
    }

    public void switchUI(GameObject active)
    {
        for (int i = 0; i < uiBtn.Length; i++)
        {
            uiBtn[i].SetActive(false);
        }

        if (active != null)
        {
            active.SetActive(true);
        }
    }
}
