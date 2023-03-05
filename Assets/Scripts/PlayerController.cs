using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 5.0f;
    private Vector2 moveInput;

    public Rigidbody2D rigidBody;

    public Transform weapon;

    private Camera cam;

    public Animator animator;

    public GameObject bullet;

    public Transform firePoint;

    public float shootingInterval;

    private float fireCount;

    public SpriteRenderer body;

    private float activeMoveSpeed;

    public float dashSpeed = 8f;

    public float dashLength = 0.5f;

    public float dashCoolDown = 1f;

    public float dashInvincibility = 0.5f;

    [HideInInspector]
    public float dashCounter;
    
    public float dashCoolCounter;

    [HideInInspector]
    public bool canMove = true;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove && !LevelManager.instance.isPaused) {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

            // 캐릭터 이동 급가속 되는거 방지
            moveInput.Normalize();

            rigidBody.velocity = moveInput * activeMoveSpeed;

            // 마우스 위치 얻어오기
            Vector3 mousePosition = Input.mousePosition;
            
            // 스크린 포인트 구하기
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);


            // 방향에 따라 다르게
            if(mousePosition.x < screenPoint.x) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                weapon.localScale = new Vector3(-1f, -1f, 1f);
            } else {
                transform.localScale = Vector3.one;
                weapon.localScale = Vector3.one;
            }

            // 각도 구하기
            Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            // 총 돌리기
            weapon.rotation = Quaternion.Euler(0, 0, angle);




            // 총 발사 ==========

            // 마우스 첫번째 버튼 눌렀을 때
            // if(Input.GetMouseButtonDown(0)) {
            //     Instantiate(bullet, firePoint.position, firePoint.rotation);
            // }

            if(Input.GetMouseButton(0)) {
                fireCount -= Time.deltaTime;
                

                if(fireCount <= 0) {
                    AudioManager.instance.playSFX(12);

                    Instantiate(bullet, firePoint.position, firePoint.rotation);

                    fireCount = shootingInterval;
                }
            }

            // 대시 ============
            if(Input.GetKeyDown(KeyCode.Space)) {

                if(dashCoolCounter <= 0 && dashCounter <= 0) {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;

                    // 대시 애니메이션 트리거
                    animator.SetTrigger("dash");

                    AudioManager.instance.playSFX(8);

                    // 대시하는 동안 무적으로 만들기
                    PlayerHealthController.instance.makeInvincible(dashInvincibility);
                }
            }

            if(dashCounter > 0) {
                dashCounter -= Time.deltaTime;

                if(dashCounter <= 0) {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCoolDown;
                }
            }

            if(dashCoolCounter > 0) {
                dashCoolCounter -= Time.deltaTime;
            }

            if(moveInput != Vector2.zero) {
                animator.SetBool("isMoving", true);
            } else {
                animator.SetBool("isMoving", false);
            }
        } else {
            rigidBody.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }
}
