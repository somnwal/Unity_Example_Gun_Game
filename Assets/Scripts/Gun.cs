using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;

    public Transform firePoint;

    public float shootingInterval;

    private float fireCount;

    public string weaponName;

    public Sprite gunUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.canMove && !LevelManager.instance.isPaused) {
            // 총 발사 ==========
            if(fireCount > 0) {
                fireCount -= Time.deltaTime;
            } else {
                // 마우스 첫번째 버튼 눌렀을 때
                if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
                    AudioManager.instance.playSFX(12);
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    fireCount = shootingInterval;
                }
            }
        }
    }
}
