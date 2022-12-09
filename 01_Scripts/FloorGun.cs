using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGun : MonoBehaviour
{
    public GameObject gunPrefab;
    GunBag gunBag;
    SpriteRenderer gunSprite;
    // Start is called before the first frame update
    void Start()
    {
        gunBag = GameObject.FindGameObjectWithTag("Gunbag").GetComponent<GunBag>();
        gunSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && gunBag.gunBag.Count < 3)
        {
            gunBag.addGun(gunPrefab);
            GameObject.FindGameObjectWithTag("GunUI").GetComponent<GunUI>().setImage(gunSprite);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && gunBag.gunBag.Count >= 3)
        {
            gunBag.replaceCurrentGun(gunPrefab,gunSprite,gameObject);
        }
    }
}
