﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;

    public bool openWhenEnemiesCleared;

    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    private bool roomAcitve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count > 0 && openWhenEnemiesCleared) {
            for(int i=0; i<enemies.Count; i++) {
                if(enemies[i] == null) {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if(enemies.Count == 0) {
                foreach(GameObject door in doors) {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            CameraController.instance.changeTarget(transform);

            if(closeWhenEntered) {
                foreach(GameObject door in doors) {
                    door.SetActive(true);
                }
            }

            roomAcitve = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            roomAcitve = false;
        }
    }
}
