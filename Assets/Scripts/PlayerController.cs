using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        // 캐릭터 이동 급가속 되는거 방지
        moveInput.Normalize();

        rigidBody.velocity = moveInput * moveSpeed;

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
                Instantiate(bullet, firePoint.position, firePoint.rotation);

                fireCount = shootingInterval;
            }
        }

        if(moveInput != Vector2.zero) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }
}
