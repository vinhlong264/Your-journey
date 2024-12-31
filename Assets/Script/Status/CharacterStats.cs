using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour, IDameHandlePhysical, IDameHandleMagical, IBuffStats, IHarmfulEffect
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
    [SerializeField] private bool isShocked;

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

        ApplyBurn(); // gây dame cháy mỗi giây
    }


    #region Buff stats
    public void increaseModfierStatus(int _amount, float _duration, Stats _status) // buff stat
    {
        StartCoroutine(addModifierStatus(_amount, _duration, _status));
    }

    IEnumerator addModifierStatus(int _amount, float _duration, Stats _status)
    {
        Debug.Log("Buff");
        BuffStats(_status, _amount); // Nhận buff
        yield return new WaitForSeconds(_duration);
        Debug.Log("Stop buff");
        StopBuffStats(_status, _amount); // Xóa buff
    }


    public void BuffStats(Stats stats , int amount)
    {
        stats.addModifiers(amount);
    }

    public void StopBuffStats(Stats stats, int amount)
    {
        stats.removeModifiers(amount);
    }

    #endregion

    #region calculate dame magic
    public void DameDoMagical(CharacterStats _statSender)
    {
        int _fireDame = _statSender.getFireDame();
        int _iceDame = _statSender.getIceDame();
        int _lightingDame = _statSender.getLightingDame();

        LogicApplyAilement(_fireDame, _iceDame, _lightingDame);

        int totalDame = CheckMagicResistance(_fireDame, _iceDame, _lightingDame);
        //Debug.Log($"{_statSender.name} ,Sender: {totalDame}");

        takeDame(totalDame);

    }

    public int CheckMagicResistance(int _fireDame, int _iceDame, int _lightingDame)
    {
        int _magicDame = _fireDame + _iceDame + _lightingDame;

        int _magicResistance = Mathf.RoundToInt((magicResistance.getValue() + inteligent.getValue()));
        if (isChill)
        {
            _magicResistance = Mathf.RoundToInt(_magicResistance * 0.8f);
        }


        //Debug.Log(this.name+" :"+_magicResistance);

        _magicDame -= _magicResistance;

        return Mathf.Clamp(_magicDame, 0, int.MaxValue);
    }

    public int getFireDame() => fireDame.getValue() + inteligent.getValue();
    public int getIceDame() => iceDame.getValue() + inteligent.getValue();
    public int getLightingDame() => lightingDame.getValue() + inteligent.getValue();

    #endregion

    #region Apply Aliment

    public void LogicApplyAilement(int _fireDame, int _iceDame, int _lightingDame)
    {
        bool _canApplyIngnite = _fireDame > _iceDame && _fireDame > _lightingDame;
        bool _canApplyChill = _iceDame > _fireDame && _iceDame > _lightingDame;
        bool _canApplyLighting = _lightingDame > _fireDame && _lightingDame > _iceDame;


        if (_canApplyIngnite)
        {
            setUpIngnite(Mathf.RoundToInt(_fireDame * 0.2f));
        }

        if (_canApplyLighting)
        {
            setUpDameLinghting(Mathf.RoundToInt(_lightingDame * 0.2f));
        }

        ApplyAilement(_canApplyIngnite , _canApplyChill , _canApplyLighting);

    }

    public void ApplyAilement(bool _ingnite, bool _chill, bool _shock)
    {
        bool applyIngnite = !isChill && !isShocked;
        if (applyIngnite && _ingnite)
        {
            Debug.Log(this.name +" Receive Burn");
            isIngnite = _ingnite;
            ingniteTimer = ailmentDuration;
            fx.ingniteColorFor(ailmentDuration);
        }

        bool applyShock = !isChill && !isIngnite;
        if(applyShock && _shock)
        {
            Debug.Log(this.name + " Receive Shock");
            isShocked = _shock;
            if (!isShocked)
            {
                Debug.Log(this.name + "Apply Shock");
                ApplyShock(_shock);
            }
            else
            {
                if (GetComponent<Player>() != null) return;

                effectStrikeEnemyClosest();
            }
        }

        bool applyChill = !isIngnite && !isShocked;
        if(applyChill && _chill)
        {
            isChill = _chill;
            chillTimer = ailmentDuration;
            GetComponent<Entity>().slowEntityBy(0.2f , ailmentDuration);
            fx.chillColorFor(ailmentDuration);
        }
    }


    public void ApplyBurn()
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

    public void ApplyChill(bool _canChill)
    {
        
    }

    public void ApplyShock(bool _canShock)
    {
        if (_canShock)
        {
            shockTimer = ailmentDuration;
            isShocked = _canShock;
            fx.shockColorFor(ailmentDuration);
        }
    }
    
    public void setUpIngnite(int _dame) => ingniteDame = _dame;// quản lý dame của ingniteDame

    public void setUpDameLinghting(int _dame) => strikeDame = _dame; // quản lý dame của lighting

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

    public void DoDamePhysical(CharacterStats _statSender) // Dame handle
    {
        if (AvoidAttack())
        {
            Debug.Log("Né dame");
            return;
        }
        //Debug.Log("Không thể né dame");

        int damage = _statSender.dame.getValue() + _statSender.strength.getValue();

        if (CanCrit(_statSender))
        {
            Debug.Log("Crit");
            damage = CalculateCritDame(damage, _statSender);
        }

        damage = CheckArmor(damage);
        //Debug.Log($"{_statSender.name} Sender : {damage} - {this.name} Receive: {damage}");
        takeDame(damage);
    }

    public int CheckArmor(int _value)
    {
        float fullArmor = armor.getValue() + vitality.getValue() + (armor.getValue() * 0.3f);
        if (fullArmor > 0)
        {
            if (_value < fullArmor)
            {
                _value = 0;
            }
            else
            {
                _value -= Mathf.RoundToInt(fullArmor);
            }
        }

        return _value;
    }

    public bool AvoidAttack()
    {
        int totalEvasion = evasion.getValue() + ability.getValue();
        //Debug.Log(this.name + ", Evasion: " + totalEvasion);

        return sytemRate(totalEvasion);
    }

    public bool CanCrit(CharacterStats _statSender)
    {
        int critRate = _statSender.critRate.getValue() + _statSender.ability.getValue();

        //Debug.Log(_statSender.name + ",CritRate: " + critRate);
        return sytemRate(critRate);
    }


    public int CalculateCritDame(int _finalDame, CharacterStats _statSender)
    {
        float dameCritPower = (_statSender.critPower.getValue() + _statSender.strength.getValue()) * 0.01f;

        //Debug.Log(_statSender.name + ", dameCritPower: " + (_finalDame + dameCritPower));
        return Mathf.RoundToInt(_finalDame + dameCritPower);
    }


    public virtual void applyBleedingHealth(bool _isBleeding)
    {

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
        if (currentHealth > getMaxHealth())
        {
            currentHealth = getMaxHealth();
        }

        if (onUiHealth != null)
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
        if (onUiHealth != null)
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
