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
    //切枪  子弹cd完了才允许切换
    void SwitchGun()
    {
        if(currentGunIndex == 3 && gunBag[currentGunIndex].GetComponent<GunBase>().interval <= 0 )
        {
            gunBag[currentGunIndex].SetActive(false);//当前
            currentGunIndex = 1;
            gunBag[currentGunIndex].SetActive(true);//之后
            
        }else if(currentGunIndex < 3 && gunBag[currentGunIndex].GetComponent<GunBase>().interval <= 0)
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
                
                gun.SetActive(false);
                gunBag.Add(gunBag.Count+1,gun);
                
            }
        }
        
    }
    //捡多了替换
    public void replaceCurrentGun(GameObject gunPrefab,SpriteRenderer gunSprite,GameObject gunfloor)
    {
        
         
        if(Input.GetKeyDown(KeyCode.I))
        {
            var gun = Instantiate(gunPrefab,transform);
            Destroy(gunBag[1]);
            gunBag[1] = gun;
            GameObject.FindGameObjectWithTag("GunUI").GetComponent<GunUI>().setImage1(gunSprite);
            Destroy(gunfloor);
            
            
        }else if(Input.GetKeyDown(KeyCode.O))
        {
            var gun = Instantiate(gunPrefab,transform);
            Destroy(gunBag[2]);
            gunBag[2] = gun;
            GameObject.FindGameObjectWithTag("GunUI").GetComponent<GunUI>().setImage2(gunSprite);
            Destroy(gunfloor);
            
        }else if(Input.GetKeyDown(KeyCode.P))
        {
            var gun = Instantiate(gunPrefab,transform);
            Destroy(gunBag[3]);
            gunBag[3] = gun;
            GameObject.FindGameObjectWithTag("GunUI").GetComponent<GunUI>().setImage3(gunSprite);
            Destroy(gunfloor);
            
        }
        
    }
}
