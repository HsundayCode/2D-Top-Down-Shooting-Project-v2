using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    FSM manage;
    EnemyAttribute attribute;
    public IdleState(FSM manage)
    {
        this.manage = manage;
        attribute = manage.attribute;
    }

    public void OnEnter()
    {
        attribute.animator.Play("Idle");
    }


    public void OnUpdate()
    {
        if(attribute.TakeHit)
        {
            manage.TransitionState(StateType.TakeHit);
        }
        
        if(Vector2.Distance(manage.transform.position,attribute.TargetPos.position) >= 5)
        {
            
            manage.TransitionState(StateType.Run);
        }
    }
    
    public void OnExit()
    {
        Debug.Log("IdleExit");
    }
}
