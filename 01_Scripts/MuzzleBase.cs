using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleBase : MonoBehaviour
{
    public float BulletNum;//子弹数量
    public float Deflectionangle;//偏转角度
    public GameObject bulletType;//子弹类型
    public Vector3 direction;//子弹发射的方向，偏转基础  鼠标和枪口的位置减法 不能用枪口，可能会出现鼠标在角色中时子弹往角色里飞
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

    }
    //枪支调用的方法
    public virtual void Shoot()
    {
        
        float miden = BulletNum / 2;//判断角度是负的还是正
        for(int i =0;i<BulletNum;i++)
        {
            GameObject bullet = Instantiate(bulletType);
            bullet.transform.position = transform.position;
            bullet.GetComponent<BulletBase>().direction = Quaternion.AngleAxis((i-miden) * Deflectionangle,Vector3.forward) * direction;
        }
    }
}
