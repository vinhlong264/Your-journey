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
        Observer.Instance.subscribeListener(GameEvent.UpdateUI , updateCurrentExp);
    }

    private void OnDisable()
    {
        Observer.Instance.unsubscribeListener(GameEvent.UpdateUI , updateCurrentExp);
    }

    void Start()
    {
        myStats = GameManager.Instance.playerStats;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();
        levelTxt.text = $"Lv. {GameManager.Instance.playerLevel.Level} + {GameManager.Instance.playerLevel.ExpCurrent * 0.1}%";
    }

    private void updateHealthBar()
    {
        healthBarSlider.maxValue = myStats.getMaxHealth();
        healthBarSlider.value = myStats.currentHealth;
    }

    public void updateCurrentExp(object value)
    {
        levelTxt.text = $"Lv. {GameManager.Instance.playerLevel.Level} + {GameManager.Instance.playerLevel.ExpCurrent * 0.1}%";
    }
}
