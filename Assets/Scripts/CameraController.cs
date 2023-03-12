using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.M)) {
            if(!bigMapActive) {
                activateBigMap();
            } else {
                deactivateBigMap();
            }
        }
    }

    public void changeTarget(Transform newTarget) {
        target = newTarget;
    }

    public void activateBigMap() {
        if(!LevelManager.instance.isPaused) {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;

            UIController.instance.minimap.SetActive(false);
        }
    }

    public void deactivateBigMap() {
        if(!LevelManager.instance.isPaused) {
            bigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;

            UIController.instance.minimap.SetActive(true);
        }
    }
}
