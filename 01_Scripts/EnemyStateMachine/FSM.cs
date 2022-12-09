using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType{
    Idle,Attack,Death,TakeHit,Run,
}

//因为属性的变化而改变状态，或状态得执行需要属性支持
[Serializable]
public class EnemyAttribute
{
    public Animator animator;
    public Rigidbody2D rig2d;
    public float Hp;//生命值
    public float moveSpeed;//Run
    public bool TakeHit;//被击
    public Transform TargetPos;//移动的目标位置 run attack idle
}
public class FSM : MonoBehaviour
{
    public GameObject[] attackAry;
    private IState currentIstate;//当前执行的状态机
    private Dictionary<StateType,IState> states = new Dictionary<StateType, IState>();
    public EnemyAttribute attribute;
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle,new IdleState(this));
        states.Add(StateType.Run,new RunState(this));
        states.Add(StateType.Attack,new AttackState(this));
        states.Add(StateType.TakeHit,new TakeHit(this));
        states.Add(StateType.Death,new DeathState(this));
        attribute.animator = transform.GetComponent<Animator>();
        attribute.rig2d = transform.GetComponent<Rigidbody2D>();
        attribute.TargetPos = GameObject.FindGameObjectWithTag("Player").transform;
        TransitionState(StateType.Idle);

    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(attribute.rig2d.velocity);
        currentIstate.OnUpdate();
        if(attribute.TakeHit == true)
        {
            TransitionState(StateType.TakeHit);
        }
    }

    public void TransitionState(StateType type)
    {
        if(currentIstate != null)
        currentIstate.OnExit();

        currentIstate = states[type];
        currentIstate.OnEnter();
    }

    public void FlipTo(Transform TargetPos)
    {
        if(transform.position.x < attribute.TargetPos.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(transform.position.x > attribute.TargetPos.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            TransitionState(StateType.TakeHit);
            attribute.Hp -= other.GetComponent<BulletBase>().Damage;
            Vector2 direction = other.GetComponent<BulletBase>().direction;//获得子弹方向
            attribute.rig2d.velocity = direction.normalized * 5;//击退 这个值因为没有持续多久又被move函数给赋回来了
            StartCoroutine(changedir());
            
        }
    }

    IEnumerator changedir()
    {
        yield return new WaitForSeconds(0.5f);//0.5秒后反方向的速度为0
        TransitionState(StateType.Run);
    }

    //动画帧事件
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    //攻击触发盒 用于动画帧事件
    public void attack_1()
    {
        attackAry[0].SetActive(true);
        StartCoroutine(attackEnable(0));
    }

    public void attack_2()
    {
        attackAry[1].SetActive(true);
        StartCoroutine(attackEnable(1));
    }

    IEnumerator attackEnable(int i)
    {
        yield return new WaitForSeconds(0.01f);
        attackAry[i].SetActive(false);

    }
}
