
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        stateTimer = _player.counterAttackDurarion; // Cooldown của CounterSate
        _player.animator.SetBool("SuccesfullCounterAttack", false);
    }

    public override void Execute()
    {
        base.Execute();
        _player.setZeroVelocity();
        Collider2D[] col = Physics2D.OverlapCircleAll(_player.AttackCheck.position, _player.attackRadius);
        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<Skeleton>() != null)
            {
                if (hit.GetComponent<Skeleton>().checkStunned())
                {
                    Debug.Log("Đang ở SuccesfullCounterAttack");
                    stateTimer = 10;
                    _player.animator.SetBool("SuccesfullCounterAttack", true);

                    _player.skill.parry_Skill.CanUseSkill(); // Hồi phục

                    if (canCreateClone)
                    {
                         canCreateClone = false;
                        _player.skill.parry_Skill.parryCreatMirage(hit.transform);
                    }                    
                }
            }
        }


        if (stateTimer < 0 || triggerCalled)
        {
            _stateMachine.changeState(_player._idleState);
            _player.animator.SetBool("SuccesfullCounterAttack", false);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
