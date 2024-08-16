using UnityEngine;

public class PlayerAnimSwordState : PlayerState
{
    public PlayerAnimSwordState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.sword_Skill.DotsActive(true);       
    }

    public override void Execute()
    {
        base.Execute();

        _player.setZeroVelocity();
        if (Input.GetKeyUp(KeyCode.Mouse1)) 
        {
            _stateMachine.changeState(_player._idleState);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(_player.transform.position.x > mousePos.x && _player.isFacingDir == 1f) 
        {
            //Nếu vị trí của Player lớn hơn vị trí con trỏ chuột và hướng hiện tại là 1 thì sẽ xoay qua trái
            _player.Flip();
        }
        else if(_player.transform.position.x < mousePos.x && _player.isFacingDir == -1f)
        {
            _player.Flip();
        }

        
    }

    public override void Exit()
    {
        base.Exit();
        _player.StartCoroutine("isBusyFor", 0.2f);
    }
}
