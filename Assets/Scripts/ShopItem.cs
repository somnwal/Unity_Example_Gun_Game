using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private bool isPlayerIn;

    public bool isHealthInStore, isHealthUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeAmount;

    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerIn) {
            if(Input.GetKeyDown(KeyCode.E)) {
                if(LevelManager.instance.currentCoins >= itemCost) {
                    LevelManager.instance.spendCoins(itemCost);

                    if(isHealthInStore) {
                        PlayerHealthController.instance.healPlayer(PlayerHealthController.instance.max_hp);
                    }

                    if(isHealthUpgrade) {
                        PlayerHealthController.instance.increaseMaxHealth(healthUpgradeAmount);
                    }

                    if(isWeapon) {
                        bool hasGun = false;

                        foreach(Gun tmp_gun in PlayerController.instance.weapons) {
                            if(tmp_gun.weaponName == gun.weaponName) {
                                hasGun = true;
                            }
                        }

                        if(!hasGun) {
                            Gun newGun = Instantiate(gun);

                            newGun.transform.parent = PlayerController.instance.weapon;
                            newGun.transform.position = PlayerController.instance.weapon.position;

                            newGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
                            newGun.transform.localScale = Vector3.one;

                            PlayerController.instance.weapons.Add(newGun);

                            PlayerController.instance.currentWeapon = PlayerController.instance.weapons.Count - 1;
                            PlayerController.instance.switchGun();
                        }
                    }

                    gameObject.SetActive(false);
                    isPlayerIn = false;

                    AudioManager.instance.playSFX(18);
                } else {
                    AudioManager.instance.playSFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            isPlayerIn = false;
        }
    }
}
