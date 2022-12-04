using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGun : MonoBehaviour
{
    public GameObject gunPrefab;
    GunBag gunBag;
    // Start is called before the first frame update
    void Start()
    {
        gunBag = GameObject.FindGameObjectWithTag("Gunbag").GetComponent<GunBag>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            gunBag.addGun(gunPrefab);
        }
        Destroy(gameObject);
    }
}
