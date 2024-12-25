using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider mySlider;
    private PlayerStats myStats;

    [SerializeField] private Image dashCoolDownImage;
    [SerializeField] private Image parryCoolDownImage;
    [SerializeField] private Image crystalCoolDownImage;
    [SerializeField] private Image throwSwordCoolDownImage;
    [SerializeField] private Image UltimateCoolDownImage;

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

        if(Input.GetKeyDown(KeyCode.Q) && SkillManager.instance.parry_Skill.parryUnlock)
        {
            setCoolDownOf(parryCoolDownImage);
        }

        if(Input.GetKeyDown(KeyCode.E) && SkillManager.instance.crystal_skill.crystalSkillUnlocked)
        {
            setCoolDownOf(crystalCoolDownImage);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && SkillManager.instance.sword_Skill.throwSwordUnlocked)
        {
            setCoolDownOf(throwSwordCoolDownImage);
        }

        if(Input.GetKeyDown(KeyCode.R) && SkillManager.instance.blackHole_skill.blackHoleUnlocked)
        {
            setCoolDownOf(UltimateCoolDownImage);
        }

        checkCoolDownOf(dashCoolDownImage, SkillManager.instance.dash_skill.CoolDown);
        checkCoolDownOf(parryCoolDownImage , SkillManager.instance.parry_Skill.CoolDown);
        checkCoolDownOf(crystalCoolDownImage, SkillManager.instance.crystal_skill.CoolDown);
        checkCoolDownOf(throwSwordCoolDownImage, SkillManager.instance.sword_Skill.CoolDown);
        checkCoolDownOf(UltimateCoolDownImage, SkillManager.instance.blackHole_skill.CoolDown);
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
