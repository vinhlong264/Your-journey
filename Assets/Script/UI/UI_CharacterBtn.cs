using TMPro;
using UnityEngine;

public class UI_CharacterBtn : MonoBehaviour
{
    [SerializeField] private LevelSO levelData;
    [SerializeField] private GameObject[] uiBtn;

    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI pointExpText;
    [SerializeField] private TextMeshProUGUI pointSkillText;

    private void Start()
    {
        pointExpText.text = "Điểm kinh nghiệm: " + levelData.levelSystem.pointAtributte;
        pointSkillText.text = "Điềm kĩ năng: " + levelData.levelSystem.pointSkill;

        for (int i = 0; i < uiBtn.Length; i++)
        {
            uiBtn[i].gameObject.SetActive(false);
        }

        uiBtn[0].SetActive(true);
    }

    private void Update()
    {
        pointExpText.text = "Điểm kinh nghiệm: " + levelData.levelSystem.pointAtributte;
        pointSkillText.text = "Điềm kĩ năng: " + levelData.levelSystem.pointSkill;
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
