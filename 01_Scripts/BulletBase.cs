using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//自需要负责方向和伤害和移动
public class BulletBase : MonoBehaviour
{
    float moveSpeed = 5;//移动
    public Vector3 direction;//方向
    public GameObject expolosion;
    public  float Damage;//伤害
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Shoot();
    }

    public virtual void Shoot(){
        if(direction != null){
            //transform.right = direction.normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player")
        {
            //Destroy(gameObject);
            ObjectPool.Instance.PushObject(gameObject);
            //Instantiate(expolosion,transform.position,Quaternion.identity);
            GameObject ex = ObjectPool.Instance.GetObject(expolosion);
            ex.transform.position = transform.position;
            
        }
        
    }
}
