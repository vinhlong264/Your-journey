using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private itemDrop dropSystem;

    [SerializeField] int level;
    [Range(0, 1f)]
    [SerializeField] private float perCanStage;

    [SerializeField] private int expReward;

    [Header("Aliment Bleeding")]
    private float dameFinalBleeding;
    [SerializeField] private bool isBleeding; // Kiểm tra xem có đang bleeding
    [SerializeField] private float percentBleeding;
    [SerializeField] private float bleedingTimer;
    [SerializeField] private float bleedingCoolDown; //Thời gian làm mới bleeding

    protected override void Start()
    {
        applyPower();

        base.Start();
        enemy = GetComponent<Enemy>();
        dropSystem = GetComponent<itemDrop>();

    }

    protected override void Update()
    {
        base.Update();
        isBleedingHealth();
    }

    private void isBleedingHealth()
    {
        if (isBleeding)
        {
            bleedingTimer -= Time.deltaTime;
            if (bleedingTimer < 0)
            {
                dameFinalBleeding = (dame.getValue() + strength.getValue()) * percentBleeding / 100;
                decreaseHealthBy(Mathf.RoundToInt(dameFinalBleeding));
                if (currentHealth <= 0)
                {
                    Die();
                    isBleeding = false;
                }

                bleedingTimer = bleedingCoolDown;
            }
        }
    }

    private void applyPower()
    {
        applyModifier(vitality);
        applyModifier(strength);
        applyModifier(dame);
    }

    private void applyModifier(Stats _status)
    {
        for (int i = 1; i < level; i++)
        {
            float modifrer = _status.getValue() * perCanStage;
            _status.addModifiers(Mathf.RoundToInt(modifrer));
        }
    }

    public override void takeDame(int _dame)
    {
        base.takeDame(_dame);
        enemy.dameEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();

        //Observer.Instance.NotifyEvent(GameEvent.RewardExp, expReward);
        dropSystem.generateDrop();
        return;
    }
    public override void applyBleedingHealth(bool _isBleeding)
    {
        isBleeding = _isBleeding;
    }
}
