using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{

    static ObjectPool instance;

    public static ObjectPool Instance()
    {
        if(instance == null)
        {
            instance = new ObjectPool();
        }
        return instance;
    }
    public void PushObject(GameObject Object){
        
    }

     public GameObject GetObject(GameObject prefab){
        GameObject _object;
        return _object;
    }
}
