using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
    private PlayerStats myStats;

    [SerializeField] private Image dashCoolDownImage;
    [SerializeField] private Image dashUIActive;

    void Start()
    {
        myStats = PlayerManager.Instance.playerStats;
        myStats.onUiHealth += updateHealthBar;
        updateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash_skill.dashUnlock)
        {
            setCoolDownOf(dashCoolDownImage);
        }

        checkCoolDownOf(dashCoolDownImage, SkillManager.instance.dash_skill.CoolDown);
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
