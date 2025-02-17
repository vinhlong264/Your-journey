using UnityEngine;
using UnityEngine.UI;

public abstract class UI_SkillHotKeyAbstract : MonoBehaviour
{
    [SerializeField] protected Skilltype skillType; // Loại skill
    [SerializeField] protected KeyCode SkillHotKey;
    [SerializeField] protected Image skillImageCoolDown; // UI coolDown

    [SerializeField] protected Image skillImage;
    protected SkillManager skill;

    protected virtual void OnEnable()
    {
        skill = SkillManager.instance;
    }

    protected virtual void Start()
    {
        //skill = SkillManager.instance;
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
