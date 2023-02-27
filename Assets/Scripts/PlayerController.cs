using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveInput;

    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

        rigidBody.velocity = moveInput * moveSpeed;

        // 마우스 위치 얻어오기
        Vector3 mousePosition = Input.mousePosition;
        
        Vector3 

    }
}
