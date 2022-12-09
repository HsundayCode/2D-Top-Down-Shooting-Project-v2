using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : IState
{

    FSM manage;
    EnemyAttribute attribute;
    private AnimatorStateInfo info;
    public TakeHit(FSM manage)
    {
        this.manage = manage;
        attribute = manage.attribute;
    }
    public void OnEnter()
    {
        attribute.animator.Play("TakeHit");
    }
    //通过被击动画剩余时间来转变
    public void OnUpdate()
    {
        if(attribute.Hp <= 0)
        {
            manage.TransitionState(StateType.Death);
        }
        info = attribute.animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 0.95f)
        {
            attribute.TargetPos = GameObject.FindGameObjectWithTag("Player").transform;
            //manage.TransitionState(StateType.Run);
        }
    }

    
    public void OnExit()
    {
        attribute.TakeHit = false;
    }

}
