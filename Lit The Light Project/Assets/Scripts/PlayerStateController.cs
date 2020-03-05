using UnityEngine;

public class PlayerStateController
{
    private Animator animator;

    public PlayerStateController(Animator targetAnimator)
    {
        animator = targetAnimator;
    }

    public void SetState(State value)
    {
        if (!IsCurrentState(value))
            animator.SetInteger("State", (int)value);
    }

    public bool IsCurrentState(State value)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(value.ToString());
    }

    public bool IsCurrentStateInRange(params State[] values)
    {
        foreach (var value in values)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(value.ToString()))
                return true;
        }
        return false;
    }

    public void SetVertSpeedInfo(float speed)
    {
        animator.SetFloat("vSpeed", speed);
    }
}

public enum State
{
    Idle,       //0
    Run,        //1
    Jump,       //2
    Squat,      //3
    Climb,      //4
    Appear,     //5
    Disappear   //6
}