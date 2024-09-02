using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Major status info")]
    public Status strength; // với mỗi điểm nâng cấp thì dame cơ bản sẽ tăng lên 1 và 1% sát thương chí mạng
    public Status ability; // với mỗi điểm nâng cấp thì sẽ tăng các kĩ năng ví dụ là 1% né và 1% chí mạng
    public Status inteligent; // với mỗi điểm nâng cấp thì sẽ tăng 1 sức mạnh phép thuật và 3 giáp phép
    public Status vitality; // với mỗi điểm nâng cấp sẽ tăng hp, giáp vật lý, giáp phép

    [Header("Offensive status info")]
    public Status dame; //dame vật lý
    public Status critChance; // tỉ lệ chí mạng
    public Status critPower; // DefaultValue là 150%

    [Header("Defend status info")]
    public Status maxHealth; // chỉ số máu tối đa
    public Status armor; // chỉ số giáp
    public Status evasion; // chỉ số né chiêu
    public Status magicResistance;
    public int currentHealth;

    [Header("Magic status info")]
    public Status fireDame;
    public Status iceDame;
    public Status lightingDame;


    [SerializeField] private bool isIngnite; // Gây dame cháy liên tục trong 1 khoảng thời gian
    [SerializeField] private bool isChill; // Giảm giáp đi 20%
    [SerializeField] private bool isShocked; // Giảm khả năng đánh chính xác đi 20%

    private float ingniteTimer; // thời gian hiệu ứng 
    private float chillTimer;
    private float shockTimer;

    //Dame effect burn
    private int ingniteDame;
    private float ingniteDameTimer;
    private float ingniteDameCoolDown = 1f;


    public System.Action onUiHealth;




    protected virtual void Start()
    {
        critPower.setDfaultValue(150);
        currentHealth = getMaxHealth();
    }


    protected virtual void Update()
    {
        ingniteTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;

        ingniteDameTimer -= Time.deltaTime;

        if(chillTimer < 0)
        {
            isChill = false;
        }

        if (shockTimer < 0)
        {
            isShocked = false;
        }

        if (ingniteTimer < 0)
        {
            isIngnite = false;
        }

        if (ingniteDameTimer < 0 && isIngnite)
        {
            decreaseHealthBy(ingniteDame);
           
            if(currentHealth <= 0)
            {
                Die();
            }

            ingniteDameTimer = ingniteDameCoolDown;
        }
    }


    public virtual void DoDame(CharacterStatus _targetStatus) // Quản lý việc gây dame vật lý
    {
        if (AvoidAttack(_targetStatus))
        {
            Debug.Log("Attack avoid");
            return;
        }
        int totalDame = dame.getValue() + strength.getValue();

        if (CanCrit())
        {
            totalDame = calculateCritalDame(totalDame);
        }

        totalDame = CheckTargetArmor(totalDame, _targetStatus);

        _targetStatus.takeDame(totalDame);

        doDameMagical(_targetStatus);
    }


    public virtual void takeDame(int _dame) // Attack
    {
        decreaseHealthBy(_dame);
        if (currentHealth < 0)
        {
            Die();
        }
    }


    private void decreaseHealthBy(int _dame)
    {
        currentHealth -= _dame;
        if(onUiHealth != null)
        {
            onUiHealth();
        }
    }

    protected virtual void Die()
    {

    }


    public void doDameMagical(CharacterStatus _targetStatus) // Quản lý việc gây dame phép
    {
        int _fireDame = fireDame.getValue();
        int _iceDame = iceDame.getValue();
        int _lightingDame = lightingDame.getValue();
        int totalMagicDame = checkTargetMagicResistance(_targetStatus, _fireDame, _iceDame, _lightingDame);

        _targetStatus.takeDame(totalMagicDame);

        if (Mathf.Max(_fireDame, _iceDame, _lightingDame) <= 0) return;


        bool canAplyIngnite = _fireDame > _iceDame && _fireDame > _lightingDame;
        bool canAplyChill = _iceDame > _fireDame && _iceDame > _lightingDame;
        bool canAplyShock = _lightingDame > _iceDame && _lightingDame > _fireDame;

        while (!canAplyIngnite && !canAplyChill && !canAplyShock)
        {
            if (Random.value < 0.5f && _fireDame > 0)
            {
                canAplyIngnite = true;
                _targetStatus.aplyAilment(canAplyIngnite, canAplyChill, canAplyShock);
                return;
            }

            if (Random.value < 0.5f && _iceDame > 0)
            {
                canAplyChill = true;
                _targetStatus.aplyAilment(canAplyIngnite, canAplyChill, canAplyShock);
                return;
            }

            if (Random.value < 0.5f && _lightingDame > 0)
            {
                canAplyShock = true;
                _targetStatus.aplyAilment(canAplyIngnite, canAplyChill, canAplyShock);
                return;
            }
        }

        if(canAplyIngnite)
        {
            _targetStatus.setUpIngnite(Mathf.RoundToInt(_fireDame * 0.2f));
        }

        _targetStatus.aplyAilment(canAplyIngnite, canAplyChill, canAplyShock);

    }

    public void setUpIngnite(int _dame) => ingniteDame = _dame;


    public void aplyAilment(bool _ingnite, bool _chill, bool _shocked) // nhận các hiệu ứng gây hại
    {
        if (isIngnite || isChill || isShocked) return;

        if (_ingnite)
        {
            isIngnite = _ingnite;
            ingniteTimer = 4f;
        }

        if (_chill)
        {
            isChill = _chill;
            chillTimer = 3;
        }

        if (_shocked)
        {
            isShocked = _shocked;
            shockTimer = 3f;
        }
    }

    //Hàm tính toán dame phép gây ra
    private int checkTargetMagicResistance(CharacterStatus _targetStatus, int _fireDame, int _iceDame, int _lightingDame)
    {
        int magicDame = _fireDame + _iceDame + _lightingDame + inteligent.getValue();
        magicDame -= _targetStatus.magicResistance.getValue() + (_targetStatus.inteligent.getValue() * 3);

        magicDame = Mathf.Clamp(magicDame, 0, int.MaxValue);
        return magicDame;
    }

    private int CheckTargetArmor(int dame, CharacterStatus _target) // hàm tính toán dame vật lý gây ra
    {
        if (_target.isChill)
        {
            dame -= Mathf.RoundToInt(_target.armor.getValue() * 0.8f);
            Debug.Log("Dame after chill: " +dame);
        }
        else
        {
            dame -= _target.armor.getValue();
        }

        dame = Mathf.Clamp(dame, 0, int.MaxValue);

        return dame;
    }

    private bool AvoidAttack(CharacterStatus _target) // Tính toán tỉ lệ né đòn
    {
        int toltalEvasion = _target.evasion.getValue() + _target.ability.getValue();

        if (isShocked)
        {
            toltalEvasion += 20;
        }

        return sytemRate(toltalEvasion);
    }

    private bool CanCrit() // tính toán tỉ lệ chí mạng
    {
        int criticalRate = critChance.getValue() + ability.getValue();
        return sytemRate(criticalRate);
    }

    private int calculateCritalDame(int _dame) // Tính toán sát thương chí mạng
    {
        float totalCriticalPower = (critPower.getValue() + strength.getValue()) * 0.01f;
        float finalDame = _dame * totalCriticalPower;

        return Mathf.RoundToInt(finalDame);
    }


    private bool sytemRate(int _value) // hệ thống xử lý tỉ lệ
    {
        if (Random.Range(0, 100) <= _value)
        {
            return true;
        }

        return false;
    }


    public int getMaxHealth()
    {
        return maxHealth.getValue() + vitality.getValue();
    }
}




[System.Serializable]
public class Status // Class để chứa các thay đổi về chỉ số
{
    [SerializeField] private int baseValue; // chỉ số gốc 
    public List<int> modifiers = new List<int>(); // list các chỉ số dùng để thay đổi baseValue

    public int getValue() // lấy giá trị
    {
        int finalValue = baseValue;
        foreach (var modifier in modifiers)
        {
            finalValue += modifier;
        }

        return finalValue;
    }


    public void setDfaultValue(int _value) // đặt giá trị mặc định
    {
        baseValue = _value;
    }


    public void addModifiers(int _modifier) // thêm sự thay đổi
    {
        modifiers.Add(_modifier);
    }

    public void removeModifiers(int _modifier) // xóa sự thay đổi
    {
        modifiers.Remove(_modifier);
    }
}
