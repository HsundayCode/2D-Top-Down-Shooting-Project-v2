using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//: MonoBehaviour// 不能继承

public class ObjectPool {

    private static ObjectPool instance;
    private GameObject pool;//所有对象池的父物体
    //key 对象池名 ，存储对象的容器
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    public static ObjectPool Instance
    {
        get
        {
             if(instance == null)
        {
            instance = new ObjectPool();
        }
        return instance;
        }
       
    }
    //push是代码层面上的池
    public void PushObject(GameObject prefab){
        string _name = prefab.name.Replace("(Clone)",string.Empty);//去掉尾巴
        
        if(!objectPool.ContainsKey(_name))
        {
            //Debug.Log(objectPool.ContainsKey(_name));
            objectPool.Add(_name,new Queue<GameObject>());
        }
        objectPool[_name].Enqueue(prefab);
        prefab.SetActive(false);
    }

    
    //get是编辑器里看到的池
     public GameObject GetObject(GameObject prefab){
        GameObject _object;
        //Debug.Log(prefab.name);Bullet
        //对象池里的对象不够用新建一个
        
        if(!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            //Debug.Log(_object.name);Bullet(Clone)
            //新建一个总对象池物体
            if(pool == null)
            {
                pool = new GameObject("ObjectPool");//父物体 总对象池
            }
            //特定物体的对象池 有名字的物体
            GameObject childPool = GameObject.Find(prefab.name+"Pool");
            if(!childPool)//物体对象池不存在
            {
                childPool = new GameObject(prefab.name+"Pool");
                childPool.transform.SetParent(pool.transform);//挂在总对象池下
            }
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }
}
