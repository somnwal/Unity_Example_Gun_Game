using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPiece;
    public int maxPieces = 5;

    public bool isDrop;

    public GameObject[] items;

    public float itemDropPercent;

    public int breakSoundIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(PlayerController.instance.dashCounter > 0) {
                Destroy(gameObject);

                AudioManager.instance.playSFX(breakSoundIndex);

                // 부서진 조각 보여주기
                int piecesToDrop = Random.Range(1, maxPieces);

                for(int i=0; i<piecesToDrop; i++) {
                    int selected = Random.Range(0, brokenPiece.Length);
                    Instantiate(brokenPiece[selected], transform.position, transform.rotation);
                }

                // 아이템 드랍
                if(isDrop) {
                    float dropChance = Random.Range(0f, 100f);

                    if(dropChance < itemDropPercent) {
                        int itemToDrop = Random.Range(0, items.Length);

                        Instantiate(items[itemToDrop], transform.position, transform.rotation);
                    }
                }
                
            }
        }
    }
}
