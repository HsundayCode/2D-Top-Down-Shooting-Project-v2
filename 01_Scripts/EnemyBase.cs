using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed;//移动速度
    public float backSpeed;//后退速度
    public Transform PlayerPos;//玩家位置
    Vector2 TargetDirction;//目标方向
    public GameObject[] attackAry;
    Rigidbody2D rig2d;
    Animator EnemyAni;
    public float maxHp;
    float hp;
    bool moveAble =true;//是否碰撞玩家 是否可以移动
    bool isHit;//播发被击动画以及击退效果
    // Start is called before the first frame update
    void Start()
    {
        
        hp = maxHp;
        rig2d = GetComponent<Rigidbody2D>();
        EnemyAni = GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetDirction = (PlayerPos.position - transform.position).normalized;
        EnemyMove();
        Death();
    }
    //差个判断在角色上方需要调整到角色左右 踩攻击，不然是无效攻击
    public void Death()
    {
        if(hp <= 0)
        {
            EnemyAni.SetBool("isDeath",true);
            rig2d.velocity = Vector2.zero;
        }
    }

    void EnemyMove()
    {
        //在角色的左
        if(PlayerPos.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else
        {
             transform.rotation = Quaternion.Euler(0,180,0);
        }
        if(Vector2.Distance(PlayerPos.position,transform.position) > 1f && !isHit)
        {
            moveAble = true;
        }
        if(moveAble)
        {
            rig2d.velocity = TargetDirction * moveSpeed;
            EnemyAni.SetBool("isMove",true);
        }
    }

    //碰撞玩家后不能移动，不能推着对面走
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            moveAble = false;
        }   
    }
    //一直碰撞时，取消部分run动画，触发攻击
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            EnemyAni.SetBool("isMove",false);
            EnemyAni.SetTrigger("Attack");
        }
    }
    //对方是触发器 也要用OnTrigger
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet")
        {
            moveAble = false;
            isHit = true;
            //x攻击动画在被击前了所以这个被击没有播放  各个动画之间需要联系
            EnemyAni.SetTrigger("isHit");//x播发动画不会后退？
            hp -= other.GetComponent<BulletBase>().Damage;
            Vector3 direction = other.GetComponent<BulletBase>().direction;//获得子弹方向
            rig2d.velocity = direction * backSpeed;//击退 这个值因为没有持续多久又被move函数给赋回来了
            StartCoroutine(changedir());
        }
       
    }
    IEnumerator changedir()
    {
        yield return new WaitForSeconds(0.5f);//0.5秒后反方向的速度为0
        isHit = false;
        //moveAble = false;
    }


    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }


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
