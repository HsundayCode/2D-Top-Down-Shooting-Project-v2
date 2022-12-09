using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rig2d;
    private Animator ani;
    private float hx,vy;
    public float Forcetime;
    public float moveSpeed;
    public float maxHp;
    float hp;
    Vector3 mosePos;
    bool isDie = false;
    bool isHit;//被击 
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        rig2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0)
        {
            isDie = true;
        }
        hx = Input.GetAxisRaw("Horizontal");
        vy = Input.GetAxisRaw("Vertical");
        mosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(hx != 0 || vy !=0)
        {
            move(true,mosePos);
        }else
        {
            move(false,mosePos);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
        
    }


    void move(bool isRun,Vector3 direction){

        Vector2 inpute = new Vector2(hx,vy).normalized;
        rig2d.velocity = inpute * moveSpeed;
        ani.SetBool("isRun",isRun);
        if(direction.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if(direction.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }


    public void takeHit(float Damage)
    {
        if(!isDie)
        {
            hp -= Damage;
            Debug.Log(hp);
        }
        
    }

    public void Dash()
    {
        rig2d.velocity = transform.right.normalized * 30;

    }

    IEnumerator removeForce(float Forcetime)
    {
        yield return new WaitForSeconds(Forcetime);
        rig2d.velocity = Vector3.zero;
    }
}
