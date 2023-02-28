using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;

    public float range;

    public float shootRange;

    private Vector3 moveDirection;

    public Animator animator;

    public int hp = 150;

    public GameObject[] deathEffects;

    public GameObject hitEffect;

    public bool isShoot;

    public GameObject bullet;
    
    public Transform firePoint;

    public float fireRate;

    private float fireCount;

    public SpriteRenderer body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 화면에 보일때만 공격
        if(body.isVisible) {
            // 플레이어와 캐릭터 사이 거리 구하기 (거리가 range 보다 적으면)
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < range) {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            } else {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();

            rigidBody.velocity = moveDirection * moveSpeed;

            if(moveDirection != Vector3.zero) {
                animator.SetBool("isMoving", true);
            } else {
                animator.SetBool("isMoving", false);
            }

            // 슈팅 거리 안에 들어왔을 때만 공격
            if(isShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= shootRange) {
                fireCount -= Time.deltaTime;

                if(fireCount <= 0) {
                    fireCount = fireRate;

                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }
        }
    }

    public void DamageEnemy(int damage) {
        hp -= damage;

        if(hp <= 0) {
            Destroy(gameObject);

            int selected = Random.Range(0, deathEffects.Length);

            int rotation = Random.Range(0, 4);

            Instantiate(deathEffects[selected], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
            Instantiate(hitEffect, transform.position, transform.rotation);
        }
    }
}
