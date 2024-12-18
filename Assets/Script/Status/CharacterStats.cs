using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamagePhysical, IDameMagical
{
    #region Variable
    private EntityFx fx;

    [Header("Major status info")]
    public Stats strength; // với mỗi điểm nâng cấp thì dame cơ bản sẽ tăng lên 1 và 1% sát thương chí mạng
    public Stats ability; // với mỗi điểm nâng cấp thì sẽ tăng các kĩ năng ví dụ là 1% né và 1% chí mạng
    public Stats inteligent; // với mỗi điểm nâng cấp thì sẽ tăng 1 sức mạnh phép thuật và 3% giáp phép
    public Stats vitality; // với mỗi điểm nâng cấp sẽ tăng hp, 3% giáp vật lý

    [Header("Offensive status info")]
    public Stats dame; //dame vật lý
    public Stats critRate; // tỉ lệ chí mạng
    public Stats critPower; // DefaultValue là 150%

    [Header("Defend status info")]
    public Stats maxHealth; // chỉ số máu tối đa
    public Stats armor; // chỉ số giáp
    public Stats evasion; // chỉ số né chiêu
    public Stats magicResistance; // chỉ số giáp phép

    [Header("Magic status info")]
    public Stats fireDame;
    public Stats iceDame;
    public Stats lightingDame;

    [SerializeField] private float ailmentDuration;
    [SerializeField] private bool isIngnite; // Gây dame cháy liên tục trong 1 khoảng thời gian
    [SerializeField] private bool isChill; // Giảm giáp đi 20%
    [SerializeField] private bool isShocked; // Giảm khả năng đánh chính xác đi 20%

    private float ingniteTimer; // thời gian hiệu ứng cháy
    private float chillTimer; // Thời gian hiệu ứng làm chậm
    private float shockTimer; // Thời gian hiệu ứng sốc

    //Dame effect burn
    private int ingniteDame; // dame cháy
    private float ingniteDameTimer; // thời gian gây dame liên tục
    private float ingniteDameCoolDown = 1f; // thời gian hồi

    //Dame effect lighting
    protected int strikeDame;
    [SerializeField] protected GameObject thunerPrefabs;
    protected Transform closestToEnemy;

    [Space]
    public int currentHealth;

    public System.Action onUiHealth; //Event health bar

    protected float expReceive;

    #endregion

    protected virtual void Start()
    {
        critPower.setDfaultValue(150);
        currentHealth = getMaxHealth();
        fx = GetComponent<EntityFx>();
    }


    protected virtual void Update()
    {
        ingniteTimer -= Time.deltaTime;
        chillTimer -= Time.deltaTime;
        shockTimer -= Time.deltaTime;

        ingniteDameTimer -= Time.deltaTime;

        if (chillTimer < 0)
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

        applyIngniteDame(); // gây dame cháy mỗi giây
    }


    public void increaseModfierStatus(int _amount, float _duration, Stats _status) // buff stat
    {
        StartCoroutine(addModifierStatus(_amount, _duration, _status));
    }

    IEnumerator addModifierStatus(int _amount, float _duration, Stats _status)
    {
        _status.addModifiers(_amount); // Nhận buff
        yield return new WaitForSeconds(_duration);
        _status.removeModifiers(_amount); // Xóa buff
    }




    #region calculate dame magic and appy ailment
    public virtual void doDameMagical(CharacterStats _targetStatus) // Quản lý việc gây dame phép
    {
        int _fireDame = getFireDame();
        int _iceDame = getIceDame();
        int _lightingDame = getLightingDame();

        int totalMagicDame = checkTargetMagicResistance(_targetStatus, _fireDame, _iceDame, _lightingDame);
        Debug.Log(this.name+" Dame Magical: "+totalMagicDame);
        _targetStatus.takeDame(totalMagicDame);

        //Logic add ailment
        if (Mathf.Max(_fireDame, _iceDame, _lightingDame) <= 0) return;

        logicCanApplyAilment(_targetStatus, _fireDame, _iceDame, _lightingDame);

    }

    //Hàm tính toán nhận dame phép
    protected int checkTargetMagicResistance(CharacterStats _targetStatus, int _fireDame, int _iceDame, int _lightingDame)
    {
        int _magicDame = _fireDame + _iceDame + _lightingDame;

        int _magicResistance = Mathf.RoundToInt((_targetStatus.magicResistance.getValue() + _targetStatus.inteligent.getValue()) * 0.03f);

        _magicDame -= _magicResistance;
        Debug.Log(this.name +" magic dame 3 kind: "+_magicDame);

        _magicDame = Mathf.Clamp(_magicDame, 0, int.MaxValue);
        return _magicDame;
    }

    protected int getFireDame() => fireDame.getValue() + inteligent.getValue();
    protected int getIceDame() => iceDame.getValue() + inteligent.getValue();
    protected int getLightingDame() => lightingDame.getValue() + inteligent.getValue();

    protected virtual void logicCanApplyAilment(CharacterStats _targetStatus, int _fireDame, int _iceDame, int _lightingDame)  // logic apply Ailment
    {
        bool canApplyIngnite = _fireDame > _iceDame && _fireDame > _lightingDame;
        bool canApplyChill = _iceDame > _fireDame && _iceDame > _lightingDame;
        bool canApplyShock = _lightingDame > _iceDame && _lightingDame > _fireDame;

        //ApplyRadomAilment(_targetStatus, _fireDame, _iceDame, _lightingDame, ref canApplyIngnite, ref canApplyChill, ref canApplyShock);

        if (canApplyIngnite)
        {
            _targetStatus.setUpIngnite(Mathf.RoundToInt(_fireDame * 0.2f));
        }


        if (canApplyShock)
        {
            _targetStatus.setUpDameLinghting(Mathf.RoundToInt(_lightingDame * 0.2f));
        }

        _targetStatus.aplyAilment(canApplyIngnite, canApplyChill, canApplyShock);
    }

    //Logic apply ngẫu nhiên các Ailment khi các chỉ số dame phép bằng nhau
    protected void ApplyRadomAilment(CharacterStats _targetReceive, int _fireDame, int _iceDame, int _lightingDame, ref bool canApplyIngnite, ref bool canApplyChill, ref bool canApplyShock)
    {
        while (!canApplyIngnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < 0.5f && _fireDame > 0)
            {
                canApplyIngnite = true;
                _targetReceive.aplyAilment(canApplyIngnite, canApplyChill, canApplyShock);
                return;
            }

            if (Random.value < 0.5f && _iceDame > 0)
            {
                canApplyChill = true;
                _targetReceive.aplyAilment(canApplyIngnite, canApplyChill, canApplyShock);
                return;
            }

            if (Random.value < 0.5f && _lightingDame > 0)
            {
                canApplyShock = true;
                _targetReceive.aplyAilment(canApplyIngnite, canApplyChill, canApplyShock);
                return;
            }
        }
    }

    public virtual void aplyAilment(bool _ingnite, bool _chill, bool _shocked) // nhận các hiệu ứng gây hại
    {

        bool canAplyIngnite = !isChill && !isShocked;
        bool canAplyChill = !isIngnite && !isShocked;
        bool canAplyShock = !isIngnite && !isChill;

        if (_ingnite && canAplyIngnite) //Apply effect burn
        {
            isIngnite = _ingnite;
            ingniteTimer = ailmentDuration;
            fx.ingniteColorFor(ailmentDuration);
        }

        if (_chill && canAplyChill) //Apply effect Slow
        {
            isChill = _chill;
            chillTimer = ailmentDuration;
            GetComponent<Entity>().slowEntityBy(0.2f, ailmentDuration);
            fx.chillColorFor(ailmentDuration);
        }

        if(_shocked && canAplyShock) // Apply effect shock
        {
            if (!isShocked)
            {
                AplyShock(_shocked);
            }
            else
            {
                if (GetComponent<Player>() != null) return;

                effectStrikeEnemyClosest();
            }
        }
    }

    public void setUpIngnite(int _dame) => ingniteDame = _dame;// quản lý dame của ingniteDame

    protected virtual void applyIngniteDame() // gây dame hiệu ứng liên tục của ingnite
    {
        if (ingniteDameTimer < 0 && isIngnite)
        {
            decreaseHealthBy(ingniteDame);
            Debug.Log(ingniteDame);

            if (currentHealth <= 0)
            {
                Die();
            }

            ingniteDameTimer = ingniteDameCoolDown;
        }
    }
    public virtual void AplyShock(bool _shocked)
    {
        if (isShocked)
        {
            return;
        }

        shockTimer = ailmentDuration;
        isShocked = _shocked;

        fx.shockColorFor(ailmentDuration);
    }
    public void setUpDameLinghting(int _dame) =>strikeDame = _dame; // quản lý dame của lighting

    protected virtual void effectStrikeEnemyClosest() // gây hiệu ứng sét giật với enemy gần nhất
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float distanceToClosest = Mathf.Infinity;
        closestToEnemy = null;

        foreach (var hit in coliders)
        {
            if (hit.GetComponent<EnemyStats>() != null && Vector2.Distance(transform.position, hit.transform.position) < 0.1f)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < distanceToClosest)
                {
                    distanceToClosest = distance;
                    closestToEnemy = hit.transform;
                }
            }
        }
        if (closestToEnemy != null)
        {
            GameObject newThunder = Instantiate(thunerPrefabs, closestToEnemy.position, Quaternion.identity);
            newThunder.GetComponent<sockThunderCtrl>().setUpThunder(strikeDame, closestToEnemy.GetComponent<EnemyStats>());
        }
    }

    #endregion

    #region calculate dame physics
    public virtual void DoDamePhysical(CharacterStats _targetReceive) // Quản lý việc gây dame vật lý
    {
        if (AvoidAttack(_targetReceive)) // kiểm tra việc tránh dame
        {
            _targetReceive.onEvasion();
            Debug.Log("Attack avoid");
            return;
        }
        int totalDame = dame.getValue() + strength.getValue();

        if (CanCrit()) // kiểm tra việc có thể chí mạng
        {
            totalDame = calculateCritalDame(totalDame);
        }

        totalDame = CheckTargetArmor(totalDame, _targetReceive);
        Debug.Log(this.name +" Total Dame Physical: " + totalDame);

        _targetReceive.takeDame(totalDame);
    }

    public virtual void applyBleedingHealth(bool _isBleeding)
    {
        
    }

    protected int CheckTargetArmor(int dame, CharacterStats _targetReceive) // hàm tính toán dame vật lý gây ra
    {
        if (_targetReceive.isChill) // nếu nhận hiệu ứng Chill thì sẽ giảm giáp
        {
            dame -= Mathf.RoundToInt(_targetReceive.armor.getValue() * 0.8f);
            Debug.Log("Dame after chill: " +dame);
        }
        else
        {
            dame -= _targetReceive.armor.getValue();
        }

        dame = Mathf.Clamp(dame, 0, int.MaxValue);

        return dame;
    }

    protected bool AvoidAttack(CharacterStats _target) // Tính toán tỉ lệ né đòn
    {
        int toltalEvasion = _target.evasion.getValue() + _target.ability.getValue();

        if (isShocked) // tăng khả năng né dame của target
        {
            toltalEvasion += 20;
        }

        return sytemRate(toltalEvasion);
    }
    

    protected bool CanCrit() // tính toán tỉ lệ chí mạng
    {
        int criticalRate = critRate.getValue() + ability.getValue();
        return sytemRate(criticalRate);
    }

    protected int calculateCritalDame(int _dame) // Tính toán sát thương chí mạng
    {
        float totalCriticalPower = (critPower.getValue() + strength.getValue()) * 0.01f;
        float finalDame = _dame * totalCriticalPower;

        return Mathf.RoundToInt(finalDame);
    }
    #endregion

    #region Dame impact and System rate
    protected bool sytemRate(int _value) // hệ thống xử lý tỉ lệ
    {
        if (Random.Range(0, 100) <= _value)
        {
            return true;
        }

        return false;
    }

    public int getMaxHealth()
    {
        return maxHealth.getValue() + vitality.getValue() * 5;
    }

    public int getMaxDame()
    {
        return dame.getValue() + strength.getValue();
    }

    public void restoreHealthBy(int _amountHeal)
    {
        currentHealth += _amountHeal;
        if(currentHealth > getMaxHealth())
        {
            currentHealth = getMaxHealth();
        }

        if(onUiHealth != null)
        {
            onUiHealth();
        }
    }

    public virtual void takeDame(int _dame) // Attack
    {
        decreaseHealthBy(_dame);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void decreaseHealthBy(int _dame) 
    {
        currentHealth -= _dame;
        if(onUiHealth != null)
        {
            onUiHealth();
        }
    }

    public virtual void onEvasion()
    {

    }

    protected virtual void Die()
    {

    }
    #endregion



}


[System.Serializable]
public class Stats // Class để chứa các thay đổi về chỉ số cơ bản
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


    public void addModifiers(int _modifier) // thêm thay đổi
    {
        modifiers.Add(_modifier);
    }

    public void removeModifiers(int _modifier) // xóa thay đổi
    {
        modifiers.Remove(_modifier);
    }
}
