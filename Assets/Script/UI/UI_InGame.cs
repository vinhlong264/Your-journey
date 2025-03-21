using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;


    private PlayerStats myStats;
    private LevelSystem myLevel;

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.UpdateCurrentExp, updateCurrentExp);
    }

    void Start()
    {
        if (levelTxt == null) return;

        myStats = GameManager.Instance.PlayerStats;
        myLevel = GameManager.Instance.PlayerLevel.Level;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();

        float percentExp = (float)myLevel.currentExp / myLevel.expToNextLevel * 100f;
        levelTxt.text = $"Lv. {myLevel.level} + {percentExp:F1}%";
    }

    private void updateHealthBar()
    {
        healthBarSlider.maxValue = myStats.getMaxHealth();
        healthBarSlider.value = myStats.currentHealth;
    }

    public void updateCurrentExp(object value)
    {
        float percentExp = (float)myLevel.currentExp / myLevel.expToNextLevel * 100f;
        levelTxt.text = $"Lv. {myLevel.level} + {percentExp:F1}%";
    }
}
