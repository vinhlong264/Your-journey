using UnityEngine;
using UnityEngine.UI;

public abstract class UI_SkillHotKeyAbstract : MonoBehaviour
{
    [SerializeField] protected Skill_HotKey skillType; // Loại skill
    [SerializeField] protected KeyCode SkillHotKey;
    [SerializeField] protected Image skillImageCoolDown; // UI coolDown

    [SerializeField] protected Image skillImage;
    protected SkillManager skill;
    protected Inventory inventory;

    protected virtual void OnEnable()
    {
        skill = GameManager.Instance.Skill;
    }

    protected virtual void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    private void OnValidate()
    {
        this.gameObject.name = "UiHotKey_" + skillType.ToString();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandelInput();
    }

    protected void setCoolDownOf(Image _image)
    {
        if (_image == null) return;

        if(_image.fillAmount <= 0)
        {
            _image.fillAmount = 1f;
        }
    }

    protected virtual void checkOfCoolDownOf(Image _image , float _coolDown)
    {
        if(_image == null) return;

        if(_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _coolDown * Time.deltaTime;
        }
    }

    protected abstract void HandelInput();
}
