using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;


    private PlayerStats myStats;

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.UpdateCurrentExp , updateCurrentExp);
    }

    void Start()
    {
        myStats = GameManager.Instance.playerStats;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();

        float percentExp = (float)levelData.level.currentExp / levelData.level.expToNextLevel * 100f;
        levelTxt.text = $"Lv. {levelData.level.level} + {percentExp:F1}%";
    }

    private void updateHealthBar()
    {
        healthBarSlider.maxValue = myStats.getMaxHealth();
        healthBarSlider.value = myStats.currentHealth;
    }

    public void updateCurrentExp(object value)
    {
        float percentExp = (float)levelData.level.currentExp / levelData.level.expToNextLevel * 100f;
        levelTxt.text = $"Lv. {levelData.level.level} + {percentExp:F1}%";
    }
}
