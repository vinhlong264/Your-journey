using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI levelTxt;


    private PlayerStats myStats;

    private void OnEnable()
    {
        //Observer.Instance.subscribeListener(GameEvent.UpdateCurrentExp, updateCurrentExp);
    }


    void Start()
    {
        myStats = PlayerManager.Instance.playerStats;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();
        levelTxt.text = $"Lv. {PlayerManager.Instance.CurrentLevel} + {PlayerManager.Instance.CurrentExp * 0.1}%";
    }

    private void updateHealthBar()
    {
        healthBarSlider.maxValue = myStats.getMaxHealth();
        healthBarSlider.value = myStats.currentHealth;
    }

    public void updateCurrentExp(object value)
    {
        levelTxt.text = $"Lv. {PlayerManager.Instance.CurrentLevel} + {(float)value * 0.1}%";
    }
}
