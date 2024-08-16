using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = _player.sword.transform;

        if (_player.transform.position.x > sword.position.x && _player.isFacingDir == 1f)
        {
            //Nếu vị trí chuột hiện tại nhỏ hơn vị trí của Player và hướng nhìn là 1 thì sẽ xoay sang trái và ngược lại
            _player.Flip();
        }
        else if (_player.transform.position.x < sword.position.x && _player.isFacingDir == -1f)
        {
            _player.Flip();
        }

        rb.AddForce(new Vector2(7 * -_player.isFacingDir, rb.velocity.y), ForceMode2D.Impulse);
    }

    public override void Execute()
    {
        base.Execute();
        if (triggerCalled)
        {
            _stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.StartCoroutine("isBusyFor", 0.1f);
    }
}
