using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    FSM manage;
    EnemyAttribute attribute;
    public AttackState(FSM manage)
    {
        this.manage = manage;
        attribute = manage.attribute;
    }
    public void OnEnter()
    {
        attribute.animator.Play("Attack");
    }

    public void OnUpdate()
    {
        if(attribute.TakeHit)
        {
            manage.TransitionState(StateType.TakeHit);
        }
        if(Vector2.Distance(manage.transform.position,attribute.TargetPos.position) >= 2f)
        {
            manage.TransitionState(StateType.Run);
        }
    }

    public void OnExit()
    {
        Debug.Log("AttackExit");
    }
}
