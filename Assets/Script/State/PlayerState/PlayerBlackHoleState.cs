using UnityEngine;
public class PlayerBlackHoleState : PlayerState
{
    private float flyTimer = 0.4f;
    private bool canUseSkill;
    private float defaultGravity;
    public PlayerBlackHoleState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = rb.gravityScale; // lấy ra giá trị gravity gốc của Player
        Debug.Log(defaultGravity);


        stateTimer = flyTimer;
        canUseSkill = false;
        rb.gravityScale = 0f;
        
    }

    public override void Execute()
    {
        base.Execute();
        if(stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 15f);
        }

        if(stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -0.1f);
            if(!canUseSkill)
            {
                if (SkillManager.instance.blackHole_skill.CanUseSkill())
                {
                    canUseSkill = true;
                    Debug.Log("Cast BlackHole");
                }
            }

        }

        if (SkillManager.instance.blackHole_skill.skillCompelete())
        {
            _stateMachine.changeState(_player._airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = defaultGravity;
        _player.makeTransprent(false);
    }
}
