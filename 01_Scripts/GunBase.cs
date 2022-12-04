using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    Animator Gunani;
    Transform muzzlePos;
    Vector2 direction ;
    public GameObject Bullet;
    Vector3 mosePos;
    // Start is called before the first frame update
    void Start()
    {
        Gunani = GetComponent<Animator>();
        //muzzlePos = GetComponentInChildren<Muzzle>().transform;
        muzzlePos = transform.Find("Muzzle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        mosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mosePos - transform.position;
        transform.right = direction.normalized;
        if(mosePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(1,-1,1);
        }else
        {
             transform.localScale = new Vector3(1,1,1);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire(){
        GameObject bullet = Instantiate(Bullet,muzzlePos.position,Quaternion.identity);
        //bullet.GetComponent<BulletBase>().Shoot(direction.normalized); 死机代码
        bullet.GetComponent<BulletBase>().direction = direction.normalized;
        Gunani.SetTrigger("Fire");
    }
}
