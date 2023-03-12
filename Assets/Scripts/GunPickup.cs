using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{

    public Gun gun;

    public float waitingTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingTime > 0) {
            waitingTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && waitingTime <= 0) {

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

            Destroy(gameObject);
            AudioManager.instance.playSFX(7);
        }
    }
}
