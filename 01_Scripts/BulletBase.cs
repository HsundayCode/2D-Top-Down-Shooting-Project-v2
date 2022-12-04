using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    float moveSpeed = 5;
    public Vector3 direction;
    public GameObject expolosion;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        transform.right = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction != null)
        Shoot();
    }

    public void Shoot(){
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            Instantiate(expolosion,transform.position,Quaternion.identity);
        }
        
    }
}
