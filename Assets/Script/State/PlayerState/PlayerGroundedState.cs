using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();


        if (Input.GetKeyDown(KeyCode.R))
        {
            _stateMachine.changeState(_player._blackHoleState);
        }

        //Kích hoạt sword skill
        if (Input.GetKeyDown(KeyCode.Mouse1) && hasNoSword())
        {
            _stateMachine.changeState(_player._animSwordState);
        }

        // Kích hoạt đòn đánh gây choáng
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _stateMachine.changeState(_player._counterAttackState);
        }

        // Kích hoạt tấn công thường
        if (Input.GetMouseButtonDown(0))
        {
            _stateMachine.changeState(_player._attackState);
        }

        if (!_player.groundCheck()) //Kiểm tra nếu không có Player ở trên mặt đất sẽ chuyển sang trạng thái _airState
        {
            _stateMachine.changeState(_player._airState);
        }

        if (Input.GetButtonDown("Jump") && _player.groundCheck()) // Jumpping
        {
            _stateMachine.changeState(_player._jumpState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


    bool hasNoSword() // hàm kiểm tra hiện tại Player có Sword không
    {
        if (!_player.sword)
        {
            return true;
        }
        Debug.Log("Take sword");
        Sword_Skill_Controller scl = _player.sword.GetComponent<Sword_Skill_Controller>();
        if (scl != null)
        {
            scl.ReturnSword();
        }
        return false;
    }
}
