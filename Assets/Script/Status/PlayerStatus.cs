using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    private Player player;
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }


    public override void takeDame(float _dame)
    {
        base.takeDame(_dame);
        player.DameEffect();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}

