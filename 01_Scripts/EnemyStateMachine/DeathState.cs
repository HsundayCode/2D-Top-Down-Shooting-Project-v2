using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{

    FSM manage;
    EnemyAttribute attribute;
    private AnimatorStateInfo info;
    public DeathState(FSM manage)
    {
        this.manage = manage;
        attribute = manage.attribute;
    }
    
    public void OnEnter()
    {
        attribute.animator.Play("Death");
    }


    public void OnUpdate()
    {
        info = attribute.animator.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 1)
        {
           
        }
    }

    
    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
