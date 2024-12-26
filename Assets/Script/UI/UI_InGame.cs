using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
    private PlayerStats myStats;
    void Start()
    {
        myStats = PlayerManager.Instance.playerStats;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();
    }

    private void updateHealthBar()
    {
        mySlider.maxValue = myStats.getMaxHealth();
        mySlider.value = myStats.currentHealth;
    }

    private void setCoolDownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    private void checkCoolDownOf(Image _image, float _coolDown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
        }
    }
}
