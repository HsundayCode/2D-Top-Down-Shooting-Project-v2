using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//保存 切换 替换
public class GunBag : MonoBehaviour
{
     public Dictionary<float,GameObject> gunBag;
     static GameObject instance;
     int currentGunIndex = 1;//当前枪索引
     bool moreGun;

     public GameObject Instance()
     {
        if(instance == null)
        {
            instance = gameObject;
        }
        return instance;
     }
    // Start is called before the first frame update
    void Start()
    {
        gunBag = new Dictionary<float, GameObject>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGun();
        }
    }
    //切枪
    void SwitchGun()
    {
        if(currentGunIndex == 3 )
        {
            gunBag[currentGunIndex].SetActive(false);//当前
            currentGunIndex = 1;
            gunBag[currentGunIndex].SetActive(true);//之后
            
        }else if(currentGunIndex < 3)
        {
            gunBag[currentGunIndex].SetActive(false);
            currentGunIndex += 1;
            gunBag[currentGunIndex].SetActive(true);

        }
    }
    //捡枪
    public void addGun(GameObject gunPrefab)
    {
        var gun = Instantiate(gunPrefab,transform);
        
        if(gunBag.Count+1 <= 3 )//添加了才会加1，不能用这个来判断 给他加个1
        {
            if(gunBag.Count == 0)
            {
                gunBag.Add(1,gun);
                //gunBag[1].SetActive(true);
            }else
            {
                ;
                gun.SetActive(false);
                gunBag.Add(gunBag.Count+1,gun);
                
            }
        }else{
            replaceCurrentGun(gun);
        }
        
    }
    //捡多了替换
    void replaceCurrentGun(GameObject gun)
    {
        
        if(Input.GetKeyDown(KeyCode.I))
        {
            gunBag[0] = gun;
            
        }else if(Input.GetKeyDown(KeyCode.O))
        {
            gunBag[1] = gun;
            
        }else if(Input.GetKeyDown(KeyCode.P))
        {
            gunBag[2] = gun;
            
        }
        
    }
}
