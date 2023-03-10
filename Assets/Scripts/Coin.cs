using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    public float waitToBeCollected;

    // Start is called before the first frame update
    void Update()
    {
        if(waitToBeCollected > 0) {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && waitToBeCollected <= 0) {
            LevelManager.instance.getCoins(coinValue);
            
            Destroy(gameObject);
            AudioManager.instance.playSFX(7);
        }
    }
}
