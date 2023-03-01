using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;

    public int damage = 50;

    public Rigidbody2D rigidBody;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        
        AudioManager.instance.playSFX(4);

        if(other.tag == "Enemy") {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
        
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);    
    }
}
