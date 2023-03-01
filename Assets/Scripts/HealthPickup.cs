using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;

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
            PlayerHealthController.instance.healPlayer(healAmount);

            Destroy(gameObject);
            AudioManager.instance.playSFX(7);
        }
    }
}
