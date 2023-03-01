using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int current_hp;

    public int max_hp;

    public float damageInvincLength = 1f;

    private float invincCount;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        current_hp = max_hp;

        // UI 컨트롤러를 통해 UI 설정

        // 슬라이더에 최대 hp 설정
        UIController.instance.hpSlider.maxValue = max_hp;

        // 슬라이더에 현재 hp 설정
        UIController.instance.hpSlider.value = current_hp;

        // hp 텍스트 설정
        UIController.instance.hpText.text = current_hp + " / " + max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0) {
            invincCount -= Time.deltaTime;

            if(invincCount <= 0) {
                PlayerController.instance.body.color = new Color(PlayerController.instance.body.color.r, PlayerController.instance.body.color.g, PlayerController.instance.body.color.b, 1f);

            }
        }


    }

    public void damagePlayer() {

        if(invincCount <= 0) {
            current_hp -= 1;

            PlayerController.instance.body.color = new Color(PlayerController.instance.body.color.r, PlayerController.instance.body.color.g, PlayerController.instance.body.color.b, 0.5f);

            AudioManager.instance.playSFX(11);

            if(current_hp <= 0) {
                PlayerController.instance.gameObject.SetActive(false);

                AudioManager.instance.playGameOver();
                AudioManager.instance.playSFX(9);

                UIController.instance.deathScreen.SetActive(true);
            }

            // 슬라이더에 현재 hp 설정
            UIController.instance.hpSlider.value = current_hp;

            // hp 텍스트 설정
            UIController.instance.hpText.text = current_hp + " / " + max_hp;

            invincCount = damageInvincLength;
        }        
    }

    public void healPlayer(int healAmount) {
        current_hp += healAmount;

        if(current_hp > max_hp) {
            current_hp = max_hp;
        }

        // 슬라이더에 현재 hp 설정
        UIController.instance.hpSlider.value = current_hp;

        // hp 텍스트 설정
        UIController.instance.hpText.text = current_hp + " / " + max_hp;
    }

    public void makeInvincible(float length) {
        invincCount = length;
    }
}
