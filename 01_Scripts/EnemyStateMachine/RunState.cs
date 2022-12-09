using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    FSM manage;
    EnemyAttribute attribute;
    public RunState(FSM manage)
    {
        this.manage = manage;
        this.attribute = manage.attribute;
    }
    public void OnEnter()
    {
        attribute.animator.Play("Run");
    }

    public void OnUpdate()
    {
        if(attribute.TakeHit)
        {
            manage.TransitionState(StateType.TakeHit);
        }

        manage.FlipTo(attribute.TargetPos);

        if(Vector2.Distance(manage.transform.position,attribute.TargetPos.position) >= 1f 
        && Vector2.Distance(manage.transform.position,attribute.TargetPos.position) < 10f)
        {
            attribute.rig2d.velocity = (attribute.TargetPos.position - manage.transform.position).normalized * attribute.moveSpeed;
        }

        if(Vector2.Distance(manage.transform.position,attribute.TargetPos.position) <= 1f)
        {
            manage.TransitionState(StateType.Attack);
        }
    }

    public void OnExit()
    {
        attribute.rig2d.velocity = Vector2.zero;
        Debug.Log("RunExit");
    }

}
