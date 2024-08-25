
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
                    SkillManager.instance.clone_skill.CreateCloneCounterAttack(hit.transform);
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
